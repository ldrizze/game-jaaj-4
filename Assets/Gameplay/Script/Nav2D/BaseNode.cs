namespace Nav2D
{
    public class BaseNode
    {
        public BaseNode cameFrom = null;
        public int x = 0;
        public int y = 0;
        public bool isWalkable = true;

        public BaseNode()
        {

        }
    }
}