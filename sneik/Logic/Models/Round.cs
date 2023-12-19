using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    internal class Round
    {
        private static Round instance;

        private Round() { }

        public static Round Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Round();
                }
                return instance;
            }
        }   
    }
}
