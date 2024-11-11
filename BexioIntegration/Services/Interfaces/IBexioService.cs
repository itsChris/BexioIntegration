using BexioIntegration.Models.Request;

namespace BexioIntegration.Services.Interfaces
{
    public interface IBexioService
    {
        Task ListAllContactsAsync(string accessToken);
        Task ListAllUsersAsync(string accessToken);
        Task ListAllInvoicesAsync(string accessToken);
        Task ListItemPositionsAsync(string accessToken, string documentType, int documentId);
        Task CreateInvoiceAsync(string accessToken);
        Task AddItemPositionAsync(string accessToken, DocumentType documentType, int documentId, ItemPositionRequest itemPosition);
        Task AddItemDefaultPositionAsync(string accessToken, DocumentType documentType, int documentId, DefaultPositionRequest itemPosition);
        Task ListAllAccountsAsync(string accessToken);
        Task ListAllUnitsAsync(string accessToken);
        Task ListAllTaxesAsync(string accessToken);
        Task ListAllArticlesAsync(string accessToken);
    }

}
