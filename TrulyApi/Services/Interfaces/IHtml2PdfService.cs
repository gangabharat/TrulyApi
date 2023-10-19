namespace TrulyApi.Services.Interfaces
{
    public interface IHtml2PdfService
    {
        public Task ConvertToPdf(string sourceHtmlFileName, string destinationPdfFileName);
        public Task ConvertToPdf(string sourceHtmlFileName, string destinationPdfFileName, string? userPassword, string? ownerPassword);
    }
}