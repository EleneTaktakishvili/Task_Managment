using Microsoft.AspNetCore.Identity;

namespace TaskManagment.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();
    }
}