using SmartOffice.ModelsDocControl;
using System;
using System.Linq;
using Xunit;

namespace SmartOffice.Test
{
    public class UnitTest1
    {
        //[Fact]
        //public void Test1()
        //{
        //    Document document = new Document();
        //    PdfCopy copy = new PdfSmartCopy(document, new FileOutputStream(dest));
        //    document.open();
        //    PdfReader reader;
        //    String line = br.readLine();
        //    // loop over readers
        //    // add the PDF to PdfCopy
        //    reader = new PdfReader(baos.toByteArray());
        //    copy.addDocument(reader);
        //    reader.close();
        //    // end loop
        //    document.close();
        //}
      
        public void Test2()
        {
            DocumentControlContext _dbContext = new DocumentControlContext();
            var res = _dbContext.DocumentConditionHist.Where(i => i.Template == "approve");
           
        }
    }
}
