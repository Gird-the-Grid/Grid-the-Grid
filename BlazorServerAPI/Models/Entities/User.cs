using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BlazorServerAPI.Models.Entities
{
    public class User : BaseEntity
    {
        

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Admin { get; set; }

        public bool Activated { get; set; }

        public User() : this( "test@email.com", "testPassword1")
        { }

        public User(string email, string password) : base()
        {
            Email = email;
            Password = password;
            Admin = false;
            Activated = false;
        }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
