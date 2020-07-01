using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGame.Models
{
    public class GameModel
    {
        int gameid;
        int userid;
        string drawText;
        bool active;
        string img;
        int correctGuess;


        public GameModel(MySqlDataReader record)
        {
            this.Gameid = (int)record["gameid"];
            this.Userid = (int)record["userid"];
            this.DrawText = (string)record["drawText"];
            this.Active = (bool)record["active"];
            this.img = Convert.IsDBNull(record["img"]) ? " " : (string)record["img"];
            this.correctGuess = (int)record["correctGuess"];


        }

        public int Gameid { get => gameid; set => gameid = value; }
        public int Userid { get => userid; set => userid = value; }
        public string DrawText { get => drawText; set => drawText = value; }
        public bool Active { get => active; set => active = value; }
        public string Img { get => img; set => img = value; }
        public int CorrectGuess { get => correctGuess; set => correctGuess = value; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
            //return DrawText;
        }

        public static List<GameModel> getGame()
        {
            var games = new Database.GameDB();
            var result = games.GetAll();
            
            return result;
        }

    }
}
