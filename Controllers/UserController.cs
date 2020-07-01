using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GuessGame.Database;
using GuessGame.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;


namespace GuessGame.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/Users
        [HttpGet]
        public string GetUsers()
        {
            var users = new UsersDB();
            var result = users.GetAll();
            var listResult = new List<string>();
            //Convert array to string
            foreach (var item in result)
            {
                listResult.Add(item.ToString());
            }
            return string.Join(",",listResult.ToArray());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public string GetUser(int id)
        {
            var users = new UsersDB();
            var result = users.GetUser(id);
            return result == null ? "{The requested item doesn't exist in database}" : result.ToString();
        }

        // POST: api/Users-
        [HttpPost]
        public string AddUser(IFormCollection value)
        {
            var email = value["email"];
            var nickName = value["nickName"];
            var password = value["password"];
            var avatar = value["avatar"];

            var users = new UsersDB();
            var result = users.Add(nickName, email, password, avatar);

            return "New Users Added succesffully";
        }
        
        

        [HttpPut("{id}", Name = "PUT")]
        public string EditUser(IFormCollection value, int id)
        {
            var email = value["email"];
            var nickName = value["nickName"];
            var password = value["password"];
            var avatar = value["avatar"];

            var users = new UsersDB();
            users.Edit(id, nickName, email, password, avatar);
            var user = new UserModel(id, nickName, email, password, avatar);
            return user.ToString();
        }

        // PUT: api/User/5
        [HttpPut]
        public string AuthUser(IFormCollection value)
        {
            var email = value["signInEmail"];
            var password = value["signInPassword"];
            var db = new DB_Connection();
            try
            {
                db.OpenConnection();
                string query = $"SELECT * FROM users WHERE email = '{email}' AND password = '{password}'";
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        var user = new UserModel(dataReader);
                        dataReader.Close();
                        db.CloseConnection();
                        return user.ToString();
                    }
                }
                return "The User email or pass not exist or doesn't match";


            }
            catch (MySqlException)
            {
                return "The User email or pass not exist or doesn't match";
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("Delete/{id}")]
        public string Delete(int id)
        {
            Debug.WriteLine("Deleted user: " + id);
            return new UsersDB().Delete(id);
        }




    }
}

