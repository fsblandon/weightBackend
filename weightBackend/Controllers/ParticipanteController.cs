using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using weightBackend.Models;

namespace weightBackend.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ParticipanteController: ControllerBase
    {
        private readonly ParticipanteContext _dbContext;
        private int cedula;
        private DateTime fecha_exec;

        public ParticipanteController(ParticipanteContext context)
        {
            _dbContext = context;

            if(_dbContext.Participantes.Count() == 0)
            {
                _dbContext.Participantes.Add(new Participante { cedula = cedula, fecha_exec = fecha_exec });
                _dbContext.SaveChanges();
            }

        }

        [HttpPost]
        public ActionResult GenerateFile([FromBody] Participante data)
        {
            cedula = data.cedula;
            fecha_exec = data.fecha_exec;
            try
            {
                _dbContext.Participantes.Add(new Participante { cedula = cedula, fecha_exec = fecha_exec });
                _dbContext.SaveChanges();

            }
            catch
            {
                return NotFound();
            }

            return Ok(data.file);
        }
    }
}
