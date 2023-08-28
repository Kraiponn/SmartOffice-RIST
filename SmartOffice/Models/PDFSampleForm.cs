using System;
namespace SmartOffice.Models
{
    public class PDFSampleForm
    {
        private int _evaluate_score;
        public string request_date { get; set; }
        public string dept { get; set; }
        public string sect { get; set; }
        public string sup_evaluate { get; set; }
        public string name { get; set; }
        public string emp_no { get; set; }
        public string shift { get; set; }
        public string quest1 { get; set; }
        public int quest1_score { get; set; }
        public string quest2 { get; set; }
        public int quest2_score { get; set; }
        public string quest3 { get; set; }
        public int quest3_score { get; set; }
        public string quest4 { get; set; }
        public int quest4_score { get; set; }
        public string quest5 { get; set; }
        public int quest5_score { get; set; }
        public string evaluate { get; set; }  
        public int evaluate_score
        {
            get
            {
                _evaluate_score = quest1_score + quest2_score + quest3_score + quest4_score + quest5_score;
                return _evaluate_score;
            }
            set
            {
            }
        }
        public string evaluate_desc { get; set; }
        //public bool AwesomeCheck { get; set; }
    }
}
