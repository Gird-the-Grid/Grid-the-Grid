using BlazorServer.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Admin { get; set; }

    }
}
