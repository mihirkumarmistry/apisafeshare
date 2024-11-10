using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeShareAPI.Model
{
    public class User
    {
        [Key] public int Id { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
        [Required] public int UserTypeId { get; set; }
        [ForeignKey(nameof(UserTypeId))] public UserType UserType { get; set; }

        [Required] public bool IsActive { get; set; } = true;
        [Required] public bool IsDeleted { get; set; } = false;
        [Required] public bool IsUniversal { get; set; } = false;
        [Required] public bool IsNewPassword { get; set; } = false;

        [NotMapped] public string UserTypeName { get; set; }
        [NotMapped] public string AccessToken { get; set; }
    }
    public class UserType
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public bool IsAdmin { get; set; } = false;
        [Required] public bool IsActive { get; set; } = true;
    }
}
