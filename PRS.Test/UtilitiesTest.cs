using System;
using Xunit;
using PRS.Utils;

namespace PRS.Test
{
    public class UtilitiesTest : IDisposable
    {

        Utilities _utilsTest;

        public UtilitiesTest()
        {
            _utilsTest = new Utilities();
        }

        [Theory]
        [InlineData(true, "teste@teste.com.br")]
        [InlineData(true, "teste@teste.com")]
        [InlineData(false, "teste@teste.")]
        [InlineData(false, "teste@teste")]
        [InlineData(false, "teste@")]
        [InlineData(false, "teste")]
        public void CheckEmail_Test(bool result, string email)
        {
            Assert.Equal(result, _utilsTest.CheckEmail(email));            
        }

        [Theory]
        [InlineData(2, "24/04/2021", "26/04/2021")]
        [InlineData(7, "24/04/2021", "01/05/2021")]
        [InlineData(0, "24/04/2021", "24/04/2021")]
        [InlineData(-1, "24/04/2021", "23/04/2021")]
        [InlineData(-7, "24/04/2021", "17/04/2021")]
        public void ConvertDeadLineInDays_Test(int days, string start, string deadline)
        {
            Assert.Equal(days, _utilsTest.ConvertDeadlineInDays(start, deadline));
        }
                
        public void Dispose()
        {
            _utilsTest = null;
        }
    }
}
