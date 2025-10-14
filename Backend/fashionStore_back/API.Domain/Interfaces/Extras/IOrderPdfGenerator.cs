namespace API.Domain.Interfaces.Extras
{
    public interface IOrderPdfGenerator
    {
        byte[] GenerateOrderPdf(Guid orderId);
    }
}