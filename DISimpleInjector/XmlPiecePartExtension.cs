using System.Xml;

namespace DISimpleInjector
{
    public class XmlPiecePartExtension : XmlPartExtension
    {
        private PiecePart _piecePart;

        public XmlPiecePartExtension(PiecePart part)
        {
            _piecePart = part;
        }

        public override XmlElement XmlElement
        {
            get
            {
                XmlElement e = NewElement("PiecePart");
                e.AppendChild(NewTextElement("PartNumber", _piecePart.PartNumber));
                e.AppendChild(NewTextElement("Description", _piecePart.Description));
                e.AppendChild(NewTextElement("Cost", _piecePart.Cost.ToString()));
                return e;
            }
        }
    }
}