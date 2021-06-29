
using System.Net;
using System.IO;
using System;
using K2_Betterware_Assistance.Core.Dtos;
using Newtonsoft.Json.Linq;

namespace K2_Betterware_Assistance.Infraestructure.Services{
    public class ClientService{
 
        public string getBiostartEvents(Proceso criterios)
        {
            string responseBody = "";
            try
            {
                //string url = "https://localhost:5002/api/Assistance?f_ini=2021-06-20T10:26:57&f_nal=2021-06-20T20:26:57&limit=51&type=4867";
                string url = "https://localhost:5002/api/Assistance?f_ini=" + criterios.fechaInicio + "&f_nal=" + criterios.fechaFin + "&limit=51&type=" + criterios.tipoEvento;
                Console.WriteLine($"        URL Biostar:: {url}");
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "GET";

                WebResponse response = myRequest.GetResponse();
                Stream strReader = response.GetResponseStream();
                StreamReader objReader = new StreamReader(strReader);
                responseBody = objReader.ReadToEnd().ToString();
                //Console.WriteLine($"        Events Biostar:: {responseBody}");

            }
            catch (Exception ex) {
                Console.WriteLine(ex);
                responseBody = "Error:" ;
            }
            return responseBody;
        }

        public string getWorkbeatEmployees(Evento evento)
        {
            string responseBody = "";
            try
            {
                string url = "https://localhost:5004/api/getEmpleados";
                Console.WriteLine($"            URL Workbeat Employees:: {url}");
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "GET";

                WebResponse response = myRequest.GetResponse();
                Stream strReader = response.GetResponseStream();
                StreamReader objReader = new StreamReader(strReader);
                responseBody = objReader.ReadToEnd().ToString();
                //Console.WriteLine($"        Events Biostar:: {responseBody}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                responseBody = "Error:" + ex.Message;
            }
            return responseBody;
        }

        public string putWorkbeatAssistance(Evento evento)
        {
           string responseBody = "";
            try
            {
                string url = "https://localhost:5004/api/putAssistance?nip=" + evento.nip + "&fechaHora=" + evento.fechaEvento + "&idDispositivo=" + evento.idDispositivo + "&posicion=[E|1|N]" ;
                Console.WriteLine($"            URL Workbeat Employees:: {url}");
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "GET";

                WebResponse response = myRequest.GetResponse();
                Stream strReader = response.GetResponseStream();
                StreamReader objReader = new StreamReader(strReader);
                responseBody = objReader.ReadToEnd().ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                responseBody = "Error:" + ex.Message ;
            }
            return responseBody;
        }

        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors){
            return true;
        }


    }
}

