using API.Dtos;
using API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/v1/Games")]
    public class GamesController : ApiController
    {
        [Route("")]
        [HttpGet]
        public async Task<List<GameDto>> GetAll()
        {
            using(OlympicsContext context = new OlympicsContext())
            {
                return await context.Games
                    .Select(s=> new GameDto { 
                        Id = s.Id,
                        Games = s.Games,
                        Year = s.Year,
                        Season = s.Season
                    })
                    .ToListAsync();
            }
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOne(int id)
        {
            using (OlympicsContext context = new OlympicsContext())
            {
                var g = await context.Games
                    .Select(s => new GameDto
                    {
                        Id = s.Id,
                        Games = s.Games,
                        Year = s.Year,
                        Season = s.Season
                    })
                    .FirstOrDefaultAsync(w=>w.Id==id);

                if (g != null) return Ok(g);
                return NotFound();
                //BadRequest()
                //InternalServerError()

                //throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [Route("")]
        [HttpPost]
        public async Task<GameDto> newGame([FromBody]GameDto game)
        {
            try
            {
                using (var context = new OlympicsContext())
                {
                    Game g = new Game
                    {
                        Games = game.Games,
                        Season = game.Season,
                        Year = game.Year
                    };

                    context.Games.Add(g);
                    await context.SaveChangesAsync();
                    return new GameDto
                    {
                        Id = g.Id,
                        Season = g.Season,
                        Year = g.Year,
                        Games = g.Games
                    };
                }
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}