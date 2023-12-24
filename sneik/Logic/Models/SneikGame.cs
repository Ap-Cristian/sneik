using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class SneikGame
    {
        private static SneikGame instance;

        private SneikGame() { }

        public static SneikGame Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SneikGame();
                }
                return instance;
            }
        }


    }
}
