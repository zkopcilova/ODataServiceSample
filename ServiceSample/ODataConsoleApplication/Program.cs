using System;
using System.Globalization;
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
            //DataServiceQuery entityObject = context.SalesOrderLines.AddQueryOption("$skip", 10000).AddQueryOption("$top", 10000);
            DataServiceQuery entityObject = context.SalesOrderLines.AddQueryOption("$top", 10);

            string lineAmt;
            double beginWithOne = 0;

            foreach (SalesOrderLine line in entityObject)
            { 
                Console.WriteLine("{0}", line.LineAmount);
                lineAmt = line.LineAmount.ToString();
                if (lineAmt[0] == '1')
                    beginWithOne++;
            }

            double beginWithOneRatio = (beginWithOne/10)*100;

            Console.WriteLine("Jedničkou začíná {0} % řádků:", beginWithOneRatio);
            Console.ReadLine();
        }
    }
}
