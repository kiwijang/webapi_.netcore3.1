using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MessageBoard.API.Models
{
    public class MsgModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required(ErrorMessage = "必填欄位")]
        [StringLength(30,ErrorMessage ="長度不得超過 30 字")]
        public string Name { get; set; }
        [Required(ErrorMessage = "必填欄位")]
        [StringLength(30, ErrorMessage = "長度不得超過 30 字")]
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
