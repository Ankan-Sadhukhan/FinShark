using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public String Content { get; set; } =string.Empty;
        public DateTime CraetedOn { get; set; } = DateTime.Now;

        public int? StockId { get; set; }
    }
}