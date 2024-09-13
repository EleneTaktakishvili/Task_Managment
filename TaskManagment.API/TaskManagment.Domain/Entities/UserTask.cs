using System.ComponentModel.DataAnnotations;

namespace TaskManagment.Domain.Entities
{
    public class UserTask
    {        
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        [Required]
        public int TaskId { get; set; }
        public Task Task { get; set; } = default!;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ModifiedDate { get; set; } = DateTime.Now; //additional property
    }
}