using System;
using System.Globalization;
using System.Linq;
using System.Net;
using AuthenticationUtility;
using Microsoft.OData.Client;
using ODataUtility.Microsoft.Dynamics.DataEntities;

namespace ODataConsoleApplication
{
    class Program
    {
        public static string ODataEntityPath = ClientConfiguration.Default.UriString + "data";

        static void Main(string[] args)
        {
            // To test custom entities, regenerate "ODataClient.tt" file.
            // https://blogs.msdn.microsoft.com/odatateam/2014/03/11/tutorial-sample-how-to-use-odata-client-code-generator-to-generate-client-side-proxy-class/

            //TODO: Force TLS 1.2
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Uri oDataUri = new Uri(ODataEntityPath, UriKind.Absolute);
            var context = new Resources(oDataUri);

            context.SendingRequest2 += new EventHandler<SendingRequest2EventArgs>(delegate (object sender, SendingRequest2EventArgs e)
            {
                var authenticationHeader = OAuthHelper.GetAuthenticationHeader(useWebAppAuthentication: true);
                e.RequestMessage.SetHeader(OAuthHelper.OAuthHeader, authenticationHeader);
            });


            //TODO: Read OData entity and do some action on it

            //var linesTotal = context.SalesOrderLines.Count();
            double linesTotal = 1000;

            //temporary variables
            DataServiceQuery data;
            FirstDigitDistribution digits = new FirstDigitDistribution();
            int topLines = 10000;
            

            for (int skipLines = 0; skipLines < linesTotal; skipLines += 10000)
            {
                //Only read remaining number of lines (<10,000) during the last iteration
                if (linesTotal - skipLines < 10000)
                    topLines = (int)linesTotal - skipLines;

                data = DataFunctions.ReadSalesOrderLines(context, skipLines, topLines);
                DataFunctions.GetFirstDigits(data, digits);

            }

            DataFunctions.CompareBenford(digits, linesTotal);

            //DataFunctions.TestBenford(digits, linesTotal);

            Console.ReadLine();
        }
    }
}
