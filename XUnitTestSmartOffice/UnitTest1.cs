using SmartOffice.EmailCore.Models;
using SmartOffice.Controllers;
using SmartOffice.EmailCore.IResponsitory;
using SmartOffice.IResponsitory;
using SmartOffice.Models;
using SmartOffice.Services;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using System;
using System.Xml;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.SyndicationFeed;

namespace XUnitTestSmartOffice
{
    public class UnitTest1
    {
       
        private readonly ISendEmail _Sendmail;
        private readonly ITestOutputHelper _output;
        private IHostingEnvironment _hostingEnvironment;
        public UnitTest1(ITestOutputHelper output, ISendEmail Sendmail, IHostingEnvironment hostingEnvironment)
        {
            _Sendmail =  Sendmail;
            _hostingEnvironment = hostingEnvironment;
            _output = output;
        }


        //[Fact]
        //public void FillForm()
        //{

        //    PDFSampleForm sampleFormModel = new PDFSampleForm()
        //    {
        //        request_date = "23/มิ.ย./2562",
        //        dept = "IS",
        //        sect = "System Develop1",
        //        sup_evaluate = "10",
        //        name = "Tanaphat Mawan",
        //        emp_no = "009254",
        //        shift = "-",
        //        quest1 = "test1",
        //        quest1_score = 20,
        //        quest2 = "test2",
        //        quest2_score = 15,
        //        quest3 = "test3",
        //        quest3_score = 10,
        //        quest4 = "test4",
        //        quest4_score = 15,
        //        quest5 = "test5",
        //        quest5_score = 15,
        //        evaluate_desc = "ok",

        //    };
        //    using (Stream pdfInputStream = new FileStream(path: "HR030.pdf", mode: FileMode.Open))
        //    using (Stream resultPDFOutputStream = new FileStream(path: "SampleHR030-1.pdf", mode: FileMode.Create))
        //    using (Stream resultPDFStream = _samplePDFFormService.FillForm(pdfInputStream, sampleFormModel))
        //    {
        //        resultPDFStream.Position = 0;
        //        _output.WriteLine(resultPDFStream.Position.ToString());
        //        resultPDFStream.CopyTo(resultPDFOutputStream);
        //    }
        //}



        //[Fact]
        //public void TestMail()
        //{
        //    string htmlbody = System.IO.File.ReadAllText("alert _realtime.html");
        //    var a = _ISendEmail.EmailApprove_Alert(null, "Pranida.Yat@Rist.Local", "Tanaphat.Maw@Rist.Local", "Tanaphat.Maw@Rist.Local", null, "Test", "Test Mail Body", "test", null, null, null, null);
        //}

        //[Fact]
        //public void FillForm2()
        //{
        //    Dictionary<string, string> My_dict1 = new Dictionary<string, string>(){
        //        {"SCI003","test1"},
        //        {"SCI004","test2"},
        //        {"SCI005","test3"},
        //        {"SCI006","test4"},
        //        {"SCI007","test5"},
        //        {"SCI008","test6"},
        //        {"SCI009","test7"},
        //        {"SCI010","test8"},
        //        {"SCI011","test9"},
        //        {"SCI012","test10"},
        //        {"SCI013","test11"},
        //        {"SCI014","test12"},
        //        {"SCI015","test13"},
        //        {"SCI016","test14"},
        //        {"SCI017","test15"},
        //        {"SCI018","test16"},
        //        {"SCI019","test17"},
        //        {"SCI020","test18"},
        //        {"SCI021","test19"},
        //    };


        //    using (Stream pdfInputStream = new FileStream(path: "\\\\10.29.1.88\\E$\\eSmartOffice\\Document\\SC100F.pdf", mode: FileMode.Open))
        //    using (Stream resultPDFOutputStream = new FileStream(path: "SC100F-2.pdf", mode: FileMode.Create))
        //    using (Stream resultPDFStream = _samplePDFFormService.FillForm(pdfInputStream, My_dict1))

        //    {

        //        resultPDFStream.Position = 0;
        //        _output.WriteLine(resultPDFStream.Position.ToString());
        //        resultPDFStream.CopyTo(resultPDFOutputStream);
        //    }
        //}
        //[Fact]
        //public static string GenerateKey()
        //{
        //    long i = 1;
        //    foreach (byte b in Guid.NewGuid().ToByteArray())
        //    {
        //        i *= ((int)b + 1);
        //    }
        //    var ss = string.Format("{0:x}", i - DateTime.Now.Ticks);

        //    return string.Format("{0:x}", i - DateTime.Now.Ticks);

        //}
        [Fact]
        public void Testreadxml()
        {
            string webRootPath = _hostingEnvironment.WebRootPath + "\\File\\fxrate-all.xml";
            string newPath = Path.Combine(webRootPath);
            // make XML document class instance
            var objXmlDoc = new XmlDocument();
            // read XML file
            objXmlDoc.Load(newPath);

            for (int i = 0; i <= objXmlDoc.ChildNodes[1].ChildNodes.Count - 1; i++)
            {
                //if (objXmlDoc.ChildNodes[1].ChildNodes[i].Attributes["systemname"].Value.ToUpper() == pstrSystemName.ToUpper())
                //{
                //    for (int j = 0; j <= objXmlDoc.ChildNodes[1].ChildNodes[i].ChildNodes.Count - 1; j++)
                //    {
                //        if (objXmlDoc.ChildNodes[1].ChildNodes[i].ChildNodes.Item(j).Name == pstrFieldName)
                //        {
                //            strRet = objXmlDoc.ChildNodes[1].ChildNodes[i].ChildNodes.Item(j).InnerText;
                //            break;
                //        }
                //    }
                //    break;
                //}
            }

        }

    }
}
