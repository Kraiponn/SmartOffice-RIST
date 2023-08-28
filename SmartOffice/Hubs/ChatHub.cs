using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using SmartOffice.EHelpdesk.Models.ViewModels;
using SmartOffice.EHelpdesk.Models;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using SmartOffice.ModelsDocControl;
using SmartOffice.ModelsEsmartOffice;
using SmartOffice.IResponsitory;
using SmartOffice.ModelsHRMSLocal;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;

namespace SmartOffice.Hubs
{
    //[Authorize]
    public class ChatHub : Hub
    {
       
        private readonly ESmartOfficeContext _context;
        private  IUserConnectionManager _userConnectionManager;
        private  DocumentControlContext _DocumentContext;
        private HRMSLocalContext _HrmsContext;
        private  IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;

        #region Properties

        /// <summary>
        /// List of online users
        /// </summary>
        public readonly static List<UserViewModel> _Connections = new List<UserViewModel>();

        /// <summary>
        /// List of available chat rooms
        /// </summary>
        private readonly static List<RoomViewModel> _Rooms = new List<RoomViewModel>();

        /// <summary>
        /// Mapping SignalR connections to application users.
        /// (We don't want to share connectionId)
        /// </summary>
        private readonly static Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();
        #endregion

       
        public async Task SendAsync(string roomName, string message)
        {
            if (message.StartsWith("/private"))
                SendPrivate(message);
            else
               await SendToRoomAsync(roomName, message);
        }

        public void SendPrivate(string message)
        {
            // message format: /private(receiverName) Lorem ipsum...
            string[] split = message.Split(')');
            string receiver = split[0].Split('(')[1];
            string userId;
            if (_ConnectionsMap.TryGetValue(receiver, out userId))
            {
                // Who is the sender;
                var sender = _Connections.Where(u => u.Username == IdentityName).First();

                message = Regex.Replace(message, @"\/private\(.*?\)", string.Empty).Trim();

                // Build the message
                MessageViewModel messageViewModel = new MessageViewModel()
                {
                    From = sender.DisplayName,
                    Avatar = sender.Avatar,
                    To = "",
                    Content = Regex.Replace(message, @"(?i)<(?!img|a|/a|/img).*?>", String.Empty),
                    Timestamp = DateTime.Now.ToLongTimeString()
                };

                // Send the message
                Clients.Client(userId).SendAsync("newMessage", messageViewModel);
                Clients.Caller.SendAsync("newMessage", messageViewModel);
            }
        }

        public async Task SendToRoomAsync(string roomName, string message)
        {
            try
            {

                var user = _HrmsContext.HrmsEmployee.Where(u => u.Codempid == IdentityName).FirstOrDefault();
                var room = _DocumentContext.Rooms.Where(r => r.Name == roomName).FirstOrDefault();

                // Create and save message in database

                Messages msg = new Messages()
                {
                    Content = Regex.Replace(message, @"(?i)<(?!img|a|/a|/img).*?>", String.Empty),
                    Timestamp = DateTime.Now.Ticks.ToString(),
                    FromUserId = user.Codempid,
                    ToRoomId = room.Id
            
                };
                MessageModel msg1 = new MessageModel()
                {
                    Content = Regex.Replace(message, @"(?i)<(?!img|a|/a|/img).*?>", String.Empty),
                    Timestamp = DateTime.Now.Ticks.ToString(),
                    FromUser = user,
                    ToRoom = room,
                    FromUserId = user.Codempid,
                    ToRoomId = room.Id

                };
                await  _DocumentContext.Messages.AddAsync(msg);
             await _DocumentContext.SaveChangesAsync();

                // Broadcast the message
              
                var messageViewModel = Mapper.Map<MessageModel, MessageViewModel>(msg1);
                await  Clients.Group(roomName).SendAsync("newMessage", messageViewModel);

            }

            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Message not send!");
            }
        }

        public void Join(string roomName)
        {
            try
            {
                var user = _Connections.Where(u => u.Username == IdentityName).FirstOrDefault();
                if (user.CurrentRoom != roomName)
                {
                    // Remove user from others list
                    if (!string.IsNullOrEmpty(user.CurrentRoom))
                        //Clients.Group(roomName).SendAsync("SendToRoom", messageViewModel);
                    Clients.OthersInGroup(roomName).SendAsync("removeUser",user);

                    // Join to new chat room
                    Leave(user.CurrentRoom);
                    Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                    user.CurrentRoom = roomName;

                    // Tell others to update their list of users
                    Clients.OthersInGroup(user.CurrentRoom).SendAsync("addUser", user);
                    //Clients.OthersInGroup(roomName).addUser(user);
                 
                }
            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("You failed to join the chat room!" + ex.Message);
            }
        }

        private void Leave(string roomName)
        {
            Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public void CreateRoom(string roomName)
        {
            try
            {
                
                    // Accept: Letters, numbers and one space between words.
                    Match match = Regex.Match(roomName, @"^\w+( \w+)*$");
                    if (!match.Success)
                    {
                        Clients.Caller.SendAsync("Invalid room name!\nRoom name must contain only letters and numbers.");
                    }
                    else if (roomName.Length < 5 || roomName.Length > 20)
                    {
                        Clients.Caller.SendAsync("Room name must be between 5-20 characters!");
                    }
                    else if (_DocumentContext.Rooms.Any(r => r.Name == roomName))
                    {
                        Clients.Caller.SendAsync("Another chat room with this name exists");
                    }
                    else
                    {
                        // Create and save chat room in database
                        var user = _context.AspNetUsers.Where(u => u.UserName == IdentityName).FirstOrDefault();
                        var room = new Rooms()
                        {
                            Name = roomName,
                            UserAccountId = IdentityName
                        };
                        _DocumentContext.Rooms.Add(room);
                        _DocumentContext.SaveChanges();

                        if (room != null)
                        {
                            // Update room list
                            var roomViewModel = Mapper.Map<Rooms, RoomViewModel>(room);
                            _Rooms.Add(roomViewModel);
                        Clients.All.SendAsync("addChatRoom", roomViewModel);
                        }
                    }
               
            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("Couldn't create chat room: " + ex.Message);
            }
        }
        public string GetConnectionId()
        {
            var userId = Context.User.Identity.Name;
            if (userId != null)
            {
                _userConnectionManager.KeepUserConnection(userId, Context.ConnectionId);
                return Context.ConnectionId;
            }
            return null;
        }

        public void DeleteRoom(string roomName)
        {
            try
            {
               
                    // Delete from database
                    var room = _DocumentContext.Rooms.Where(r => r.Name == roomName && r.UserAccountId == IdentityName).FirstOrDefault();
                _DocumentContext.Rooms.Remove(room);
                _DocumentContext.SaveChanges();

                    // Delete from list
                    var roomViewModel = _Rooms.First<RoomViewModel>(r => r.Name == roomName);
                    _Rooms.Remove(roomViewModel);

                    // Move users back to Lobby
                    Clients.Group(roomName).SendAsync("onRoomDeleted", string.Format("Room {0} has been deleted.\nYou are now moved to the Lobby!", roomName));

                    // Tell all users to update their room list
                    Clients.All.SendAsync("removeChatRoom", roomViewModel);
            
            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("onError","Can't delete this chat room."+ex.Message);
            }
        }

        public IEnumerable<MessageViewModel> GetMessageHistory(string roomName)
        {
            var roomitem = _DocumentContext.Rooms.Where(i => i.Name == roomName).FirstOrDefault();
            var messageHistory = _DocumentContext.Messages.Where(m => m.ToRoomId == roomitem.Id)
                .OrderByDescending(m => m.Timestamp)
                .Take(20)
                .AsEnumerable()
                .Reverse()
                .ToList();
            var messageHistory1 = new List<MessageModel>();

            foreach (var item in messageHistory)
            {
                var user = _HrmsContext.HrmsEmployee.Where(u => u.Codempid == item.FromUserId).FirstOrDefault();
                var username = user.Namempe;
                user.Namempe = username;
                username = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(username.ToLower());
                var room = _DocumentContext.Rooms.Where(r => r.Id == item.ToRoomId).FirstOrDefault();
                var msgitem1 = new MessageModel()
                {
                    Content = item.Content,
                    Timestamp = item.Timestamp,
                    FromUser = user,
                    ToRoom = room,
                    FromUserId = user.Codempid,
                    ToRoomId = room.Id
                };
                messageHistory1.Add(msgitem1);
            }

            return Mapper.Map<IEnumerable<MessageModel>, IEnumerable<MessageViewModel>>(messageHistory1);



        }

        public IEnumerable<RoomViewModel> GetRooms()
        {
            
                // First run?
                if (_Rooms.Count == 0)
                {
                    foreach (var room in _DocumentContext.Rooms)
                    {
                        var roomViewModel = _mapper.Map<Rooms, RoomViewModel>(room);
                        _Rooms.Add(roomViewModel);
                    }
                }
           

            return _Rooms.ToList();
        }

        public IEnumerable<UserViewModel> GetUsers(string roomName)
        {
            return _Connections.Where(u => u.CurrentRoom == roomName).ToList();
        }

        #region OnConnected/OnDisconnected
        public override Task OnConnectedAsync()
        {
           
                try
                {
                    var user = _HrmsContext.HrmsEmployee.Where(u => u.Codempid == IdentityName).FirstOrDefault();
                    var username = _HrmsContext.HrmsEmployee.Find(IdentityName).Namempe;
                    username = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(username.ToLower());
                var userViewModel = _mapper.Map<HrmsEmployee, UserViewModel>(user);
                    userViewModel.Device = GetDevice();
                    userViewModel.CurrentRoom = "";
                    userViewModel.DisplayName = username;
                    _Connections.Add(userViewModel);
                    _ConnectionsMap.Add(IdentityName, Context.ConnectionId);

                    Clients.Caller.SendAsync("getProfileInfo", username, "~/../../image/user/" + user.Codempid.Trim()+".jpg");
                }
                catch (Exception ex)
                {
                    Clients.Caller.SendAsync("OnConnected:" + ex.Message);
                }
           

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception ex)
        {
            try
            {
                var user = _Connections.Where(u => u.Username == IdentityName).First();
                _Connections.Remove(user);

                // Tell other users to remove you from their list
                Clients.OthersInGroup(user.CurrentRoom).SendAsync("removeUser", user);

                // Remove mapping
                _ConnectionsMap.Remove(user.Username);
            }
            catch (Exception ext)
            {
                Clients.Caller.SendAsync("OnDisconnected: " + ext.Message);
            }

            return base.OnDisconnectedAsync(ex);
        }

    #endregion

        private string IdentityName
        {
            get { return Context.User.Identity.Name; }
        }

      

        private string GetDevice()
        {          
            return "Web";
        }
        public ChatHub(IMapper mapper, ESmartOfficeContext context, DocumentControlContext DocumentControlContext, IUserConnectionManager userConnectionManager,
            HRMSLocalContext HrmsContext, IHostingEnvironment hostingEnvironmen)
        {
            _context = context;
            _userConnectionManager = userConnectionManager;
            _DocumentContext = DocumentControlContext;
            _mapper = mapper;
            _HrmsContext = HrmsContext;
            _hostingEnvironment = hostingEnvironmen;
        }
    }
}