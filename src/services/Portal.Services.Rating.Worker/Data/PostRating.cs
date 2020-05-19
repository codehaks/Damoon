using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Services.Ratings.Api.Data
{
    public class PostRating
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public DateTime TimeCreated { get; set; }
        public int Rate { get; set; }
    }
}
