using RedBusService.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace RedBusService.Requests
{
    public class RegisterRequest
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string phone { get; set; }

        [Eighteenplus]
        public int Age { get; set; }

        public ChooseRole ChooseRole {  get; set; }
    }
    public enum ChooseRole
    {
        Admin=1,User,Driver
    }
}

