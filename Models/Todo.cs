using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniTodo.Models
{
    //public record Todo(Guid Id, string Title, bool Done);
    public class Todo
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public bool Done { get; set; }
    }
}