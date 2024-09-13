using TaskManagment.Domain.Entities;

namespace TaskManagment.Application.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }      
        public string Title { get; set; } = default!;       
        public string Description { get; set; } = default!;       
        public int Priority { get; set; }
        public int Status { get; set; } 
        
    }
}
