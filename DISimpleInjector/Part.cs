using System.Collections;
using System.Xml.Linq;

namespace DISimpleInjector
{
    public abstract class Part
    {
        private Hashtable _extensions = new Hashtable();

        public void AddExtension(string extensionType, PartExtension extension)
        {
            _extensions[extensionType] = extension;
        }

        public PartExtension GetExtension(string extensionType)
        {
            PartExtension pe = _extensions[extensionType] as PartExtension;
            if(pe == null)
            {
                pe = new BadPartExtension();
            }
            return pe;
        }
    }
}