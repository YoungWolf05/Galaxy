using MathOperations;
using NUnit.Framework;
using static MathOperations.MathOperation;
namespace MathOperations_UnitTest
{
    public class MathOperation_Test
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(
            "((15 / (7 - (1 + 1)) ) * -3 ) -(2 + (1 + 1))",
            ExpectedResult = -13,
            TestName = "Expression_01")]
        [TestCase(
            "((2+8)*8)-(5*(5+2))",
            ExpectedResult = 45,
            TestName = "Expression_02")]
        [TestCase(
            "((2+8)*8)--(5*(5+2))",
            ExpectedResult = 115,
            TestName = "Expression_03")]
        [TestCase(
            "((2+8)*8)-(-5*(5+2))",
            ExpectedResult = 115,
            TestName = "Expression_04")]
        [TestCase(
            "(1+1)*2",
            ExpectedResult = 4,
            TestName = "Expression_05")]
        [TestCase(
            "(1+-1)*2",
            ExpectedResult = 0,
            TestName = "Expression_06")]
        [TestCase(
            "(1+-1*2",
            ExpectedResult = -1,
            TestName = "Expression_07")]
        [TestCase(
            "((1+(-1*2",
            ExpectedResult = -2,
            TestName = "Expression_08")]
        [TestCase(
            "3+-1*2+4",
            ExpectedResult = 5,
            TestName = "Expression_09")]
        [TestCase(
            "3*-1+2*4",
            ExpectedResult = 5,
            TestName = "Expression_10")]
        public int VerifyAnswer(string expression)
        {
            string postfix = PostFix(expression);
            Node r = ExpressionTree(postfix);
            return EvalTree(r);
        }

        [TestCase(
            "1+-1)*2",
            TestName = "InvalidExpression_01")]
        [TestCase(
            ")1)+)-1*2",
            TestName = "InvalidExpression_02")]
        public void InvalidAnswer(string expression)
        {
            Assert.That(() => PostFix(expression), Throws.Exception.TypeOf<System.InvalidOperationException>());
        }
    }
}