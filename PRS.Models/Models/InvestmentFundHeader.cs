using System;
using System.Collections.Generic;
using System.Text;

namespace PRS.Models.Models
{
    public class InvestmentFundHeader
    {
        public string name { get; set; }
        public string day { get; set; }
        public string accumulatedMonth { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string month12 { get; set; }
        public string month24 { get; set; }
        public string month36 { get; set; }
        public string month12_PL_Avarage { get; set; }
        public string annualAdministrationFee { get; set; }
        public string quotaDate { get; set; }
        public string quotaValue { get; set; }
        public string startDate { get; set; }    
    }
}
