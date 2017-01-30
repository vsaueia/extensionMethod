using System.Dynamic;

namespace DISimpleInjector
{
    public class PiecePart : Part
    {
        public string PartNumber { get; }
        public string Description { get; }
        public double Cost { get; }

        public PiecePart(string partNumber, string description, double cost)
        {
            PartNumber = partNumber;
            Description = description;
            Cost = cost;            
            AddExtension("XML", new XmlPiecePartExtension(this));
            AddExtension("CSV", new CsvPiecePartExtension(this));
        }
    }    
}