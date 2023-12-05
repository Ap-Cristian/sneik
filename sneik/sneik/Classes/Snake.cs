using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sneik.Classes
{
    public class Snake
    {
        private int size;

        public Snake(int size)
        {
            this.size = size;
        }

        public int GetSize()
        {
            return size;
        }

        public void SetSize(int size)
        {
            this.size = size;
        }

        public void IncreaseSize(int size)
        {
            this.size += size;
        }
    }
}
