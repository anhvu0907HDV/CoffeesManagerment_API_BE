using Asignment_PRN231_API_FE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;

namespace Asignment_PRN231_API_FE.Pages.OwnerSide.ManageEmployees.Manager
{
    [Authorize(Roles = "Owner,Manager")]
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public List<ManagerVM> Managers { get; set; } = new();


        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _httpClient.GetAsync("owner/get-all-manager");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Managers = JsonSerializer.Deserialize<List<ManagerVM>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return Page();
        }
    }
}
