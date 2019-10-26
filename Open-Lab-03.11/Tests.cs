using System;
using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Open_Lab_03._11
{
    [TestFixture]
    public class Tests
    {

        private Checker checker;
        private bool shouldStop;

        private const int RandStrMinSize = 50;
        private const int RandStrMaxSize = 10000;
        private const int RandSeed = 311311311;
        private const int RandTestCasesCount = 96;
        private const int RandPalindromeChance = 3;

        [OneTimeSetUp]
        public void Init()
        {
            checker = new Checker();
            shouldStop = false;
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure ||
                TestContext.CurrentContext.Result.Outcome == ResultState.Error)
                shouldStop = false;
        }

        [TestCase("oko", true)]
        [TestCase("auto", false)]
        [TestCase("testing", false)]
        [TestCase("abcddcba", true)]
        public void IsPalindromeTest(string str, bool expected) =>
            Assert.That(checker.IsPalindrome(str), Is.EqualTo(expected));

        [TestCaseSource(nameof(GetRandom))]
        public void IsPalindromeTestRandom(string str, bool expected)
        {
            if (shouldStop)
                Assert.Ignore("Previous test failed!");

            Assert.That(checker.IsPalindrome(str), Is.EqualTo(expected));
        }

        private static IEnumerable GetRandom()
        {
            var rand = new Random(RandSeed);

            for (var i = 0; i < RandTestCasesCount; i++)
            {
                var arr = new char[rand.Next(RandStrMinSize, RandStrMaxSize + 1)];
                var isPalindrome = rand.Next(RandPalindromeChance) == 0;

                for (var j = 0; j < (isPalindrome ? arr.Length / 2 : arr.Length); j++)
                    arr[j] = (char) rand.Next('a', 'z' + 1);

                if (isPalindrome)
                {
                    for (var j = 0; j < arr.Length / 2; j++)
                        arr[^(j + 1)] = arr[j];
                    
                    if (arr.Length % 2 == 1)
                        arr[arr.Length / 2] = (char) rand.Next('a', 'z' + 1);
                }

                yield return new TestCaseData(new string(arr), isPalindrome);
            }
        }

    }
}
