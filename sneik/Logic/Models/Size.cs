namespace Logic.Models
{
    public class Size
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Size(int width, int height) { 
            this.Width = width;
            this.Height = height;
        }
    }
}
