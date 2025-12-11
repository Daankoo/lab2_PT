using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System;
using System.Diagnostics;
using System.IO;

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

        [TestClass]
        public class ProgramExitCodeTests
        {
            private static (int ExitCode, string StdOut, string StdErr) RunApp(string input)
            {
                string assemblyPath = typeof(BubbleSortApp.Program).Assembly.Location;

                var psi = new ProcessStartInfo
                {
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                if (Path.GetExtension(assemblyPath).Equals(".dll", StringComparison.OrdinalIgnoreCase))
                {
                    psi.FileName = "dotnet";
                    psi.Arguments = $"\"{assemblyPath}\"";
                }
                else
                {
                    psi.FileName = assemblyPath;
                }

                using var process = new Process { StartInfo = psi };
                process.Start();

                if (input != null)
                {
                    process.StandardInput.WriteLine(input);
                }
                process.StandardInput.Close();

                string stdout = process.StandardOutput.ReadToEnd();
                string stderr = process.StandardError.ReadToEnd();

                process.WaitForExit();

                return (process.ExitCode, stdout.Trim(), stderr.Trim());
            }

            [TestMethod]
            public void Main_ValidInput_ExitCodeZero_AndSortedOutput()
            {
                var result = RunApp("4 2 9 6 5 1");

                Assert.AreEqual(0, result.ExitCode);
                Assert.AreEqual("1 2 4 5 6 9", result.StdOut);
                Assert.AreEqual(string.Empty, result.StdErr);
            }

            [TestMethod]
            public void Main_EmptyInput_NonZeroExitCode_AndErrorToStderr()
            {
                var result = RunApp("");

                Assert.AreNotEqual(0, result.ExitCode);
                Assert.AreEqual(string.Empty, result.StdOut);
                Assert.IsTrue(result.StdErr.Contains("empty input", StringComparison.OrdinalIgnoreCase));
            }

            [TestMethod]
            public void Main_InvalidNumber_NonZeroExitCode_AndErrorToStderr()
            {
                var result = RunApp("4 2 a 6");

                Assert.AreNotEqual(0, result.ExitCode);
                Assert.AreEqual(string.Empty, result.StdOut);
                Assert.IsTrue(result.StdErr.Length > 0);
            }
        }
    }

}
