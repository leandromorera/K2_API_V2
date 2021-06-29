using K2_Betterware_Workbeat_Assistance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace K2_Betterware_Workbeat_Assistance.Core.Interfaces
{
    public interface IRepository
    {
        
        string getToken();
        string get_employed(string stp);
        string get_empleado(string stp);
        string get_persona(string stp, string npr);

        string checando(string id, string fechora, string disid, string direccion);

        

    }
}
