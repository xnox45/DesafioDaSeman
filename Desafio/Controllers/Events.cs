using Desafio.Data;
using Desafio.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Desafio.Controllers.Validações;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Controllers.DAO;

namespace Desafio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Events : ControllerBase
    {

        private readonly EventContext _context;

        public Events(EventContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("InserirEvento")]
        public async Task<IActionResult> InserirEvento(Evento evento)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                Evento verification = _context.Events.Where(x => x.Date == evento.Date).Where(x => x.Locality == evento.Locality).FirstOrDefault();

                if (verification == null)
                {
                    evento.Name = evento.Name.ToLower();
                    evento.Locality = evento.Locality.ToLower();

                    await new EventoDAO(_context).InserirEvento(evento);

                    string result = $"{evento.Name.ToUpper()} adiconado para a data: {evento.Date.Value.ToString("d")}, no local: {evento.Locality.ToUpper()}";

                    return Ok(result);
                }

                return Ok("Evento indisponivel para essa data e local");
            }

            catch (Exception ex)
            {
                return Ok("Erro ao inserir evento");
            }

        }

        [HttpGet]
        [Route("BuscarEvento")]
        public IActionResult BuscarEvento(GetEvento evento)
        {
            Evento result = null;
            if (new Validação().ValidaçãoDate(evento.Date))
                result = new EventoDAO(_context).BuscarEvento(evento);

            else
                return Ok("data invalida");

            if (result == null)
            {
                return Ok("Evento não existe");
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("CompraDeIngresso")]
        public async Task<IActionResult> CompraDeIngresso(Participant participant, GetEvento getEvento)
        {
            try
            {
                Evento evento = _context.Events.Where(x => x.Name == getEvento.Name).Where(x => x.Locality == getEvento.Locality).Where(x => x.Date == getEvento.Date).FirstOrDefault();

                if (evento == null)
                {
                    return Ok("Evento inexistente");
                }

                List<Participant> participantes = _context.Participants.Where(a => a.EventId == evento.Id).ToList();

                if (participantes.Count == evento.Tickets)
                {
                    return Ok("Ingressos esgotados");
                }

                foreach (Participant participantFor in participantes)
                {
                    if (participant.TaxNumber.Equals(participantFor.TaxNumber))
                    {
                        return Ok("Ingresso já comprado nesse CPF");
                    }
                }

                await new EventoDAO(_context).CompraDeIngresso(participant);

                return Ok("Ingresso Comprado com sucesso");
            }
            catch (Exception ex)
            {
                return Ok("Erro ao comprar ingresso");
            }
        }
    }
}
