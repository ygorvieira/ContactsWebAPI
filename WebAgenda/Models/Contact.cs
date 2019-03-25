using System.ComponentModel.DataAnnotations;

namespace WebAgenda.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}