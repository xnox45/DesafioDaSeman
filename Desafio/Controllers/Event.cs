using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Desafio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Event : ControllerBase
    {
        [HttpPost]
        public void InsertEvent()
        { 
        }
    }
}
