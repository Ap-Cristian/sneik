using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Attributes
{
    internal class Collider
    {
        public Point Position { get; set; }
        public Size Size { get; set; }
        public Collider(Point position, Size size)
        {
            Position = position;
            Size = size;
        }
    }
}
