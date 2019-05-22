using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using weightBackend.Models;

namespace weightBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsPolicy")]
    public class ParticipanteController: ControllerBase
    {
        private readonly ParticipanteContext _dbContext;
        Participante participante = new Participante();

        public ParticipanteController(ParticipanteContext context)
        {
            _dbContext = context;
        }

        [HttpPost]
        [Route("generateFile")]
        public object GenerateFile([FromQuery] int cedula)
        {
            try
            {
                participante.cedula = cedula;
                DateTime now = DateTime.Now;
                participante.fecha_exec = now;

                var file = HttpContext.Request.Form.Files["file"];

                var calculoDias = "";
                calculoDias = Domain.CalcularViajesDia.CalcularDias(file);
                if(calculoDias != "")
                {
                    saveLog();
                    var stream = new MemoryStream(Encoding.ASCII.GetBytes(calculoDias));
                    return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain").MediaType)
                    {
                        FileDownloadName = "lazy_loading_output.txt"
                    };
                }
                else
                {
                    return NoContent();
                }
            }
            catch(Exception e)
            {
                return new JsonResult(e.Message);
            }
        }

        private void saveLog()
        {
            _dbContext.Participantes.Add(new Participante { cedula = participante.cedula, fecha_exec = participante.fecha_exec });
            _dbContext.SaveChanges();
        }
    }
}
