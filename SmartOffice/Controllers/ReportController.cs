using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FastReport.Utils;
using FastReport.Web;
using Microsoft.AspNetCore.Mvc;
using SmartOffice.Models;
using SmartOffice.ModelsDocControl;

namespace SmartOffice.Controllers
{
    public class ReportController : Controller
    {
        private readonly DocumentControlContext _DocumentContext;
        string ReportsFolder = FindReportsFolder();
        public ReportController(DocumentControlContext DocumentControlContext)
        {
            this._DocumentContext = DocumentControlContext;
        }
        public static string FindReportsFolder()
        {
            string FReportsFolder = "";
            string thisFolder = FastReport.Utils.Config.ApplicationFolder;

            for (int i = 0; i < 6; i++)
            {
                string dir = Path.Combine(thisFolder, "Reports");
                if (Directory.Exists(dir))
                {
                    string rep_dir = Path.GetFullPath(dir);
                    if (System.IO.File.Exists(Path.Combine(rep_dir, "reports.xml")))
                    {
                        FReportsFolder = rep_dir;
                        break;
                    }
                }
                thisFolder = Path.Combine(thisFolder, @"..");
            }
            return FReportsFolder;
        }

        public IActionResult Index()
        {

          var Report = new WebReport();

            var reportToLoad = "Table";


            Report.Report.Load(Path.Combine(ReportsFolder, $"{reportToLoad}.frx"));

            var dataSet = new DataSet();
            dataSet.ReadXml(Path.Combine(ReportsFolder, "nwind.xml"));
            Report.Report.RegisterData(dataSet, "NorthWind");
            ViewBag.WebReport = Report;

            return View();
          
        }
    }
}