using System;
using System.Xml;
using NUnit.Framework;

namespace DISimpleInjector
{
    [TestFixture]
    public class BomXmlTest
    {
        private PiecePart p1;
        private PiecePart p2;
        private Assembly a;

        [SetUp]
        public void SetUp()
        {
            p1 = new PiecePart("997624", "MyPart", 3.20);
            p2 = new PiecePart("7734", "Hell", 666);
            a = new Assembly("5879", "MyAssembly");
        }

        [Test]
        public void CreatePart()
        {
            Assert.AreEqual("997624", p1.PartNumber);
            Assert.AreEqual("MyPart", p1.Description);
            Assert.AreEqual(3.20, p1.Cost, .01);
        }

        [Test]
        public void CreateAssembly()
        {
            Assert.AreEqual("5879", a.PartNumber);
            Assert.AreEqual("MyAssembly", a.Description);
        }

        [Test]
        public void Assembly()
        {
            a.Add(p1);
            a.Add(p2);
            Assert.AreEqual(2, a.Parts.Count);
            Assert.AreEqual(a.Parts[0], p1);
            Assert.AreEqual(a.Parts[1], p2);            
        }

        [Test]
        public void AssemblyOfAssemblies()
        {
            Assembly subAssembly = new Assembly("1324", "SubAssembly");
            subAssembly.Add(p1);
            a.Add(subAssembly);
            Assert.AreEqual(subAssembly, a.Parts[0]);
        }

        private string ChildText(XmlElement element, string childName)
        {
            return Child(element, childName).InnerText;
        }

        private XmlElement Child(XmlElement element, string childName)
        {
            XmlNodeList children = element.GetElementsByTagName(childName);
            return children.Item(0) as XmlElement;
        }

        [Test]
        public void PiecePart1XML()
        {
            PartExtension e = p1.GetExtension("XML");
            XmlPartExtension xe = e as XmlPartExtension;
            XmlElement xml = xe.XmlElement;
            Assert.AreEqual("PiecePart", xml.Name);
            Assert.AreEqual("997624", ChildText(xml, "PartNumber"));
            Assert.AreEqual("MyPart", ChildText(xml, "Description"));
            Assert.AreEqual(3.2, Double.Parse(ChildText(xml, "Cost")), .01);
        }

        [Test]
        public void PiecePart2XML()
        {
            PartExtension e = p2.GetExtension("XML");
            XmlPartExtension xe = e as XmlPartExtension;
            XmlElement xml = xe.XmlElement;
            Assert.AreEqual("PiecePart", xml.Name);
            Assert.AreEqual("7734", ChildText(xml, "PartNumber"));
            Assert.AreEqual("Hell", ChildText(xml, "Description"));
            Assert.AreEqual(666, Double.Parse(ChildText(xml, "Cost")), .01);
        }

        [Test]
        public void SimpleAssemblyXML()
        {
            PartExtension e = a.GetExtension("XML");
            XmlPartExtension xe = e as XmlPartExtension;
            XmlElement xml = xe.XmlElement;
            Assert.AreEqual("Assembly", xml.Name);
            Assert.AreEqual("5879", ChildText(xml, "PartNumber"));
            Assert.AreEqual("MyAssembly", ChildText(xml, "Description"));
            XmlElement parts = Child(xml, "Parts");
            XmlNodeList partList = parts.ChildNodes;
            Assert.AreEqual(0, partList.Count);        
        }

        [Test]
        public void AssemblyWithPartsXML()
        {
            a.Add(p1);
            a.Add(p2);
            PartExtension e = a.GetExtension("XML");
            XmlPartExtension xe = e as XmlPartExtension;
            XmlElement xml = xe.XmlElement;
            Assert.AreEqual("Assembly", xml.Name);
            Assert.AreEqual("5879", ChildText(xml, "PartNumber"));
            Assert.AreEqual("MyAssembly", ChildText(xml, "Description"));

            XmlElement parts = Child(xml, "Parts");
            XmlNodeList partList = parts.ChildNodes;
            Assert.AreEqual(2, partList.Count);

            XmlElement partElement = partList.Item(0) as XmlElement;
            Assert.AreEqual("PiecePart", partElement.Name);
            Assert.AreEqual("997624", ChildText(partElement, "PartNumber"));

            partElement = partList.Item(1) as XmlElement;
            Assert.AreEqual("PiecePart", partElement.Name);
            Assert.AreEqual("7734", ChildText(partElement, "PartNumber"));                      
        }

        [Test]
        public void PiecePart1ToCSV()
        {
            PartExtension e = p1.GetExtension("CSV");
            CsvPartExtension ce = e as CsvPartExtension;
            string csv = ce.CsvText;
            Assert.AreEqual("PiecePart,997624,MyPart,3,2", csv);
        }

        [Test]
        public void PiecePart2ToCSV()
        {
            PartExtension e = p2.GetExtension("CSV");
            CsvPartExtension ce = e as CsvPartExtension;
            string csv = ce.CsvText;
            Assert.AreEqual("PiecePart,7734,Hell,666", csv);
        }

        [Test]
        public void SimpleAssemblyCSV()
        {
            PartExtension e = a.GetExtension("CSV");
            CsvPartExtension ce = e as CsvPartExtension;
            String csv = ce.CsvText;
            Assert.AreEqual("Assembly,5879,MyAssembly", csv);
        }

        [Test]
        public void AssemblyWithPartsCSV()
        {
            a.Add(p1);
            a.Add(p2);

            PartExtension e = a.GetExtension("CSV");
            CsvPartExtension ce = e as CsvPartExtension;
            String csv = ce.CsvText;

            Assert.AreEqual("Assembly,5879,MyAssembly,{PiecePart,997624,MyPart,3,2},{PiecePart,7734,Hell,666}", csv);
        }

        [Test]
        public void BadExtension()
        {
            PartExtension pe = p1.GetExtension("ThisStringDoesNotMatchAnyExtension");
            Assert.IsTrue(pe is BadPartExtension);
        }
    }
}