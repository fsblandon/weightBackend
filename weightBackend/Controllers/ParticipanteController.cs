using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using weightBackend.Models;

namespace weightBackend.Controllers
{
    [Route("api/Participante")]
    [ApiController]
    [EnableCors("MyCorsPolicy")]
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

        [HttpPost("generateFile")]
        public ActionResult GenerateFile([FromBody] Participante data)
        {

            cedula = data.cedula;
            fecha_exec = data.fecha_exec;
            try
            {
                _dbContext.Participantes.Add(new Participante { cedula = cedula, fecha_exec = fecha_exec });
                _dbContext.SaveChanges();
                //var file = Request.Form.Files[0];

            }
            catch
            {
                return NotFound();
            }

            return Ok(data.file);
        }
    }
}
