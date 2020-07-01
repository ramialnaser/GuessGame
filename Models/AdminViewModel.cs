using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGame.Models
{
    public class AdminViewModel
    {
        List<UserModel> users;
        List<GameModel> games;

        public List<UserModel> Users { get => users; set => users = value; }
        public List<GameModel> Games { get => games; set => games = value; }
    }
}
