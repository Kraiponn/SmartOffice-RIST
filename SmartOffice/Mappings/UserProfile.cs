using AutoMapper;
using SmartOffice.EHelpdesk.Models;
using SmartOffice.EHelpdesk.Models.ViewModels;
using SmartOffice.Models;
using SmartOffice.ModelsEsmartOffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartOffice.ModelsHRMSLocal;
using System.Globalization;

namespace SmartOffice.EHelpdesk.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<HrmsEmployee, UserViewModel>()
                .ForMember(dst => dst.Username, (IMemberConfigurationExpression<HrmsEmployee, UserViewModel, string> opt) => opt.MapFrom(x => x.Codempid.Trim()))
                .ForMember(dst => dst.DisplayName, (IMemberConfigurationExpression<HrmsEmployee, UserViewModel, string> opt) => opt.MapFrom(x => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.Namempe.ToLower())))
                .ForMember(dst => dst.Avatar, (IMemberConfigurationExpression<HrmsEmployee, UserViewModel, string> opt) => opt.MapFrom(x => "~/../../image/User/" + x.Codempid + ".jpg"));
        }
    }
}