using System.IO;

namespace DataGenerator
{
    public class FileOutput : IOutput
    {
        private readonly StreamWriter _streamWriter;

        public FileOutput(string path) =>
            _streamWriter = new StreamWriter(path);

        public void PrintLine()
        {
        }

        public void PrintRow(params string[] columns)
        {
            for (int index = 0; ; index++)
            {
                if (index == columns.Length - 1)
                {
                    _streamWriter.WriteLine(columns[index]);
                    break;
                }

                _streamWriter.Write($"{columns[index]},");
            }
        }
        
        public void Dispose()
        {
            _streamWriter.Close();
            _streamWriter.Dispose();
        }
    }
}