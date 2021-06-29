using System;
using System.Collections.Generic;
using System.Text;

namespace K2_Betterware_Assistance.Core.Dtos
{
    public class Proceso
    {
        public string idProceso { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public string tipoEvento { get; set; }
        public string status { get; set; }
    }
}
