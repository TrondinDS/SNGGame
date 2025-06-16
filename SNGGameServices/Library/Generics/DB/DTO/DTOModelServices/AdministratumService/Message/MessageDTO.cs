using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.DB.DTO.DTOModelServices.AdministratumService.Message
{
    public class MessageDTO
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string Content { get; set; }
        public DateTime Date { get; set; }

        // Навигационные свойства
        public Guid ChatFeedbackId { get; set; }
    }
}
