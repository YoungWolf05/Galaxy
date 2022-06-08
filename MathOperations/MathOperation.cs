using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace MathOperations
{
    public static class MathOperation
    {
        /// <summary>
        /// Post process the input expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>processed expression</returns>
        public static string PostFix(string expression)
        {
            string trim = expression.Replace(" ", "").Replace("--", "+"); // trim empty space and convert -- to + 
            string output = string.Empty;
            Stack<string> st = new Stack<string>();
            // use regex to identify positive/negative number and operator +-*/
            List<string> operands = Regex.Split(trim, @"((?<=\d)[+*\/-]|[()]|\-?[\d.]+)").ToList().Where(x => x != "").ToList();

            foreach (string x in operands)
            {
                if (isOperator(x) || x == "(") //push operators and ( to stack 
                    st.Push(x);
                else if (x == ")") // pop until ( to output and remove ( from stack
                {
                    while (st.Peek() != "(")
                        output += st.Pop();
                    st.Pop();
                }
                else
                {   // enclose every number with bracket () to output
                    if (int.TryParse(x, out int result))
                        output += $"({x})";
                    else
                        output += x;
                }
            }
            while (st.Count != 0) // add remaining stack to output
                output += st.Pop();
            return output;
        }

        /// <summary>
        /// Create binary tree
        /// </summary>
        /// <param name="postfix"></param>
        /// <returns>binary tree nodes</returns>
        public static Node ExpressionTree(string postfix)
        {
            Stack<Node> st = new Stack<Node>();
            Node temp;
            string number = string.Empty;
            bool record = false;
            foreach (char x in postfix)
            {
                if (isOperator(x) && !record) // Operator as root, first pop to the right, then second pop to the left
                {
                    temp = new Node(x.ToString());
                    temp.right = st.Pop();
                    if (st.Count > 0)
                        temp.left = st.Pop();
                    st.Push(temp);
                }
                else if (x == '(') //start record number string eg. (15)
                    record = true;
                else if (x == ')') //end record number string 
                {
                    record = false;
                    temp = new Node(number);
                    st.Push(temp);
                    number = String.Empty;
                }
                else if (record) //record number string
                    number += x;
            }
            temp = st.Pop();
            return temp;
        }

        /// <summary>
        /// This function receives a node of the syntax
        /// tree and recursively evaluate it
        /// </summary>
        /// <param name="root"></param>
        /// <returns>final answer to math expression</returns>
        public static int EvalTree(Node root)
        {
            // empty tree
            if (root == null)
                return 0;

            // integer tree without subtree
            if (root.left == null && root.right == null)
            {
                if (int.TryParse(root.data.ToString(), out int result))
                    return result;
                return 0;
            }

            // evaluate left subtree
            int leftEval = EvalTree(root.left);

            // evaluate right subtree
            int rightEval = EvalTree(root.right);

            // apply math operation
            if (root.data == "+")
                return leftEval + rightEval;

            if (root.data == "-")
                return leftEval - rightEval;

            if (root.data == "*")
                return leftEval * rightEval;

            return leftEval / rightEval;
        }

        private static bool isOperator(char ch)
        {
            return isOperator(ch.ToString());
        }

        private static bool isOperator(string ch)
        {
            if (ch == "+" || ch == "-" || ch == "*" || ch == "/")
                return true;
            return false;
        }
    }
    
}
