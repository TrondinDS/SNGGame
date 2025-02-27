using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public interface IIsDeleted
    {
        public bool IsDeleted { get; set; }
        public DateTime DateDeleted { get; set; }
    }
}
