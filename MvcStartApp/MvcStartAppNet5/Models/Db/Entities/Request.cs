using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace MvcStartAppNet5.Models.Db.Entities
{
    [Table("Requests")]
    public class Request
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
    }
}
