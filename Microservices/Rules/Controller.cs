using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;


namespace Controllers
{
    [ApiController]
    [Route("rules")]
    public class Controller : ControllerBase
    {

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
        [Route("help")]
        public ActionResult<String> AccountHelp(){
            return "End Point URI's\n1.";
        }
    }
}