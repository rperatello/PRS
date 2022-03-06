using System;
using Xunit;
using PRS.Utils;
using PRS.Models.Enumerators;

namespace PRS.Test
{
    public class CalculateTest : IDisposable
    {

        Calculate _calcTest;

        public CalculateTest()
        {
            _calcTest = new Calculate();
        }

        [Theory]
        [InlineData(0.000315, 12, Periods.AO_ANO)]
        [InlineData(0.000398, 1.2, Periods.AO_MÊS)]
        [InlineData(0.000392, 0.0392, Periods.DIÁRIA)]
        [InlineData(0.012000, 1.2, Periods.DIÁRIA)]
        [InlineData(0.000311, 0.0311, Periods.DIÁRIA)]
        [InlineData(0.000315, 0.9489, Periods.AO_MÊS)]
        public void ConvertToDecimalDailyInterestRate_Test(double result, double interestRate, Periods sourcePeriod)
        {
            Assert.Equal(result, Math.Round(_calcTest.ConvertToDecimalDailyInterestRate(interestRate, sourcePeriod), 6));            
        }

        [Theory]
        [InlineData(0, 0.225)]
        [InlineData(180, 0.225)]
        [InlineData(181, 0.2)]
        [InlineData(360, 0.2)]
        [InlineData(361, 0.175)]
        [InlineData(720, 0.175)]
        [InlineData(721, 0.15)]
        public void FindIRTax_Test(int days, double tax)
        {
            Assert.Equal(tax, _calcTest.GetTax(days));
        }

        public void Dispose()
        {
            _calcTest = null;
        }
    }
}
