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
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Portal.Web.Common;


namespace Portal.Web.Areas.User.Pages.Posts
{


    public class CreateModel : PageModel
    {
        public CreateModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async Task<IActionResult> OnPost()
        {
            var client = new HttpClient();
            client.BaseAddress = Configuration.GetServiceUri("api");
            Post.UserId = User.GetUserId();
            var data = JsonConvert.SerializeObject(Post);
            var response = await client.PostAsync("/api/post", new StringContent(data, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return RedirectToPage("/index");
        }

        [BindProperty]
        public PostCreateModel Post { get; set; }

        public class PostCreateModel
        {
            [Required]
            public int CityId { get; set; }
            [Required]
            public int CategoryId { get; set; }

            [MaxLength(100)]
            [Required]
            public string Name { get; set; }
            
            [MaxLength(256)]
            [Required]
            public string Description { get; set; }

            public string UserId { get; set; }
        }

    }
}
