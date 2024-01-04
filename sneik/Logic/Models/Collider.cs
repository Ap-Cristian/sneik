using Logic.Interfaces;
using Logic.Models;

namespace Logic.Models
{
    public class Collider { 
        public Point Position { get; set; }
        public Size Size { get; set; }
        public Collider(Point position, Size size)
        {
            Position = position;
            Size = size;
        }
    }
}
