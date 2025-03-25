using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Asignment_PRN231_API_FE.Pages.Common;
using Asignment_PRN231_API_FE.Services;
using Asignment_PRN231_API_FE.ViewModel;

namespace Asignment_PRN231_API_FE.Pages.Manager
{
    public class TableManagementModel : BasePageModel
    {
        public TableManagementModel(IHttpContextAccessor httpContextAccessor, AuthService authService, IHttpClientFactory httpClientFactory)
            : base(httpContextAccessor, authService, httpClientFactory) { }

        [BindProperty]
        public TableVM Table { get; set; } = new TableVM();

        [BindProperty]
        public List<TableVM> Tables { get; set; } = new List<TableVM>();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Tables = await _httpClient.GetFromJsonAsync<List<TableVM>>("/api/tables") ?? new List<TableVM>();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while fetching tables.");
                Console.WriteLine($"Error fetching tables: {ex.Message}");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostCreateOrUpdateAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["Toast"] = JsonSerializer.Serialize(Toast.Error());
                return Page();
            }

            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(Table.Name ?? ""), "Name");
            content.Add(new StringContent(Table.ShopId.ToString()), "ShopId");
            content.Add(new StringContent(Table.Status.ToString()), "Status");

            HttpResponseMessage response;
            if (Table.Id > 0)
            {
                response = await _httpClient.PutAsync($"/api/tables/{Table.Id}", content);
            }
            else
            {
                response = await _httpClient.PostAsync("/api/create-table", content);
            }

            if (response.IsSuccessStatusCode)
            {
                TempData["Toast"] = JsonSerializer.Serialize(Toast.RegisterSuccess());
                return RedirectToPage("/Manager/TableManagement");
            }
            else
            {
                TempData["Toast"] = JsonSerializer.Serialize(Toast.RegisterError());
                var errorResponse = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", "Error saving table data: " + errorResponse);
                return Page();
            }
        }
    }

}
