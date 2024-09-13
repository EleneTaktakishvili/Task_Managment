namespace TaskManagment.Application.DTOs
{
    public class UserTaskDto
    {
        public Guid UserId { get; set; }
        public int TaskId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
