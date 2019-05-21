using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        Participante participante;

        public ParticipanteController(ParticipanteContext context)
        {
            _dbContext = context;

            //if (_dbContext.Participantes.Count() == 0)
            //{
            //    _dbContext.Participantes.Add(new Participante { id = new Guid(), cedula = cedula, fecha_exec = fecha_exec }); ;
            //    _dbContext.SaveChanges();
            //}

        }

        [HttpPost]
        [Route("generateFile")]
        public ActionResult GenerateFile([FromQuery] int cedula)
        {
            try
            {
                participante.cedula = cedula;
                var fecha_exec = new DateTime();
                participante.fecha_exec = fecha_exec;

                participante.file = HttpContext.Request.Form.Files["file"];

                var calculoDias = "";
                calculoDias = Domain.CalcularViajesDia.CalcularDias(participante.file);
                if(calculoDias != "")
                {
                    this.saveLog();
                }
                // descargar archivo generado en memoria
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Accepted)
                {
                    Content = new StringContent(calculoDias)
                };
                httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "lazy_loading_example_output.txt"
                };
                httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                ResponseMessageResult responseMessageResult = responseMessageResult(httpResponseMessage);
                return responseMessageResult;

            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }

            return Ok();
        }

        private void saveLog()
        {
            _dbContext.Participantes.Add(new Participante { cedula = participante.cedula, fecha_exec = participante.fecha_exec });
            _dbContext.SaveChanges();
        }
    }
}
