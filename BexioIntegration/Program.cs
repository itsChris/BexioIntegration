using BexioIntegration.Models.Request;
using BexioIntegration.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BexioIntegration
{
    class Program
    {
        private static string _accessToken = null; // Store the access token after authentication

        static async Task Main(string[] args)
        {
            // Setup configuration and services
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddLogging(configure => configure.AddConsole())
                .AddSingleton<AuthService>()
                .AddSingleton<BexioService>()
                .AddSingleton<HttpClient>()
                .BuildServiceProvider();

            var authService = serviceProvider.GetService<AuthService>();
            var bexioService = serviceProvider.GetService<BexioService>();
            var logger = serviceProvider.GetService<ILogger<Program>>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Bexio Integration Menu:");
                Console.WriteLine("1. Authenticate");
                Console.WriteLine("2. List Contacts");
                Console.WriteLine("3. Create Invoice");
                Console.WriteLine("4. List Users");
                Console.WriteLine("5. Add Item Position to Document");
                Console.WriteLine("6. List Accounts");
                Console.WriteLine("7. List Units");
                Console.WriteLine("8. List Taxes");
                Console.WriteLine("9. List Articles");
                Console.WriteLine("10. List Item Positions");
                Console.WriteLine("11. List Invoices");
                Console.WriteLine("12. Add Default Item Position to Document");
                Console.WriteLine("13. Search contact");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        if (_accessToken == null)
                        {
                            try
                            {
                                Console.WriteLine("Authenticating...");
                                _accessToken = await authService.GetAccessTokenAsync();
                                Console.WriteLine("Authentication successful. Access token received.");
                            }
                            catch (Exception ex)
                            {
                                logger.LogError($"Authentication error: {ex.Message}");
                                Console.WriteLine("Authentication failed.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Already authenticated.");
                        }
                        break;

                    case "2":
                        if (_accessToken == null)
                        {
                            Console.WriteLine("Please authenticate first (Option 1).");
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine("Listing contacts...");
                                await bexioService.ListAllContactsAsync(_accessToken);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError($"Error listing contacts: {ex.Message}");
                                Console.WriteLine("Failed to list contacts.");
                            }
                        }
                        break;

                    case "3":
                        if (_accessToken == null)
                        {
                            Console.WriteLine("Please authenticate first (Option 1).");
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine("Creating an invoice...");
                                await bexioService.CreateInvoiceAsync(_accessToken);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError($"Error creating invoice: {ex.Message}");
                                Console.WriteLine("Failed to create invoice.");
                            }
                        }
                        break;

                    case "4":
                        if (_accessToken == null)
                        {
                            Console.WriteLine("Please authenticate first (Option 1).");
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine("Listing users...");
                                await bexioService.ListAllUsersAsync(_accessToken);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError($"Error listing users: {ex.Message}");
                                Console.WriteLine("Failed to list users.");
                            }
                        }
                        break;

                    case "5":
                        if (_accessToken == null)
                        {
                            Console.WriteLine("Please authenticate first (Option 1).");
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine("Adding an item position to a document...");
                                Console.WriteLine("Select the document type:");
                                Console.WriteLine("1. kb_offer");
                                Console.WriteLine("2. kb_order");
                                Console.WriteLine("3. kb_invoice");
                                Console.Write("Enter the number corresponding to the document type: ");

                                string documentTypeChoice = Console.ReadLine();
                                DocumentType documentType;

                                switch (documentTypeChoice)
                                {
                                    case "1":
                                        documentType = DocumentType.kb_offer;
                                        break;
                                    case "2":
                                        documentType = DocumentType.kb_order;
                                        break;
                                    case "3":
                                        documentType = DocumentType.kb_invoice;
                                        break;
                                    default:
                                        Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                                        return; // Exit to ensure documentType is not used unassigned
                                }

                                Console.Write("Enter document ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int documentId))
                                {
                                    Console.WriteLine("Invalid document ID.");
                                    return; // Exit to avoid further execution with an invalid document ID
                                }

                                var itemPosition = new ItemPositionRequest
                                {
                                    Amount = "5.000000",
                                    UnitId = 1,
                                    AccountId = 101, // 3200
                                    TaxId = 28,
                                    Text = "Apples",
                                    UnitPrice = "3.560000",
                                    DiscountInPercent = "0.000000",
                                    ArticleId = 1
                                };

                                await bexioService.AddItemPositionAsync(_accessToken, documentType, documentId, itemPosition);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError($"Error adding item position: {ex.Message}");
                                Console.WriteLine("Failed to add item position.");
                            }
                        }
                        break;

                    case "6":
                        if (_accessToken == null)
                        {
                            Console.WriteLine("Please authenticate first (Option 1).");
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine("Listing accounts...");
                                await bexioService.ListAllAccountsAsync(_accessToken);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError($"Error listing accounts: {ex.Message}");
                                Console.WriteLine("Failed to list accounts.");
                            }
                        }
                        break;
                    case "7":
                        if (_accessToken == null)
                        {
                            Console.WriteLine("Please authenticate first (Option 1).");
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine("Listing units...");
                                await bexioService.ListAllUnitsAsync(_accessToken);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError($"Error listing units: {ex.Message}");
                                Console.WriteLine("Failed to list units.");
                            }
                        }
                        break;
                    case "8":
                        if (_accessToken == null)
                        {
                            Console.WriteLine("Please authenticate first (Option 1).");
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine("Listing taxes...");
                                await bexioService.ListAllTaxesAsync(_accessToken);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError($"Error listing taxes: {ex.Message}");
                                Console.WriteLine("Failed to list taxes.");
                            }
                        }
                        break;
                    case "9":
                        if (_accessToken == null)
                        {
                            Console.WriteLine("Please authenticate first (Option 1).");
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine("Listing articles...");
                                await bexioService.ListAllArticlesAsync(_accessToken);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError($"Error listing articles: {ex.Message}");
                                Console.WriteLine("Failed to list articles.");
                            }
                        }
                        break;

                    case "10":
                        if (_accessToken == null)
                        {
                            Console.WriteLine("Please authenticate first (Option 1).");
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine("Select the document type:");
                                Console.WriteLine("1. kb_offer");
                                Console.WriteLine("2. kb_order");
                                Console.WriteLine("3. kb_invoice");
                                Console.Write("Enter the number corresponding to the document type: ");

                                string documentTypeChoice = Console.ReadLine();
                                DocumentType documentType;

                                switch (documentTypeChoice)
                                {
                                    case "1":
                                        documentType = DocumentType.kb_offer;
                                        break;
                                    case "2":
                                        documentType = DocumentType.kb_order;
                                        break;
                                    case "3":
                                        documentType = DocumentType.kb_invoice;
                                        break;
                                    default:
                                        Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                                        return; // Exit to ensure documentType is not used unassigned
                                }

                                Console.Write("Enter document ID: ");
                                if (!int.TryParse(Console.ReadLine(), out int documentId))
                                {
                                    Console.WriteLine("Invalid document ID.");
                                    return; // Exit to avoid further execution with an invalid document ID
                                }

                                Console.WriteLine("Listing item positions...");
                                await bexioService.ListItemPositionsAsync(_accessToken, documentType.ToString().ToLower(), documentId);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError($"Error listing item positions: {ex.Message}");
                                Console.WriteLine("Failed to list item positions.");
                            }
                        }
                        break;


                    case "11":
                        if (_accessToken == null)
                        {
                            Console.WriteLine("Please authenticate first (Option 1).");
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine("Listing invoices...");
                                await bexioService.ListAllInvoicesAsync(_accessToken);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError($"Error listing invoices: {ex.Message}");
                                Console.WriteLine("Failed to list invoices.");
                            }
                        }
                        break;

                    case "12":
                        if (_accessToken == null)
                        {
                            Console.WriteLine("Please authenticate first (Option 1).");
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine("Adding an item position to a document...");
                                Console.WriteLine("Select the document type (default is kb_invoice):");
                                Console.WriteLine("1. kb_offer");
                                Console.WriteLine("2. kb_order");
                                Console.WriteLine("3. kb_invoice");
                                Console.Write("Enter the number corresponding to the document type (or press Enter for default): ");

                                string documentTypeChoice = Console.ReadLine();
                                DocumentType documentType = documentTypeChoice switch
                                {
                                    "1" => DocumentType.kb_offer,
                                    "2" => DocumentType.kb_order,
                                    "3" or "" => DocumentType.kb_invoice,
                                    _ => throw new ArgumentException("Invalid choice. Please select 1, 2, or 3.")
                                };

                                Console.Write("Enter document ID (or press Enter for default 1): ");
                                string documentIdInput = Console.ReadLine();
                                int documentId = string.IsNullOrWhiteSpace(documentIdInput) ? 1 :
                                                 int.TryParse(documentIdInput, out int parsedDocumentId) ? parsedDocumentId :
                                                 throw new ArgumentException("Invalid document ID.");

                                Console.Write("Enter amount (or press Enter for default 1.000000): ");
                                string amountInput = Console.ReadLine();
                                string amount = string.IsNullOrWhiteSpace(amountInput) ? "1.000000" : amountInput;

                                Console.Write("Enter Unit ID (or press Enter for default 1): ");
                                string unitIdInput = Console.ReadLine();
                                int unitId = string.IsNullOrWhiteSpace(unitIdInput) ? 1 :
                                             int.TryParse(unitIdInput, out int parsedUnitId) ? parsedUnitId :
                                             throw new ArgumentException("Invalid Unit ID.");

                                Console.Write("Enter Account ID (or press Enter for default 101): ");
                                string accountIdInput = Console.ReadLine();
                                int accountId = string.IsNullOrWhiteSpace(accountIdInput) ? 101 :
                                                int.TryParse(accountIdInput, out int parsedAccountId) ? parsedAccountId :
                                                throw new ArgumentException("Invalid Account ID.");

                                Console.Write("Enter Tax ID (or press Enter for default 28): ");
                                string taxIdInput = Console.ReadLine();
                                int taxId = string.IsNullOrWhiteSpace(taxIdInput) ? 28 :
                                            int.TryParse(taxIdInput, out int parsedTaxId) ? parsedTaxId :
                                            throw new ArgumentException("Invalid Tax ID.");

                                Console.Write("Enter text (or press Enter for default 'Default text position'): ");
                                string textInput = Console.ReadLine();
                                string text = string.IsNullOrWhiteSpace(textInput) ? "Default text position" : textInput;

                                Console.Write("Enter unit price (or press Enter for default 100.000000): ");
                                string unitPriceInput = Console.ReadLine();
                                string unitPrice = string.IsNullOrWhiteSpace(unitPriceInput) ? "100.000000" : unitPriceInput;

                                var itemPosition = new DefaultPositionRequest
                                {
                                    Amount = amount,
                                    UnitId = unitId,
                                    AccountId = accountId,
                                    TaxId = taxId,
                                    Text = text,
                                    UnitPrice = unitPrice,
                                    DiscountInPercent = "0.000000",
                                };

                                await bexioService.AddItemDefaultPositionAsync(_accessToken, documentType, documentId, itemPosition);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError($"Error adding item position: {ex.Message}");
                                Console.WriteLine("Failed to add item position.");
                            }
                        }
                        break;

                    case "13":
                        if (_accessToken == null)
                        {
                            Console.WriteLine("Please authenticate first (Option 1).");
                        }
                        else
                        {
                            try
                            {
                                Console.Write("Enter the search field (e.g., 'name_1'): ");
                                string searchField = Console.ReadLine();

                                Console.Write("Enter the search term: ");
                                string searchTerm = Console.ReadLine();

                                Console.WriteLine("Searching for contacts...");
                                await bexioService.SearchContactsAsync(_accessToken, searchField, searchTerm);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError($"Error searching contacts: {ex.Message}");
                                Console.WriteLine("Failed to search contacts.");
                            }
                        }
                        break;

                    case "0":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
            }
        }
    }
}
