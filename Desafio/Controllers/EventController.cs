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
using Microsoft.EntityFrameworkCore;

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

        #region
        /// <summary>
        /// Método de Adicionar um evento.
        /// </summary>
        /// <param name="evento">
        /// <![CDATA[
        /// Mínimo de 15 tickets para a criação do evento.<br/>
        /// Data para criação de evento deve ser no mínimo um dia após o dia de hoje. <br />
        /// Confira se o local já não está ocupado nessa data com o método BuscarEvento.<br />
        ///]]>
        /// </param>
        /// <returns>
        /// Nome do evento adiconado para a data: Data do evento, no local: local do evento
        /// </returns>
        #endregion
        [HttpPost]
        [Route("InserirEvento")]
        public async Task<IActionResult> InserirEvento(Evento evento)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Informações para criação de evento incompletas");
                }

                string msgValidate = new Validacao().ValidacaoInsertEVento(evento, _context);

                if (!string.IsNullOrEmpty(msgValidate))
                    return BadRequest(msgValidate);

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

        #region
        /// <summary>
        /// Método para buscar evento.
        /// </summary>
        /// <param name="evento">
        /// <![CDATA[
        /// Esse método só busca eventos a partir de 2020.</br>
        /// Busca evento por local e data caso não haja nome.
        /// ]]>
        /// </param>
        /// <returns>Evento com a quantidade de ingressos disponiveis.</returns>
        #endregion
        [HttpGet]
        [Route("BuscarEvento")]
        public async Task<IActionResult> BuscarEvento(EventoModel evento)
        {
            try
            {
                Evento result = null;

                string validate = new Validacao().ValidacaoBuscaDeEvento(evento);

                if (!string.IsNullOrEmpty(validate))
                    return BadRequest(validate);

                result = await new EventoDAO(_context).BuscarEvento(evento);

                if (result == null)
                    return BadRequest("Evento não existe");

                List<Participant> participant = await _context.Participants.Where(x => x.EventId == result.Id.Value).ToListAsync();

                result.TotalTickets = result.Tickets - participant.Count;

                return Ok($"Nome: {result.Name.ToUpper()}\nData: {result.Date.Value.ToString("d")}\ningressos disponiveis: {result.TotalTickets}");
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Erro");
            }

        }
        #region
        /// <summary>
        /// Método Para compra de ingresso
        /// </summary>
        /// <param name="model">
        /// <![CDATA[
        /// Esse método confere se tem ingressos disponíveis.<br/>
        /// Confira se o CPF está realmente correto.<br/>
        /// Informações do evento obrigatorio.<br/>
        ///]]>
        /// </param>
        /// <returns>
        /// Ingresso comprado com o NOME e CPF do participante
        /// </returns>
        #endregion
        [HttpPost]
        [Route("CompraDeIngresso")]
        public async Task<IActionResult> CompraDeIngresso(ParticipanteModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Evento.Name))
                    return BadRequest("Nome do evento sem valor");

                Evento evento = null;

                evento = await _context.Events.Where(x => x.Name == model.Evento.Name).Where(x => x.Locality == model.Evento.Locality).Where(x => x.Date == model.Evento.Date).FirstOrDefaultAsync();

                if (evento == null)
                    return BadRequest("Evento inexistente\nVerifique os dados do evento");

                string msgValidate = new Validacao().ValidacaoComprarIngresso(model, evento, _context);

                if (!string.IsNullOrEmpty(msgValidate))
                    return BadRequest(msgValidate);


                Participant result = new Participant()
                {
                    EventId = evento.Id.Value,
                    Name = model.Name,
                    TaxNumber = model.TaxNumber
                };

                await new EventoDAO(_context).CompraDeIngresso(result);

                return Ok($"Ingresso comprado com sucesso\nNome: {model.Name}\nCPF: {result.TaxNumber}");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao comprar ingresso");
            }
        }

        #region
        /// <summary>
        /// Metodo para busca de participantes
        /// </summary>
        /// <param name="participant"><![CDATA[
        /// Com CPF conseguimos buscar todos os eventos que a pessoa participou.<br/>
        /// Se informamos os dados do Evento conseguimos informar  se a pessoa participou do evento ou não.<br/>
        ///]]></param>
        /// <returns></returns>
        #endregion
        [HttpGet]
        [Route("BuscaDeParticipante")]
        public async Task<IActionResult> BuscaDeParticipante(GetParticipant participant)
        {
            try
            {
                string msgValidate = new Validacao().ValidacaoBuscaDeParticipante(participant);

                if (!string.IsNullOrEmpty(msgValidate))
                {
                    return BadRequest(msgValidate);
                }

                List<GetParticipant> result = await new EventoDAO(_context).BuscaParticipante(participant);

                if (result.Count == 0)
                    return BadRequest($"CPF({participant.Taxnumber}) não cadastrado em nenhum evento\nOu dados do evento incorretos");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar participante");
            }

        }
    }
}
