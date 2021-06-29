using System;
using System.Net;
using System.IO;

namespace K2_betterware_Biostart_Assistance.Infrastructure.Service
{
    public class AssistanceService
    {
        public string config_biostart()
        {   
            /////////// credenciales biostar /////////////////////////////////////
            string url_bio = "http://10.10.26.55:443/api/login";
            string usr_bio = "consultas";
            string id_bio = "Consulta1#";

            /////////// metodos ///////////////////////////////////////////////
            string usr = "http://10.10.26.55:443/api/users?group_id=1&limit=1&offset=1&order_by=user_id%3Afalse&userId=1&last_modified=10009";
            string serch = "http://10.10.26.55:443/api/events/search";
            string dev = "http://10.10.26.55:443/api/devices?monitoring_permission=false";
            string ev = "http://10.10.26.55:443/api/events/search";

            return url_bio + '*' + usr_bio + '*' + id_bio + '*' + usr + '*' + serch + '*' + dev + '*' + ev;
        }


        ////////////////////////////// metodos biostar /////////////////////////////////////////////////////////////
        public string token_bio()
        {
            string responseBody = "nada";
            string vv = "nada";
            
            string url = config_biostart().Split('*')[0];
            string usr = config_biostart().ToString().Split('*')[1];
            string id = config_biostart().ToString().Split('*')[2];

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Accept", "application/json");

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string jsonb = "{\"User\":{\"login_id\":\"" + usr + "\",\"password\":\"" + id + "\"}}";
                streamWriter.Write(jsonb);
            }
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) ;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            responseBody = objReader.ReadToEnd().ToString();
                            vv = response.Headers.Get(1).ToString();   //obtencion de los headers
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex);
                responseBody = ex.ToString();
            }
            return vv;
        }

        public string user_bio()
        {
            string url = config_biostart().Split('*')[3];
            // string url = "http://10.10.26.55:443/api/users?group_id=1&limit=1&offset=1&order_by=user_id%3Afalse&userId=1&last_modified=10009";
            var myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            myRequest.Headers.Add("bs-session-id", token_bio());
            WebResponse response = myRequest.GetResponse();
            Stream strReader = response.GetResponseStream();
            StreamReader objReader = new StreamReader(strReader);
            string responseBody = objReader.ReadToEnd().ToString();
            return responseBody;
        }

        public string event_search_bio()
        {
            string responseBody = "nada";
            
            string url = config_biostart().Split('*')[4];
            // string url = "http://10.10.26.55:443/api/events/search";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add("bs-session-id", token_bio());
            request.ContentType = "application/json";
            request.Headers.Add("Accept", "application/json");
            // limite, operador, valor fecha-hora //
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string jsonb = "{\"Query\":{\"limit\":51,\"conditions\":[{\"column\":\"datetime\",\"operator\":3,\"values\":[\"2019-07-30T15:00:00.000Z\"]}],\"orders\":[{\"column\":\"datetime\",\"descending\":false}]}}";
                streamWriter.Write(jsonb);
            }
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) ;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            responseBody = objReader.ReadToEnd().ToString();
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


        public string device_bio()
        {
            string responseBody = "nada";
            string vv = "nada";

            string url = config_biostart().Split('*')[5];
         // string url = "http://10.10.26.55:443/api/devices?monitoring_permission=false";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("bs-session-id", token_bio());
            request.ContentType = "application/json";
            request.Headers.Add("Accept", "application/json");
            WebResponse response = request.GetResponse();
            Stream strReader = response.GetResponseStream();
            StreamReader objReader = new StreamReader(strReader);
            responseBody = objReader.ReadToEnd().ToString();
            return responseBody;
        }


        public String bio_event_search(string tk_bio, string jsonb) 
        {
            string responseBody = "nada";
            try
            {   
                string url = config_biostart().Split('*')[6];
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Headers.Add("bs-session-id", tk_bio);
                request.ContentType = "application/json";
                request.Headers.Add("Accept", "application/json");

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(jsonb);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) ;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {   
                            responseBody = objReader.ReadToEnd().ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                responseBody = "Error: " + ex.ToString();
            }
           
            return responseBody;
        }
    }
}
