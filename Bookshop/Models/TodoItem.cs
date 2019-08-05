using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookshop.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string ItemName { get; set; }
        public bool IsComplete { get; set; }
    }
}
