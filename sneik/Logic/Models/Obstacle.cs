using Logic.Attributes;

namespace Logic.Models
{
    public class Obstacle : Collidable
    {
        public Cell Cell { get; set; }
        public Obstacle(Point position, Size size, Color color = Color.TEA_GREEN) 
            : base(new Cell(position, size, Color.TRANSPARENT))
        {
            this.Cell = new Cell(position, size, color);
        }

    }
}
