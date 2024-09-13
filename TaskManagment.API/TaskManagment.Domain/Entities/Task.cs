using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace TaskManagment.Domain.Entities
{
    public class Task
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = default!;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = default!;

        [Required]
        public int Priority { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.New; // Enum property
        public ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();
    }

    public enum TaskStatus
    {
        [Description("New")]
        New = 0,

        [Description("InProgress")]
        InProgress = 1,

        [Description("Done")]
        Done = 2,
    }
}