using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessGame.Models
{
    public class Data
    {
        int elem1;
        int elem2;
        

        public Data(int users, int games)
        {
            this.Elem1 = users;
            this.Elem2 = games;
        }

        public int Elem1 { get => elem1; set => elem1 = value; }
        public int Elem2 { get => elem2; set => elem2 = value; }
    }
}
