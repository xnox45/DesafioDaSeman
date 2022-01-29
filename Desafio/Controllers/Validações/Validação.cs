using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Controllers.Validações
{
    public class Validação : Controller
    {
        public bool ValidaçãoDate(DateTime? date)
        {


            if (date < Convert.ToDateTime("01/01/2020 00:00:00") || date == null)
            {
                string MsgDate = "Data invalida";
                return false;
            }

            return true;
        }
    }
}
