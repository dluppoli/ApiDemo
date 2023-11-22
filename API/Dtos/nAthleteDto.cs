using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Dtos
{
    public class nAthleteDto
    {
        public int IdAthlete { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public short? Height { get; set; }
        public short? Weight { get; set; }
    }
}