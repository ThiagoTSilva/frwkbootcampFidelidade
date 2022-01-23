using System;

namespace FrwkBootCampFidelidade.Dominio.Base
{
    public class MessageInputModel
    {
        public string Queue { get; set; }
        public string Method { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public MessageInputModel(string queue, string method, string content)
        {
            Queue = queue;
            Method = method;
            Content = content;
        }
    }
}
