
using System;
using K2_betterware_Biostart_Assistance.Core.Interfases;
using K2_betterware_Biostart_Assistance.Infrastructure.Service;

namespace K2_betterware_Biostart_Assistance.Infrastructure.Repositories
{
    public class AssistanceRepository : IRepository
    {
       
        //////////////////////// metodos biostar ///////////////////////////////
        public string Token_bio()
        {
            AssistanceService assistanceService = new AssistanceService();
            String strResponse = assistanceService.token_bio(); // servicio y parametro persona
            return strResponse;
        }

        public string User_bio()
        {
            AssistanceService assistanceService = new AssistanceService();
            String strResponse = assistanceService.user_bio(); // servicio y parametro persona
            return strResponse;
        }

        public string Event_search_bio()
        {
            AssistanceService assistanceService = new AssistanceService();
            String strResponse = assistanceService.event_search_bio(); // servicio y parametro persona
            return strResponse;
        }

        public string Device_bio()
        {
            AssistanceService assistanceService = new AssistanceService();
            String strResponse = assistanceService.device_bio(); // servicio y parametro persona
            return strResponse;
        }


        public String bio_event_search(string tk_bio, string jsonb)
        {
            AssistanceService assistanceService = new AssistanceService();
            String strResponse = assistanceService.bio_event_search(tk_bio, jsonb); // servicio y parametro persona
            return strResponse;
        }

    }
}
