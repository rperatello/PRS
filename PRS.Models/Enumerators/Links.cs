using System;
using System.Collections.Generic;
using System.Text;

namespace PRS.Models.Enumerators
{
    public enum Link
    {
        BB_PAGE,
        IPCA_ALL_VARIABLES_AND_PERIODS,
        IPCA_ANNUAL_AVERAGE_LAST_12_PERIODS,
        IPCA_ANNUAL_AVERAGE_LAST_PERIOD,
        IPCA_MONTHLY_VARIATION_LAST_12_PERIODS,
        IPCA_MONTHLY_VARIATION_IN_PERIODS,
        IPCA_INDEX_IN_PERIODS,
        SELIC_LAST_12_PERIODS,
        SELIC_ANUALIZADA_BASE_252_LAST_VALUE
    }

    public static class Links
    {
        public static string GetLink(this Link idx)
        {
            switch (idx)
            {
                case Link.BB_PAGE:
                    return "https://www37.bb.com.br/portalbb/tabelaRentabilidade/rentabilidade/gfi7,802,9085,9089,1.bbx";
                case Link.SELIC_LAST_12_PERIODS:
                    return "http://api.bcb.gov.br/dados/serie/bcdata.sgs.4390/dados/ultimos/12?formato=json";
                case Link.SELIC_ANUALIZADA_BASE_252_LAST_VALUE:
                    return "http://api.bcb.gov.br/dados/serie/bcdata.sgs.1178/dados/ultimos/1?formato=json";
                case Link.IPCA_ALL_VARIABLES_AND_PERIODS:
                    return "https://apisidra.ibge.gov.br/values/t/1737/n1/all/v/all/p/all/d/v63%202,v69%202,v2266%2013,v2263%202,v2264%202,v2265%202?formato=json";
                case Link.IPCA_ANNUAL_AVERAGE_LAST_PERIOD:
                    return "https://apisidra.ibge.gov.br/values/t/1737/n1/all/p/last%201/v/2265?formato=json";
                case Link.IPCA_ANNUAL_AVERAGE_LAST_12_PERIODS:
                    return "https://apisidra.ibge.gov.br/values/t/1737/n1/all/p/last%2012/v/2265?formato=json";
                case Link.IPCA_MONTHLY_VARIATION_LAST_12_PERIODS:
                    return "https://apisidra.ibge.gov.br/values/t/1737/n1/all/p/last%2012/v/63?formato=json";
                case Link.IPCA_MONTHLY_VARIATION_IN_PERIODS:
                    return "https://apisidra.ibge.gov.br/values/t/1737/n1/all/p/{0}-{1}/v/63?formato=json";
                case Link.IPCA_INDEX_IN_PERIODS:
                    return "https://apisidra.ibge.gov.br/values/t/1737/n1/all/p/{0}-{1}/v/2266?formato=json";
                default:
                    return "";
            }
        }
    }
}
