using System;
using System.Collections.Generic;
using K2_Betterware_Assistance.Core.Dtos;
using K2_Betterware_Assistance.Core.Interfaces;
using K2_Betterware_Assistance.Infraestructure.Services;
using K2_Betterware_Schedule_Assistance.Core.Dtos;
using K2_Betterware_Schedule_Assistance.Infraestructure.Services;

namespace K2_Betterware_Assistance.Infraestructure.Repositories{
    public class Repository : IRepository
    {
        LogsAssistanceService logService = new LogsAssistanceService();

        //////////////////////// metodos biostar ///////////////////////////////
        public string getBiostartEvents(Proceso criterios)
        {
            ClientService service = new ClientService();
            String strResponse = service.getBiostartEvents(criterios);
            return strResponse;
        }

        public string getWorkbeatEmployees(Evento evento)
        {
            ClientService service = new ClientService();
            String strResponse = service.getWorkbeatEmployees(evento);
            return strResponse;
        }


        public string putWorkbeatAssistance(Evento evento)
        {
            ClientService service = new ClientService();
            String strResponse = service.putWorkbeatAssistance(evento);
            return strResponse;
        }

        

        public void openEvent()
        {
            this.logService = new LogsAssistanceService();
            logService.openEvent();
        }

        public void closeEvent()
        { 
            this.logService.closeEvent();
        }

        public int saveEvent(Proceso proceso, Evento evento)
        {
            return this.logService.saveEvent(proceso, evento);
        }

        public int updateEvent(Evento evento)
        {
            return this.logService.updateEvent(evento); ;
        }

        public int deleteEvents()
        {
            return this.logService.deleteEvents(); ;
        }

   
        public List<Evento> getEvents(Evento evento)
        {
            return this.logService.getEvents(evento);
        }

            public int saveProcess(Proceso proceso)
        {
            return this.logService.saveProcess(proceso);
        }

        public int updateProcess(Proceso proceso)
        {
            return this.logService.updateProcess(proceso); ;
        }

        public int deleteProcess()
        {
            return this.logService.deleteProcess(); ;
        }

        public List<Proceso> getLastProcess(Proceso proceso)
        {   
            return this.logService.getLastProcess(proceso); 
        }

        public int updateConfigProcess(ConfigProcess configProcess) {
            return this.logService.updateConfigProcess(configProcess);
        }
        public List<ConfigProcess> getConfigProcess() {
            return this.logService.getConfigProcess();
        }
    }
}

