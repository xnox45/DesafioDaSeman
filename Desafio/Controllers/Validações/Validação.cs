using Desafio.Data;
using Desafio.Models.Entity;
using Desafio.Models.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Controllers.Validacoes
{
    public class Validacao : Controller
    {
        public bool ValidacaoDate(DateTime? date)
        {


            if (date < Convert.ToDateTime("01/01/2020 00:00:00") || date == null)
            {
                string MsgDate = "Data invalida";
                return false;
            }

            return true;
        }

        public string ValidacaoInsertEVento(Evento evento, EventContext _context)
        {
            string msg = null;

            if (evento.Tickets < 15)
                return msg = "Minimo de 15 tickets para a criação do evento";

            if (evento.Date <= DateTime.Now.Date)
                return msg = "Data para criação de evento invalida";
            
            Evento verification = _context.Events.Where(x => x.Date == evento.Date).Where(x => x.Locality == evento.Locality).FirstOrDefault();

            if (verification != null)
                return msg = "Evento indisponivel para essa data e local";

            return msg;
        }

        public string ValidacaoComprarIngresso(ParticipanteModel model, Evento evento,EventContext _context)
        {
            string msg = null;

          
            List<Participant> participantes = _context.Participants.Where(a => a.EventId == evento.Id).ToList();

            if (participantes.Count == evento.Tickets)
            {
                return msg = "Ingressos esgotados";
            }

            foreach (Participant participantFor in participantes)
            {
                if (model.TaxNumber.Equals(participantFor.TaxNumber))
                {
                    return msg = "Ingresso já comprado nesse CPF";
                }
            }

            return msg;
        }
    }
}
