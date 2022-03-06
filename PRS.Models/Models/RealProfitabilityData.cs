using System;
using System.Collections.Generic;
using System.Text;

namespace PRS.Models.Models
{
    public class RealProfitabilityData
    {
        public string name { get; set; }
        public double realDayProfitability { get; set; }
        public double nominalDayProfitability { get; set; }
        public double ipcaDay { get; set; }
        public double realAccumulatedMonthProfitability { get; set; }
        public double nominalAccumulatedMonthProfitability { get; set; }
        public double ipcaAccumulatedMonth { get; set; }
        public double realLastMonthProfitability { get; set; }
        public double nominalLastMonthProfitability { get; set; }
        public double ipcaLastMonth { get; set; }
        public double realYearProfitability { get; set; }
        public double nominalYearProfitability { get; set; }
        public double ipcaYear { get; set; }
        public double realMonth12Profitability { get; set; }
        public double nominalMonth12Profitability { get; set; }
        public double ipcaMonth12 { get; set; }
        public double realMonth24Profitability { get; set; }
        public double nominalMonth24Profitability { get; set; }
        public double ipcaMonth24 { get; set; }
        public double realMonth36Profitability { get; set; }
        public double nominalMonth36Profitability { get; set; }
        public double ipcaMonth36 { get; set; }
        public string annualAdministrationFee { get; set; }
        public string type { get; set; }        
    }
}
