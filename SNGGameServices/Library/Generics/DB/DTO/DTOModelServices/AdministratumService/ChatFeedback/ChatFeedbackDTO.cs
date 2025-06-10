using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ChatFeedback
{
    public class ChatFeedbackDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
    }
}
