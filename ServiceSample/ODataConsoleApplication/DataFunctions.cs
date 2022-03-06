using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OData.Client;
using ODataUtility.Microsoft.Dynamics.DataEntities;

namespace ODataConsoleApplication
{
    public class DataFunctions
    {
        public static DataServiceQuery ReadSalesOrderLines(Resources file, int skip, int top)
        {
            int errorCnt = 0;
            DataServiceQuery data;

            while (errorCnt < 5)
            {
                data = file.SalesOrderLines.AddQueryOption("$skip", skip).AddQueryOption("$top", top);
                if (data == null)
                    errorCnt++;
                else
                    return data;
            }

            Console.WriteLine("Wasn't able to load data.");
            return null;
        }

        public static void GetFirstDigits(DataServiceQuery data, FirstDigitDistribution digits)
        {
            int firstDigit = 0;
            int lineAmt;

            foreach (SalesOrderLine line in data)
            {
                lineAmt = (int) Math.Abs(line.LineAmount);

                if (lineAmt < 10)
                    firstDigit = lineAmt;
                if (10 < lineAmt && lineAmt < 100)
                    firstDigit = lineAmt / 10;
                if (100 < lineAmt && lineAmt < 1000)
                    firstDigit = lineAmt / 100;
                if (1000 < lineAmt && lineAmt < 10000)
                    firstDigit = lineAmt / 1000;
                if (lineAmt > 10000)
                    firstDigit = lineAmt.ToString()[0] - '0';

                switch (firstDigit)
                {
                    case 1:
                        digits.One++;
                        break;
                    case 2:
                        digits.Two++;
                        break;
                    case 3:
                        digits.Three++;
                        break;
                    case 4:
                        digits.Four++;
                        break;
                    case 5:
                        digits.Five++;
                        break;
                    case 6:
                        digits.Six++;
                        break;
                    case 7:
                        digits.Seven++;
                        break;
                    case 8:
                        digits.Eight++;
                        break;
                    case 9:
                        digits.Nine++;
                        break;
                    default:
                        break;
                }
            }

        }

        public static void TestBenford(FirstDigitDistribution digits, double samples)
        {
            //Pearson's chi-squared test (https://en.wikipedia.org/wiki/Chi-squared_test | https://cs.wikipedia.org/wiki/Test_dobr%C3%A9_shody)
            double critical = 15.507; //chi-squared distribution for 8 degrees of freedom and p-value 0.05

            double chi = Math.Pow(digits.One - 0.301 * samples, 2) / (samples * 0.301);
            chi += Math.Pow(digits.Two - 0.176 * samples, 2) / (samples * 0.176);
            chi += Math.Pow(digits.Three - 0.125 * samples, 2) / (samples * 0.125);
            chi += Math.Pow(digits.Four - 0.097 * samples, 2) / (samples * 0.097);
            chi += Math.Pow(digits.Five - 0.079 * samples, 2) / (samples * 0.079);
            chi += Math.Pow(digits.Six - 0.067 * samples, 2) / (samples * 0.067);
            chi += Math.Pow(digits.Seven - 0.058 * samples, 2) / (samples * 0.058);
            chi += Math.Pow(digits.Eight - 0.051 * samples, 2) / (samples * 0.051);
            chi += Math.Pow(digits.Nine - 0.046 * samples, 2) / (samples * 0.046);

            if (chi < critical)
                Console.WriteLine("\nSe spolehlivostí 95% nelze říci, že by data byla uměle upravena.");
            else
                Console.WriteLine("\nData se spolehlivostí 95% nesplňují Benfordův zákon.");
        }

        public static void CompareBenford(FirstDigitDistribution digits, double samples)
        {
            Console.WriteLine("Na {0} vzorcích byly zjištěny následující výsledky:", samples);
            Console.WriteLine("[číslice / očekávané zastoupení / reálné zastoupení]\n");

            Console.WriteLine("1  / 30,1% / {0: 0.0}%", (digits.One / samples) * 100);
            Console.WriteLine("2  / 17,6% / {0: 0.0}%", (digits.Two / samples) * 100);
            Console.WriteLine("3  / 12,5% / {0: 0.0}%", (digits.Three / samples) * 100);
            Console.WriteLine("4  / 9,7%  / {0: 0.0}%", (digits.Four / samples) * 100);
            Console.WriteLine("5  / 7,9%  / {0: 0.0}%", (digits.Five / samples) * 100);
            Console.WriteLine("6  / 6,7%  / {0: 0.0}%", (digits.Six / samples) * 100);
            Console.WriteLine("7  / 5,8%  / {0: 0.0}%", (digits.Seven / samples) * 100);
            Console.WriteLine("8  / 5,1%  / {0: 0.0}%", (digits.Eight / samples) * 100);
            Console.WriteLine("9  / 4,6%  / {0: 0.0}%", (digits.Nine / samples) * 100);

        }
    }
}
