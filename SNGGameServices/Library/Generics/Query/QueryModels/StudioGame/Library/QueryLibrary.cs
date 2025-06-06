using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.Query.QueryModels.StudioGame.Library;

public class QueryLibrary
{
    public Guid? UserId { get; set; }
    [Range(1, 10, ErrorMessage = "Значение Rating должно быть от 1 до 10.")]
    public int? Rating { get; set; }
}
