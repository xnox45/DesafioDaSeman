using Desafio.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using Desafio.Controllers.Validacoes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Models.Model;
using Desafio.Models.Entity;
using Desafio.Models.DAO;

namespace Desafio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly EventContext _context;

        public EventController(EventContext context)
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
                    return BadRequest("Informações para criação de envento incompletas");
                }

                string msgValidate = new Validacao().ValidacaoInsertEVento(evento, _context);

                if (msgValidate != null)
                    return Ok(msgValidate);

                evento.Name = evento.Name.ToLower();
                evento.Locality = evento.Locality.ToLower();

                await new EventoDAO(_context).InserirEvento(evento);

                string result = $"{evento.Name.ToUpper()} adiconado para a data: {evento.Date.Value.ToString("d")}, no local: {evento.Locality.ToUpper()}";

                return Ok(result);

            }

            catch (Exception ex)
            {
                return Ok("Erro ao inserir evento");
            }

        }

        [HttpGet]
        [Route("BuscarEvento")]
        public IActionResult BuscarEvento(EventoModel evento)
        {
            Evento result = null;
            if (new Validacao().ValidacaoDate(evento.Date))
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
        public async Task<IActionResult> CompraDeIngresso(ParticipanteModel model)
        {
            try
            {

                Evento evento = _context.Events.Where(x => x.Name == model.Evento.Name).Where(x => x.Locality == model.Evento.Locality).Where(x => x.Date == model.Evento.Date).FirstOrDefault();

                if (evento == null)
                    return Ok("Evento inexistente");

                string msgValidation = new Validacao().ValidacaoComprarIngresso(model, evento, _context);

                if (msgValidation != null)
                    return Ok(msgValidation);


                Participant result = new Participant()
                {
                    EventId = evento.Id,
                    Name = model.Name,
                    TaxNumber = model.TaxNumber
                };

                await new EventoDAO(_context).CompraDeIngresso(result);

                return Ok("Ingresso Comprado com sucesso");
            }
            catch (Exception ex)
            {
                return Ok("Erro ao comprar ingresso");
            }
        }
    }
}
