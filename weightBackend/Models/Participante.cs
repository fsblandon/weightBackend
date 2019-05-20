using Microsoft.AspNetCore.Http.Internal;
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
        public FormFile file { get; set; }

        public Participante()
        {
        }
    }
}
