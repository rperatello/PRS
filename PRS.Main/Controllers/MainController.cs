using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PRS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using PRS.Models.Builders;
using System.Diagnostics;
using System.Text;

namespace PRS.Main.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : Controller
    {

        private CommunicationSetting communicationSettings = null;
        private int systemAPIPort;
        private int userAPIPort;
        private int collectorAPIPort;
        private int calculateAPIPort;

        public MainController()
        {
            this.communicationSettings = new CommunicationSetting();
            this.systemAPIPort = communicationSettings.communicationPort.systemAPIPort;
            this.userAPIPort = communicationSettings.communicationPort.userAPIPort;
            this.collectorAPIPort = communicationSettings.communicationPort.collectorAPIPort;
            this.calculateAPIPort = communicationSettings.communicationPort.calculateAPIPort;
            Debug.WriteLine($"systemAPIPort: {systemAPIPort} | userAPIPort: {userAPIPort} | collectorAPIPort: {collectorAPIPort} | calculateAPIPort: {calculateAPIPort}");
        }

        private ActionResult<dynamic> SendResponse(HttpResponseMessage response)
        {
            try
            {
                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.Created)
                    return Created(response.Headers.Location.AbsoluteUri, JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result));

                if (statusCode == HttpStatusCode.NotFound)
                    return NotFound(JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result));

                if (statusCode == HttpStatusCode.MethodNotAllowed)
                    return StatusCode(StatusCodes.Status405MethodNotAllowed, JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result));

                if (statusCode == HttpStatusCode.Unauthorized)
                    return Unauthorized(JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result));

                if (statusCode == HttpStatusCode.BadRequest)
                    return BadRequest(JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result));

                if (statusCode == HttpStatusCode.Conflict)
                    return Conflict(JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result));

                if (statusCode == HttpStatusCode.InternalServerError)
                    return StatusCode(StatusCodes.Status500InternalServerError, JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result));

                return Ok(JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("selic")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> GetSelic()
        {
            try
            {
                string host = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                HttpClient request = new HttpClientBuilder().Port(collectorAPIPort).ClientIP(host).Build();
                var response = await request.GetAsync("collector/selic");
                return this.SendResponse(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("ipca")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> GetIPCA()
        {
            try
            {
                string host = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                HttpClient request = new HttpClientBuilder().Port(collectorAPIPort).ClientIP(host).Build();
                var response = await request.GetAsync("collector/ipca");
                return this.SendResponse(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("comparescenarios")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> CompareScenarios([FromBody] InvestimentsData scenarios)
        {
            try
            {
                string host = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                string httpContent = JsonConvert.SerializeObject(scenarios);

                HttpClient request = new HttpClientBuilder().Port(calculateAPIPort).ClientIP(host).Build();
                var response = await request.PostAsync("calculate/comparescenarios", new StringContent(httpContent, Encoding.UTF8, "application/json"));
                return this.SendResponse(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("bbinvestiments")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> BBInvestiments()
        {
            try
            {
                string host = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                HttpClient request = new HttpClientBuilder().Port(collectorAPIPort).ClientIP(host).Build();
                var response = await request.GetAsync("collector/bbinvestiments");
                return this.SendResponse(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("getusers")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> GetUsers()
        {
            try
            {
                string host = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                HttpClient request = new HttpClientBuilder().Port(userAPIPort).ClientIP(host).Build();
                var response = await request.GetAsync("user/getusers");
                return this.SendResponse(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("getcontacts")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> GetContacts()
        {
            try
            {
                string host = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                HttpClient request = new HttpClientBuilder().Port(userAPIPort).ClientIP(host).Build();
                var response = await request.GetAsync("user/getcontacts");
                return this.SendResponse(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("getcontact/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> GetContactById(int id)
        {
            try
            {
                string host = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                HttpClient request = new HttpClientBuilder().Port(userAPIPort).ClientIP(host).Build();
                var response = await request.GetAsync($"user/getcontact/{id}");
                return this.SendResponse(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("savecontact")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> SaveContact([FromBody] Contact contact)
        {
            try
            {
                string host = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                string httpContent = JsonConvert.SerializeObject(contact);

                HttpClient request = new HttpClientBuilder().Port(userAPIPort).ClientIP(host).Build();
                var response = await request.PostAsync("user/savecontact", new StringContent(httpContent, Encoding.UTF8, "application/json"));
                return this.SendResponse(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("deletecontact/{email}")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> DeleteContact(string email)
        {
            try
            {
                string host = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                HttpClient request = new HttpClientBuilder().Port(userAPIPort).ClientIP(host).Build();
                var response = await request.DeleteAsync($"user/deletecontact/{email}");
                return this.SendResponse(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
