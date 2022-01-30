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
        public string ValidacaoBuscaDeEvento(EventoModel evento)
        {
            string MsgDate = null;

            if (evento.Date < Convert.ToDateTime("01/01/2020 00:00:00") || evento.Date == null || evento.Date < Convert.ToDateTime("01/01/2020"))
            {
                MsgDate = "Data invalida";
                return MsgDate;
            }

            return MsgDate;
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

        public string ValidacaoComprarIngresso(ParticipanteModel model, Evento evento, EventContext _context)
        {
            string msgValidate = null;

            if (!model.TaxNumber.Length.Equals(11))
            {
                return msgValidate = "CPF Invalido";
            }

            List<Participant> participantes = _context.Participants.Where(a => a.EventId == evento.Id.Value).ToList();

            if (participantes.Count == evento.Tickets)
            {
                return msgValidate = "Ingressos esgotados";
            }



            foreach (Participant participantFor in participantes)
            {
                if (model.TaxNumber.Equals(participantFor.TaxNumber))
                {
                    return msgValidate = "Ingresso já comprado nesse CPF";
                }
            }

            return msgValidate;
        }

        public string ValidacaoBuscaDeParticipante(GetParticipant getParticipant)
        {
            string msgValidate = null;

            if (!getParticipant.Taxnumber.Length.Equals(11))
            {
                return msgValidate = "CPF Invalido";
            }

            if (getParticipant.Evento != null)
            {
                if (getParticipant.Evento.Name == null)
                {
                    if (getParticipant.Evento.Date != null)
                    {
                        return msgValidate = "Nome do evento obrigatorio";
                    }
                }
            }

            return msgValidate;
        }
    }
}
