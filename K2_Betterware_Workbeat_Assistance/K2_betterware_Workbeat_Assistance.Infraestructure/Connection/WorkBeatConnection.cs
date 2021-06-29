using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;


namespace K2_Betterware_Workbeat_Assistance.Infraestructure.ExternaService
{
    class WorkBeatConnection
    {

        static public string posting(string service, string parammeters, string petition_type, string cont_type)
        {
            WebRequest request = WebRequest.Create(service);
            string postData = parammeters;
            request.Method = petition_type;
            string stresponse = process_send_data(postData, request, cont_type);
            return stresponse;
        }

        /// solicitud
        static public string process_send_data(string Data, WebRequest request, string cont_type)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(Data);
            request.ContentType = cont_type;
            request.ContentLength = byteArray.Length;
            //request.ContentType = new System.Net.Http.StringContent("{}", Encoding.UTF8, cont_type).ToString();
            System.IO.Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine(responseFromServer);
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
    }
}
