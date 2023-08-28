using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using QRCoder;
using SmartOffice.IResponsitory;
using SmartOffice.ModelsDocControl;
using SmartOffice.ModelsForm;
using static iTextSharp.text.pdf.AcroFields;
using Document = iTextSharp.text.Document;

namespace SmartOffice.Services
{

    public class PDFFormService : IPDFFormService
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly DocumentControlContext _context;
    
        public PDFFormService(IHostingEnvironment hostingEnvironment, DocumentControlContext context)
        {
            
            _hostingEnvironment = hostingEnvironment;
            _context = context;
           
        }
      
        public Stream FillForm(Stream inputStream, List<ModelItemList_Result> model, List<DocumentItemValueTableDetail> tabledetail, List<Flow> listapprove, List<ApprovalFlow> approvalFlows ,string last)
        {

            BaseFont.AddToResourceSearch("iTextAsian.dll");
            BaseFont.AddToResourceSearch("iTextAsianCmaps.dll"); //"STSong-Light", "UniGB-UCS2-H", 
            BaseFont baseFT = BaseFont.CreateFont("STSong-Light", "UniGB-UCS2-H", BaseFont.EMBEDDED);
            string webRootPath = _hostingEnvironment.WebRootPath + "\\fonts\\THSarabunNew.ttf";      
            string JPfontPath = _hostingEnvironment.WebRootPath + "\\fonts\\Kokoro.otf";
            BaseFont bfjp = BaseFont.CreateFont(JPfontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED, true);
            BaseFont bf = BaseFont.CreateFont(webRootPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);

            Font defaultFont = new Font(bf, 16, Font.NORMAL);
            Font defaultFont1 = new Font(bfjp, 12, Font.NORMAL);


            Stream outStream = new MemoryStream();
            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            //Stream inStream = null;
            MemoryStream msOutput = new MemoryStream();

            PdfStamper stamper;
            MemoryStream msTemp = null;
            PdfReader pdfTemplate = null;
            PdfReader pdfFile = null;
            Document doc = new Document();

     

            if (listapprove.Count() != 0)
                {
                    var checkmultireport = _context.DocumentItem.Where(i => i.DocumentNo == listapprove.First().DocumentNo && i.Remark != null && i.Remark != "").FirstOrDefault();
                    if (checkmultireport != null)
                    {
                        string[] checkfunction = checkmultireport.Remark.Split(":");
                        var mainapprove = listapprove.Where(i => i.SeqNo == int.Parse(checkfunction[1]) && i.ApprovalDate != "").ToList();
                        if (checkfunction[0] == "LoopReport" && mainapprove.Count()!=0)
                        {

                        try
                        {
                            msOutput?.Flush();
                            msOutput.Position = 0;

                            var pCopy = new PdfSmartCopy(doc, msOutput);
                            doc.Open();

                            foreach (var item in mainapprove)
                            {
                                inputStream?.Flush();
                                inputStream.Position = 0;
                                msTemp = new MemoryStream();
                                pdfTemplate = new PdfReader(inputStream);
                                stamper = new PdfStamper(pdfTemplate, msTemp);
                                stamper.Writer.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                              
                                var itemaprove = listapprove.Where(i => i.SeqNo != int.Parse(checkfunction[1])).ToList();
                                itemaprove.Add(item);
                                AcroFields form = stamper.AcroFields;
                                form.AddSubstitutionFont(bfjp);
                                form.AddSubstitutionFont(bf);
                                form = StampField(form, model, tabledetail);
                                form = StampApprove(form, itemaprove, model, approvalFlows, stamper,last);                             
                                stamper.FormFlattening = true;
                                stamper.Close();
                                pdfFile = new PdfReader(msTemp.ToArray());
                                ((PdfSmartCopy)pCopy).AddPage(pCopy.GetImportedPage(pdfFile, 1));
                                pCopy.FreeReader(pdfFile);
                                form = null;
                                //msTemp.Close();
                                //pdfTemplate.Close();
                                //stamper.Close();
                                //// end loop
                                //pdfFile.Close();                         
                            }
                            pCopy.Close();
                            return msOutput;
                        }
                        catch (Exception ex)
                        {
                            var aa = ex.Message;
                            return null;
                        }
                        finally
                        {

                            doc?.Close();
                            msTemp?.Close();
                            pdfTemplate?.Close();
                            pdfReader?.Close();
                            pdfFile?.Close();
                            pdfStamper?.Close();

                            //Force garbage collection.
                            GC.Collect();
                            // Wait for all finalizers to complete before continuing.
                            GC.WaitForPendingFinalizers();
                            //send real time notification  to user
                            //msOutput?.Close();
                            //outStream?.Close();
                        }



                    }
                    }
                }
          
         
            try
            {
                inputStream?.Flush();
                inputStream.Position = 0;
                pdfReader = new PdfReader(inputStream);
                outStream?.Flush();
                outStream.Position = 0;
                int itemaprove;
                double checkpage;
                double fresult;
                if (model.Count() > 0)
                {
                    if (_context.DocumentConditionHist.Any(i => i.Template == "page" && i.DocumentNo == model.FirstOrDefault().DocumentNo) && model.FirstOrDefault().DocumentNo != "" && !model.FirstOrDefault().DocumentNo.StartsWith("D"))
                    {
                        var thiscon = _context.DocumentConditionHist.Where(i => i.Template == "page" && i.DocumentNo == model.FirstOrDefault().DocumentNo).FirstOrDefault();
                        
                        if (thiscon != null)
                        {
                            var res = thiscon.Value.ToString().Split(':');
                            switch (thiscon.Condition)
                            {

                                case "1":
                                    // Select page by condittion
                                    var detail = model.Where(i => i.ItemCode.Trim() == res[0].ToString().Trim() && i.FinalResult.Trim() == res[1].ToString().Trim()).Count();
                                    if (detail != 0)
                                    {
                                        pdfReader.SelectPages(res[2].ToString());
                                    }
                                    else
                                    {
                                        pdfReader.SelectPages(res[3].ToString());
                                    }
                                    break;

                                case "2":
                                    // loop page by Seq
                                     itemaprove = listapprove.Where(i => i.SeqNo == int.Parse(res[1])).Count();
                                     checkpage = double.Parse(itemaprove.ToString()) / double.Parse(res[2]);
                                     fresult = Math.Ceiling(checkpage);
                                    if (int.Parse(fresult.ToString())==1)
                                    {
                                        pdfReader.SelectPages("1");
                                    }
                                    else
                                    {
                                        pdfReader.SelectPages("1-"+ int.Parse(fresult.ToString()).ToString());
                                    }                                  
                                    break;
                                case "3":
                                    // select page by count Seq item
                                    itemaprove = listapprove.Where(i => i.SeqNo == int.Parse(res[1])).Count();
                                     //checkpage = double.Parse(itemaprove.ToString()) / double.Parse(res[2]);
                                     //fresult = Math.Ceiling(checkpage);
                                    if (itemaprove <= int.Parse(res[2]))
                                    {
                                        pdfReader.SelectPages(res[3].ToString());
                                    }
                                    else
                                    {
                                        pdfReader.SelectPages(res[4].ToString());
                                    }
                                    break;
                                case "4":
                                    // select page by count row table                                   
                                    itemaprove = _context.DocumentItemValueTableDetail.Where(x => x.DocumentNo == model.FirstOrDefault().DocumentNo && x.TableCode == res[1].ToString() && x.DisplayOrder == 1).Count();
                                    checkpage = double.Parse(itemaprove.ToString()) / double.Parse(res[2]);
                                    fresult = Math.Ceiling(checkpage);

                                    int pagedefault = int.Parse(res[3]);
                                    int pageother = int.Parse(res[4]);

                                    if (int.Parse(fresult.ToString()) == 1 || int.Parse(fresult.ToString()) == 0)
                                    {
                                        //pdfReader.SelectPages("1");
                                        pdfReader.SelectPages("1-" + (1 + pagedefault).ToString());
                                    }
                                    else
                                    {
                                        //pdfReader.SelectPages("1-" + int.Parse(fresult.ToString()).ToString());
                                        pdfReader.SelectPages("1-" + (int.Parse(fresult.ToString()) + pageother).ToString());
                                    }
                                    break;
                                default:
                                   
                                    break;
                            }
                           

                        }

                    }
                    else if (model.FirstOrDefault().DocumentNo.StartsWith("D"))
                    {
                        var thiscon = _context.DocumentCondition.Where(i => i.Template == "page" && i.DocumentCode == model.FirstOrDefault().DocumentCode).FirstOrDefault();
                        
                        if (thiscon != null)
                        {
                            var res = thiscon.Value.ToString().Split(':');
                            switch (thiscon.Condition)
                            {

                                case "1":
                                    // Select page by condittion
                                    var detail = model.Where(i => i.ItemCode.Trim() == res[0].ToString().Trim() && i.FinalResult.Trim() == res[1].ToString().Trim()).Count();                                    
                                        pdfReader.SelectPages(res[2].ToString());                                  
                                    break;

                                case "2":
                                    // select page by Seq
                                    itemaprove = listapprove.Where(i => i.SeqNo == int.Parse(res[1])).Count();
                                    checkpage = double.Parse(itemaprove.ToString()) / double.Parse(res[2]);
                                    fresult = Math.Ceiling(checkpage);
                                    pdfReader.SelectPages(res[3].ToString());
                                    break;
                                case "3":
                                    // select page by count Seq item
                                    itemaprove = listapprove.Where(i => i.SeqNo == int.Parse(res[1])).Count();                                
                                     pdfReader.SelectPages(res[3].ToString());                                
                                    break;
                                case "4":
                                    // select page by count row table                                   
                                    itemaprove = _context.DocumentItemValueTableDetail.Where(x => x.DocumentNo == model.FirstOrDefault().DocumentNo && x.TableCode == res[1].ToString() && x.DisplayOrder == 1).Count();
                                    checkpage = double.Parse(itemaprove.ToString()) / double.Parse(res[2]);
                                    fresult = Math.Ceiling(checkpage);

                                    int pagedefault = int.Parse(res[3]);
                                    int pageother = int.Parse(res[4]);

                                    if (int.Parse(fresult.ToString()) == 1 || int.Parse(fresult.ToString()) == 0)
                                    {
                                        //pdfReader.SelectPages("1");
                                        pdfReader.SelectPages("1-" + (1 + pagedefault).ToString());
                                    }
                                    else
                                    {
                                        //pdfReader.SelectPages("1-" + int.Parse(fresult.ToString()).ToString());
                                        pdfReader.SelectPages("1-" + (int.Parse(fresult.ToString()) + pageother).ToString());
                                    }
                                    break;
                                default:

                                    break;
                            }
                        }
                    }
                }


                pdfStamper = new PdfStamper(pdfReader, outStream);
                pdfStamper.Writer.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
             
                AcroFields form = pdfStamper.AcroFields;                   
                form.AddSubstitutionFont(bfjp);
                form.AddSubstitutionFont(bf);        
                form =  StampField(form,model,tabledetail);
                form = StampApprove(form, listapprove, model, approvalFlows,pdfStamper,last);
                pdfStamper.FormFlattening = true;        
                form =null;
                return outStream;
            }
            catch (Exception ex)
            {
                var aa = ex.Message;
                //Force garbage collection.
                GC.Collect();
                // Wait for all finalizers to complete before continuing.
                GC.WaitForPendingFinalizers();
                return null;
            }
            finally
            {
                pdfStamper?.Close();
                pdfReader?.Close();
                //Force garbage collection.
                GC.Collect();
                // Wait for all finalizers to complete before continuing.
                GC.WaitForPendingFinalizers();
                //msOutput.Close();
                //outStream.Close();
            }

        }
        public AcroFields StampField(AcroFields form, List<ModelItemList_Result> model, List<DocumentItemValueTableDetail> tabledetail)
        {
            BaseFont.AddToResourceSearch("iTextAsian.dll");
            BaseFont.AddToResourceSearch("iTextAsianCmaps.dll"); //"STSong-Light", "UniGB-UCS2-H", 
            BaseFont baseFT = BaseFont.CreateFont("STSong-Light", "UniGB-UCS2-H", BaseFont.EMBEDDED);
            //iTextSharp.text.Font font = new iTextSharp.text.Font(baseFT);
            string webRootPath = _hostingEnvironment.WebRootPath + "\\fonts\\THSarabunNew.ttf";
            string JPfontPath = _hostingEnvironment.WebRootPath + "\\fonts\\Kokoro.otf";

            //FontSelector selector = new FontSelector();
            //selector.AddFont(FontFactory.GetFont(webRootPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED));
            //selector.AddFont(FontFactory.GetFont(JPfontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED));

            BaseFont bfjp = BaseFont.CreateFont(JPfontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED, true);
            BaseFont bf = BaseFont.CreateFont(webRootPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);

            Font defaultFont = new Font(bf, 12, Font.NORMAL);
            Font defaultFont1 = new Font(bfjp, 12, Font.NORMAL);
            if (model.Count() != 0)
            {
                form.SetField("DOCNO", model.FirstOrDefault().DocumentNo);
            }

            if (model != null)
            {
                foreach (var item in model)
                {

                    switch (item.InputType.Trim())
                    {
                        case "checkbox":
                        case "cbandtxt":
                            string[] liststring = item.FinalResult.Split(',');
                            foreach (string sloop in liststring)
                            {

                                form.SetField(sloop.Trim(), "On");
                                form.SetField(sloop.Trim(), "Yes");
                            }
                            if (item.DataType == "checkspe")
                            {
                                string[] listspe = item.FinalResult.Split(',');
                                foreach (string sloop in listspe)
                                {
                                    form.SetFieldProperty(sloop.Trim(), "clrflags", PdfAnnotation.FLAGS_HIDDEN, null);/*Slash stamp*/
                                }
                            };
                            break;
                        case "combo":
                            form.SetFieldProperty(item.ItemCode.Trim(), "textfont", bf, null);
                            var textresult = _context.ValueList.Where(i => i.Id.Trim() == item.FinalResult.ToString().Trim()).FirstOrDefault();
                            if (textresult != null)
                            {
                                var text = "";
                                var checkL = false;
                                string[] words = item.Language.Split(',');
                                foreach (string word in words)
                                {
                                    var W = word.Trim().ToUpper();


                                    if (W.Contains("T"))
                                    {
                                        if (textresult.ValueE != textresult.ValueT || checkL == false)
                                        {
                                            text = text + " " + textresult.ValueT;
                                            checkL = true;
                                        }
                                    }
                                    else if (W.Contains("E"))
                                    {
                                        if (textresult.ValueE != textresult.ValueT || checkL == false)
                                        {
                                            text = text + " " + textresult.ValueE;
                                            checkL = true;
                                        }
                                    }
                                    else if (W.Contains("J"))
                                    {
                                        text = text + " " + textresult.ValueJ;

                                    }
                                }

                                form.SetField(item.ItemCode.ToString().Trim(), text);
                            }

                            break;
                        case "date":
                        case "datedef":
                        case "dateres":
                            form.SetFieldProperty(item.ItemCode.Trim(), "textfont", bf, null);
                            form.SetField(item.ItemCode.ToString().Trim(), String.Format("{0:d-MM-yy}", item.FinalResult.ToString().Trim()));
                            break;
                        case "radio":
                            if (item.DataType == "radio")
                            {
                                string[] listradio = item.FinalResult.Split(',');
                                foreach (string sloop in listradio)
                                {
                                    //form.SetField(sloop.Trim(), "1");
                                    //form.SetField(sloop.Trim(), "true");
                                    form.SetField(sloop.Trim(), "On");
                                    form.SetField(sloop.Trim(), "Yes");
                                }
                            };

                            if (item.DataType == "checkspe")
                            {
                                string[] listspe = item.FinalResult.Split(',');
                                foreach (string sloop in listspe)
                                {
                                   
                                    form.SetFieldProperty(sloop.Trim(), "clrflags", PdfAnnotation.FLAGS_HIDDEN, null);/*Slash stamp*/
                                }
                            };

                            break;
                        case "memo":
                            //form.SetFieldProperty(item.ItemCode.ToString().Trim(), "textsize", 14.0f, null);
                            form.SetField(item.ItemCode.ToString().Trim(), item.FinalResult.ToString().Trim(), item.FinalResult.ToString().Trim());
                           
                            break;
                        case "text":
                        case "txtwtcb":
                        case "time":
                        case "dateyy":
                        case "datemm":
                        case string district when district.Contains("district"):
                        case string amphoe when amphoe.Contains("amphoe"):
                        case string province when province.Contains("province"):
                        case string zipcode when zipcode.Contains("zipcode"):
                            form.SetFieldProperty(item.ItemCode.Trim(), "textfont", bf, null);
                            //form.SetFieldProperty(item.ItemCode.Trim(), "textsize", 14.0f, null);
                            if (item.DataType == "number")
                            {
                                if (item.DecimalNo == 1)
                                { //แบบลงช่อง
                                    char[] charArr0 = item.FinalResult.ToString().Trim().ToCharArray();
                                    int maxsize0 = 0;
                                    maxsize0 = int.Parse(item.Maxlength.ToString());
                                    foreach (char adata in charArr0.Reverse())
                                    {
                                        if (adata.ToString() != ".")
                                        {
                                            form.SetFieldProperty(item.ItemCode.Trim() + "_" + maxsize0.ToString(), "textfont", bf, null);
                                            form.SetField(item.ItemCode.Trim() + "_" + maxsize0.ToString(), adata.ToString());
                                        }
                                        maxsize0 = maxsize0 - 1;
                                    }
                                }
                                else if (item.DecimalNo == 0) //แบบค่าเงิน comma
                                {
                                    if (item.FinalResult.Trim().ToString() != "")
                                    {
                                        form.SetField(item.ItemCode.ToString().Trim(), string.Format("{0:#,0.##########}", decimal.Parse(item.FinalResult.Trim().ToString())));

                                    }
                                }                               
                                else //แบบเลขธรรมดา ทั้ง decimal และ int
                                {
                                    if (item.FinalResult.Trim().ToString() != "")
                                    {

                                        form.SetField(item.ItemCode.ToString().Trim(), item.FinalResult.Trim().ToString());
                                    }
                                }
                            }
                            else
                            {
                                form.SetField(item.ItemCode.ToString().Trim(), item.FinalResult.ToString().Trim());
                            }

                            break;
                        case "textsplit":
                            char[] charArr = item.FinalResult.ToString().Trim().ToCharArray();
                            int maxsize = 0;
                            maxsize = int.Parse(item.Maxlength.ToString());
                            foreach (char adata in charArr.Reverse())
                            {
                                if (adata.ToString() != ".")
                                {
                                    form.SetFieldProperty(item.ItemCode.Trim() + "_" + maxsize.ToString(), "textfont", bf, null);
                                    form.SetField(item.ItemCode.Trim() + "_" + maxsize.ToString(), adata.ToString());
                                }
                                maxsize = maxsize - 1;
                            }

                            break;
                        case "fileimage":
                            if (!string.IsNullOrEmpty(item.FinalResult))
                            {
                                string ImagePath = _hostingEnvironment.WebRootPath + "\\Attach\\" + item.DocumentCode.Trim() + "\\" + item.DocumentNo.Trim() + "\\" + item.ItemCode.Trim() + "\\" + item.FinalResult.Trim();
                                PushbuttonField ad = form.GetNewPushbuttonFromField(item.ItemCode.Trim());
                                ad.Layout = PushbuttonField.LAYOUT_ICON_ONLY;
                                //ad.ScaleIcon= PushbuttonField.SCALE_ICON_ALWAYS;
                                //ad.IconFitToBounds = true;                                
                                ad.ProportionalIcon = false;
                                ad.Image = Image.GetInstance(ImagePath);
                                form.ReplacePushbuttonField(item.ItemCode.Trim(), ad.Field);
                            }
                            break;
                        case "table":
                            var alllisitem = tabledetail.Where(i => i.InputItemCode.Trim() == item.ItemCode.Trim() && i.TableCode.Trim() == item.ValueCode.Trim());
                            foreach (var item1 in alllisitem)
                            {
                                int seq = int.Parse(item1.ItemId.ToString());
                                var field_item = item.ItemCode.Trim() + "_" + item.ValueCode.Trim() + "_" + item1.DisplayOrder + "_" + (seq + 1);
                                form.SetFieldProperty(field_item, "textfont", bf, null);
                                //form.SetFieldProperty(item.ItemCode.Trim(), "textsize", 14.0f, null);
                                if (item1.InputType.Contains("textsum") || item1.DataType == "number")
                                {
                                    bool isNumeric = decimal.TryParse(item1.FinalResult.ToString().Trim(), out decimal n);
                                    if (isNumeric == true)
                                    {
                                        form.SetField(field_item, string.Format("{0:#,0.##########}", decimal.Parse(item1.FinalResult.ToString().Trim())));
                                    }
                                    else
                                    {
                                        form.SetField(field_item, item1.FinalResult.ToString().Trim());
                                    }
                                }else if (item1.InputType.Contains("combo") && item1.DataType == "textspe")
                                {
                                    var test = field_item + "_" + item1.FinalResult.ToString().Trim();
                                   form.SetFieldProperty(test, "clrflags", PdfAnnotation.FLAGS_HIDDEN, null);/*Slash stamp*/
                                }
                                else
                                {
                                    form.SetField(field_item, item1.FinalResult.ToString().Trim());
                                }

                            }
                            break;
                        default:
                            break;
                    }


                }

            }


            return form;
        }
        public AcroFields StampApprove(AcroFields form,List<Flow> listapprove, List<ModelItemList_Result> model, List<ApprovalFlow> approvalFlows, PdfStamper pdfStamper,string last)
        {

            if (last != "")
            {
                form.SetFieldProperty("watermark1", "setflags", PdfFormField.FLAGS_INVISIBLE, null);
                form.SetFieldProperty("watermark2", "setflags", PdfFormField.FLAGS_INVISIBLE, null);
                form.SetFieldProperty("watermark3", "setflags", PdfFormField.FLAGS_INVISIBLE, null);
            }
            else
            {
                form.SetFieldProperty("watermark1", "setflags", PdfFormField.FLAGS_HIDDEN, null);
                form.SetFieldProperty("watermark2", "setflags", PdfFormField.FLAGS_HIDDEN, null);
                form.SetFieldProperty("watermark3", "setflags", PdfFormField.FLAGS_HIDDEN, null);
            }



            if (listapprove.Count > 0)
            {

                foreach (var item in listapprove.Where(x => x.AssignFlag == "0").OrderBy(x => x.SeqNo))
                {
                    var countuser = listapprove.Where(x => x.AssignFlag == "0" && x.SeqNo == item.SeqNo).Count() + 1;
                    //for (int i = countuser; i < 30; i++)
                    //{
                    //    form.SetFieldProperty("1-B" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*cycle stamp*/
                    //    form.SetFieldProperty("1-BB" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/
                    //    form.SetFieldProperty("2-B" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*cycle stamp*/
                    //    form.SetFieldProperty("2-BB" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/
                    //    form.SetFieldProperty("3-B" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*cycle stamp*/
                    //    form.SetFieldProperty("3-BB" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/
                    //}

                    //สำหรับเงื่อนไขการ Skip ช่องเปล่า                   
                    var checkskipother = _context.DocumentItem.Where(i => i.DocumentCode == listapprove.First().DocumentCode && i.Remark != null && i.Remark != "").FirstOrDefault();
                    if (checkskipother != null)
                    {
                        if (checkskipother.Remark.Contains("showskipother") == true)
                        {
                            for (int i = countuser; i < 30; i++)
                            {
                                form.SetFieldProperty("1-B" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*cycle stamp*/
                                form.SetFieldProperty("2-B" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*cycle stamp*/
                                form.SetFieldProperty("3-B" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*cycle stamp*/

                            }
                        }
                        else
                        {
                            for (int i = countuser; i < 30; i++)
                            {
                                form.SetFieldProperty("1-B" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*cycle stamp*/
                                form.SetFieldProperty("1-BB" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/
                                form.SetFieldProperty("2-B" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*cycle stamp*/
                                form.SetFieldProperty("2-BB" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/
                                form.SetFieldProperty("3-B" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*cycle stamp*/
                                form.SetFieldProperty("3-BB" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/
                            }
                        }

                    }
                    else
                    {
                        for (int i = countuser; i < 30; i++)
                        {
                            form.SetFieldProperty("1-B" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*cycle stamp*/
                            form.SetFieldProperty("1-BB" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/
                            form.SetFieldProperty("2-B" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*cycle stamp*/
                            form.SetFieldProperty("2-BB" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/
                            form.SetFieldProperty("3-B" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*cycle stamp*/
                            form.SetFieldProperty("3-BB" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/
                        }
                    }
                }

                var nummultiple = 1;
                var colnummultiple = 0;
                var role = "";
                foreach (var item in listapprove.OrderBy(x => x.SeqNo))
                {
                    if (role != item.SeqNo.ToString().Trim())
                    {
                        nummultiple = 1;
                    }

                    if (item.SkipFlag == true || item.checkmin == "2") /*แบบ Skip  ปิด cycle ทั้งหมด  เปิด slash*/
                    {
                        if (item.AssignFlag != "0") /*แบบคนเดียวใน role*/
                        {
                            //default
                            form.SetFieldProperty("B" + item.SeqNo.ToString(), "setflags", PdfFormField.FLAGS_HIDDEN, null);/*Cycle stamp ปิด*/


                            var skipall = listapprove.Where(x => x.SkipFlag == false && x.ApprovalItemCode == item.ApprovalItemCode && item.checkmin != "2").FirstOrDefault();

                            if (skipall == null)
                            {
                                //เช็ค ApprovalItemCode ที่ไม่ใช่ Seq ตัวเอง และ ApprovalItemCode
                                if (item.ApprovalItemCode.Substring(4, 2) != item.SeqNo.ToString() && item.ApprovalItemCode.Contains("-") == false)
                                {
                                    //spacial ที่การ approve seqno ตัวเอง ไปมีความสัมพันธ์กับ seqno อื่น
                                    var seq = Convert.ToInt32(item.ApprovalItemCode.Substring(4, 2));
                                    var checkspacialskip = listapprove.Where(x => x.SeqNo.ToString() == seq.ToString()).FirstOrDefault();

                                    if (seq == item.SeqNo) {
                                        //True all      seq ตัวเอง
                                        //ไม่สัมพันธ์กับ seqno อื่น
                                        form.SetFieldProperty("BB" + item.SeqNo.ToString(), "setflags", PdfFormField.FLAGS_INVISIBLE, null);/*Slash stamp เปิด*/
                                    } 
                                    else if (checkspacialskip != null)
                                    {
                                        if (checkspacialskip.SkipFlag == true && item.SkipFlag == true) //skip ทั้งคู่
                                        {
                                            form.SetFieldProperty("BB" + item.SeqNo.ToString(), "setflags", PdfFormField.FLAGS_INVISIBLE, null);/*Slash stamp เปิด*/
                                        }
                                        else
                                        {
                                            form.SetFieldProperty("BB" + item.SeqNo.ToString(), "setflags", PdfFormField.FLAGS_HIDDEN, null);/*Slash stamp ปิด*/

                                        }
                                    }

                                }
                                else
                                {
                                    //True all      seq ตัวเอง
                                    //ไม่สัมพันธ์กับ seqno อื่น
                                    form.SetFieldProperty("BB" + item.SeqNo.ToString(), "setflags", PdfFormField.FLAGS_INVISIBLE, null);/*Slash stamp เปิด*/
                                }
                            }
                            else
                            {
                                //Group is skip have "false"
                                form.SetFieldProperty("BB" + item.SeqNo.ToString(), "setflags", PdfFormField.FLAGS_HIDDEN, null);/*Slash stamp ปิด*/
                            }



                        }
                        else  /*แบบหลายคนใน role*/
                        {

                            //Spacial stamp check comment
                            if (item.Comment.Trim() != "-" && item.Judge == "OK") //Approve & Revise have comment 
                            {
                                /*2*/
                                form.SetFieldProperty("2-BB" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_INVISIBLE, null);/*Slash stamp*/
                                form.SetFieldProperty("1-BB" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null);/*Slash stamp*/
                                form.SetFieldProperty("3-BB" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null);/*Slash stamp*/
                            }
                            else if (item.Judge == "NG") //Reject
                            {
                                //3
                                form.SetFieldProperty("3-BB" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_INVISIBLE, null);/*Slash stamp*/
                                form.SetFieldProperty("1-BB" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null);/*Slash stamp*/
                                form.SetFieldProperty("2-BB" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null);/*Slash stamp*/
                            }
                            else //Approve no comment
                            {
                                //1
                                form.SetFieldProperty("1-BB" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_INVISIBLE, null);/*Slash stamp*/
                                form.SetFieldProperty("2-BB" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null);/*Slash stamp*/
                                form.SetFieldProperty("3-BB" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null);/*Slash stamp*/
                            }

                            form.SetFieldProperty("1-B" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Cycle stamp*/
                            form.SetFieldProperty("2-B" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Cycle stamp*/
                            form.SetFieldProperty("3-B" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Cycle stamp*/
                        }
                    }
                    else if (item.ApprovalDate != null && item.ApprovalDate != "") //ไม่ Skip
                    {
                        var nation = item.ApproverOperatorID.Trim().Substring(0, 1);

                        string[] multiArray = item.ApproverOperatorName.ToString().Trim().Split(new Char[] { ' ', '.' });
                        var num = 0;
                        var dept2 = item.DEPARTMENT2U;
                        var dept3 = item.DEPARTMENT3U;
                        var position = item.POSITIONU;
                        var name = "";
                        foreach (string author in multiArray)
                        {
                            if (author.Trim() != "" && nation.ToUpper() != "J")
                            {
                                if (num == 1)
                                {
                                    name = author.Trim().ToUpper();
                                }
                                else if (num != 0)
                                {
                                    name = name + ' ' + author.Trim().Substring(0, 1).ToUpper() + ".";
                                }
                                num++;
                            }
                            else if (author.Trim() != "" && nation.ToUpper() == "J")
                            {
                                if (num == 1)
                                {
                                    name = author.Trim().Substring(0, 1).ToUpper();
                                }
                                else if (num != 0)
                                {
                                    name = name + "." + author.Trim().ToUpper();
                                }
                                num++;
                            }

                        }

                        if (item.AssignFlag != "0") /*แบบคนเดียวใน role*/
                        {
                            form.SetField("S" + item.SeqNo.ToString(), name);
                            form.SetField("SS" + item.SeqNo.ToString(), SetDateFormat(Convert.ToDateTime(item.ApprovalDate).ToString("y.M.d")));
                            form.SetField("DEPT2" + item.SeqNo.ToString(), dept2);
                            form.SetField("DEPT3" + item.SeqNo.ToString(), dept3);
                            form.SetField("POS" + item.SeqNo.ToString(), position);                            
                            form.SetField("C" + item.SeqNo.ToString(), item.Comment);

                            if (item.ApproverOperatorSign != null && item.ApproverOperatorSign != "")
                            {
                                string imagepath = item.ApproverOperatorSign;
                                Byte[] bytes = Convert.FromBase64String(Regex.Replace(imagepath, @"^data:image\/[a-zA-Z]+;base64,", string.Empty));
                                var pdfContentByte = pdfStamper.GetOverContent(1); // page number
                                Image image1 = Image.GetInstance(bytes);

                                PushbuttonField ad = form.GetNewPushbuttonFromField("B" + item.SeqNo.ToString());
                                if (ad != null)
                                {
                                    ad.Layout = PushbuttonField.LAYOUT_ICON_ONLY;
                                    ad.ProportionalIcon = true;
                                    ad.Image = Image.GetInstance(image1);
                                    form.ReplacePushbuttonField("B" + item.SeqNo.ToString(), ad.Field);
                                    Item itemField = form.GetFieldItem("SS" + item.SeqNo.ToString());
                                    PdfDictionary widget = itemField.GetWidget(0);
                                    PdfArray rect = widget.GetAsArray(PdfName.Rect);
                                    //rect.se(2, new PdfNumber(rect.GetAsNumber(2).FloatValue - 10f));


                                    //Check both of the Y values
                                    rect[1] = new PdfNumber(rect.GetAsNumber(1).IntValue - 15);
                                    //rect[3] = new PdfNumber(rect.GetAsNumber(3).IntValue - 300);
                                    form.SetFieldProperty("S" + item.SeqNo.ToString(), "setflags", PdfFormField.FLAGS_HIDDEN, null);//ปิด ชื่อ
                                }

                            }

                            form.SetFieldProperty("B" + item.SeqNo.ToString(), "setflags", PdfFormField.FLAGS_INVISIBLE, null); /*cycle stamp*/
                            form.SetFieldProperty("BB" + item.SeqNo.ToString(), "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/





                            if (item.Comment.Trim() != "-" && item.Judge == "OK") //Approve & Revise have comment 
                            {
                                colnummultiple = 2;
                            }
                            else if (item.Judge == "NG") //Reject
                            {
                                colnummultiple = 3;
                            }
                            else //Approve no comment
                            {
                                colnummultiple = 1;
                            }


                            switch (form.GetFieldType(colnummultiple + "-SS" + item.SeqNo.ToString()))
                            {
                                case AcroFields.FIELD_TYPE_CHECKBOX:
                                    //Set Special Approve 
                                    form.SetField(colnummultiple + "-SS" + item.SeqNo.ToString(), "On"); //checkbox on                             
                                                                                                         //form.SetField(colnummultiple + "-SS" + item.SeqNo.ToString(), "Yes");
                                    break;
                                case AcroFields.FIELD_TYPE_PUSHBUTTON:
                                    form.SetFieldProperty(colnummultiple + "-SS" + item.SeqNo.ToString(), "clrflags", PdfAnnotation.FLAGS_HIDDEN, null);
                                    break;
                                default:

                                    break;
                            }







                            //form.SetFieldProperty(colnummultiple + "-SS" + item.SeqNo.ToString(), "setflags", PdfFormField.FLAGS_INVISIBLE, null); 
                        }
                        else /*แบบหลายคนเดียวใน role*/
                        {
                            switch (form.GetFieldType(colnummultiple + "-SS" + item.SeqNo.ToString()))
                            {
                                case AcroFields.FIELD_TYPE_CHECKBOX:
                                    //Set Special Approve 
                                    form.SetField(colnummultiple + "-SS" + item.SeqNo.ToString(), "On"); //checkbox on                             
                                                                                                         //form.SetField(colnummultiple + "-SS" + item.SeqNo.ToString(), "Yes");
                                    break;
                                case AcroFields.FIELD_TYPE_PUSHBUTTON:
                                    form.SetFieldProperty(colnummultiple + "-SS" + item.SeqNo.ToString(), "clrflags", PdfAnnotation.FLAGS_HIDDEN, null);
                                    break;
                                default:

                                    break;
                            }


                            //Spacial stamp check comment
                            if (item.Comment.Trim() != "-" && item.Judge == "OK" && model.FirstOrDefault().DocumentCode.Trim() != "AC018") //Approve & Revise have comment 
                            {
                                /*2*/
                                colnummultiple = 2;

                                form.SetFieldProperty("2-B" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_INVISIBLE, null);/*cycle stamp*/
                                form.SetFieldProperty("1-B" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null);/*cycle stamp*/
                                form.SetFieldProperty("3-B" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null);/*cycle stamp*/

                                if (item.ApproverOperatorSign != null && item.ApproverOperatorSign != "")
                                {
                                    string imagepath = item.ApproverOperatorSign;
                                    Byte[] bytes = Convert.FromBase64String(Regex.Replace(imagepath, @"^data:image\/[a-zA-Z]+;base64,", string.Empty));
                                    var pdfContentByte = pdfStamper.GetOverContent(1); // page number
                                    Image image1 = Image.GetInstance(bytes);

                                    PushbuttonField ad = form.GetNewPushbuttonFromField("2-B" + item.SeqNo.ToString() + "-" + nummultiple);
                                    if (ad != null)
                                    {
                                        ad.Layout = PushbuttonField.LAYOUT_ICON_ONLY;
                                        ad.ProportionalIcon = true;
                                        ad.Image = Image.GetInstance(image1);
                                        form.ReplacePushbuttonField("2-B" + item.SeqNo.ToString() + "-" + nummultiple, ad.Field);
                                        Item itemField = form.GetFieldItem(colnummultiple + "-SS" + item.SeqNo.ToString() + "-" + nummultiple);
                                        PdfDictionary widget = itemField.GetWidget(0);
                                        PdfArray rect = widget.GetAsArray(PdfName.Rect);
                                        //rect.se(2, new PdfNumber(rect.GetAsNumber(2).FloatValue - 10f));


                                        //Check both of the Y values
                                        rect[1] = new PdfNumber(rect.GetAsNumber(1).IntValue - 15);
                                        //rect[3] = new PdfNumber(rect.GetAsNumber(3).IntValue - 300);
                                        form.SetFieldProperty("2-S" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null);//ปิด ชื่อ
                                    }
                                }
                            }
                            else if (item.Judge == "NG") //Reject
                            {
                                //3
                                colnummultiple = 3;
                                form.SetFieldProperty("3-B" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_INVISIBLE, null);/*cycle stamp*/
                                form.SetFieldProperty("1-B" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null);/*cycle stamp*/
                                form.SetFieldProperty("2-B" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null);/*cycle stamp*/

                                if (item.ApproverOperatorSign != null && item.ApproverOperatorSign != "")
                                {
                                    string imagepath = item.ApproverOperatorSign;
                                    Byte[] bytes = Convert.FromBase64String(Regex.Replace(imagepath, @"^data:image\/[a-zA-Z]+;base64,", string.Empty));
                                    var pdfContentByte = pdfStamper.GetOverContent(1); // page number
                                    Image image1 = Image.GetInstance(bytes);

                                    PushbuttonField ad = form.GetNewPushbuttonFromField("3-B" + item.SeqNo.ToString() + "-" + nummultiple);
                                    if (ad != null)
                                    {
                                        ad.Layout = PushbuttonField.LAYOUT_ICON_ONLY;
                                        ad.ProportionalIcon = true;
                                        ad.Image = Image.GetInstance(image1);
                                        form.ReplacePushbuttonField("3-B" + item.SeqNo.ToString() + "-" + nummultiple, ad.Field);

                                        Item itemField = form.GetFieldItem(colnummultiple + "-SS" + item.SeqNo.ToString() + "-" + nummultiple);
                                        PdfDictionary widget = itemField.GetWidget(0);
                                        PdfArray rect = widget.GetAsArray(PdfName.Rect);
                                        //rect.se(2, new PdfNumber(rect.GetAsNumber(2).FloatValue - 10f));


                                        //Check both of the Y values
                                        rect[1] = new PdfNumber(rect.GetAsNumber(1).IntValue - 15);
                                        //rect[3] = new PdfNumber(rect.GetAsNumber(3).IntValue - 300);
                                        form.SetFieldProperty("3-S" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null);//ปิด ชื่อ
                                    }

                                }
                            }
                            else //Approve no comment
                            {
                                //1
                                colnummultiple = 1;
                                form.SetFieldProperty("1-B" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_INVISIBLE, null);/*cycle stamp*/
                                form.SetFieldProperty("2-B" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null);/*cycle stamp*/
                                form.SetFieldProperty("3-B" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null);/*cycle stamp*/

                                if (item.ApproverOperatorSign != null && item.ApproverOperatorSign != "")
                                {
                                    string imagepath = item.ApproverOperatorSign;
                                    Byte[] bytes = Convert.FromBase64String(Regex.Replace(imagepath, @"^data:image\/[a-zA-Z]+;base64,", string.Empty));
                                    var pdfContentByte = pdfStamper.GetOverContent(1); // page number
                                    Image image1 = Image.GetInstance(bytes);

                                    PushbuttonField ad = form.GetNewPushbuttonFromField("1-B" + item.SeqNo.ToString() + "-" + nummultiple);
                                    if (ad != null)
                                    {
                                        ad.Layout = PushbuttonField.LAYOUT_ICON_ONLY;
                                        ad.ProportionalIcon = true;
                                        ad.Image = Image.GetInstance(image1);
                                        form.ReplacePushbuttonField("1-B" + item.SeqNo.ToString() + "-" + nummultiple, ad.Field);
                                        Item itemField = form.GetFieldItem(colnummultiple + "-SS" + item.SeqNo.ToString() + "-" + nummultiple);
                                        PdfDictionary widget = itemField.GetWidget(0);
                                        PdfArray rect = widget.GetAsArray(PdfName.Rect);
                                        //rect.se(2, new PdfNumber(rect.GetAsNumber(2).FloatValue - 10f));


                                        //Check both of the Y values
                                        rect[1] = new PdfNumber(rect.GetAsNumber(1).IntValue - 15);
                                        //rect[3] = new PdfNumber(rect.GetAsNumber(3).IntValue - 300);
                                        form.SetFieldProperty("1-S" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null);//ปิด ชื่อ
                                    }

                                }
                            }

                            form.SetFieldProperty("1-BB" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/
                            form.SetFieldProperty("2-BB" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/
                            form.SetFieldProperty("3-BB" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/

                            //Spacial stamp                              
                            form.SetField("S" + item.SeqNo.ToString() + "-" + nummultiple, name);
                            form.SetField("SS" + item.SeqNo.ToString() + "-" + nummultiple, SetDateFormat(Convert.ToDateTime(item.ApprovalDate).ToString("y.M.d")));
                            form.SetField("DEPT2" + item.SeqNo.ToString() + "-" + nummultiple, dept2);
                            form.SetField("DEPT3" + item.SeqNo.ToString() + "-" + nummultiple, dept3);
                            form.SetField("POS" + item.SeqNo.ToString() + "-" + nummultiple, position);
                            form.SetField("C" + item.SeqNo.ToString() + "-" + nummultiple, item.Comment);

                            form.SetField(colnummultiple + "-S" + item.SeqNo.ToString() + "-" + nummultiple, name); /*ชื่อ*/
                            form.SetField(colnummultiple + "-SS" + item.SeqNo.ToString() + "-" + nummultiple, SetDateFormat(Convert.ToDateTime(item.ApprovalDate).ToString("y.M.d"))); /*วันที่*/
                            form.SetField(colnummultiple + "-DEPT2" + item.SeqNo.ToString() + "-" + nummultiple, dept2);
                            form.SetField(colnummultiple + "-DEPT3" + item.SeqNo.ToString() + "-" + nummultiple, dept3);
                            form.SetField(colnummultiple + "-POS" + item.SeqNo.ToString() + "-" + nummultiple, position);
                            form.SetField(colnummultiple + "-C" + item.SeqNo.ToString() + "-" + nummultiple, item.Comment);

                        }
                    }
                    else //ปิดทั้งหมด
                    {
                        if (item.AssignFlag != "0") /*แบบคนเดียวใน role*/
                        {
                            form.SetFieldProperty("B" + item.SeqNo.ToString(), "setflags", PdfFormField.FLAGS_HIDDEN, null);/*cycle stamp*/
                            form.SetFieldProperty("BB" + item.SeqNo.ToString(), "setflags", PdfFormField.FLAGS_HIDDEN, null);/*Slash stamp*/

                        }
                        else
                        {
                            form.SetFieldProperty("1-BB" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/
                            form.SetFieldProperty("2-BB" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/
                            form.SetFieldProperty("3-BB" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/

                            form.SetFieldProperty("1-B" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null);/*cycle stamp*/
                            form.SetFieldProperty("2-B" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null);/*cycle stamp*/
                            form.SetFieldProperty("3-B" + item.SeqNo.ToString() + "-" + nummultiple, "setflags", PdfFormField.FLAGS_HIDDEN, null);/*cycle stamp*/
                        }
                    }

                    //check status approve
                    if (item.ApprovalDate != "" && item.ApprovalDate != null  && item.Judge == "OK")
                    {
                        form.SetField("STOK" + item.SeqNo.ToString(), "On");
                        form.SetField("STOK" + item.SeqNo.ToString(), "Yes");

                        form.SetField("STOK" + item.SeqNo.ToString() + "-" + nummultiple, "On");
                        form.SetField("STOK" + item.SeqNo.ToString() + "-" + nummultiple, "Yes");
                    }
                    else if (item.Judge == "NG")
                    {
                        form.SetField("STNG" + item.SeqNo.ToString(), "On");
                        form.SetField("STNG" + item.SeqNo.ToString(), "Yes");

                        form.SetField("STNG" + item.SeqNo.ToString() + "-" + nummultiple, "On");
                        form.SetField("STNG" + item.SeqNo.ToString() + "-" + nummultiple, "Yes");
                    }


                    nummultiple++;
                    role = item.SeqNo.ToString().Trim();

                }

                //QR
                var dataqr = _context.DocumentQRCode.Where(i => i.DocumentCode == model.FirstOrDefault().DocumentCode.Trim());
                foreach (var i in dataqr)
                {
                    var dataitem = model.Where(x => x.ItemCode == i.ItemCode).Select(x => x.FinalResult).FirstOrDefault();
                    if (dataitem != null && dataitem != "")
                    {
                        var qrGenerator = new QRCodeGenerator();
                        var qrCodeData = qrGenerator.CreateQrCode(dataitem, QRCodeGenerator.ECCLevel.Q);
                        var qrCode = new Base64QRCode(qrCodeData);
                        var qrCodeImageAsBase64 = qrCode.GetGraphic(i.Graphic);
                        var imageBytes = Convert.FromBase64String(qrCodeImageAsBase64);
                        var img = Image.GetInstance(imageBytes);
                        img.ScaleAbsoluteWidth(i.Width);
                        img.ScaleAbsoluteHeight(i.Height);
                        img.SetAbsolutePosition(i.Position1, i.Position2);
                        pdfStamper.GetOverContent(i.Page).AddImage(img);
                    }
                }
            }
            else //ปิดทั้งหมด
            {

                foreach (var item in approvalFlows)
                {

                    if (item.AssignFlag != "0") /*แบบคนเดียวใน role*/
                    {
                        form.SetFieldProperty("B" + item.SeqNo.ToString(), "setflags", PdfFormField.FLAGS_HIDDEN, null);
                        form.SetFieldProperty("BB" + item.SeqNo.ToString(), "setflags", PdfFormField.FLAGS_HIDDEN, null);
                    }
                    else
                    {
                        for (int i = 0; i < 30; i++)
                        {
                            form.SetFieldProperty("1-B" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*cycle stamp*/
                            form.SetFieldProperty("1-BB" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/
                            form.SetFieldProperty("2-B" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*cycle stamp*/
                            form.SetFieldProperty("2-BB" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/
                            form.SetFieldProperty("3-B" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*cycle stamp*/
                            form.SetFieldProperty("3-BB" + item.SeqNo.ToString() + "-" + i, "setflags", PdfFormField.FLAGS_HIDDEN, null); /*Slash stamp*/
                        }
                    }
                }
            }

           
            return form;
        }
        public Stream FillFormXFA(Stream inputStream, List<ModelItemList_Result> formModel, List<Flow> listapprove, List<ApprovalFlow> approvalFlows)
        {
            throw new NotImplementedException();
        }
        public ICollection GetFormFields(Stream pdfStream)
        {
            PdfReader reader = null;
            try
            {
                PdfReader pdfReader = new PdfReader(pdfStream);
                AcroFields acroFields = pdfReader.AcroFields;
                return acroFields.Fields.Keys;
            }
            finally
            {
                reader?.Close();
            }
        }
       private string SetDateFormat(string datestr)
        {
            var datedata = datestr.Split(".");
            if (datedata[2].Length == 1)
            {
                datedata[2] = "-" + datedata[2];
            }
            string datereturn = datedata[0] + "." + datedata[1] + "." + datedata[2];
            return datereturn;
        }
    }
}