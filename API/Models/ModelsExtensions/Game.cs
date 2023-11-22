using API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace API.Models
{
    public partial class Game
    {
        public Game(GameDto g)
        {
            UpdatefromDto(g);
        }

        public GameDto ToDto()
        {
            return new GameDto
            {
                Id = Id,
                Season = Season,
                Year = Year,
                Games = Games
            };
        }

        public void UpdatefromDto(GameDto g)
        {
            Games = g.Games;
            Season = g.Season;
            Year = g.Year;
        }
    }
}