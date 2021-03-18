using System;

namespace DataGenerator
{
    public class DataOutput : IOutput
    {
        public void PrintLine() =>
            Console.WriteLine();

        public void PrintRow(params string[] columns)
        {
            for (int index = 0; ; index++)
            {
                Console.Write($"{columns[index]}, ");
                if (index != columns.Length - 1) continue;
                
                Console.WriteLine(columns[index]);
                break;
            }
        }

        public void Dispose()
        {
        }
    }
}