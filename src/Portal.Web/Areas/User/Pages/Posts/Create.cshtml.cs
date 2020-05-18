using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Portal.Web.Common;

namespace Portal.Web.Areas.User.Pages.Posts
{


    public class CreateModel : PageModel
    {
        public async Task<IActionResult> OnPost()
        {
            var client = new HttpClient();
            Post.UserId = User.GetUserId();
            var data = JsonConvert.SerializeObject(Post);
            var response = await client.PostAsync("http://localhost:5501/api/post", new StringContent(data, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return RedirectToPage("/index");
        }

        [BindProperty]
        public PostCreateModel Post { get; set; }

        public class PostCreateModel
        {
            [MaxLength(100)]
            public string Name { get; set; }

            public string UserId { get; set; }

            [MaxLength(256)]
            public string Description { get; set; }
            public Byte[] ImageData { get; set; }
        }

    }
}
