using System;

namespace FrwkBootCampFidelidade.Core.RabbitMq
{
    public class MessageInputModel
    {
        public string Queue { get; set; }
        public string Method { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
