using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Portal.Web.Controllers
{
    [Route("api/post/rating")]
    [ApiController]
    public class PostRatingController : ControllerBase
    {
        public IActionResult Post([FromBody]PostRatingModel model)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();


            channel.QueueDeclare(queue: "inbox",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);



            string msg = JsonConvert.SerializeObject(model);
            var body = Encoding.UTF8.GetBytes(msg);

            channel.BasicPublish(exchange: "",
                 routingKey: "post_rate",
                 basicProperties: null,
                 body: body);


            return Ok();
        }
    }

    public class PostRatingModel
    {
        public string PostId { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; }

    }
}
