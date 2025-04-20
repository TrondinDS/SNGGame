using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.Query.QueryModels.StudioGame.Library;

public class QueryLibrary
{
    [Range(1, 10, ErrorMessage = "Значение Rating должно быть больше или равно 1 и меньше или равно 10.")]
    public int? Rating { get; set; }
    public Guid? gameInLibraryUserId { get; set; }
}
