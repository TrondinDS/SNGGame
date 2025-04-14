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
        public GameDTO Game { get; set; }
        public StudioDTO Studio { get; set; }
        public List<GenreDTO> Genres { get; set; }

        // public ImgDTO Image { get; set; }
    }
}
