using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace LINQToXMLTests
{
    [TestClass]
    public class GroupByCityAndGetRentIntense
    {
        [TestMethod]
        public void Task7()
        {
            string FileName = $@"{Environment.CurrentDirectory}\Customers.xml";
            XDocument Document = XDocument.Load(FileName);
            var query = Document.Elements().
       Elements().Where(x => x.Elements("orders").Elements().Count() != 0).Select(x => new
       {
           city = x.Element("city").Value,
           countOfOrders = x.Elements("orders").Elements().Count(),
           sumRent = x.Elements("orders").Elements().Elements("total").Select(y => double.Parse(y.Value.Replace(".", ","))).Average(),
       }).GroupBy(x => x.city).Select(x => new
       {
           city = x.Key,
           averageSumRent = x.Select(y => y.sumRent).Average(),
           averageCountOfOrders = x.Select(y => y.countOfOrders).Average(),
       }).GroupBy(x => x);
            
            using (StreamWriter file = new StreamWriter($@"{Environment.CurrentDirectory}\CustomersGroupedByCityAndGetRentIntens.txt"))
            {
                foreach (var varGroup in query)
                {
                    foreach (var a in varGroup)
                    {
                        file.WriteLine(a.city);
                        file.WriteLine($"{a.averageSumRent:#0.0#} ");
                        file.WriteLine($"{ a.averageCountOfOrders:#0.0#} ");
                    }
                    
                }
            }
        }
    }
}
