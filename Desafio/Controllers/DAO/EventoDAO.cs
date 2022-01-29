using Desafio.Data;
using Desafio.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Controllers.DAO
{
    public class EventoDAO : Controller
    {
        private readonly EventContext _contextDAO;

        public EventoDAO(EventContext context)
        {
            _contextDAO = context;
        }
        public async Task<Evento> InserirEvento(Evento evento)
        {
            try
            {
                _contextDAO.Events.Add(evento);
                await _contextDAO.SaveChangesAsync();


                return evento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Evento BuscarEvento(GetEvento getEvento)
        {
            Evento evento = _contextDAO.Events.Where(x => x.Name == getEvento.Name).Where(x => x.Date == getEvento.Date).Where(x => x.Locality == getEvento.Locality).FirstOrDefault();

            return evento;
        }

        public async Task<Participant> CompraDeIngresso(Participant participant)
        {
            _contextDAO.Participants.Add(participant);
            await _contextDAO.SaveChangesAsync();

            return participant;
        }
    }
}
