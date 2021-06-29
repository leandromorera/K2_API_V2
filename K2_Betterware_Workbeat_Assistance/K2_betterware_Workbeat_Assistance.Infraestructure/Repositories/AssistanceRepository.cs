using K2_Betterware_Workbeat_Assistance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using K2_Betterware_Workbeat_Assistance.Core.Interfaces;
using System.Threading.Tasks;
using K2_Betterware_Workbeat_Assistance.Infraestructure.Service;

namespace K2_Betterware_Workbeat_Assistance.Infraestructure.Repositories
{
    public class AssistanceRepository : IRepository
    {

        


        public string getToken()
        {
            AssistanceService assistanceService = new AssistanceService();

            String strResponse = assistanceService.getToken();

            return strResponse;
        }


        public string get_employed(string word)
        {
            AssistanceService assistanceService = new AssistanceService();

            String strResponse = assistanceService.giveme_workbeat_generic(word);

            return strResponse;
        }

        public string get_empleado(string word)
        {
            AssistanceService assistanceService = new AssistanceService();

            String strResponse = assistanceService.giveme_workbeat_empleados(word);  // servicio lista de empleados

            return strResponse;
        }

        public string get_persona(string word, string npr)
        {
            AssistanceService assistanceService = new AssistanceService();

            String strResponse = assistanceService.giveme_workbeat_persona(word, npr); // servicio y parametro persona

            return strResponse;
        }


        public string checando(string id, string fechora, string disid, string direccion)
        {
            AssistanceService assistanceService = new AssistanceService();

            String strResponse = assistanceService.Checando(id, fechora, disid, direccion); // servicio y parametro persona

            return strResponse;
        }

        


    }
}
