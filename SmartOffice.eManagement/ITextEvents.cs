using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;

namespace SmartOffice.eManagement
{
    public class ITextEvents : PdfPageEventHelper
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate headerTemplate, footerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;
        public ITextEvents(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        #region Fields
        private string _header;
        #endregion

        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        #endregion


        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
                PrintTime = DateTime.Now;
                string fontPath = _hostingEnvironment.WebRootPath + "\\fonts\\THSarabunNew.ttf";
                bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(50, 50);
                footerTemplate = cb.CreateTemplate(50, 50);
   
        }

        public override void OnEndPage(PdfWriter writer,Document document)
        {
            base.OnEndPage(writer, document);
            string fontPath = _hostingEnvironment.WebRootPath + "\\fonts\\THSarabunNew.ttf";

            //set font ในกรณีที่เป็นภาษาไทนแนะนำให้ใช้ เป็น font Sarabun ของ sipa
            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font defaultFont = new Font(bf, 12);
            Font boldTableFont = new Font(bf, 16, Font.UNDERLINE);
            Font baseFontBig = new Font(bf, 16, Font.BOLD);
            Font headTB1 = new Font(bf, 14, Font.UNDERLINE);
            Font body = new Font(bf, 16, Font.NORMAL);
            Font body1 = new Font(bf, 12, Font.BOLD);
            Font bodysmall = new Font(bf, 9, Font.NORMAL);


            //Create PdfTable object
            PdfPTable pdfTab = new PdfPTable(3)
            {
                WidthPercentage = 100
            };

            //Row 1
            //PdfPCell pdfCell1 = new PdfPCell(new Phrase("F-QM-031", body));
            //PdfPCell pdfCell2 = new PdfPCell();
            //PdfPCell pdfCell3 = new PdfPCell(new Phrase("REV:03(EFFECTIVE DATE) FEB 21,2017", body));
            String text = "Page " + writer.PageNumber + " of ";
            String tdate = "Date : " + DateTime.Now.ToShortDateString();
            String ttime = "Time : " + DateTime.Now.ToShortTimeString();

            //Add paging to header
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 8);
                cb.SetTextMatrix(document.PageSize.GetRight(100), document.PageSize.GetTop(20));
                cb.ShowText(tdate);
                cb.SetTextMatrix(document.PageSize.GetRight(100), document.PageSize.GetTop(28));
                cb.ShowText(ttime);
                cb.EndText();
                float len = bf.GetWidthPoint(text, 8);
                //Adds "12" in Page 1 of 12
                cb.AddTemplate(headerTemplate, document.PageSize.GetRight(100) + len, document.PageSize.GetTop(45));
            }

            //Add paging to footer
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 8);
                cb.SetTextMatrix(document.PageSize.GetRight(100), document.PageSize.GetBottom(15));
                cb.ShowText(text);
                cb.EndText();
                float len = bf.GetWidthPoint(text, 8);
                cb.AddTemplate(footerTemplate, document.PageSize.GetRight(100) + len, document.PageSize.GetBottom(15));
            }

            

            pdfTab.TotalWidth = document.PageSize.Width - 67f;
            pdfTab.WidthPercentage = 100;

           
            pdfTab.WriteSelectedRows(0, -1, 35, document.PageSize.Height - 775, writer.DirectContent);
            
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 8);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText((writer.PageNumber-1).ToString());
            headerTemplate.SetFontAndSize(bf, 8);
            footerTemplate.EndText();

        }

    }
}
