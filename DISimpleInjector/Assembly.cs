using System.Collections;

namespace DISimpleInjector
{
    public class Assembly : Part
    {
        public string PartNumber { get; }
        public string Description { get; }
        public IList parts = new ArrayList();

        public Assembly(string partNumber, string description)
        {
            PartNumber = partNumber;
            Description = description;         
            AddExtension("XML", new XmlAssemblyExtension(this));
            AddExtension("CSV", new CsvAssemblyExtension(this));
        }

        public void Add(Part part)
        {
            parts.Add(part);
        }

        public IList Parts => parts;
    }
}