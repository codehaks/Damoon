using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Portal.Web.Areas.User.Pages.Posts
{
    public class EditModel : PageModel
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        public EditModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }
        public IConfiguration Configuration { get; }
        public async Task<IActionResult> OnGet(string id)
        {
            var client = new HttpClient
            {
                BaseAddress = Configuration.GetServiceUri("api")
            };
            _logger.LogWarning(client.BaseAddress.AbsoluteUri);

            var response = await client.GetAsync("/api/post/"+id);
            Post = JsonConvert.DeserializeObject<PostViewModel>
                (await response.Content.ReadAsStringAsync());
            response.EnsureSuccessStatusCode();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Put, "http://localhost:5501/api/post");
            var data = JsonConvert.SerializeObject(Post);
            req.Content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(req);
            response.EnsureSuccessStatusCode();
            return RedirectToPage("/index");

        }

        [BindProperty]
        public PostViewModel Post { get; set; }

        public class PostViewModel
        {
            public string Id { get; set; }
            public string Name { get; set; }

            public string Description { get; set; }
            public Byte[] ImageData { get; set; }
        }
    }
}
