using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartOffice.Hubs;
using SmartOffice.IResponsitory;
using SmartOffice.Models;
using SmartOffice.ModelsDocControl;
using SmartOffice.ModelsEsmartOffice;
using SmartOffice.ModelsHRMSLocal;
using SmartOffice.Persistence;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace SmartOffice.Class
{
    public class ESmartNotiRepository : IESmartNotiRepository
    {
        private readonly ESmartNotiContext _contextFactory;
        private readonly IUserConnectionManager _userConnectionManager;
        private readonly ESmartOfficeContext _ESmartOfficeContext;
        private readonly HRMSLocalContext _hRMSLocalContext;
        private readonly DocumentControlContext _DocumentContext;
        private readonly IHubContext<NotiHub> _notificationUserHubContext;
        //public IEnumerable<DocumentItem> ItemResult => GetItemResult();
        private readonly IConfiguration _configuration;
        public ESmartNotiRepository(ESmartNotiContext context, IUserConnectionManager userConnectionManager, IConfiguration configuration, ESmartOfficeContext ESmart,
            HRMSLocalContext hRMSLocalContext, DocumentControlContext DocumentControlContext,IHubContext<NotiHub> notificationUserHubContext)
        {
            _contextFactory = context;
            _userConnectionManager = userConnectionManager;
            _configuration = configuration;
            _ESmartOfficeContext = ESmart;
            _hRMSLocalContext = hRMSLocalContext;
            _DocumentContext = DocumentControlContext;
            _notificationUserHubContext = notificationUserHubContext;
        }

        //public void Boardcastnotify()
        //{
        //    var all = _userConnectionManager.GetAllUserConnections();
        //    List<string> JobStatus;
        //    ConnDoc dp;

        //    try
        //    {
        //        dp = new ConnDoc(_configuration);
        //        var ApproveDoc = dp.GetWaitApprove("", "", "","", "", "");
        //        var EditDoc = dp.GetEditDocument();
        //        foreach (var item in all)
        //        {
        //            JobStatus = new List<string>();
                
        //            var thisgroup = _ESmartOfficeContext.AspNetUsers.Where(i => i.UserName == item).FirstOrDefault();
        //            var thisuserdata = _hRMSLocalContext.HrmsEmployee.Where(i => i.Codempid == item).FirstOrDefault();
        //            //var username = User.Identity.Name;

                   
        //            var GroupDoc = dp.GetGroupIssueDocument(thisgroup.GroupCateg, item).ToList();



        //            //Approve
        //            var ApproveDocument = ApproveDoc.Where(i => (i.CODEMPID == item || (i.ReqOperatorID == item && i.CODEMPID == ""))
        //            && i.DocumentStatus.Trim() != "Complete" && i.DocumentStatus.Trim() != "Cancel" && i.DocumentStatus.Trim() != "Draft").Distinct().OrderByDescending(i => i.IssuedDate).Select(x => new
        //            {
        //                DocumentNo = x.DocumentNo.Trim(),
        //                DocumentCode = x.DocumentCode.Trim(),
        //                DocumentNameE = x.DocumentNameE.Trim(),
        //                DocumentNameT = x.DocumentNameT.Trim(),
        //                DocumentNameJ = x.DocumentNameJ.Trim(),
        //                ReqDescription1 = x.ReqDescription1.Trim(),
        //                ReqOperatorName = x.ReqOperatorName.Trim(),
        //                x.IssuedDate,
        //                DocumentStatus = x.DocumentStatus.Trim() ?? "",
        //                x.SeqNo
        //            }).Distinct().ToList();
        //            var dataApprove = new
        //            {
        //                rows = ApproveDocument
        //            };

        //            //Edit
        //            var EditDocument = EditDoc.Where(i => i.ApproverOperatorID.Trim() == item
        //            && i.DocumentStatus != "Complete" && i.DocumentStatus.Trim() != "Cancel" && i.DocumentStatus.Trim() != "Draft").Distinct().OrderByDescending(i => i.IssuedDate).Select(x => new
        //            {
        //                DocumentNo = x.DocumentNo.Trim(),
        //                DocumentCode = x.DocumentCode.Trim(),
        //                DocumentNameE = x.DocumentNameE.Trim(),
        //                DocumentNameT = x.DocumentNameT.Trim(),
        //                DocumentNameJ = x.DocumentNameJ.Trim(),
        //                ReqDescription1 = x.ReqDescription1.Trim(),
        //                ReqOperatorName = x.ReqOperatorName.Trim(),
        //                //IssuedDate = x.IssuedDate == null ? "" : Convert.ToDateTime(x.IssuedDate).ToString("dd/MM/yy HH:mm"),
        //                x.IssuedDate,
        //                DocumentStatus = x.DocumentStatus.Trim() ?? "",
        //                x.UpdDate,
        //                x.SeqNo

        //            }).Distinct().ToList();
        //            var dataEdit = new
        //            {
        //                rows = EditDocument
        //            };

        //            //Pending Status
        //            JobStatus.Add(ApproveDocument.Distinct().Count().ToString());

        //            //Reject 
        //            JobStatus.Add(ApproveDocument.Where(i => i.DocumentStatus.Trim() == "Reject").Distinct().Count().ToString());


        //            //Over time
        //            JobStatus.Add(ApproveDocument.Where(i => ((DateTime.Now - Convert.ToDateTime(i.IssuedDate)).Days >= 7)).Distinct().Count().ToString());



        //            //MyDoc
        //            var MyDocument = (from p in _DocumentContext.DocumentItem
        //                              where p.ReqOperatorId == item
        //                              select new
        //                              {
        //                                  DocumentNo = p.DocumentNo.Trim(),
        //                                  DocumentCode = p.DocumentCode.ToString().Trim(),
        //                                  DocumentNameE = p.DocumentNameE.Trim(),
        //                                  DocumentNameT = p.DocumentNameT.Trim(),
        //                                  DocumentNameJ = p.DocumentNameJ.Trim(),
        //                                  ReqDescription1 = p.ReqDescription1.Trim(),
        //                                  ReqOperatorName = p.ReqOperatorName.Trim(),
        //                                  p.IssuedDate,
        //                                  DocumentStatus = p.DocumentStatus.Trim() ?? ""
        //                              }).OrderByDescending(i => i.IssuedDate).ToList();
        //            var dataMyDoc = new
        //            {
        //                rows = MyDocument
        //            };
        //            //Pending Status
        //            JobStatus.Add(MyDocument.Where(i => i.DocumentStatus != "Complete" && i.DocumentStatus.Trim() != "Cancel" && i.DocumentStatus.Trim() != "Draft").Distinct().Count().ToString());

        //            //Reject 
        //            JobStatus.Add(MyDocument.Where(i => i.DocumentStatus.Trim() == "Reject").Distinct().Count().ToString());


        //            //Over time
        //            JobStatus.Add(MyDocument.Where(i => i.DocumentStatus != "Complete" && i.DocumentStatus.Trim() != "Cancel" && i.DocumentStatus.Trim() != "Draft"
        //            && ((DateTime.Now - Convert.ToDateTime(i.IssuedDate)).Days >= 7)).Distinct().Count().ToString());


        //            //GroupIssue
        //            var GroupIssueDocument = GroupDoc.Distinct().OrderByDescending(i => i.IssuedDate).Select(x => new
        //            {
        //                DocumentNo = x.DocumentNo.Trim(),
        //                DocumentCode = x.DocumentCode.Trim(),
        //                DocumentNameE = x.DocumentNameE.Trim(),
        //                DocumentNameT = x.DocumentNameT.Trim(),
        //                DocumentNameJ = x.DocumentNameJ.Trim(),
        //                ReqDescription1 = x.ReqDescription1.Trim(),
        //                ReqOperatorName = x.ReqOperatorName.Trim(),
        //                x.IssuedDate,
        //                DocumentStatus = x.DocumentStatus.Trim() ?? "",
        //                x.FlagUpdDate
        //            }).Distinct().ToList();
        //            var dataGroupIssue = new
        //            {
        //                rows = GroupIssueDocument
        //            };


        //            var connections = _userConnectionManager.GetUserConnections(item);
        //            if (connections != null && connections.Count > 0)
        //            {
        //                foreach (var connectionId in connections)
        //                {
        //                    //var return_data = ListDoc.Count();
        //                    //Random rnd = new Random();
        //                    //int month = rnd.Next(1, 13);
        //                    var return_data = new { dataapprove = dataApprove, datamydoc = dataMyDoc, itemCount = dataApprove.rows.Count(), dataedit = dataEdit, datastatus = JobStatus, datagroupissue = dataGroupIssue };

        //                    _notificationUserHubContext.Clients.Client(connectionId).SendAsync("ReceiveDocPending", return_data);//send to user 
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        all = null;
        //        JobStatus = null;
        //        dp = null;
        //        //Force garbage collection.
        //        GC.Collect();
        //        // Wait for all finalizers to complete before continuing.
        //        GC.WaitForPendingFinalizers();
        //    }
        //    finally
        //    {
        //        all = null;
        //        JobStatus = null;
        //        dp = null;
        //        //Force garbage collection.
        //        GC.Collect();
        //        // Wait for all finalizers to complete before continuing.
        //        GC.WaitForPendingFinalizers();
        //    }


        //}
        public void Boardcastnotify(string DocumentNo, string Username)
        {
            var all = _userConnectionManager.GetAllUserConnections();
         
            ConnDoc dp;

            try
            {
                dp = new ConnDoc(_configuration);
                var ApproveDoc = dp.GetWaitApprove();
                var EditDoc = dp.GetEditDocument();

                var distinctemp = ApproveDoc.Where(i=>i.DocumentNo == DocumentNo).Select(i => i.CODEMPID).Distinct().ToList();
                distinctemp.AddRange(EditDoc.Where(i=>i.DocumentNo == DocumentNo).Select(i => i.ApproverOperatorID).Distinct().ToList());
                distinctemp.AddRange(ApproveDoc.Where(i => i.DocumentNo == DocumentNo).Select(i => i.ReqOperatorID).Distinct().ToList());
                distinctemp.AddRange(EditDoc.Where(i => i.DocumentNo == DocumentNo).Select(i => i.ReqOperatorID).Distinct().ToList());

                if (Username != null)
                {
                    distinctemp.Add(Username);
                }                              
                var allempstring = all.Where(x => distinctemp.Contains(x)).Distinct().ToList(); //connect user open page

                foreach (var item in allempstring)
                {
                    List<string> JobStatus = new List<string>();
                    //Approve
                    var ApproveDocument = ApproveDoc.Where(i => i.CODEMPID == item ).OrderByDescending(i => i.IssuedDate).Select(x => new
                    {
                        DocumentNo = x.DocumentNo.Trim(),
                        DocumentCode = x.DocumentCode.Trim(),
                        DocumentNameE = x.DocumentNameE.Trim(),
                        DocumentNameT = x.DocumentNameT.Trim(),
                        DocumentNameJ = x.DocumentNameJ.Trim(),
                        ReqDescription1 = x.ReqDescription1.Trim(),
                        ReqOperatorName = x.ReqOperatorName.Trim(),
                        x.IssuedDate,
                        x.IssuedDateChange,
                        DocumentStatus = x.DocumentStatus.Trim() ?? "",
                        x.SeqNo,
                        x.approvepast,
                        x.Countapprovepast,
                        x.CountIssuedDate
                    }).ToList();
                    var dataApprove = new
                    {
                        rows = ApproveDocument
                    };

                    //Edit
                    var EditDocument = EditDoc.Where(i => i.ApproverOperatorID.Trim() == item).OrderByDescending(i => i.IssuedDate).Select(x => new
                    {
                        DocumentNo = x.DocumentNo.Trim(),
                        DocumentCode = x.DocumentCode.Trim(),
                        DocumentNameE = x.DocumentNameE.Trim(),
                        DocumentNameT = x.DocumentNameT.Trim(),
                        DocumentNameJ = x.DocumentNameJ.Trim(),
                        ReqDescription1 = x.ReqDescription1.Trim(),
                        ReqOperatorName = x.ReqOperatorName.Trim(),
                        x.IssuedDate,
                        x.IssuedDateChange,
                        DocumentStatus = x.DocumentStatus.Trim() ?? "",
                        x.UpdDate,
                        x.SeqNo,
                        x.CountIssuedDate
                    }).ToList();
                    var dataEdit = new
                    {
                        rows = EditDocument
                    };

                    //My Doc
                    var MyDoc = dp.GetMyDocument(item).ToList();
                    var MyDocument = MyDoc.Select(p => new
                    {
                        DocumentNo = p.DocumentNo.Trim(),
                        DocumentCode = p.DocumentCode.ToString().Trim(),
                        DocumentNameE = p.DocumentNameE.Trim(),
                        DocumentNameT = p.DocumentNameT.Trim(),
                        DocumentNameJ = p.DocumentNameJ.Trim(),
                        ReqDescription1 = p.ReqDescription1.Trim(),
                        ReqOperatorName = p.ReqOperatorName.Trim(),
                        p.IssuedDate,
                        p.IssuedDateChange,
                        DocumentStatus = p.DocumentStatus.Trim() ?? "",
                        p.NextApprove,
                        p.CountIssuedDate,
                        p.YearMonth
                    }).ToList().OrderByDescending(i => i.IssuedDate);
                    var dataMyDoc = new
                    {
                        rows = MyDocument
                    };

                    //Pending Status menu approve doc
                    JobStatus.Add(ApproveDocument.Distinct().Count().ToString());

                    //Reject menu approve doc
                    JobStatus.Add(ApproveDocument.Where(i => i.DocumentStatus.Trim() == "Reject").Distinct().Count().ToString());


                    //Over time menu approve doc
                    JobStatus.Add(ApproveDocument.Where(i => (Convert.ToInt32(i.Countapprovepast) >= 7)).Distinct().Count().ToString());


                    //Pending Status
                    JobStatus.Add(MyDocument.Where(i => i.DocumentStatus.Trim() != "Complete" && i.DocumentStatus.Trim() != "Cancel" && i.DocumentStatus.Trim() != "Draft").Distinct().Count().ToString());

                    //Reject 
                    JobStatus.Add(MyDocument.Where(i => i.DocumentStatus.Trim() == "Reject").Distinct().Count().ToString());


                    //Over time
                    JobStatus.Add(MyDocument.Where(i => i.DocumentStatus.Trim() != "Complete" && i.DocumentStatus.Trim() != "Cancel" && i.DocumentStatus.Trim() != "Draft"
                    && (Convert.ToInt32(i.CountIssuedDate) >= 7)).Distinct().Count().ToString());



                    var connections = _userConnectionManager.GetUserConnections(item);
                    if (connections != null && connections.Count > 0)
                    {
                        foreach (string connectionId in connections)
                        {
                            var return_data = new { dataapprove = dataApprove, datamydoc = dataMyDoc, itemCount = dataApprove.rows.Count(), dataedit = dataEdit, datastatus = JobStatus };
                            _notificationUserHubContext.Clients.Client(connectionId).SendAsync("ReceiveDocPending", return_data);//send to user 
                        }
                    }
                }
            }
            catch
            {
                all = null;
                dp = null;
                //Force garbage collection.
                GC.Collect();
                // Wait for all finalizers to complete before continuing.
                GC.WaitForPendingFinalizers();
            }
            finally
            {
                all = null;
                dp = null;
                //Force garbage collection.
                GC.Collect();
                // Wait for all finalizers to complete before continuing.
                GC.WaitForPendingFinalizers();
            }
        }       
    }
}
