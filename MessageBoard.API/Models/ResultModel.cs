using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MessageBoard.API.Models
{
    public class ResultModel
    {
        public bool IsSuccess { get; set; }
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        [JsonIgnore]
        public object Data { get; set; }
    }
}
