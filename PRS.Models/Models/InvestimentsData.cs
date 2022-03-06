using System;
using System.Collections.Generic;
using System.Text;

namespace PRS.Models.Models
{
    public class InvestimentsData
    {
        public double amount { get; set; }
        public string deadline { get; set; }
        public Investiment stage1 { get; set; }
        public Investiment stage2 { get; set; }
        public Investiment stage3 { get; set; }

    }
}
