using BlazorServer.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class User : BaseEntity
    {
        public User(){}

        public User(string v1, string v2)
        {
            this.Email = v1;
            this.Password = v2;
        }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Admin { get; set; }

    }
}
