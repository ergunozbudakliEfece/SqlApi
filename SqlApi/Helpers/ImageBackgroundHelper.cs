
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SqlApi.Helpers
{
    class ImageBackgroundHelper : PdfPageEventHelper
    {
        private Image img;
        public ImageBackgroundHelper(Image img)
        {
            this.img = img;
        }
        /**
         * @see com.itextpdf.text.pdf.PdfPageEventHelper#onEndPage(
         *      com.itextpdf.text.pdf.PdfWriter, com.itextpdf.text.Document)
         */
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            writer.DirectContentUnder.AddImage(img);
        }
    }
}
