using System.Globalization;
using System.Text;

namespace DISimpleInjector
{
    internal class CsvPiecePartExtension : CsvPartExtension
    {
        private readonly PiecePart _piecePart;

        public CsvPiecePartExtension(PiecePart piecePart)
        {
            this._piecePart = piecePart;
        }

        public string CsvText
        {
            get
            {
                StringBuilder b = new StringBuilder("PiecePart,");
                b.Append(_piecePart.PartNumber);
                b.Append(",");
                b.Append(_piecePart.Description);
                b.Append(",");
                b.Append(_piecePart.Cost.ToString("#.#"));
                return b.ToString();
            }
        }
    }
}