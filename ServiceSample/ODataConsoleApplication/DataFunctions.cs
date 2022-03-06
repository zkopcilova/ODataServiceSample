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
            
        }

        public static void CompareBenford(FirstDigitDistribution digits, double samples)
        {
            Console.WriteLine("Formát: číslice / očekávané zastoupení / reálné zastoupení");
            Console.WriteLine("Na {0} vzorcích byly zjištěny následující výsledky:\n", samples);

            Console.WriteLine("1 / 30,1% / " + (digits.One / samples) * 100 + "%");
            Console.WriteLine("2 / 17,6% / " + (digits.Two / samples) * 100 + "%");
            Console.WriteLine("3 / 12,5% / " + (digits.Three / samples) * 100 + "%");
            Console.WriteLine("4 /  9,7% / " + (digits.Four / samples) * 100 + "%");
            Console.WriteLine("5 /  7,9% / " + (digits.Five / samples) * 100 + "%");
            Console.WriteLine("6 /  6,7% / " + (digits.Six / samples) * 100 + "%");
            Console.WriteLine("7 /  5,8% / " + (digits.Seven / samples) * 100 + "%");
            Console.WriteLine("8 /  5,1% / " + (digits.Eight / samples) * 100 + "%");
            Console.WriteLine("9 /  4,6% / " + (digits.Nine / samples) * 100 + "%");

        }
    }
}
