
namespace SmartOffice.SurveyApp.ViewModel
{
    public class FormModel
    {
        public string OperationCode { get; set; }
        public string OperationName { get; set; }       
        public string ItemCateg { get; set; }
        public string OperatorType { get; set; }
        public string KeyTime { get; set; }
        public virtual ViewItemCategory ItemCategory { get; set; }
    }
}
