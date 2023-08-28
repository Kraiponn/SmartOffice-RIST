
using Microsoft.AspNetCore.Http;
using SmartOffice.eAppointment.ModelsForm;
using SmartOffice.EAppointment.ModelsForm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartOffice.EAppointment.IResponsitory
{
    public interface IEAppointment
    {
        Task<List<Appointment>> GetEvents();
        Task<List<VewOperatorAppointment>> GetEventsSetting(string UserName);
        Task<List<VewOperatorAppointment>> GetEventsDay(string UserName);
        Task<dynamic> SaveEventAsync(int AppointmentId, string Subject, DateTime StartDate, DateTime EndDate, string Description, string ThemeColor,
            bool IsFullDay, string AppointType, string UserName, string invitepeople, List<IFormFile> files);
        Task<dynamic> DeleteEvent(Appointment e, string UserName, List<string> invitepeople); 
        Task<dynamic> DeleteInvitepeople(int appointment_ID, string operatorID);
        Task<dynamic> GetCalendarMain(string UserName);
        Task<dynamic> GetCalendarMainUser(string UserName);
        Task<dynamic> GetCalendarMainprivate(string newstype, string _user);
        Task<dynamic> Getpeople_inClass(int AppointmentId);
        void JoinAppointment(int appointmentid, string username);
        List<AppointmentAttachFile> GetFileAttachdata(int AppointmentId);
        dynamic Deletefile(int AppointmentId, string filename);
    }
}
