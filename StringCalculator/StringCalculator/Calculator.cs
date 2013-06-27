using NUnit.Framework;
namespace StringCalculator
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator _calculator;
        [SetUp]
        public void SetUp()
        {
            _calculator = new Calculator();
        }
        [Test]
        public void CanInstantiate()
        {
            var calculator = new Calculator();
            Assert.That(calculator, Is.Not.Null);
        }

        [Test]
        public void AnEmptyStringReturnsZero()
        {
            int result = _calculator.Add("");
            Assert.That(result, Is.EqualTo(0));
        }

        [TestCase("1", 1)]
        [TestCase("2", 2)]
        [TestCase("3", 3)]
        public void ASingleNumberIsReplied(string input, int expected)
        {
            var result = _calculator.Add(input);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("1,2", 3)]
        [TestCase("2,3", 5)]
        [TestCase("3,4", 7)]

        public void AddTwoNumberStringReturnsSumOfNumbers(string input, int expected)
        {
            var result = _calculator.Add(input);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void AddMultipleNumbersReturnSumOfNumbers()
        {
            var result = _calculator.Add("1,2,3");
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void AddNoNumberStringWithDelimiterLineReturnZero()
        {
            var result = _calculator.Add("//,\n");
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void AddOneNumberStringWithDelimiterLineReturnNumber()
        {
            var result = _calculator.Add("//,\n1");
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void AddTwoNumberStringWithAlternativedelimiterReturnSum()
        {
            var result = _calculator.Add("//;\n1;2");
            Assert.That(result, Is.EqualTo(3));

        }
       
    }
    public class Calculator
    {
        private const string DELIMITER_LINE_INDICATOR = "//";
        private static string _delimiter = ",";

        public int Add(string numbers)
        {
            if (HasDelimiterLine(numbers))
            {
                ParseDelimiter(numbers);
                numbers = GetNumbers(numbers);
            }
            if (IsEmptyString(numbers)) return HandledEmptyString();
            if (HasMultipleNumbers(numbers))
            {
                return HandleMultilpleNumbers(numbers);
            }
            return HandleOneNumber(numbers);
        }

        private static void ParseDelimiter(string numbers)
        {
            _delimiter = numbers.Substring(2, 1);
        }

        private static string GetNumbers(string numbers)
        {
            string[] numParts = numbers.Split(char.Parse("\n"));
            numbers = numParts[1];
            return numbers;
        }

        private static bool HasDelimiterLine(string numbers)
        {
            return numbers.StartsWith(DELIMITER_LINE_INDICATOR);
        }

        private static int HandleMultilpleNumbers(string numbers)
        {
            string[] num = numbers.Split(_delimiter.ToCharArray());
            int total = 0;
            foreach (var s in num)
            {
                total += HandleOneNumber(s);
            }
            return total;
        }

        private static bool HasMultipleNumbers(string numbers)
        {          
            return numbers.Contains(_delimiter);
        }

        private static int HandleOneNumber(string numbers)
        {
            return int.Parse(numbers);
        }

        private static int HandledEmptyString()
        {
            return 0;
        }
        private static bool IsEmptyString(string numbers)
        {
            return numbers.Length == 0;
        }
    }
}
