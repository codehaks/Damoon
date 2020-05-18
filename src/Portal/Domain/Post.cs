using Portal.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain
{
    public class Post:ITimeCreated
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public Byte[] ImageData { get; set; }
        public DateTime TimeCreated { get; set; }

        public int Price { get; set; }
        public int CityId { get; set; }
        public Guid UserId { get; set; }

    }
}
