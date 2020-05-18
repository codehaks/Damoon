using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polaris.Api.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Byte[] ImageData { get; set; }
        public DateTime TimeCreated { get; set; }

        public string UserId { get; set; }
    }
}
