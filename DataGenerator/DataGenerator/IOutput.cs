using System;

namespace DataGenerator
{
    public interface IOutput : IDisposable
    {
        void PrintLine();
        void PrintRow(params string[] columns);
    }
}