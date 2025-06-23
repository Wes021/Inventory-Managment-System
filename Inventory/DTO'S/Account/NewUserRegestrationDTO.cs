using System.ComponentModel.DataAnnotations;

namespace Inventory.DTO_S.Account
{
    public class NewUserRegestrationDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int GenderId { get; set; }

        public int StatusId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password must match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Phone Number Is required")]
        public string PhoneNumber { get; set; }

        [Compare("PhoneNumber", ErrorMessage = "Phone number must match")]
        public string ConfirmPhoneNumber { get; set; }
    }
}
