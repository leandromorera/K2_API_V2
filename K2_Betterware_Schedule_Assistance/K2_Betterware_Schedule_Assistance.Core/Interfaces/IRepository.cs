
using K2_Betterware_Assistance.Core.Dtos;
using K2_Betterware_Schedule_Assistance.Core.Dtos;
using System.Collections.Generic;

namespace K2_Betterware_Assistance.Core.Interfaces{
    public interface IRepository
    {
        public string getBiostartEvents(Proceso criterios);
        public string getWorkbeatEmployees(Evento evento);
        public string putWorkbeatAssistance(Evento evento);

        public void openEvent();

        public void closeEvent();

        public int saveEvent(Proceso proceso, Evento evento);

        public int updateEvent(Evento evento);

        public int deleteEvents();

        public List<Evento> getEvents(Evento evento);

        public int saveProcess(Proceso proceso);

        public int updateProcess(Proceso proceso);

        public int deleteProcess();

        public List<Proceso> getLastProcess(Proceso proceso);

        public int updateConfigProcess(ConfigProcess configProcess);

        public List<ConfigProcess> getConfigProcess();
    
    }
}
