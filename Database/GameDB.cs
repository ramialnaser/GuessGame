using GuessGame.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGame.Database
{
    public class GameDB
    {
        public int Add(string userid, string text)
        {
            var db = new DB_Connection();

            try
            {
                db.OpenConnection();
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(null, db.Connection);
                cmd.CommandText = "INSERT INTO games (userid, drawText)" + "VALUES(@userid, @drawText)";

                var userIdParam = new MySqlParameter("@userid", MySqlDbType.Int16, 45);
                userIdParam.Value = userid;
                var textParam = new MySqlParameter("@drawText", MySqlDbType.VarChar, 45);
                textParam.Value = text;
                


                cmd.Parameters.Add(userIdParam);
                cmd.Parameters.Add(textParam);
                
                // Call Prepare after setting the Commandtext and Parameters.
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                int gameId = (int)cmd.LastInsertedId;

                //close connection
                db.CloseConnection();
                return gameId;

            }
            catch (MySqlException)
            {
                return -1;
            }

        }



        public string Update(string id, string img)
        {
            var db = new DB_Connection();
            //open connection

            try
            {
                db.OpenConnection();
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(null, db.Connection);

                cmd.CommandText = "UPDATE games SET active= 0, img= @img WHERE gameid = @id";

                
                var imgParam = new MySqlParameter("@img", MySqlDbType.LongText, img.Length);
                imgParam.Value = img;
                               
                var idParam = new MySqlParameter("@id", MySqlDbType.Int32, 11);
                idParam.Value = id;

                cmd.Parameters.Add(imgParam);
                cmd.Parameters.Add(idParam);
                
                

                // Call Prepare after setting the Commandtext and Parameters.
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                //close connection
                db.CloseConnection();
                return "true";

            }
            catch (MySqlException ex)
            {
                return ex.ToString();
            }
        }
        
        
        public string Delete(int id)
        {
            var db = new DB_Connection();
            //open connection

            try
            {
                db.OpenConnection();
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(null, db.Connection);

                cmd.CommandText = "DELETE FROM games WHERE gameid = @id";                
                
                               
                var idParam = new MySqlParameter("@id", MySqlDbType.Int32, 11);
                idParam.Value = id;
                cmd.Parameters.Add(idParam);
                
                

                // Call Prepare after setting the Commandtext and Parameters.
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                //close connection
                db.CloseConnection();
                return "true";

            }
            catch (MySqlException ex)
            {
                return ex.ToString();
            }
        }
        
        
        
        
        public string UpdateCorrectGuess(string id, string guess)
        {
            var game = GetGame(int.Parse(id));
            if(game.DrawText == guess)
            {
                var db = new DB_Connection();
                //open connection

                try
                {
                    db.OpenConnection();
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(null, db.Connection);

                    cmd.CommandText = "UPDATE games SET correctGuess= correctGuess + 1 WHERE gameid = @id";


                    var idParam = new MySqlParameter("@id", MySqlDbType.Int32, 11);
                    idParam.Value = id;

                    cmd.Parameters.Add(idParam);



                    // Call Prepare after setting the Commandtext and Parameters.
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                    //close connection
                    db.CloseConnection();
                    return "true";

                }
                catch (MySqlException ex)
                {
                    return ex.ToString();
                }
            }
            return "";
            
        }

        public List<GameModel> GetAll()
        {
            var db = new DB_Connection();
            List<GameModel> result = new List<GameModel>();
            try
            {
                db.OpenConnection();
                string query = "SELECT * FROM games";
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    var game = new GameModel(dataReader);
                    result.Add(game);
                }

                //close Data Reader
                dataReader.Close();
                db.CloseConnection();
                return result;

            }
            catch (MySqlException)
            {
                return null;
            }

        }

        public GameModel GetGame(int gameid)
        {
            var allGames = GetAll();
            foreach (var item in allGames)
            {
                if (gameid == item.Gameid) return item;
            }
            return null;
        }

        public int GetGameId()
        {
            var allGames = GetAll();
            foreach (var item in allGames)
            {
                if (item.Active) 
                    return item.Gameid;
            }
            return 0;
        }


        public Data indexInfo()
        {

            int activeGames = 0;
            var db = new DB_Connection();
            try
            {
                db.OpenConnection();
                string query = "SELECT COUNT(*) AS count FROM games WHERE active = 1";
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                activeGames = int.Parse(cmd.ExecuteScalar().ToString());

                

                //close Data Reader
                db.CloseConnection();
                return new Data(activeGames, 0);

            }
            catch (MySqlException)
            {
                return new Data(0,0);
            }
        }

    }
}
