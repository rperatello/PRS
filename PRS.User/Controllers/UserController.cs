using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PRS.Models.Models;
using PRS.Models.Enumerators;
using PRS.Repository;
using PRS.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PRS.User.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private CommunicationSetting communicationSetting = null;
        private int mainApiPort;
        private PRSContext context = null;
        private IRepository<Models.Models.User> userRepository = null;
        private IRepository<Contact> contactRepository = null;
        private Utilities utilities = null;

        public UserController(PRSContext context)
        {
            this.context = context;
            this.userRepository = new Repository<Models.Models.User>(this.context);
            this.contactRepository = new Repository<Contact>(this.context);
            this.communicationSetting = new CommunicationSetting();
            this.mainApiPort = communicationSetting.communicationPort.mainAPIPort;
            this.utilities = new Utilities();
        }

        [HttpGet("getusers")]
        [AllowAnonymous]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                Debug.WriteLine($"Consultando todos os usuários cadastrados");

                List<Models.Models.User> usersList = (from users in userRepository.GetByQuery()
                                                      select users).ToList();

                return Ok(JsonConvert.SerializeObject(usersList));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("getcontacts")]
        [AllowAnonymous]
        public async Task<ActionResult> GetContacts()
        {
            try
            {
                Debug.WriteLine($"Consultando todos os contatos cadastrados");

                List<Contact> contactsList = (from contacts in contactRepository.GetByQuery()
                                                select contacts).ToList();

                return Ok(JsonConvert.SerializeObject(contactsList));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("getcontact/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetContactById(int id)
        {
            try
            {
                if (id <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, TxTResponses.GetTxTResponse(TxTResponse.validId));

                Contact contact = contactRepository.GetById(id);

                if (contact == null)
                    return StatusCode(StatusCodes.Status400BadRequest, TxTResponses.GetTxTResponse(TxTResponse.UnregisteredContact));

                return Ok(JsonConvert.SerializeObject(contact));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("savecontact")]
        [AllowAnonymous]
        public async Task<ActionResult> SaveContact([FromBody] Contact contact)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Debug.WriteLine($"Salvando: {JsonConvert.SerializeObject(contact)}");

                    bool isValidEmail = utilities.CheckEmail(contact.email);

                    if (isValidEmail == false)
                        return StatusCode(StatusCodes.Status400BadRequest, TxTResponses.GetTxTResponse(TxTResponse.validEmail));

                    Contact checkcontact = (from data in contactRepository.GetByQuery()
                                          where data.email == contact.email
                                          select data).FirstOrDefault<Contact>();

                    if (checkcontact != null)
                        return StatusCode(StatusCodes.Status400BadRequest, TxTResponses.GetTxTResponse(TxTResponse.RegisteredEmail));

                    int maxID = (from max in contactRepository.GetByQuery()
                                select (int?)max.id ?? 0).DefaultIfEmpty().Max();

                    contact.id = maxID + 1;
                    contactRepository.Add(contact);
                    context.SaveChanges();
                    transaction.Commit();

                    return Created($"http://localhost:{mainApiPort}/main/getcontact/{contact.id}", TxTResponses.GetTxTResponse(TxTResponse.RegisterOk));
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }
        }

        [HttpDelete("deletecontact/{email}")]
        [AllowAnonymous]
        public async Task<ActionResult> DeleteContact(string email)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    bool isValidEmail = utilities.CheckEmail(email);

                    if (isValidEmail == false)
                        return StatusCode(StatusCodes.Status400BadRequest, TxTResponses.GetTxTResponse(TxTResponse.validEmail));

                    Contact contact = (from c in contactRepository.GetByQuery()
                                       where c.email == email
                                       select c).FirstOrDefault();

                    if (contact == null)
                        return StatusCode(StatusCodes.Status400BadRequest, TxTResponses.GetTxTResponse(TxTResponse.UnregisteredContact));

                    contactRepository.Delete(contact);
                    context.SaveChanges();
                    transaction.Commit();

                    return Ok(TxTResponses.GetTxTResponse(TxTResponse.DeleteOk));
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }        
        }

        
    }
}
