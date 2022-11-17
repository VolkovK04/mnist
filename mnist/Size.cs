namespace mnist
{
    public struct Size
    {
        public Size(int weidth, int height)
        {
            Width = weidth;
            Height = height;
        }
        public int Width { get; set; }
        public int Height { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                return false;
            else
            {
                Size size = (Size)obj;
                return (Width == size.Width) && (Height == size.Height);
            }
        }
        public override int GetHashCode()
        {
            return Width^Height;
        }
        public static bool operator ==(Size s1, Size s2)
        {
            return s1.Equals(s2);
        }
        public static bool operator !=(Size s1, Size s2)
        {
            return !s1.Equals(s2);
        }
    }
}
