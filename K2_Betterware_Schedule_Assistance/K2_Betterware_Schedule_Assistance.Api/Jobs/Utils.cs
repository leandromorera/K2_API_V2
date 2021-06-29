using K2_Betterware_Assistance.Core.Dtos;
using System;
using Newtonsoft.Json.Linq;
using System.IO;
using K2_Betterware_Assistance.Core.Interfaces;
using K2_Betterware_Assistance.Infraestructure.Repositories;
using System.Collections.Generic;

namespace K2_Betterware_Assistance.Scheduler.Jobs
{

    public class Utils 
	{
		

		public Proceso getProcessDates(IRepository _repository) {
			Proceso proceso = new Proceso();
			
			List<Proceso> lstProcesos = _repository.getLastProcess(proceso);
			Console.WriteLine($"  getProcessDates Obtener de BD {lstProcesos.Count} ");
			if (lstProcesos != null && lstProcesos.Count > 0) {
				//1.- Si existe registros de procesos tomar la fecha del ultimo procesos.
				proceso.fechaInicio = lstProcesos[0].fechaFin;
				proceso.fechaFin = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
			}
			else {
				//2.- En caso de no existir registros tomar la fecha del dia actual del proceso desde inicios del dia.
				proceso.fechaInicio = DateTime.Now.ToString("yyyy-MM-ddT05:00:00");
				proceso.fechaFin = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
			}

			proceso.tipoEvento = "4867";
			proceso.idProceso = "Assistance_" + proceso.fechaInicio;
			proceso.status = "0";
				
			Console.WriteLine($"  Fecha Inicio: {proceso.fechaInicio}");
			Console.WriteLine($"  Fecha Fin: {proceso.fechaFin}");
			Console.WriteLine($"  Tipo Evento: {proceso.tipoEvento}");

			return proceso;
		}



		public void insertarAssistance(IRepository _repository, Proceso proceso, string jsonBiostartEvents)
		{
			

			if (!jsonBiostartEvents.Contains("Error"))
			{
				JObject joResponse = JObject.Parse(jsonBiostartEvents);
				JObject result = (JObject)joResponse["EventCollection"];
				if (typeof(JArray).IsInstanceOfType(result["rows"]))
				{
					
					JArray array = (JArray)result["rows"];
					foreach (JObject j in array)
					{

						Evento evento = this.getPropiedades(j);
						try
						{
							//Realizar la consulta de datos del empleado para obtener su NIP
							var jsonEmployees = _repository.getWorkbeatEmployees(evento);
							if (!jsonEmployees.Contains("Error"))
							{
								String nip = this.searchNip(jsonEmployees, evento);
								if (!nip.Equals(""))
								{
									evento.nip = nip;

									//Realizar el registro de asistencia en Workbeat y almacenar los registros que no se pudieron registrar.
									//String response = _repository.putWorkbeatAssistance(evento);
									//if (response.Contains("Error")){
									// Rutina de manejo de error similar al punto 7
									//}
								}
								else
								{
									//6.- Si no se puede encuentra el NIP del Empleado se guarda para un futuro procesamiento. 
									evento.error = "NIP no Localizado";
									_repository.saveEvent(proceso, evento);
								}
							}
							else
							{
								//6.- Si no se puede encuentra el NIP del Empleado se guarda para un futuro procesamiento. 
								evento.error = jsonEmployees;
								_repository.saveEvent(proceso, evento);
							}
						}
						catch (Exception ex)
						{
							Console.WriteLine($"            Error Servicio AssistanceJob " + ex.Message);
							evento.error = "Excepcion en procesamiento de empleado.";
							_repository.saveEvent(proceso, evento);
						}
					}
					
				}
				else
				{
					Console.WriteLine($"            Sin Resultados de Eventos Biometricos ");
				}
			}
		}


		public Evento getPropiedades(JObject j) {
			Evento evento = new Evento();
			foreach (var property in j.Properties()) {
				//Console.WriteLine($"         Name: {property.Name}  Value: {property.Value}");
				if (property.Name.Equals("datetime")) {
					evento.fechaEvento = (string)property.Value;
				}else if (property.Name.Equals("user_id")){
					JObject user = (JObject)property.Value;
					evento.idEmpleado = (string)user["user_id"];
				}else if (property.Name.Equals("device_id")){
					JObject device = (JObject)property.Value;
					evento.idDispositivo = (string)device["id"];
				}
			}
			Console.WriteLine($"            idEmpleado: {evento.idEmpleado} idDispositivo: {evento.idDispositivo}   fechaEvento: {evento.fechaEvento} ");
			return evento;
		}

		public String searchNip(String jsonData, Evento evento)
		{
			String nip = "";
			try{
				JObject o = JObject.Parse("{'data': " + jsonData + "}");
				JToken acme = o.SelectToken("$.data[?(@.NumeroEmpleado == '0" + evento.idEmpleado + "')]");
				if (acme != null) nip = (String)acme["Generales"][0]["NIP"];
				Console.WriteLine("            NIP Empleado:" + nip);
			}catch (Exception ex){
				Console.WriteLine(ex);
			}

			return nip;
		}

	}
}
