using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartOffice.eManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace SmartOffice.EManagement.IResponsitory
{
    public interface IMachineOperation
    {
        Task<dynamic> ReadFromSourceAsync(IFormFile file, string datereturn);
        Task<List<SelectListItem>> GetSection();
        Task<List<SelectListItem>> GetOperationName();
        Task<FileStreamResult> ReportByDept(string Dept);
        Task<FileStreamResult> ReportByUser(string UserResponse);    
        DataTable Excel_To_DataTable(string filepath, int sheetindex);
        Tuple<bool,string> ValidateFileMachineReport(string Filenames, string DocCode, string DocNo);
        MemoryStream PrintReport(dynamic documentItemProgresses , string File,byte[] PdfApprove, string DocCode, string DocNo);
    }
}
