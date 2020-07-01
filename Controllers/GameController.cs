using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuessGame.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuessGame.Controllers
{
    [Route("api/Games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        // GET: api/Games
        [HttpGet]
        public string GetGames()
        {
            var games = new GameDB();
            var result = games.GetAll();
            var listResult = new List<string>();
            //Convert array to string
            foreach (var item in result)
            {
                listResult.Add(item.ToString());
            }
            return string.Join(",", listResult.ToArray());
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public string GetGame(int id)
        {
            var games = new GameDB();
            var result = games.GetGame(id);
            return result == null ? "{The requested item doesn't exist in database}" : result.ToString();
        }




        // PUT: api/Games/5
        [HttpPost]
        public int Put(IFormCollection value)
        {
            var userid = value["userid"];
            var text = value["drawText"];
            var actGame = new GameDB();
            var gameId = actGame.Add(userid, text);
            return gameId;

        }


        [HttpPut]
        public string UpdateGame(IFormCollection value)
        {
            var id = value["gameid"];
            var img = value["img"];
            var game = new GameDB();
            var result = game.Update(id, img);
            return result;
        }


        [HttpPut("{id}/{guess}")]
        public string UpdateCorrectGuess(string id, string guess)
        {
            var game = new GameDB();
            var result = game.UpdateCorrectGuess(id, guess);
            return result;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("Delete/{id}")]
        public string Delete(int id)
        {
            return new GameDB().Delete(id);
        }
    }
}
