using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.Query.QueryModels.StudioGame.Game
{
    public class QueryGame
    {
        public string? TitleGame { get; set; }
        public string? CountryDevelopment { get; set; }
        public string? Platform { get; set; }
        public DateTime? ReleaseYearFrom { get; set; }
        public DateTime? ReleaseYearBefore { get; set; }
    }
}
