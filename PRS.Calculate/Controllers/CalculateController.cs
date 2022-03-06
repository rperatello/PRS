using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PRS.Models.Builders;
using PRS.Models.Enumerators;
using PRS.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PRS.Utils;

namespace PRS.Calculate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculateController : Controller
    {
        private CommunicationSetting communicationSettings = null;
        private int systemAPIPort;
        private int userAPIPort;
        private int collectorAPIPort;
        private int calculateAPIPort;

        public CalculateController()
        {
            this.communicationSettings = new CommunicationSetting();
            this.systemAPIPort = communicationSettings.communicationPort.systemAPIPort;
            this.userAPIPort = communicationSettings.communicationPort.userAPIPort;
            this.collectorAPIPort = communicationSettings.communicationPort.collectorAPIPort;
            this.calculateAPIPort = communicationSettings.communicationPort.calculateAPIPort;
        }

        [HttpPost("comparescenarios")]
        [AllowAnonymous]
        public async Task<ActionResult> CompareScenario([FromBody] InvestimentsData scenarios)
        {
            try
            {
                Debug.WriteLine($"{JsonConvert.SerializeObject(scenarios)}");

                HttpClient request = new HttpClientBuilder().Port(collectorAPIPort).Build();
                var responseBCB = await request.GetAsync("collector/lastannualizedselic252");

                var statusCode = responseBCB.StatusCode;
                if (statusCode != System.Net.HttpStatusCode.OK)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, TxTResponses.GetTxTResponse(TxTResponse.Failure_GetIndicator));
                }

                Selic selic = JsonConvert.DeserializeObject<Selic>(JToken.Parse(responseBCB.Content.ReadAsStringAsync().Result).ToString());

                var responseIBGE = await request.GetAsync("collector/lastannualavarageipca");
                statusCode = responseIBGE.StatusCode;

                if (statusCode != System.Net.HttpStatusCode.OK)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, TxTResponses.GetTxTResponse(TxTResponse.Failure_GetIndicator));
                }

                Ipca ipcaData = JsonConvert.DeserializeObject<Ipca>(JToken.Parse(responseIBGE.Content.ReadAsStringAsync().Result).ToString());

                double cdi = double.Parse(selic.valor, CultureInfo.InvariantCulture);
                double ipca = double.Parse(ipcaData.V, CultureInfo.InvariantCulture);
                double calc1, calc2, calc3;
                double finalAmount1 = 0.0, finalAmount2 = 0.0, finalAmount3 = 0.0;

                Utils.Calculate calc = new Utils.Calculate();

                int days = calc.investimentDays(scenarios.deadline);

                if (scenarios.stage1 != null)
                {
                    calc1 = calc.GetProfitability(scenarios.stage1.inputYield, scenarios.stage1.investment, scenarios.stage1.profitability, cdi, ipca);
                    finalAmount1 =
                        scenarios.stage1.investment == "lci" ?
                        calc.calculateFinalAmount(days, scenarios.amount, calc1, false) :
                        calc.calculateFinalAmount(days, scenarios.amount, calc1, true);
                }

                if (scenarios.stage2 != null)
                {
                    calc2 = calc.GetProfitability(scenarios.stage2.inputYield, scenarios.stage2.investment, scenarios.stage2.profitability, cdi, ipca);
                    finalAmount2 =
                        scenarios.stage2.investment == "lci" ?
                        calc.calculateFinalAmount(days, scenarios.amount, calc2, false) :
                        calc.calculateFinalAmount(days, scenarios.amount, calc2, true);
                }

                if (scenarios.stage3 != null)
                {
                    calc3 = calc.GetProfitability(scenarios.stage3.inputYield, scenarios.stage3.investment, scenarios.stage3.profitability, cdi, ipca);
                    finalAmount3 =
                        scenarios.stage3.investment == "lci" ?
                        calc.calculateFinalAmount(days, scenarios.amount, calc3, false) :
                        calc.calculateFinalAmount(days, scenarios.amount, calc3, true);
                }

                JArray dataList = new JArray();
                JArray calc1List = new JArray();
                JArray calc2List = new JArray();
                JArray calc3List = new JArray();

                if (finalAmount1 > 0)
                {
                    calc1List.Add(finalAmount1);
                    JObject scenario1 = new JObject() {
                        new JProperty("name", "Cenário 1"), new JProperty("data", calc1List)
                    };
                    dataList.Add(scenario1);
                }

                if (finalAmount2 > 0)
                {
                    calc2List.Add(finalAmount2);
                    JObject scenario2 = new JObject() {
                        new JProperty("name", "Cenário 2"), new JProperty("data", calc2List)
                    };
                    dataList.Add(scenario2);
                }

                if (finalAmount3 > 0)
                {
                    calc3List.Add(finalAmount3);
                    JObject scenario3 = new JObject() {
                        new JProperty("name", "Cenário 3"), new JProperty("data", calc3List)
                    };
                    dataList.Add(scenario3);
                }

                JObject series = new JObject() { new JProperty("series", dataList) };

                return Ok(JsonConvert.SerializeObject(series));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("realprofitability")]
        [AllowAnonymous]
        public async Task<ActionResult> GetRealProfitability()
        {
            try
            {
                HttpClient request = new HttpClientBuilder().Port(collectorAPIPort).Build();
                var responseCollector = await request.GetAsync("collector/ipcatotal");

                var statusCode = responseCollector.StatusCode;
                if (statusCode != System.Net.HttpStatusCode.OK)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, TxTResponses.GetTxTResponse(TxTResponse.Failure_GetIndicator));
                }

                IpcaCalculated ipcaCalculated = JsonConvert.DeserializeObject<IpcaCalculated>(JToken.Parse(responseCollector.Content.ReadAsStringAsync().Result).ToString());

                responseCollector = await request.GetAsync("collector/bbinvestiments");
                statusCode = responseCollector.StatusCode;

                if (statusCode != System.Net.HttpStatusCode.OK)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, TxTResponses.GetTxTResponse(TxTResponse.Failure_GetIndicator));
                }

                Object bbInvestiments = JsonConvert.DeserializeObject<Object>(JToken.Parse(responseCollector.Content.ReadAsStringAsync().Result).ToString());

                
                Utils.Calculate calc = new Utils.Calculate();

                
                return Ok(JsonConvert.SerializeObject(ipcaCalculated));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


    }
}
