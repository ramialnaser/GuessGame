using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGame.Database
{
    public class DB_Connection
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public MySqlConnection Connection { get => connection; set => connection = value; }

        //Constructor
        public DB_Connection()
        {
            server = "localhost";
            database = "GuessGame";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            Connection = new MySqlConnection(connectionString);
        }
        
        //open connection to database
        public void OpenConnection()
        {
            
                Connection.Open();
                
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
               
        }
        

        //Close connection
        public void CloseConnection()
        {
            Connection.Close();
            
        }



    }
}
