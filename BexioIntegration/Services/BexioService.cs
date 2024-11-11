using BexioIntegration.Models.Request;
using BexioIntegration.Models.Response;
using BexioIntegration.Models.Response.BexioIntegration.Models.Response;
using BexioIntegration.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace BexioIntegration.Services
{
    public class BexioService : BaseService, IBexioService
    {
        public BexioService(HttpClient httpClient, ILogger<BexioService> logger)
            : base(httpClient, logger) { }

        public async Task ListAllContactsAsync(string accessToken)
        {
            try
            {
                ConfigureHttpClient(accessToken);
                var response = await _httpClient.GetAsync("https://api.bexio.com/2.0/contact");
                List<ListContactsResponse> contacts = await HandleResponseAsync<List<ListContactsResponse>>(response);

                DisplayContacts(contacts);
                _logger.LogInformation("Contacts retrieved and displayed successfully.");
            }
            catch (Exception ex)
            {
                LogAndHandleError("listing contacts", ex);
            }
        }

        public async Task ListAllUsersAsync(string accessToken)
        {
            try
            {
                ConfigureHttpClient(accessToken);
                var response = await _httpClient.GetAsync("https://api.bexio.com/3.0/users");
                var users = await HandleResponseAsync<JArray>(response);

                DisplayUsers(users);
                _logger.LogInformation("Users retrieved and displayed successfully.");
            }
            catch (Exception ex)
            {
                LogAndHandleError("listing users", ex);
            }
        }

        public async Task ListAllInvoicesAsync(string accessToken)
        {
            try
            {
                ConfigureHttpClient(accessToken);
                var response = await _httpClient.GetAsync("https://api.bexio.com/2.0/kb_invoice");
                var invoices = await HandleResponseAsync<List<InvoiceResponse>>(response);

                DisplayInvoices(invoices);
                _logger.LogInformation("Invoices retrieved and displayed successfully.");
            }
            catch (Exception ex)
            {
                LogAndHandleError("listing invoices", ex);
            }
        }

        public async Task ListItemPositionsAsync(string accessToken, string documentType, int documentId)
        {
            try
            {
                ConfigureHttpClient(accessToken);
                var url = $"https://api.bexio.com/2.0/{documentType}/{documentId}/kb_position_article";
                var response = await _httpClient.GetAsync(url);
                var itemPositions = await HandleResponseAsync<List<ItemPositionResponse>>(response);

                _logger.LogInformation("Item positions retrieved and displayed successfully.");
            }
            catch (Exception ex)
            {
                LogAndHandleError("listing item positions", ex);
            }
        }

        public async Task CreateInvoiceAsync(string accessToken)
        {
            try
            {
                ConfigureHttpClient(accessToken);
                var payload = new
                {
                    contact_id = 2,
                    user_id = 1,
                    logopaper_id = 1,
                    language_id = 1,
                    bank_account_id = 1,
                    currency_id = 1,
                    payment_type_id = 1,
                    header = "Thank you very much for your inquiry.",
                    footer = "We hope that our offer meets your expectations.",
                    mwst_type = 0,
                    mwst_is_net = true,
                    show_position_taxes = false,
                    is_valid_from = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                    is_valid_to = DateTime.UtcNow.AddMonths(1).ToString("yyyy-MM-dd"), 
                    title = "this is a title"

                };

                var jsonPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://api.bexio.com/2.0/kb_invoice", content);
                var invoiceResponse = await HandleResponseAsync<InvoiceResponse>(response);

                Console.WriteLine($"Invoice created successfully: ID {invoiceResponse.Id}");
                _logger.LogInformation("Invoice created successfully.");
            }
            catch (Exception ex)
            {
                LogAndHandleError("creating an invoice", ex);
            }
        }
        public async Task AddItemDefaultPositionAsync(string accessToken, DocumentType documentType, int documentId, DefaultPositionRequest itemPosition)
        {
            try
            {
                ConfigureHttpClient(accessToken);
                var jsonPayload = JsonConvert.SerializeObject(itemPosition);
                var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");
                var url = $"https://api.bexio.com/2.0/{documentType.ToString().ToLower()}/{documentId}/kb_position_custom";
                var response = await _httpClient.PostAsync(url, content);

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    var createdPosition = await HandleResponseAsync<ItemPositionResponse>(response);
                    Console.WriteLine($"Item position created: ID {createdPosition.Id}");
                    _logger.LogInformation("Item position created successfully.");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning($"Failed to create item position: {errorContent}");
                    Console.WriteLine("Failed to create item position.");
                }
            }
            catch (Exception ex)
            {
                LogAndHandleError("adding an item position", ex);
            }
        }
        public async Task AddItemPositionAsync(string accessToken, DocumentType documentType, int documentId, ItemPositionRequest itemPosition)
        {
            try
            {
                ConfigureHttpClient(accessToken);
                var jsonPayload = JsonConvert.SerializeObject(itemPosition);
                var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");
                var url = $"https://api.bexio.com/2.0/{documentType.ToString().ToLower()}/{documentId}/kb_position_article";
                var response = await _httpClient.PostAsync(url, content);

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    var createdPosition = await HandleResponseAsync<ItemPositionResponse>(response);
                    Console.WriteLine($"Item position created: ID {createdPosition.Id}");
                    _logger.LogInformation("Item position created successfully.");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning($"Failed to create item position: {errorContent}");
                    Console.WriteLine("Failed to create item position.");
                }
            }
            catch (Exception ex)
            {
                LogAndHandleError("adding an item position", ex);
            }
        }

        public async Task ListAllAccountsAsync(string accessToken)
        {
            try
            {
                ConfigureHttpClient(accessToken);
                var response = await _httpClient.GetAsync("https://api.bexio.com/2.0/accounts");
                var accounts = await HandleResponseAsync<List<AccountResponse>>(response);

                DisplayAccounts(accounts);
                _logger.LogInformation("Accounts retrieved and displayed successfully.");
            }
            catch (Exception ex)
            {
                LogAndHandleError("listing accounts", ex);
            }
        }

        public async Task ListAllUnitsAsync(string accessToken)
        {
            try
            {
                ConfigureHttpClient(accessToken);
                var response = await _httpClient.GetAsync("https://api.bexio.com/2.0/unit");
                var units = await HandleResponseAsync<List<UnitResponse>>(response);

                DisplayUnits(units);
                _logger.LogInformation("Units retrieved and displayed successfully.");
            }
            catch (Exception ex)
            {
                LogAndHandleError("listing units", ex);
            }
        }

        public async Task ListAllTaxesAsync(string accessToken)
        {
            try
            {
                ConfigureHttpClient(accessToken);
                var response = await _httpClient.GetAsync("https://api.bexio.com/3.0/taxes?types=sales_tax&scope=active");
                var taxes = await HandleResponseAsync<List<TaxResponse>>(response);

                DisplayTaxes(taxes);
                _logger.LogInformation("Taxes retrieved and displayed successfully.");
            }
            catch (Exception ex)
            {
                LogAndHandleError("listing taxes", ex);
            }
        }

        public async Task ListAllArticlesAsync(string accessToken)
        {
            try
            {
                ConfigureHttpClient(accessToken);
                var response = await _httpClient.GetAsync("https://api.bexio.com/2.0/article");
                var articles = await HandleResponseAsync<List<ArticleResponse>>(response);

                DisplayArticles(articles);
                _logger.LogInformation("Articles retrieved and displayed successfully.");
            }
            catch (Exception ex)
            {
                LogAndHandleError("listing articles", ex);
            }
        }
        public async Task SearchContactsAsync(string accessToken, string searchField, string searchTerm)
        {
            try
            {
                ConfigureHttpClient(accessToken);


                searchField = "name_1";

                var payload = new[]
                {
                    new
                    {
                        field = searchField,
                        value = searchTerm,
                        criteria = "="
                        //criteria = "like"
                    }
                };

                var jsonPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://api.bexio.com/2.0/contact/search", content);

                if (response.IsSuccessStatusCode)
                {
                    var searchResults = await HandleResponseAsync<List<SearchContactResponse>>(response);
                    DisplaySearchResults(searchResults);
                    _logger.LogInformation("Contacts searched and displayed successfully.");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning($"Failed to search contacts: {errorContent}");
                    Console.WriteLine("Failed to search contacts.");
                }
            }
            catch (Exception ex)
            {
                LogAndHandleError("searching contacts", ex);
            }
        }

        private void DisplayContacts(List<ListContactsResponse> contacts)
        {
            Console.WriteLine("Contacts List:");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("{0, -5} {1, -30} {2, -30} {3, -15} {4, -20}", "ID", "Company Name", "Email", "City", "Updated At");
            Console.WriteLine("--------------------------------------------------------------------------------");

            foreach (var contact in contacts)
            {
                Console.WriteLine("{0, -5} {1, -30} {2, -30} {3, -15} {4, -20}",
                    contact.Id,
                    contact.Name1,
                    contact.Mail ?? "N/A",
                    contact.City ?? "N/A",
                    contact.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                );
            }

            Console.WriteLine("--------------------------------------------------------------------------------");
        }

        private void DisplayUsers(JArray users)
        {
            Console.WriteLine("User List:");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("{0, -5} {1, -15} {2, -15} {3, -30}", "ID", "First Name", "Last Name", "Email");
            Console.WriteLine("--------------------------------------------------------------------------------");

            foreach (var user in users)
            {
                Console.WriteLine("{0, -5} {1, -15} {2, -15} {3, -30}",
                    user["id"],
                    user["firstname"],
                    user["lastname"],
                    user["email"] ?? "N/A"
                );
            }

            Console.WriteLine("--------------------------------------------------------------------------------");
        }

        private void DisplayInvoices(List<InvoiceResponse> invoices)
        {
            Console.WriteLine("Invoices List:");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("{0, -5} {1, -15} {2, -10} {3, -15}", "ID", "Document Nr", "Contact ID", "Total");
            Console.WriteLine("--------------------------------------------------------------------------------");

            foreach (var invoice in invoices)
            {
                Console.WriteLine("{0, -5} {1, -15} {2, -10} {3, -15}",
                    invoice.Id,
                    invoice.DocumentNr,
                    invoice.ContactId,
                    invoice.Total
                );
            }

            Console.WriteLine("--------------------------------------------------------------------------------");
        }

        private void DisplayAccounts(List<AccountResponse> accounts)
        {
            Console.WriteLine("Accounts List:");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("{0, -5} {1, -30} {2, -10} {3, -20}", "ID", "UUID", "Account No.", "Name");
            Console.WriteLine("--------------------------------------------------------------------------------");

            foreach (var account in accounts)
            {
                Console.WriteLine("{0, -5} {1, -30} {2, -10} {3, -20}",
                    account.Id,
                    account.Uuid,
                    account.AccountNo,
                    account.Name
                );
            }

            Console.WriteLine("--------------------------------------------------------------------------------");
        }

        private void DisplayUnits(List<UnitResponse> units)
        {
            Console.WriteLine("Units List:");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("{0, -5} {1, -30} {2, -10}", "ID", "Name", "Active");
            Console.WriteLine("--------------------------------------------------------------------------------");

            foreach (var unit in units)
            {
                Console.WriteLine("{0, -5} {1, -30} {2, -10}",
                    unit.Id,
                    unit.Name,
                    unit.IsActive ? "Yes" : "No"
                );
            }

            Console.WriteLine("--------------------------------------------------------------------------------");
        }

        private void DisplayTaxes(List<TaxResponse> taxes)
        {
            Console.WriteLine("Taxes List:");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("{0, -5} {1, -30} {2, -15} {3, -10} {4, -10}", "ID", "Name", "Type", "Value", "Active");
            Console.WriteLine("--------------------------------------------------------------------------------");

            foreach (var tax in taxes)
            {
                Console.WriteLine("{0, -5} {1, -30} {2, -15} {3, -10} {4, -10}",
                    tax.Id,
                    tax.Name,
                    tax.Type,
                    tax.Value,
                    tax.IsActive ? "Yes" : "No"
                );
            }

            Console.WriteLine("--------------------------------------------------------------------------------");
        }

        private void DisplayArticles(List<ArticleResponse> articles)
        {
            Console.WriteLine("Articles List:");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("{0, -5} {1, -20} {2, -30} {3, -15}", "ID", "Intern Code", "Intern Name", "Stock");
            Console.WriteLine("--------------------------------------------------------------------------------");

            foreach (var article in articles)
            {
                Console.WriteLine("{0, -5} {1, -20} {2, -30} {3, -15}",
                    article.Id,
                    article.InternCode,
                    article.InternName,
                    article.StockNr
                );
            }

            Console.WriteLine("--------------------------------------------------------------------------------");
        }
        private void DisplaySearchResults(List<SearchContactResponse> contacts)
        {
            Console.WriteLine("Search Results:");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("{0, -5} {1, -30} {2, -30} {3, -15} {4, -20}", "ID", "Company Name", "Email", "City", "Updated At");
            Console.WriteLine("--------------------------------------------------------------------------------");

            foreach (var contact in contacts)
            {
                Console.WriteLine("{0, -5} {1, -30} {2, -30} {3, -15} {4, -20}",
                    contact.Id,
                    contact.Name1,
                    contact.Mail ?? "N/A",
                    contact.City ?? "N/A",
                    contact.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                );
            }

            Console.WriteLine("--------------------------------------------------------------------------------");
        }
        private void LogAndHandleError(string action, Exception ex)
        {
            _logger.LogError($"Error {action}: {ex.Message}");
            Console.WriteLine($"Failed to {action}.");
        }
    }
}
