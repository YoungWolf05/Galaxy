namespace MathOperations
{
    public class Node
    {
        public string data;
        public Node left, right;
        public Node(string data)
        {
            this.data = data;
            left = right = null;
        }
    }
}
