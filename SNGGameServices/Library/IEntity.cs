using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public interface IEntity
    {
        public int EntityId { get; set; }
        public int EntityType { get; set; }
    }
}
