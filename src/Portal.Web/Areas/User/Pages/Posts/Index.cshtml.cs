using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Portal.Web.Areas.User.Pages.Posts
{
    public class IndexModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            var client = new HttpClient();

            var response = await client.GetAsync("http://localhost:5501/api/post");
            PostList = JsonConvert.DeserializeObject<List<PostViewModel>>
                (await response.Content.ReadAsStringAsync());
            response.EnsureSuccessStatusCode();
            return Page();
        }

        public List<PostViewModel> PostList { get; set; }

        public class PostViewModel
        {
            public string Name { get; set; }

            public string Description { get; set; }
            public Byte[] ImageData { get; set; }
        }
    }
}