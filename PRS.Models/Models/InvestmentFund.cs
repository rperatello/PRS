using System;
using System.Collections.Generic;
using System.Text;

namespace PRS.Models.Models
{
    public class InvestmentFund
    {
        public string name { get; set; }
        public double day { get; set; }
        public double accumulatedMonth { get; set; }
        public double month { get; set; }
        public double year { get; set; }
        public double month12 { get; set; }
        public double month24 { get; set; }
        public double month36 { get; set; }
        public int month12_PL_Avarage { get; set; }
        public string annualAdministrationFee { get; set; }
        public string quotaDate { get; set; }
        public double quotaValue { get; set; }
        public string startDate { get; set; }
        public string type { get; set; }        
    }
}
