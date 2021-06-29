using K2_Betterware_Assistance.Core.Dtos;
using K2_Betterware_Schedule_Assistance.Core.Dtos;
using K2_Betterware_Schedule_Assistance.Infraestructure.Connection;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace K2_Betterware_Schedule_Assistance.Infraestructure.Services {
    public class LogsAssistanceService{
        private ConnectionDB db;

        public void openEvent(){
            this.db = new ConnectionDB();
            this.db.OpenConnection();
        }

        public void closeEvent(){
            this.db.CloseConnection();
        }

        public int saveEvent(Proceso proceso, Evento evento) {
            string query = "INSERT INTO events ('idEmpleado', 'idDispositivo', 'fechaEvento', 'nip', 'error', 'idProceso') VALUES (@idEmpleado, @idDispositivo, @fechaEvento, @nip, @error, @idProceso)";
            SQLiteCommand command = new SQLiteCommand(query,this.db.conn);
            
            command.Parameters.AddWithValue("@idEmpleado", evento.idEmpleado);
            command.Parameters.AddWithValue("@idDispositivo", evento.idDispositivo);
            command.Parameters.AddWithValue("@fechaEvento", evento.fechaEvento);
            command.Parameters.AddWithValue("@nip", evento.nip);
            command.Parameters.AddWithValue("@error", evento.error);
            command.Parameters.AddWithValue("@idProceso", proceso.idProceso);

            int result = command.ExecuteNonQuery();
            Console.WriteLine($"        Insert events :: {result}");

            return result;
        }

        public int updateEvent(Evento evento){
            string query = "UPDATE events set nip = @nip WHERE idEmpleado = @idEmpleado ";
            SQLiteCommand command = new SQLiteCommand(query, this.db.conn);
            
            command.Parameters.AddWithValue("@nip", evento.nip);
            command.Parameters.AddWithValue("@idEmpleado", evento.idEmpleado);
            int result = command.ExecuteNonQuery();
            Console.WriteLine($"        Uodate events :: {result}");

            return result;
        }

        public int deleteEvents()
        {
            string query = "DELETE FROM events;";
            SQLiteCommand command = new SQLiteCommand(query, this.db.conn);

            int result = command.ExecuteNonQuery();
            Console.WriteLine($"        Truncate events :: {result}");

            return result;
        }

        public List<Evento> getEvents(Evento evento){
            Evento eventoR;
            List<Evento> results = new List<Evento>();
            string query = "SELECT * FROM events ORDER by fechaEvento ; ";
            SQLiteCommand command = new SQLiteCommand(query, this.db.conn);
            SQLiteDataReader r = command.ExecuteReader();
            while (r.Read()) {
                eventoR = new Evento();
                eventoR.idEmpleado = (string)r["idEmpleado"];
                eventoR.idDispositivo = (string)r["idDispositivo"];
                eventoR.fechaEvento = (string)r["fechaEvento"];
                eventoR.nip = (string)r["nip"];
                eventoR.error = (string)r["error"];
                results.Add(eventoR);
            }
            return results;
        }


        public int saveProcess(Proceso proceso){
            
            string query = "INSERT INTO process ('idProceso', 'fechaInicio', 'fechaFin', 'tipoEvento', 'status') VALUES (@idProceso, @fechaInicio, @fechaFin, @tipoEvento, @status)";
            SQLiteCommand command = new SQLiteCommand(query, this.db.conn);

            command.Parameters.AddWithValue("@idProceso", proceso.idProceso);
            command.Parameters.AddWithValue("@fechaInicio", proceso.fechaInicio);
            command.Parameters.AddWithValue("@fechaFin", proceso.fechaFin);
            command.Parameters.AddWithValue("@tipoEvento", proceso.tipoEvento);
            command.Parameters.AddWithValue("@status", proceso.status);
 
            int result = command.ExecuteNonQuery();
            Console.WriteLine($"        Insert process :: {result}");

            return result;
        }

        public int updateProcess(Proceso proceso){
            string query = "UPDATE process set  status =  @status  Where idProceso = @idProceso";
            SQLiteCommand command = new SQLiteCommand(query, this.db.conn);
            command.Parameters.AddWithValue("@idProceso", proceso.idProceso);
            command.Parameters.AddWithValue("@status", proceso.status);
            int result = command.ExecuteNonQuery();
            Console.WriteLine($"        Update process :: {result}");
            return result;
        }

        public int deleteProcess()
        {
            string query = "DELETE FROM process; ";
            SQLiteCommand command = new SQLiteCommand(query, this.db.conn);

            int result = command.ExecuteNonQuery();
            Console.WriteLine($"        Truncate process :: {result}");

            return result;
        }

        public List<Proceso> getLastProcess(Proceso proceso) {
            Proceso procesoR;
            List<Proceso> results = new List<Proceso>();
            try {   
                string query = "SELECT * FROM process ORDER by fechaFin DESC ;";
                SQLiteCommand command = new SQLiteCommand(query, this.db.conn);
                SQLiteDataReader r = command.ExecuteReader();
                while (r.Read()) {   
                    procesoR = new Proceso();
                    procesoR.idProceso = (string)r["idProceso"];
                    procesoR.fechaInicio = (string)r["fechaInicio"];
                    procesoR.fechaFin = (string)r["fechaFin"];
                    procesoR.tipoEvento = (string)r["tipoEvento"];
                    procesoR.status = (string)r["status"];
                    results.Add(procesoR);
                }
            } catch (Exception ex) {
                Console.WriteLine($"  Error: {ex.Message} ");
            }
            return results;
        }


        public int updateConfigProcess(ConfigProcess configProcess)
        {
            string query = "UPDATE processConfig set trigger = @trigger, active = @active , reprocess= @reprocess , insertAssistance = @insertAssistance  ";

            SQLiteCommand command = new SQLiteCommand(query, this.db.conn);
            command.Parameters.AddWithValue("@trigger", configProcess.trigger);
            command.Parameters.AddWithValue("@active", configProcess.active);
            command.Parameters.AddWithValue("@reprocess", configProcess.reprocess);
            command.Parameters.AddWithValue("@insertAssistance", configProcess.insertAssistance);

            int result = command.ExecuteNonQuery();
            Console.WriteLine($"        Update updateCongigProcess :: {result}");
            return result;
        }

        public List<ConfigProcess> getConfigProcess()
        {
            ConfigProcess procesoR;
            List<ConfigProcess> results = new List<ConfigProcess>();
            try
            {
                string query = "SELECT * FROM processConfig;";
                SQLiteCommand command = new SQLiteCommand(query, this.db.conn);
                SQLiteDataReader r = command.ExecuteReader();
                while (r.Read())
                {
                    procesoR = new ConfigProcess();
                    procesoR.trigger = (string)r["trigger"];
                    procesoR.active = (Boolean)r["active"];
                    procesoR.reprocess = (Boolean)r["reprocess"];
                    procesoR.insertAssistance = (Boolean)r["insertAssistance"];
                    results.Add(procesoR);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  Error: {ex.Message} ");
            }
            return results;
        }
    }
}
