using System;
using Microsoft.EntityFrameworkCore;

namespace weightBackend.Models
{
    public class ParticipanteContext: DbContext
    {
        public ParticipanteContext(DbContextOptions<ParticipanteContext> options): base(options)
        {
        }

        public DbSet<Participante> Participantes { get; set; }
    }
}
