using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;

//"docker_db1": "Server=localhost,1433;database=apidb;User ID= SA;password=abc123!!@;"

namespace Controllers
{
    [ApiController]
    [Route("account")]
    public class Controller : ControllerBase
    {
        private readonly AccountDB _ACDB;
        
        public Controller(ILogger<Controller> logger, AccountDB acdb)
        {            
            _ACDB = acdb;
        }

        [HttpGet]
        [Route("getIP")]
        public string GetIP()
        {
            // Getting host name
            string host = Dns.GetHostName();
            
            // Getting ip address using host name
            IPHostEntry ip = Dns.GetHostEntry(host);
            return ip.AddressList[0].ToString();
        }

        [HttpGet]
        [Route("test")]
        public ActionResult<String> TestEndPoint(){
            return "Test Succesful";
        }

        [HttpPost]
        public async Task<IResult> PostAccount(Account account)
        {
            _ACDB.Accounts.Add(account);
            await _ACDB.SaveChangesAsync();
            return Results.Created($"/{account.Email}", account);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<Account>> GetAccountByEmail(string email)
        {
            var account = await _ACDB.Accounts.Where(m => m.Email  == email).ToListAsync();
            
            if(account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        [HttpPut]
        public async Task<IResult> PutAccount(Account acc1)
        {
            var acc2 = await _ACDB.Accounts.FindAsync(acc1.Id);
            if(acc2 == null)
            {
                return Results.NotFound();
            }
            //Update Values
            acc2.Email = acc1.Email;
            acc2.Username = acc1.Username;
            acc2.Password = acc1.Password;
            await _ACDB.SaveChangesAsync();
            return Results.NoContent();
        } 

        [HttpDelete("{email}")]
        public async Task<IResult> DeleteTodo(string email)
        {
            var userAccount = await _ACDB.Accounts.Where(m => m.Email  == email).ToListAsync();
            if(userAccount[0] is Account account)
            {
                _ACDB.Accounts.Remove(account);
                await _ACDB.SaveChangesAsync();
                return Results.Ok(account);
            }
            return Results.NotFound();
        }
    }
}