using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models
{
    public class PRApporveModel
    {
        public List<G1PR> G1PR { get; set; }
        public List<G2PR> G2PR { get; set; }
        public List<G3PR> G3PR { get; set; }
        public List<G4PR> G4PR { get; set; }
        public List<G5PR> G5PR { get; set; }

        public List<Gresult01> Gresult01 { get; set; }
        public List<Gresult02> Gresult02 { get; set; }
        public List<Gresult03> Gresult03 { get; set; }
        public List<Gresult04> Gresult04 { get; set; }
        public List<Gresult05> Gresult05 { get; set; }

        public List<result01> result01 { get; set; }
        public List<result02> result02 { get; set; }
        public List<result03> result03 { get; set; }
        public List<result04> result04 { get; set; }
        public List<result05> result05 { get; set; }
    }

    /// /////////////////////////////////////start G1////////////////////////////////////////////////////////
    public class G1PR
    {
        public Gresult01 data { get; set; }
        public List<Kid01> Kids { get; set; }
    }

    public class Gresult01
    {
        public string Item { get; set; }
        public int SumCountOfPR { get; set; }
        public int CountOfPRFinish { get; set; }
        public int CountOfPRRemain { get; set; }

    }
    public class Kid01
    {
        public result01 data { get; set; }
        public List<Kid011> Kids { get; set; }
    }
    public class result01
    {
        public string Item { get; set; }
        public string OpeDate { get; set; }
        public int SumCountOfPR { get; set; }
        public int CountOfPRFinish { get; set; }
        public int CountOfPRRemain { get; set; }
    }

    public class Kid011
    {
        public result02 data { get; set; }
        public List<Kid0111> Kids { get; set; } 
    }
    public class result02
    {
        public string item { get; set; }
        public string OpeDate { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public int SumCountOfPR { get; set; }
    }
    public class Kid0111 //ไม่มี
    {
        
    }

    /// /////////////////////////////////////end G1////////////////////////////////////////////////////////


    /// /////////////////////////////////////end G2////////////////////////////////////////////////////////

    public class G2PR
    {
        public Gresult02 data { get; set; }
        public List<Kid02> Kids { get; set; }
    }

    public class Gresult02
    {
        public string Item { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string FinFlag { get; set; }
        public int CountOfPR { get; set; }
    }
    public class Kid02
    {
        public result02 data { get; set; }
        public List<Kid022> Kids { get; set; }
    }
    public class Kid022 //ไม่มี
    {

    }
    /// /////////////////////////////////////end G2////////////////////////////////////////////////////////

    /// /////////////////////////////////////end G3////////////////////////////////////////////////////////

    public class G3PR
    {
        public Gresult03 data { get; set; }
        public List<Kid03> Kids { get; set; }
    }

    public class Gresult03
    {
        public string Item { get; set; }
        public int CntOfPRNo { get; set; }
    }

    public class result03
    {
        public string item { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public int CntOfPRNo { get; set; }
    }
    public class Kid03
    {
        public result03 data { get; set; }
        public List<Kid033> Kids { get; set; }
    }
    public class Kid033 //ไม่มี
    {

    }
    /// /////////////////////////////////////end G3////////////////////////////////////////////////////////

    /// /////////////////////////////////////end G4////////////////////////////////////////////////////////

    public class G4PR
    {
        public Gresult04 data { get; set; }
        public List<Kid04> Kids { get; set; }
    }

    public class Gresult04
    {
        public string Item { get; set; }
        public int CntOfPRNo { get; set; }
    }

    public class result04
    {
        public string item { get; set; }
        public string Create_Date { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public int CntOfPRNo { get; set; }
    }
    public class Kid04
    {
        public result04 data { get; set; }
        public List<Kid044> Kids { get; set; }
    }
    public class Kid044 //ไม่มี
    {

    }
    /// /////////////////////////////////////end G4////////////////////////////////////////////////////////

    /// /////////////////////////////////////end G5////////////////////////////////////////////////////////

    public class G5PR
    {
        public Gresult05 data { get; set; }
        public List<Kid05> Kids { get; set; }
    }

    public class Gresult05
    {
        public string Item { get; set; }
        public int Purchase { get; set; }
        public int Section_Mrg { get; set; }
        public int Division_Mgr { get; set; }
        public int Group_Division_Mgr { get; set; }
        public int Factory_Mgr { get; set; }
        public int President { get; set; }
    }

    public class result05
    {
        public string item { get; set; }
        public string Approve_Date { get; set; }
        public string Operator { get; set; }
        public string OpeName { get; set; }
        public string Level_App_Code { get; set; }
        public string Level_App_Name { get; set; }
        public int CntPRNo { get; set; }
    }
    public class Kid05
    {
        public result05 data { get; set; }
        public List<Kid055> Kids { get; set; }
    }
    public class Kid055 //ไม่มี
    {

    }
    /// /////////////////////////////////////end G5////////////////////////////////////////////////////////

    

    

    

   

    
}
