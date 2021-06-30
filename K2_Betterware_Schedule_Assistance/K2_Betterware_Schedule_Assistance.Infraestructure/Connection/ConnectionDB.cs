using System.Data.SQLite;
using System.IO;

namespace K2_Betterware_Schedule_Assistance.Infraestructure.Connection
{
    public class ConnectionDB
    {   
        public SQLiteConnection conn;
        private string processTable = "CREATE TABLE process (idProceso text, fechaInicio text, fechaFin text, tipoEvento varchar(20), status varchar(50));";
        private string eventTable = "CREATE TABLE events (idEmpleado varchar(15), idDispositivo varchar(15), fechaEvento varchar(15), nip varchar(15) , error varchar(70),idProceso text);";
        private string processConfTable = "CREATE TABLE processConfig (trigger text, active boolean, reprocess boolean, insertAssistance boolean );";
        private string insertConfTable = "INSERT INTO processConfig ('trigger', 'active' , 'reprocess' , 'insertAssistance' ) VALUES ('0/35 * * * * ?' , 1 , 0 , 1); ";

        public ConnectionDB() {
            conn = new SQLiteConnection("Data Source=assistance_events.sqlite3");
            if (!File.Exists("./assistance_events.sqlite3"))
            {
                SQLiteConnection.CreateFile("assistance_events.sqlite3");
                SQLiteCommand command = new SQLiteCommand(this.eventTable, this.conn);
                this.OpenConnection();
                command.ExecuteNonQuery();
                command = new SQLiteCommand(this.processTable, this.conn);
                command.ExecuteNonQuery();

                command = new SQLiteCommand(this.processConfTable, this.conn);
                command.ExecuteNonQuery();

                command = new SQLiteCommand(this.insertConfTable, this.conn);
                command.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        public void OpenConnection() {
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
        }

        public void CloseConnection()
        {
            if (conn.State != System.Data.ConnectionState.Closed) conn.Close();
        }
    }
}
