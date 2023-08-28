
using SmartOffice.eManagement.ModelsManagementControl;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SmartOffice.eManagement.Models;

namespace SmartOffice.EManagement.IResponsitory
{
    public interface IEJounalVoucher
    {
        List<SelectListItemModel> GetSectionCode();
        List<JounalVoucherTempleate> Loaddatamaking(string monthperiod, string seccode);
        List<JournalVoucher> Loaddataexport(string monthperiod, string seccode);
    }
}
