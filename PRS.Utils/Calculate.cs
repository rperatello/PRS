using PRS.Models.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRS.Utils
{
    public class Calculate
    {

        public Calculate() { }

        public double ConvertToDecimalDailyInterestRate(double interestRate, Periods sourcePeriod)
        {

            double newInterestRate;

            if (sourcePeriod == Periods.AO_ANO)
            {
                newInterestRate = Math.Pow(1 + (interestRate / 100), 1d/360) - 1;
            }
            else if (sourcePeriod == Periods.AO_MÊS)
            {
                newInterestRate = Math.Pow(1 + (interestRate / 100), 1d/30) - 1;
            }
            else
            {
                newInterestRate = interestRate / 100;
            }

            return newInterestRate;

        }

        //prof -> pré ou pós  || invest -> cdb, lci, ipca
        public double GetProfitability(double inputYield, string investimentType, string profitability, double cdi, double ipca)
        {
            if(profitability == "prefixada")
            {
                return ConvertToDecimalDailyInterestRate(inputYield, Periods.AO_ANO);
            }

            else if (profitability == "posfixada" && (investimentType == "cdb" || investimentType == "lci"))
            {                
                return (inputYield/100) * ConvertToDecimalDailyInterestRate(cdi, Periods.AO_ANO);
            }

            else if (profitability == "posfixada" && investimentType == "ipca")
            {
                return ConvertToDecimalDailyInterestRate(inputYield, Periods.AO_ANO) + ConvertToDecimalDailyInterestRate(ipca, Periods.AO_ANO);
            }

            else
            {
                return -999999999;
            }

        }

        public int investimentDays (string finalDate)
        {
            DateTime newFinalDate = DateTime.Parse(finalDate);
            return (int)newFinalDate.Subtract(DateTime.Today).TotalDays;
        }

        public double calculateFinalAmount(int days, double amount, double yieldDay, Boolean hasIrTax)
        {
            int i = 0;
            double finalAmount = amount;
            while (i < days)
                {
                    finalAmount = finalAmount +(finalAmount * yieldDay);
                    i++;
                }

            if (hasIrTax)
                finalAmount = finalAmount - ((finalAmount - amount ) * GetTax(days));
            
            finalAmount = Math.Round(finalAmount, 2);
            return finalAmount;
        }

        public double GetTax(int days)
        {
            if(days >720)
                return 0.15;

            if (days > 360)
                return 0.175;

            if (days > 180)
                return 0.2;

           
            return 0.225;
 
        }

    }
}
