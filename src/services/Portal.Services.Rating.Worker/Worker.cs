using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Portal.Services.Ratings.Api.Data;
using Portal.Services.Ratings.Worker.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Portal.Services.Rating.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly AppDbContext _db;
        public Worker(ILogger<Worker> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "post_rate", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;

            channel.BasicConsume(queue: "post_rate", autoAck: true, consumer: consumer);

            await Task.CompletedTask;
        }

        private async void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            
            var body = e.Body;
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation(message);
            var model = JsonConvert.DeserializeObject<PostRating>(message);
            _db.PostRatings.Add(model);
            await _db.SaveChangesAsync();
            _logger.LogInformation("Save to database", model);
            
        }
    }
}
