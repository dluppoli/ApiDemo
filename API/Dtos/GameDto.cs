using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Games { get; set; }
        public short Year { get; set; }
        public string Season { get; set; }
    }
}