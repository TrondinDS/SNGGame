using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.Query.QueryModels.StudioGame.Studio;

public class QueryStudio
{
    //[Range(1, int.MaxValue, ErrorMessage = "Значение StudioId должно быть больше или равно 1.")]
    public Guid? StudioId { get; set; }
}
