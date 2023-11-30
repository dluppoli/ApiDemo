using API.Dtos;
using API.Models;
using API.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    [RoutePrefix("Sports")]
    public class SportsController : Controller
    {
        [Route("Index/{page}/{pagesize}")]
        public async Task<ActionResult> Index(int page, int pagesize)
        {
            ViewBag.Title = "Elenco sport";
            /*ViewBag.MioMessaggio = "Hello World!";

            ViewData["Sottotitolo"] = "Elenco degli sport delle olimpiadi";


            var s = new nAthleteDto
            {
                IdAthlete = 1,
                Name = "Mario Rossi",
                Weight = 80,
                Height = 180
            };*/

            if (page <= 0) page = 1;
            if(pagesize<=0) pagesize = 25;

            using(var context = new OlympicsContext())
            {
                var risultati = await context.Events
                    .OrderBy(e => e.Id)
                    .Skip( (page-1)*pagesize )
                    .Take(pagesize)
                    .ToListAsync();

                var conteggio = await context.Events.CountAsync();

                var vm = new PaginatedViewModel<Event>
                {
                    page = page,
                    pagesize = pagesize,
                    total = conteggio,
                    results = risultati
                };

                return View(vm);
            }
        }

        public async Task<ActionResult> Detail(int id)
        {
            ViewBag.Title = "Dettaglio sport";

            using(var context = new OlympicsContext())
            {
                var s = await context.Events.FirstOrDefaultAsync(w => w.Id == id);
                return View(s);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Title = "Modifica sport";

            using (var context = new OlympicsContext())
            {
                var s = await context.Events.FirstOrDefaultAsync(w => w.Id == id);
                return View(s);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Event e)
        {
            using (var context = new OlympicsContext())
            {
                var candidate = await context.Events.FirstOrDefaultAsync(w => w.Id == e.Id);
                candidate.Sport = e.Sport;
                candidate.Event1 = e.Event1;
                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Event e)
        { 
            using(var context = new OlympicsContext())
            {
                context.Events.Add(e);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            ViewBag.Title = "Cancellazione sport";

            using (var context = new OlympicsContext())
            {
                var s = await context.Events.FirstOrDefaultAsync(w => w.Id == id);
                return View(s);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Event e)
        {
            using (var context = new OlympicsContext())
            {
                var candidate = await context.Events.FirstOrDefaultAsync(w => w.Id == e.Id);
                context.Events.Remove(candidate);
                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
        }
    }
}