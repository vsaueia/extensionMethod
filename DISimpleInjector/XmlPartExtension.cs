using System.Xml;

namespace DISimpleInjector
{
    public abstract class XmlPartExtension : PartExtension
    {
        private static XmlDocument document = new XmlDocument();
        public abstract XmlElement XmlElement { get; }

        protected XmlElement NewElement(string name)
        {
            return document.CreateElement(name);
        }

        protected XmlElement NewTextElement(string name, string text)
        {
            XmlElement element = document.CreateElement(name);
            XmlText xmlText = document.CreateTextNode(text);
            element.AppendChild(xmlText);
            return element;
        }
    }
}