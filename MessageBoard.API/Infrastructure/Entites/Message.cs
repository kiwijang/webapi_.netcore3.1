using System;
using System.Collections.Generic;

namespace MessageBoard.API.Infrastructure.Entites
{
    public partial class Message
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
