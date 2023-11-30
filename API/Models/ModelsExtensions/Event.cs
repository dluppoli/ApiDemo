using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace API.Models
{
    [MetadataType(typeof(SportsEventsMetadata))]
    public partial class Event
    {
    }

    public class SportsEventsMetadata
    {
        [Required(ErrorMessage = "Obbligatorio")]
        [DisplayName("Evento")]
        public string Event1 { get; set; }

        [Required]
        public string Sport { get; set; }
    }
}