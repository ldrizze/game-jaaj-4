namespace Nav2D{
    class ANode : BaseNode
    {
        public int g = int.MaxValue;
        public int h = 0;

        public int F
        {
            get { return g + h; }
        }

        public ANode() : base()
        {
            
        }
    }
}