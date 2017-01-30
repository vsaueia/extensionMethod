using System.Text;

namespace DISimpleInjector
{
    internal class CsvAssemblyExtension : CsvPartExtension
    {
        private Assembly assembly;

        public CsvAssemblyExtension(Assembly assembly)
        {
            this.assembly = assembly;
        }

        public string CsvText
        {
            get
            {
                StringBuilder b = new StringBuilder("Assembly,");
                b.Append(assembly.PartNumber);
                b.Append(",");
                b.Append(assembly.Description);

                foreach (Part part in assembly.Parts)
                {
                    CsvPartExtension cpe = part.GetExtension("CSV") as CsvPartExtension;
                    b.Append(",{");
                    b.Append(cpe.CsvText);
                    b.Append("}");
                }
                return b.ToString();
            }
        }
    }
}