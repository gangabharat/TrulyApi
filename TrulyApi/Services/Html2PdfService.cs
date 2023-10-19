using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.StyledXmlParser.Css.Media;
using System.Text;
using TrulyApi.Services.Interfaces;

namespace TrulyApi.Services
{
    public class Html2PdfService : IHtml2PdfService
    {
        private readonly ConverterProperties _converterProperties;
        public Html2PdfService()
        {
            _converterProperties = new ConverterProperties();
            _converterProperties.SetMediaDeviceDescription(new MediaDeviceDescription(MediaType.PRINT));
        }
        public Task ConvertToPdf(string sourceHtmlFileName, string destinationPdfFileName)
        {
            FileInfo htmlFileInfo = new(sourceHtmlFileName);
            FileInfo pdfFileInfo = new(destinationPdfFileName);

            HtmlConverter.ConvertToPdf(htmlFileInfo, pdfFileInfo, _converterProperties);
            return Task.CompletedTask;
        }

        public Task ConvertToPdf(string sourceHtmlFileName, string destinationPdfFileName, string? userPassword = "", string? ownerPassword = "")
        {
            var user = Encoding.UTF8.GetBytes($"{userPassword}");
            var owner = Encoding.UTF8.GetBytes($"{ownerPassword}");
            WriterProperties writerProperties = new();
            writerProperties.SetStandardEncryption(user, owner, EncryptionConstants.STANDARD_ENCRYPTION_40, EncryptionConstants.ENCRYPTION_AES_128);
            PdfWriter writer = new($"{destinationPdfFileName}", writerProperties);

            // Creating a PdfDocument       
            PdfDocument pdfDoc = new(writer);

            HtmlConverter.ConvertToPdf(File.OpenRead(sourceHtmlFileName), pdfDoc);
            return Task.CompletedTask;
        }
    }
}
