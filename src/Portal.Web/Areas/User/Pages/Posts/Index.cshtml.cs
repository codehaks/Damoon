using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Portal.Web.Common;
using Serilog;

namespace Portal.Web.Areas.User.Pages.Posts
{
    public class IndexModel : PageModel
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        public IndexModel(IConfiguration configuration,ILogger<IndexModel> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }
        public async Task<IActionResult> OnGet()
        {
            var client = new HttpClient
            {
                BaseAddress = Configuration.GetServiceUri("api")
            };
            _logger.LogWarning(client.BaseAddress.AbsoluteUri);


            var userId = User.GetUserId();
            var response = await client.GetAsync("/api/post/user/"+userId);
            PostList = JsonConvert.DeserializeObject<List<PostViewModel>>
                (await response.Content.ReadAsStringAsync());
            response.EnsureSuccessStatusCode();
            return Page();
        }

        public List<PostViewModel> PostList { get; set; }

        public class PostViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public string Description { get; set; }
            public Byte[] ImageData { get; set; }
        }
    }
}
