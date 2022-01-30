using Desafio.Data;
using Desafio.Models.Entity;
using Desafio.Models.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Models.DAO
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

        public async Task<Evento> BuscarEvento(EventoModel getEvento)
        {
            Evento evento = null;
            if (getEvento.Name != null)
                evento = await _contextDAO.Events.Where(x => x.Name == getEvento.Name).Where(x => x.Date == getEvento.Date).Where(x => x.Locality == getEvento.Locality).FirstOrDefaultAsync();

            else
                evento = await _contextDAO.Events.Where(x => x.Date == getEvento.Date).Where(x => x.Locality == getEvento.Locality).FirstOrDefaultAsync();

            return evento;
        }

        public async Task<Participant> CompraDeIngresso(Participant participant)
        {
            _contextDAO.Participants.Add(participant);
            await _contextDAO.SaveChangesAsync();

            return participant;
        }

        public async Task<List<GetParticipant>> BuscaParticipante(GetParticipant getParticipant)
        {
            List<Participant> participants = new List<Participant>();
            List<GetParticipant> participantsAndEventos = new List<GetParticipant>();

            if (getParticipant.Evento != null)
            {
                if (!string.IsNullOrEmpty(getParticipant.Evento.Name))
                {
                    Evento evento = await _contextDAO.Events.Where(x => x.Name == getParticipant.Evento.Name).Where(x => x.Locality == getParticipant.Evento.Locality)
                        .Where(x => x.Date.Value == getParticipant.Evento.Date.Value).FirstOrDefaultAsync();

                    if (evento != null)
                    {
                        participants = await _contextDAO.Participants.Where(x => x.TaxNumber == getParticipant.Taxnumber).Where(x => x.EventId == evento.Id.Value).ToListAsync();

                        EventoModel getEvento = new EventoModel()
                        {
                            Name = evento.Name,
                            Locality = evento.Locality,
                            Date = evento.Date
                        };

                        if (participants.Count > 0)
                        {
                            participantsAndEventos.Add(new GetParticipant()
                            {
                                Name = participants[0].Name,
                                Taxnumber = participants[0].TaxNumber,
                                Evento = getEvento
                            });
                        }
                    }
                }
            }

            else
            {
                participants = await _contextDAO.Participants.Where(x => x.TaxNumber == getParticipant.Taxnumber).ToListAsync();

                if (participants.Count > 0)
                {
                    foreach (Participant participantFor in participants)
                    {
                        Evento evento = await _contextDAO.Events.Where(x => x.Id.Value == participantFor.EventId).FirstAsync();

                        if (participants.Count > 0)
                        {
                            EventoModel getEvento = new EventoModel()
                            {
                                Name = evento.Name,
                                Locality = evento.Locality,
                                Date = evento.Date
                            };

                            participantsAndEventos.Add(new GetParticipant()
                            {
                                Name = participantFor.Name,
                                Taxnumber = participantFor.TaxNumber,
                                Evento = getEvento
                            });
                        }
                    }
                }
            }

            return participantsAndEventos;
        }
    }
}
