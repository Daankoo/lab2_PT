using System;

namespace BubbleSortApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                string? line = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                {
                    Console.Error.WriteLine("Error: empty input.");
                    Environment.Exit(1);
                    return;
                }

                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int[] numbers = new int[parts.Length];

                for (int i = 0; i < parts.Length; i++)
                {
                    if (!int.TryParse(parts[i], out numbers[i]))
                    {
                        Console.Error.WriteLine($"Error: '{parts[i]}' is not a valid number.");
                        Environment.Exit(2);
                        return;
                    }
                }

                Sorter.BubbleSort(numbers);

                Console.WriteLine(string.Join(" ", numbers));

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Unknown error: " + ex.Message);
                Environment.Exit(99);
            }
        }
    }
}
