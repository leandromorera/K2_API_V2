using K2_Betterware_Assistance.Core.Dtos;
using K2_Betterware_Assistance.Core.Interfaces;
using K2_Betterware_Assistance.Infraestructure.Repositories;
using K2_Betterware_Schedule_Assistance.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace K2_Betterware_Schedule_Assistance.Api.Controllers
{
    [ApiController]
    public class ProcessController : ControllerBase {
        
        public ProcessController(){
           
        }

        [Route("api/getProcess")]
        [HttpGet]
        public List<Proceso> GetProcess(){
            Proceso proceso = new Proceso();
            IRepository _repository = new Repository();
            _repository.openEvent();
            List<Proceso> lstProcesos = _repository.getLastProcess(proceso);
            _repository.closeEvent();
            return lstProcesos;
        }

        [Route("api/getEvents")]
        [HttpGet]
        public List<Evento> GetEvents()
        {
            Evento evento = new Evento();
            IRepository _repository = new Repository();
            _repository.openEvent();
            List<Evento> lstProcesos = _repository.getEvents(evento);
            _repository.closeEvent();
            return lstProcesos;
        }

        [Route("api/deleteProcess")]
        [HttpGet]
        public IActionResult GetDeleteProcess()
        {
            IRepository _repository = new Repository();
            _repository.openEvent();
            _repository.deleteProcess();
            _repository.closeEvent();
            return Ok("Delete Process OK");
        }

        [Route("api/deleteEvents")]
        [HttpGet]
        public IActionResult GetDeleteEvents()
        {
            IRepository _repository = new Repository();
            _repository.openEvent();
            _repository.deleteEvents();
            _repository.closeEvent();
            return Ok("Delete Events OK");
        }

        [Route("api/reProcessEvents")]
        [HttpGet]
        public List<Evento> GetReprocessEvents()
        {
            Evento evento = new Evento();
            IRepository _repository = new Repository();
            _repository.openEvent();
            List<Evento> lstProcesos = _repository.getEvents(evento);
            _repository.closeEvent();
            return lstProcesos;
        }


        [Route("api/getProcessConfig")]
        [HttpGet]
        public List<ConfigProcess> GetProcessConfig()
        {   
            IRepository _repository = new Repository();
            _repository.openEvent();
            List<ConfigProcess> lstProcesos = _repository.getConfigProcess();
            _repository.closeEvent();
            return lstProcesos;
        }

    }
}
