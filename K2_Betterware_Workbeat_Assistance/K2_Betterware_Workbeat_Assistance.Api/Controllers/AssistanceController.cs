
using Microsoft.AspNetCore.Mvc;
using K2_Betterware_Workbeat_Assistance.Core.Interfaces;


namespace K2_Betterware_Workbeat_Assistance.Api.Controllers
{
    [ApiController]
    public class AssistanceController : ControllerBase
    {
        private readonly IRepository _repository;

        public AssistanceController(IRepository repository)
        {
            _repository = repository;
        }


        [Route("api/getEmpleados")]
        [HttpGet]
        public IActionResult GetEmpleados()
        {
            string emp = "/v3/asi/empleados";
            var emp_resp_empl = _repository.get_empleado(emp);

            return Ok( emp_resp_empl );
        }

        [Route("api/putAssistance")]
        [HttpGet]
        public IActionResult InsertAssistance(string nip, string fechaHora, string idDispositivo, string posicion)
        {
            /// acceso al token.
            /*string p_id = "8251";                         //NIP
            string fechahora = "2019-11-05T09:04:08";       //Fecha.
            string dispositivoId = "11948";                 //IdDispositivo
            string posi = "[E|1|N]";*/
            string strResponse = "";

            if (nip == null || fechaHora == null || idDispositivo == null || posicion == null)
            {
                strResponse = "Error;  debe ingresar NIP, FechaHora de Registro, Clave de Dispositivo Posicion";
            } else
            {
                strResponse = _repository.checando(nip, fechaHora, idDispositivo, posicion);
            }

            return Ok(strResponse);
        }

    }
}
