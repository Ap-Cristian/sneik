namespace Logic.Models
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Point Add(int value)
        {
            this.X = this.X + value;
            this.Y = this.Y + value;
            return this;
        }
        public Point Add(Point value)
        {
            this.X = this.X + value.X;
            this.Y = this.Y + value.Y;
            return this;
        }
        public Point Add(Size size)
        {
            this.X = this.X + size.Width;
            this.Y = this.Y + size.Height;
            return this;
        }
    }
}
