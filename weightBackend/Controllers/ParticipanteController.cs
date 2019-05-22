using System;
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
                var fecha_exec = new DateTime();
                participante.fecha_exec = fecha_exec;

                var file = HttpContext.Request.Form.Files["file"];

                var calculoDias = "";
                calculoDias = Domain.CalcularViajesDia.CalcularDias(file);
                if(calculoDias != "")
                {
                    saveLog();
                    /*HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent(calculoDias);
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = "lazy_loading_example_output.txt";
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");*/


                    StringBuilder sb = new StringBuilder();
                    string output = calculoDias;
                    sb.Append(output);
                    //sb.Append("\r\n");

                    string text = sb.ToString();

                    Response.Clear();
                    //Response.Headers.Clear();

                    Response.Headers.Append("Content-Length", text.Length.ToString());
                    //Response.ContentType = "text/plain";
                    Response.ContentType = "octet-stream";
                    Response.Headers.Append("Content-Disposition", "attachment;filename=\"lazy_loading_example_output.txt\"");

                    Response.WriteAsync(text);
                    //Response.End();
                    return Response;
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
