using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Genre;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.DB.DTO.DTOModelObjects.Game
{
    public class CardGameDTO
    {
        public int GameId { get; set; }
        public string GameTitle { get; set; }
        public int ImageId { get; set; }

        public string StudioTitle { get; set; }

        public List<GenreDTO> Genres { get; set; }
    }
}
