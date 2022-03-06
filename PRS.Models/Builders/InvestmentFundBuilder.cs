using PRS.Models.Interfaces;
using PRS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRS.Models.Builders
{
    public class InvestmentFundBuilder : IBuilder<Models.InvestmentFund>
    {
        private readonly InvestmentFund investmentFund;

        public InvestmentFundBuilder()
        {
            investmentFund = new InvestmentFund();
        }

        public InvestmentFundBuilder Name(string name)
        {
            investmentFund.name = name;
            return this;
        }

        public InvestmentFundBuilder Day(double day)
        {
            investmentFund.day = day;
            return this;
        }

        public InvestmentFundBuilder AccumulatedMonth(double accumulatedMonth)
        {
            investmentFund.accumulatedMonth = accumulatedMonth;
            return this;
        }


        public InvestmentFund Build()
        {
            return investmentFund;
        }
    }
}
