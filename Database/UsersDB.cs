using GuessGame.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGame.Database
{
    public class UsersDB
    {
        public bool Add(string nickName, string email, string password, string avatar)
        {
            var db = new DB_Connection();
            //open connection

            try
            {
                db.OpenConnection();
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(null, db.Connection);
                cmd.CommandText = "INSERT INTO users (username, email, password, avatar)" + "VALUES(@userName, @email, @password, @avatar)";

                var userNameParam = new MySqlParameter("@userName", MySqlDbType.VarChar, 45);
                userNameParam.Value = nickName;
                var emailParam = new MySqlParameter("@email", MySqlDbType.VarChar, 45);
                emailParam.Value = email;
                var passwordParam = new MySqlParameter("@password", MySqlDbType.VarChar, 45);
                passwordParam.Value = password;
                var avatarParam = new MySqlParameter("@avatar", MySqlDbType.VarChar, 30);
                avatarParam.Value = avatar;


                cmd.Parameters.Add(userNameParam);
                cmd.Parameters.Add(emailParam);
                cmd.Parameters.Add(passwordParam);
                cmd.Parameters.Add(avatarParam);

                // Call Prepare after setting the Commandtext and Parameters.
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                //close connection
                db.CloseConnection();
                return true;

            }catch(MySqlException)
            {
                return false;
            }                   
                    
        }
        
        public bool Edit(int id, string nickName, string email, string password, string avatar)
        {
            var db = new DB_Connection();
            //open connection

            try
            {
                db.OpenConnection();
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(null, db.Connection);
                
                cmd.CommandText = "UPDATE users  SET username= @userName, email= @email, password= @password, avatar= @avatar WHERE userId= @id";

                var userNameParam = new MySqlParameter("@userName", MySqlDbType.VarChar, 45);
                userNameParam.Value = nickName;
                var emailParam = new MySqlParameter("@email", MySqlDbType.VarChar, 45);
                emailParam.Value = email;
                var passwordParam = new MySqlParameter("@password", MySqlDbType.VarChar, 45);
                passwordParam.Value = password;
                var avatarParam = new MySqlParameter("@avatar", MySqlDbType.VarChar, 30);
                avatarParam.Value = avatar;
                var idParam = new MySqlParameter("@id", MySqlDbType.Int16, 30);
                idParam.Value = id;


                cmd.Parameters.Add(userNameParam);
                cmd.Parameters.Add(emailParam);
                cmd.Parameters.Add(passwordParam);
                cmd.Parameters.Add(avatarParam);
                cmd.Parameters.Add(idParam);

                // Call Prepare after setting the Commandtext and Parameters.
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                //close connection
                db.CloseConnection();
                return true;

            }catch(MySqlException)
            {
                return false;
            }                   
                    
        }

        public void Delete()
        {
            //Not possiable
        }

        public List<UserModel> GetAll(){
            var db = new DB_Connection();
            List<UserModel> result = new List<UserModel>();
            try
            {
                db.OpenConnection();
                string query = "SELECT * FROM users";
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    var user = new UserModel(dataReader);
                    result.Add(user);
                }

                //close Data Reader
                dataReader.Close();
                db.CloseConnection();
                return result;

            }catch(MySqlException)
            {
                return null;
            }

        }

        public Data indexInfo()
        {
            
            int users = 0;
            int games = 0;
            var db = new DB_Connection();
            try
            {
                db.OpenConnection();
                string query = "SELECT COUNT(*) AS count FROM users";
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                users = int.Parse(cmd.ExecuteScalar().ToString());

                query = "SELECT COUNT(*) AS countGames FROM games";
                cmd = new MySqlCommand(query, db.Connection);
                games = int.Parse(cmd.ExecuteScalar().ToString());
                
                //close Data Reader
                db.CloseConnection();
                return new Data(users, games);

            }
            catch (MySqlException)
            {
                return new Data(users, games);
            }
        }

        public UserModel GetUser(int userId)
        {
            var allUsers = GetAll();
            foreach (var item in allUsers)
            {
                if (userId == item.UserId) return item;
            }
            return null;
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

                cmd.CommandText = "DELETE FROM users WHERE userId = @id";


                var idParam = new MySqlParameter("@id", MySqlDbType.Int32, 11)
                {
                    Value = id
                };
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



    }


}
