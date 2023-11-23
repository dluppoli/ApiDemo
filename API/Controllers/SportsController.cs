using API.Dtos;
using API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class SportsController : Controller
    {
        public async Task<ActionResult> Index()
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

            using(var context = new OlympicsContext())
            {
                var risultati = await context.Events.ToListAsync();
                return View(risultati);
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
    }
}