using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SmartOffice.Models;
using SmartOffice.ModelsDocControl;
using SmartOffice.Class;
using SmartOffice.Services;
using SmartOffice.IResponsitory;
using SmartOffice.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using SmartOffice.ModelsEsmartOffice;
using SmartOffice.ModelsHRMSLocal;
using SmartOffice.Models.ViewModel;
namespace SmartOffice.Controllers
{
    public class EDashboardController : Controller
    {
        private readonly ESmartOfficeContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly DocumentControlContext _DocumentContext;
        private readonly IPDFFormService _samplePDFFormService;
        private readonly IHubContext<NotiHub> _notificationUserHubContext;
        private readonly IUserConnectionManager _userConnectionManager;
        private IHostingEnvironment _hostingEnvironment;
        private readonly HRMSLocalContext _hRMSLocalContext;

        public EDashboardController(UserManager<ApplicationUser> userManager, IConfiguration configuration, DocumentControlContext DocumentControlContext, IHubContext<NotiHub> notificationUserHubContext,
            IUserConnectionManager userConnectionManager, IPDFFormService PDFFormService, IHostingEnvironment hostingEnvironment, ESmartOfficeContext context, HRMSLocalContext hRMSLocalContext)
        {
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
            this._configuration = configuration;
            this._DocumentContext = DocumentControlContext;
            _notificationUserHubContext = notificationUserHubContext;
            _userConnectionManager = userConnectionManager;
            _context = context;
            _samplePDFFormService = PDFFormService;
 
            _hRMSLocalContext = hRMSLocalContext;
        }

        public JsonResult getDataAllformDoc()
        {
            ConnDashboard objrun = new ConnDashboard(_configuration);
            DataChartAllformDoc _DataAllFormDoc = new DataChartAllformDoc();

            string ChartCatg = "AllformatDoc";
            _DataAllFormDoc = objrun.GetAllformDoc(ChartCatg);




            return Json(new { data = _DataAllFormDoc.CountDoc, datas = _DataAllFormDoc.NameDepart });

        }

        public JsonResult getDataCurrentMonthDoc()
        {
            ConnDashboard objrun = new ConnDashboard(_configuration);
            DataChartcurrentmonthDoc _DataAllFormDoc = new DataChartcurrentmonthDoc(); 
            string ChartCatg = "currentMonthDoc";
             _DataAllFormDoc = objrun.GetCurrentmonthlyDoc(ChartCatg); 

            return Json(new { data  = _DataAllFormDoc.NameDepart , dataD = _DataAllFormDoc.DaftDoc, dataP = _DataAllFormDoc.ProcessDoc, dataC = _DataAllFormDoc.CompleteDoc });

        }

        public JsonResult getDataAllDoc()
        {
            ConnDashboard objrun = new ConnDashboard(_configuration);
            DataChartcurrentmonthDoc _DataAllFormDoc = new DataChartcurrentmonthDoc();
            string ChartCatg = "AllDoc";
            _DataAllFormDoc = objrun.GetCurrentmonthlyDoc(ChartCatg);

            return Json(new { data = _DataAllFormDoc.NameDepart, dataD = _DataAllFormDoc.DaftDoc, dataP = _DataAllFormDoc.ProcessDoc, dataC = _DataAllFormDoc.CompleteDoc });

        }
    }
}