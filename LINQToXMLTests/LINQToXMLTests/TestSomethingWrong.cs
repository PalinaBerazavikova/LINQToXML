using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace LINQToXMLTests
{
    [TestClass]
    public class TestSomethingWrong
    {
        [TestMethod]
        public void TestNoRegion()
        {
            NoRegion();
        }
        [TestMethod]
        public void TestPostalcodeWithLetters()
        {
            PostalcodeWithLetters();
        }
        [TestMethod]
        public void TestPhoneWithoutOperator()
        {
            PhoneWithoutOperator();
        }
        [TestMethod]
        public void TestCommonWrong()
        {
            CommonWrong();
        }
        public void CommonWrong()
        {
            string FileName = $@"{Environment.CurrentDirectory}\Customers.xml";
            XDocument Document = XDocument.Load(FileName);
            var a = Document.Elements().
                Elements().
                Select(x => new
                {
                    name = x.Element("name").Value,
                    phone = x.Element("phone").Value,
                    postalcode = x.Element("postalcode"),
                    region = x.Element("region"),
                }).ToList();
            var result = a.Where(x => ((x.region == null)||!(Regex.IsMatch(x.phone, @"^[(]")) || ((x.postalcode != null) && (Regex.IsMatch(x.postalcode.Value, "[^0-9-]"))))).ToList();
            using (StreamWriter file = new StreamWriter($@"{Environment.CurrentDirectory}\CustomersCommonWrong.txt"))
            {
                foreach (var b in result)

                {
                    file.WriteLine($"{b.name} Phone: {b.phone}");
                    if(b.postalcode != null) file.WriteLine($"Postalcode: {b.postalcode.Value}");
                }
            }
        }
        public void PhoneWithoutOperator()
        {
            string FileName = $@"{Environment.CurrentDirectory}\Customers.xml";
            XDocument Document = XDocument.Load(FileName);
            var a = Document.Elements().
                Elements().
                Select(x => new
                {
                    name = x.Element("name").Value,
                    phone = x.Element("phone").Value,
                }).ToList();

            var result = a.Where(x => !(Regex.IsMatch(x.phone, @"^[(]"))).ToList();
            using (StreamWriter file = new StreamWriter($@"{Environment.CurrentDirectory}\CustomersPhoneWithoutOperator.txt"))
            {
                foreach (var b in result)

                {

                    file.WriteLine($"{b.name} { b.phone}");
                }
            }
        }
        public void PostalcodeWithLetters()
        {
            string FileName = $@"{Environment.CurrentDirectory}\Customers.xml";
            XDocument Document = XDocument.Load(FileName);
            var a = Document.Elements().
                Elements().
                Select(x => new
                {
                    name = x.Element("name").Value,
                    postalcode = x.Element("postalcode"),
                }).ToList();

            var result = a.Where(x => ((x.postalcode != null) && (Regex.IsMatch(x.postalcode.Value, "[^0-9-]")))).ToList();
            using (StreamWriter file = new StreamWriter($@"{Environment.CurrentDirectory}\CustomersPostalcodeWithLetters.txt"))
            {
                foreach (var b in result)

                {

                    file.WriteLine($"{b.name} { b.postalcode.Value}");
                }
            }
        }

        public void NoRegion()
        {
            string FileName = $@"{Environment.CurrentDirectory}\Customers.xml";
            XDocument Document = XDocument.Load(FileName);
            var a = Document.Elements().
                Elements().
                Select(x => new
                {
                    name = x.Element("name").Value,
                    region = x.Element("region"),

                }).ToList();

            var result = a.Where(x => (x.region == null)).ToList();
            using (StreamWriter file = new StreamWriter($@"{Environment.CurrentDirectory}\CustomersWithNoRegion.txt"))
            {
                foreach (var b in result)

                {
                    file.WriteLine($"{b.name} ");
                }
            }
        }
    }
}

