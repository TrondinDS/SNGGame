using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Comment;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.DB.DTO.DTOModelView.UserActivityService.Topic
{
    public class TopicDTOView
    {
        public TopicDTO Topic { get; set; }
        public List<CommentDTO>? CommentDTOs {get; set;}
    }
}
