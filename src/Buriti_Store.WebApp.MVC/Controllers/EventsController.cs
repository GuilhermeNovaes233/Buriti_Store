using Buriti_Store.Core.Data.EventSourcing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buriti_Store.WebApp.MVC.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventSourcingRepository _eventSourcingRepository;

        public EventsController(IEventSourcingRepository eventSourcingRepository)
        {
            _eventSourcingRepository = eventSourcingRepository;
        }

        [HttpGet("events/{id:guid}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var eventos = await _eventSourcingRepository.GetEvents(id);
            return View(eventos);
        }
    }
}
