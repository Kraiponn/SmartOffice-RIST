using AutoMapper;
using SmartOffice.EHelpdesk.Helpers;
using SmartOffice.EHelpdesk.Models;
using SmartOffice.EHelpdesk.Models.ViewModels;
using System;
using System.Globalization;

namespace SmartOffice.EHelpdesk.Mappings
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<MessageModel, MessageViewModel>()
                .ForMember(dst => dst.From, opt => opt.MapFrom(x => 
                    CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.FromUser.Namempe.ToLower())))
                .ForMember(dst => dst.To, opt => opt.MapFrom(x => x.ToRoom.Name.ToString()))
                .ForMember(dst => dst.Avatar, opt => opt.MapFrom(x => "../../image/User/" + x.FromUser.Codempid.Trim()+".jpg"))
                .ForMember(dst => dst.Content, opt => opt.MapFrom(x => BasicEmojis.ParseEmojis(x.Content)))
                .ForMember(dst => dst.Timestamp, opt => opt.MapFrom(x => new DateTime(long.Parse(x.Timestamp)).ToLongTimeString()));
            CreateMap<MessageViewModel, MessageModel>();
        }
    }
}