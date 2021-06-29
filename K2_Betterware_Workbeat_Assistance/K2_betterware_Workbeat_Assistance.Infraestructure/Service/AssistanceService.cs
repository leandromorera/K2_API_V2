using K2_Betterware_Workbeat_Assistance.Core.DTOs;
using K2_Betterware_Workbeat_Assistance.Infraestructure.ExternaService;
using System;
using System.Net;
using System.Text.Json;
using System.IO;


namespace K2_Betterware_Workbeat_Assistance.Infraestructure.Service
{
    public class AssistanceService
    {
        public string config_workbeat()
        {
            //////////// credenciales workbeat ///////////////////////////////////
            string dir_tok_workbeat = "https://api.workbeat.com/oauth/token";
            string cli_cred = "7E800F92-B08E-4A12-9C5C-EFB03E170301";
            string cli_sec = "4088828B-9601-400F-BDDC-EA73818BA4C4";

            /////////// metodos ///////////////////////////////////////////////
            string url_generic = "https://api.workbeat.com";
            string url_checada = "https://api.workbeat.com/v2/asi/checada?id=";

            return dir_tok_workbeat + '_' + cli_cred + '_' + cli_sec + '_' + url_generic + '_' + url_checada;
        }


        public string getToken()
        {
            
            string srcv = config_workbeat().ToString().Split('_')[0];
            string cli = config_workbeat().ToString().Split('_')[1];
            string sec = config_workbeat().ToString().Split('_')[2];

            string meth = "POST";
            //string usr_access = "grant_type=client_credentials&client_id=7E800F92-B08E-4A12-9C5C-EFB03E170301&client_secret=4088828B-9601-400F-BDDC-EA73818BA4C4";
            string usr_access = "grant_type=client_credentials&client_id=" + cli + "&client_secret=" + sec;
            ////////////////////////////////////////////////////////////////////////////////
            string cont_type = "application/x-www-form-urlencoded";
            string respuesta_tok = WorkBeatConnection.posting(srcv, usr_access, meth, cont_type);
            //////////////////////////////// descerializando cadenas json /////////////////////////////////
            Token from_js = JsonSerializer.Deserialize<Token>(respuesta_tok);
            Console.WriteLine(from_js.access_token);
            ////////////////////////////////////////////////////////////////////////////////////////////////
            return from_js.access_token;
        }


        public string giveme_workbeat_generic(string servapi)
        {
            string generic = config_workbeat().ToString().Split('_')[3];

            //var myRequest = (HttpWebRequest)WebRequest.Create("https://api.workbeat.com" + servapi + "?access_token=" + getToken());
            var myRequest = (HttpWebRequest)WebRequest.Create(generic + servapi + "?access_token=" + getToken());
            myRequest.Method = "GET";
            WebResponse response = myRequest.GetResponse();
            Stream strReader = response.GetResponseStream();
            StreamReader objReader = new StreamReader(strReader);
            string responseBody = objReader.ReadToEnd();
            return responseBody;
        }


        public string giveme_workbeat_empleados(string servapi)
        {
            string generic = config_workbeat().ToString().Split('_')[3];

            var myRequest = (HttpWebRequest)WebRequest.Create(generic + servapi + "?access_token=" + getToken());
            myRequest.Method = "GET";
            WebResponse response = myRequest.GetResponse();
            Stream strReader = response.GetResponseStream();
            StreamReader objReader = new StreamReader(strReader);
            string responseBody = objReader.ReadToEnd();
            return responseBody;
        }


        public string giveme_workbeat_persona(string servapi, string npr)
        {
            string generic = config_workbeat().ToString().Split('_')[3];

            //var myRequest = (HttpWebRequest)WebRequest.Create("https://api.workbeat.com" + servapi + npr + "?access_token=" + getToken());
            var myRequest = (HttpWebRequest)WebRequest.Create(generic + servapi + npr + "?access_token=" + getToken());
            myRequest.Method = "GET";
            WebResponse response = myRequest.GetResponse();
            Stream strReader = response.GetResponseStream();
            StreamReader objReader = new StreamReader(strReader);
            string responseBody = objReader.ReadToEnd();
            return responseBody;
        }

        public string Checando(string p_id, string fechahora, string dispositivoId, string posi)
        {
            string token = getToken();
            string check = config_workbeat().ToString().Split('_')[4];
            //string direccion = "https: //api.workbeat.com/v2/asi/checada?id=8251&fechahora=2019-11-05T09:04:08&dispositivoId=11948&dirección=[E|1|N]";
            //string direccion = "https://api.workbeat.com/v2/asi/checada?id=" + p_id + "&fechahora=" + fechahora + "&dispositivoId=" + dispositivoId + "&dirección=" + posi;
            string direccion = check + p_id + "&fechahora=" + fechahora + "&dispositivoId=" + dispositivoId + "&dirección=" + posi;
            string responseBody = "nada";
            var url = direccion;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Bearer " + token);
            request.Headers.Add("Accept", "application/json");
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) ;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            Console.WriteLine(responseBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                responseBody = ex.ToString();
            }
            return responseBody;
        }

    }
}
