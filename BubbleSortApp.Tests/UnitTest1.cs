using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace BubbleSortApp.Tests
{
    [TestClass]
    public class SorterTests
    {
        [TestMethod]
        public void BubbleSort_SortsSimpleArray()
        {
            int[] input = { 4, 2, 9, 6, 5, 1 };
            int[] expected = { 1, 2, 4, 5, 6, 9 };

            Sorter.BubbleSort(input);

            CollectionAssert.AreEqual(expected, input);
        }

        [TestMethod]
        public void BubbleSort_SortsAlreadySortedArray()
        {
            int[] input = { 1, 2, 3, 4, 5 };
            int[] expected = { 1, 2, 3, 4, 5 };

            Sorter.BubbleSort(input);

            CollectionAssert.AreEqual(expected, input);
        }

        [TestMethod]
        public void BubbleSort_SortsReversedArray()
        {
            int[] input = { 5, 4, 3, 2, 1 };
            int[] expected = { 1, 2, 3, 4, 5 };

            Sorter.BubbleSort(input);

            CollectionAssert.AreEqual(expected, input);
        }

        [TestMethod]
        public void BubbleSort_SortsArrayWithDuplicates()
        {
            int[] input = { 3, 1, 2, 3, 2 };
            int[] expected = { 1, 2, 2, 3, 3 };

            Sorter.BubbleSort(input);

            CollectionAssert.AreEqual(expected, input);
        }

        [TestMethod]
        public void BubbleSort_SortsArrayWithNegativeNumbers()
        {
            int[] input = { -1, 5, 0, -3, 2 };
            int[] expected = { -3, -1, 0, 2, 5 };

            Sorter.BubbleSort(input);

            CollectionAssert.AreEqual(expected, input);
        }

        [TestMethod]
        public void BubbleSort_HandlesSingleElementArray()
        {
            int[] input = { 42 };
            int[] expected = { 42 };

            Sorter.BubbleSort(input);

            CollectionAssert.AreEqual(expected, input);
        }

        [TestMethod]
        public void BubbleSort_HandlesEmptyArray()
        {
            int[] input = Array.Empty<int>();
            int[] expected = Array.Empty<int>();

            Sorter.BubbleSort(input);

            CollectionAssert.AreEqual(expected, input);
        }
    }
}
