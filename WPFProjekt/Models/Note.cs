using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFProjekt.Models.Enums;

namespace WPFProjekt.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Priority Priority { get; set; }
        public bool IsDone { get; set; } = false;

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
