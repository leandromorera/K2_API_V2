using K2_Betterware_Assistance.Core.Interfaces;
using K2_Betterware_Assistance.Infraestructure.Repositories;
using K2_Betterware_Assistance.Core.Dtos;
using Quartz;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace K2_Betterware_Assistance.Scheduler.Jobs
{
    public class AssistanceJob : IJob
	{
	public async Task Execute(IJobExecutionContext context) {
			await Console.Out.WriteLineAsync("/////////////////////////////////////////////////////////////.");
			await Console.Out.WriteLineAsync("Incio de Job Scheduler Assistance Betterware.");

			IRepository _repository = new Repository();
			Utils util = new Utils();
			_repository.openEvent();

// 1.- Establecer el rango de Fecha para obtener Eventos del Biometricos.
			Proceso proceso = util.getProcessDates(_repository);
			_repository.saveProcess(proceso);
			
//2.- Obtener los registros de Eventos de Biostart.
			var jsonBiostartEvents = _repository.getBiostartEvents(proceso);

//3.- Insertar el registro de Asistencia en Workbeat.
			util.insertarAssistance(_repository, proceso, jsonBiostartEvents);

			_repository.closeEvent();
			await Console.Out.WriteLineAsync("Fin de Proceso Scheduler Assistance Betterware.");
			await Console.Out.WriteLineAsync("/////////////////////////////////////////////////////////////.");
		}
	}
}
