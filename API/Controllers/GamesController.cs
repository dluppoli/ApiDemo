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
        public async Task<List<GameDto>> GetAll([FromUri]int page=1, [FromUri]int pageSize=999999)
        {
            using(OlympicsContext context = new OlympicsContext())
            {
                var listGame = await context.Games
                    /*.Select(s=> new GameDto { 
                        Id = s.Id,
                        Games = s.Games,
                        Year = s.Year,
                        Season = s.Season
                    })*/
                    .ToListAsync();

                List<GameDto> risultati = new List<GameDto>();
                foreach (var item in listGame)
                {
                    risultati.Add(item.ToDto());
                }
                return risultati;
            }
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOne(int id)
        {
            using (OlympicsContext context = new OlympicsContext())
            {
                var g = await context.Games
                    /*.Select(s => new GameDto
                    {
                        Id = s.Id,
                        Games = s.Games,
                        Year = s.Year,
                        Season = s.Season
                    })*/
                    .FirstOrDefaultAsync(w=>w.Id==id);

                if (g != null) return Ok(g.ToDto());
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
                    Game g = new Game(game);
                    /*{
                        Games = game.Games,
                        Season = game.Season,
                        Year = game.Year
                    };*/

                    context.Games.Add(g);
                    await context.SaveChangesAsync();
                    return g.ToDto();/*new GameDto
                    {
                        Id = g.Id,
                        Season = g.Season,
                        Year = g.Year,
                        Games = g.Games
                    };*/
                }
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("{id}")]
        [HttpPut]
        public async Task editGame(int id,[FromBody] GameDto game)
        {
            if (id != game.Id) throw new HttpResponseException(HttpStatusCode.BadRequest);
            using(var context = new OlympicsContext())
            {
                var candidate = await context.Games.FirstOrDefaultAsync(w => w.Id == id);
                if( candidate==null) throw new HttpResponseException(HttpStatusCode.NotFound);

                /*candidate.Season = game.Season;
                candidate.Year = game.Year;
                candidate.Games = game.Games;*/
                candidate.UpdatefromDto(game);

                await context.SaveChangesAsync();
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task deleteGame(int id)
        {
            using (var context = new OlympicsContext())
            {
                var candidate = await context.Games.FirstOrDefaultAsync(w => w.Id == id);
                if (candidate == null) throw new HttpResponseException(HttpStatusCode.NotFound);

                context.Games.Remove(candidate);
                await context.SaveChangesAsync();
            }
        }


        [Route("Next")]
        [HttpGet]
        public string GetNext()
        {
            return "Next";
        }

        [Route("{id}/Athletes")]
        [HttpGet]
        public string GetAthletes(int id)
        {
            return $"{id}/Athletes";
        }

        [Route("{id}/Athletes/WithMedals")]
        [HttpGet]
        public string GetAthletesWithMedals(int id)
        {
            return $"{id}/Athletes/WithMedals";
        }

        [Route("{idGame}/Athletes/{idAthlete}")]
        [HttpGet]
        public string GetAthleteByGame(int idGame, int idAthlete)
        {
            return $"{idGame}/Athletes/{idAthlete}";
        }

    }
}