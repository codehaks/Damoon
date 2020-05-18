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
    public class EditModel : PageModel
    {
        public async Task<IActionResult> OnGet(string id)
        {
            var client = new HttpClient();

            var response = await client.GetAsync("http://localhost:5501/api/post/"+id);
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
