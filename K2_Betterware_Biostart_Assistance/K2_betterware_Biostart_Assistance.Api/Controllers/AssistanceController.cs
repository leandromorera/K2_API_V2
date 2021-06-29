using K2_betterware_Biostart_Assistance.Core.Interfases;
using Microsoft.AspNetCore.Mvc;

namespace K2_betterware_Biostart_Assistance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssistanceController : ControllerBase
    {

        private readonly IRepository _repository;

        public AssistanceController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAssistance(string f_ini, string f_nal, string limit, string type)
        {
            string orchesting;

            if (f_ini == null || f_nal == null || limit == null || type == null) 
            {
                orchesting = "Error;  debe ingresar fecha de inicio, fecha final, limite de registros y tipo de registro por ejemplo: https://localhost:44354/api/Assistance?df27f4c5b655409bb94c471e5c314aba&f_ini=2021-06-20T10:26:57&f_nal=2021-06-20T20:26:57&limit=51&type=4867";
            }

            else
            {
                string jsonb = "{\"Query\":{\"limit\":" + limit + ",\"conditions\":[{\"column\":\"datetime\",\"operator\":3,\"values\":[\"" + f_ini + ".00Z\",\"" + f_nal + ".00Z\"]},{\"column\":\"event_type_id.code\",\"operator\":0,\"values\":[\"" + type + "\"],\"descending\":true}]}}";
                var tk_bio = _repository.Token_bio();
                orchesting = _repository.bio_event_search(tk_bio.ToString(), jsonb);
            }

            return Ok(orchesting);
        }
    }
}
