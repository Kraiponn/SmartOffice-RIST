using SmartOffice.ModelsDocControl;
using SmartOffice.ModelsForm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SmartOffice.IResponsitory
{
    public interface IPDFFormService
    {
        Stream FillForm(Stream inputStream, List<ModelItemList_Result> formModel, List<DocumentItemValueTableDetail> tabledetail, List<Flow> listapprove,List<ApprovalFlow> approvalFlows,string last);
        Stream FillFormXFA(Stream inputStream, List<ModelItemList_Result> formModel, List<Flow> listapprove, List<ApprovalFlow> approvalFlows);
        ICollection GetFormFields(Stream pdfStream);
   
     
    }
}
