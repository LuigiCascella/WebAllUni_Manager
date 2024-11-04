using Microsoft.Data.SqlClient;
using System.Data;

namespace ClassLibrary
{

    public class DBManagerService
    {

        public readonly SqlConnection _connection = new(); // SQL Connection
        public SqlCommand _command = new(); // SQL exe Command

        public readonly bool IsDBOnline = false; // DB Status

        #region "Constructor"

        public DBManagerService(string ConnectionString) // Costruttore
        {

            try
            {

                _connection.ConnectionString = ConnectionString;
                _connection.Open();

                IsDBOnline = true;

            }
            catch (Exception ex)
            {

                Console.WriteLine("Connection failed: " + ex.Message);
                IsDBOnline = false;

            }
            finally
            {

                if (_connection.State == ConnectionState.Open)
                    _connection.Close();

            }

        }

        #endregion

        #region "Check DB Methods"

        public void CheckOpenedDB() // Controllo Connessione
        {

            if (_connection.State == ConnectionState.Closed) // Controllo
            {

                _connection.Open(); // Connessione

            }

        }

        public void CheckClosedDB(SqlDataReader? dataReader) // Controllo Chiusura
        {

            dataReader?.Close(); // Chiusura

            if (_connection.State == ConnectionState.Open) // Controllo
            {

                _connection.Close(); // Chiusura

            }

        }

        #endregion

    }

}
