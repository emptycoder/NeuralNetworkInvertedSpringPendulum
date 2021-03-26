using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace DataGenerator
{
    static class Program
    {
        private static IOutput _output;

        static void Main(string[] args)
        {
            _output = new FileOutput(Path.Combine(Environment.CurrentDirectory, "aliceData.txt"));
            // _output = new TableOutput();
            // _output = new FakeOutput();
            // DEMO Values
            // const decimal k0 = 1,
            //     kMax = 2,
            //     kCount = 2,
            //     v0 = 0,
            //     vMax = 2,
            //     t0 = 0,
            //     tMax = 10 * kMax,
            //     tCount = 5,
            //     dt = 0.5m,
            //     countOfP = 5,
            //     xMax = vMax,
            //     xMin = -xMax;
            
            // 14 variant values
            // const decimal k0 = 0.1m,
            //     kMax = 0.6m,
            //     kCount = 50,
            //     v0 = 0.1m,
            //     vMax = 0.8m,
            //     t0 = 0,
            //     tMax = 10 * kMax,
            //     tCount = 50,
            //     dt = 0.014m,
            //     countOfP = 7,
            //     xMax = vMax,
            //     xMin = -xMax;
            
            const decimal k0 = 0.1m,
                kMax = 0.4m,
                kCount = 50,
                v0 = 0.1m,
                vMax = 0.9m,
                t0 = 0,
                tMax = 10 * kMax,
                tCount = 50,
                dt = 0.007m,
                countOfP = 6,
                xMax = vMax,
                xMin = -xMax;

            // _output.PrintLine();
            // string[] headers = new string[(int)countOfP + 1];
            // for (int p = 0; p < countOfP; p++)
            // {
            //     headers[p] = $"x{p.ToString()}_norm";
            // }
            // headers[^1] = "k_norm";
            // _output.PrintRow(headers);
            // _output.PrintLine();

            for (decimal kIndex = k0; kIndex < kMax; kIndex += (kMax - k0) / (kCount))
            {
                for (decimal vIndex = v0; vIndex < vMax; vIndex += (vMax - v0) / (kCount))
                {
                    var kNormIndex = 0;
                    for (decimal tIndex = t0; tIndex < tMax; tIndex += (tMax - t0) / (tCount))
                    {
                        int index = 0;
                        var normalVariableIndex = 0;
                        StringBuilder stringBuilder = new StringBuilder();
                        var kNorm = (kIndex - k0) / (kMax - k0);
                        var t = t0 + kNormIndex * kIndex * 10 / (tCount - 1);
                        for (decimal tIndex1 = t0; tIndex1 < tMax && normalVariableIndex < countOfP; tIndex1 += (tMax - t0) / (tCount), normalVariableIndex++)
                        {
                            var preNorm = vIndex * DecimalMath.DecimalMath.Cos(kIndex * (t + normalVariableIndex * dt));
                            var valNorm = (preNorm - xMin) / (xMax - xMin);

                            stringBuilder.AppendFormat("{0}, ",
                                preNorm.ToString(CultureInfo.InvariantCulture));
                            // stringBuilder.AppendFormat("{0}, ", valNorm.ToString(CultureInfo.InvariantCulture));
                            // _output.PrintRow(
                            //     index.ToString(),
                            //     kIndex.ToString(CultureInfo.InvariantCulture),
                            //     (kIndex * 10).ToString(CultureInfo.InvariantCulture),
                            //     t.ToString(CultureInfo.InvariantCulture),
                            //     vIndex.ToString(CultureInfo.InvariantCulture),
                            //     dt.ToString(CultureInfo.InvariantCulture));
                            index++;
                            //     ,
                            // valNorm.ToString(CultureInfo.InvariantCulture),
                            // kNorm.ToString(CultureInfo.InvariantCulture)
                        }
                        _output.PrintRow(stringBuilder.Append(kNorm).ToString());
                        // _output.PrintRow(stringBuilder.Append(kNorm).ToString());
                        kNormIndex++;
                        _output.PrintLine();
                    }

                    _output.PrintLine();
                }
            }
            
            _output.Dispose();
            Console.WriteLine("OK");
        }
    }
}