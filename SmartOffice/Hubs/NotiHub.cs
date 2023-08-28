
using Microsoft.AspNetCore.SignalR;
using SmartOffice.IResponsitory;
using SmartOffice.ModelsDocControl;
using SmartOffice.ModelsForm;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SmartOffice.Hubs
{
    public class NotiHub:Hub
    {
       // public static readonly ConcurrentDictionary<string, UserHubModels> Users =new ConcurrentDictionary<string, UserHubModels>(StringComparer.InvariantCultureIgnoreCase);
        //private readonly ESmartOfficeContext context = new ESmartOfficeContext();
        //private readonly ESmartOfficeContext _context;
        private readonly DocumentControlContext _DocumentContext;
        private readonly IUserConnectionManager _userConnectionManager;
        //public NotiHub(ESmartOfficeContext context, DocumentControlContext DocumentControlContext, IUserConnectionManager userConnectionManager, ESmartOfficeContext Icontext)
        //{
        //    _context = Icontext;
        //    _DocumentContext = DocumentControlContext;
        //    _userConnectionManager = userConnectionManager;
        //}
        public Task SendValue(int value)
        {
            return Clients.All.SendAsync("ReceiveValue", value);
        }
       
        ////Specific User Call
        //public void SendNotification()
        //{
        //    try
        //    {
        //        //Send To
        //        string loggedUser = Context.User.Identity.Name;
        //        if (Users.TryGetValue(loggedUser, out UserHubModels receiver))
        //        {

        //            var ListDoc = _DocumentContext.PendingDoc.Where(i => i.ApproveName == loggedUser).Select(x => new
        //            {
        //                x.DocumentNo,
        //                x.DocumentName,
        //                x.ReqOperatorName,
        //                x.IssuedDate,
        //                DocumentStatus = x.DocumentStatus.Trim() ?? ""
        //            }).ToList();
        //            var ListDraft = _DocumentContext.DocumentItem.Where(i => i.UserName == loggedUser && i.DocumentStatus == "Draft").Select(x => new
        //            {
        //                x.DocumentNo,
        //                x.ReqDescription1,
        //                x.AddDate,
        //            }).ToList();

        //            object return_data = new { data = ListDoc, datadraft = ListDraft, itemCount = ListDoc.Count() };

        //            var cid = receiver.ConnectionIds.FirstOrDefault();                
        //            Clients.Client(cid).SendAsync("ReceiveDocPending", return_data);//send to user 
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //}

        //private string LoadNotifData(string userId)
        //{
        //    int total = 0;
        //    var query = (from t in context.Notifications
        //                 where t.SentTo == userId
        //                 select t)
        //                .ToList();
        //    total = query.Count;
        //    return total.ToString();
        //}


        //public override async Task OnConnectedAsync()
        //{
        //    string userName = Context.User.Identity.Name;
        //    string connectionId = Context.ConnectionId;

        //    var user = Users.GetOrAdd(userName, _ => new UserHubModels
        //    {
        //        UserName = userName,
        //        ConnectionIds = new HashSet<string>()
        //    });

        //    lock (user.ConnectionIds)
        //    {
        //        user.ConnectionIds.Add(connectionId);
        //        if (user.ConnectionIds.Count == 1)
        //        {

        //            Clients.Others.userConnected(userName);

        //        }
        //    }          
        //}
        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    await base.OnDisconnectedAsync(exception);


        //    string userName = Context.User.Identity.Name;
        //    string connectionId = Context.ConnectionId;

        //    UserHubModels user;
        //    Users.TryGetValue(userName, out user);

        //    if (user != null)
        //    {
        //        lock (user.ConnectionIds)
        //        {
        //            user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));
        //            if (!user.ConnectionIds.Any())
        //            {
        //                UserHubModels removedUser;
        //                Users.TryRemove(userName, out removedUser);
        //                Clients.Others.userDisconnected(userName);
        //            }
        //        }
        //    }

        //    return base.OnDisconnected(stopCalled);


        //}

        public NotiHub(IUserConnectionManager userConnectionManager,DocumentControlContext DocumentControlContext)
        {
            _userConnectionManager = userConnectionManager;
            _DocumentContext = DocumentControlContext;

        }

        public string GetConnectionId()
        {
            var userId = Context.User.Identity.Name;
            if (userId!=null)
            {
                _userConnectionManager.KeepUserConnection(userId, Context.ConnectionId);
                var all = _userConnectionManager.GetAllUserConnections();
                return all.ToList().Count().ToString();
            }
            return null;
        }



        public override Task OnDisconnectedAsync(Exception ex)
        {

            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            if (userName == null)
                return base.OnDisconnectedAsync(ex);

            //Users.TryGetValue(userName, out UserHubModels user);

            //if (user != null)
            //{
            //    lock (user.ConnectionIds)
            //    {
            //        user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));
            //        if (!user.ConnectionIds.Any())
            //        {
            //            Users.TryRemove(userName, out UserHubModels removedUser);
            //            //Clients.Others.SendAsync("userDisconnected", userName);
            Clients.User(userName).SendAsync("userDisconnected", userName);

            //        }
            //    }
            //}
            //_userConnectionManager.RemoveUserConnection(userName);
            _userConnectionManager.RemoveUserConnection(connectionId);
            return base.OnDisconnectedAsync(ex);
        }
    


    public override Task OnConnectedAsync()
    {
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            if (userName!=null)
            {
            //    var user = Users.GetOrAdd(userName, _ => new UserHubModels
            //    {
            //        UserName = userName,
            //        ConnectionIds = new HashSet<string>()
            //    });

            //    lock (user.ConnectionIds)
            //    {
            //        user.ConnectionIds.Add(connectionId);
            //    if (user.ConnectionIds.Count == 1)
            //    {
                    //Clients.Others.SendAsync("UserConnected", userName);
                    Clients.User(userName).SendAsync("UserConnected", userName);
            //        }
            //}
           }
            return base.OnConnectedAsync();
        }

       
    }
}
