using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Portal.Web.Pages.Posts
{
    public class IndexModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            var client = new HttpClient();

            var response = await client.GetAsync("http://localhost:5501/api/post");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(data))
            {
                PostList = JsonConvert.DeserializeObject<List<PostViewModel>>(data);
            }
            else
            {
                PostList = new List<PostViewModel>();
            }


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