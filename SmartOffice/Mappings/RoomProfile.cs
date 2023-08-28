using AutoMapper;
using SmartOffice.EHelpdesk.Models;
using SmartOffice.EHelpdesk.Models.ViewModels;
using SmartOffice.ModelsDocControl;

namespace SmartOffice.EHelpdesk.Mappings
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Rooms, RoomViewModel>();

            CreateMap<RoomViewModel, Rooms>();
        }
    }
}