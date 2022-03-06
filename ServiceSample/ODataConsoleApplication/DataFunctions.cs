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
            DataServiceQuery entityObject;

            while (errorCnt < 5)
            {
                entityObject = file.SalesOrderLines.AddQueryOption("$skip", skip).AddQueryOption("$top", top);
                if (entityObject == null)
                    errorCnt++;
                else
                    return entityObject;
            }

            Console.WriteLine("Wasn't able to load data.");
            return null;
        }

    }
}
