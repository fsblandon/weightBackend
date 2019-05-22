using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace weightBackend.Models
{
    public class Participante
    {
        [Key]
        public Guid id { get; set; }
        public int cedula { get; set; }
        public DateTime fecha_exec { get; set; }
        public Participante()
        {}
    }
}
