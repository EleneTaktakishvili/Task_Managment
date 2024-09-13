using AutoMapper;
using TaskManagment.Application.DTOs;
using TaskManagment.Application.Interfaces;

namespace TaskManagment.Application.Services
{
    public class TaskService
    {
        private readonly IRepository<Domain.Entities.Task> _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(IRepository<Domain.Entities.Task> taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TaskDto>> GetAllAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }
        public async Task<TaskDto> GetByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            return _mapper.Map<TaskDto>(task);
        }
        public void AddAsync(TaskDto taskDto)
        {
            var task = _mapper.Map<Domain.Entities.Task>(taskDto);
            _taskRepository.AddAsync(task);
        }
        public void UpdateAsync(TaskDto taskDto)
        {
            var task = _mapper.Map<Domain.Entities.Task>(taskDto);
            _taskRepository.UpdateAsync(task);
        }
        public void DeleteAsync(int id)
        {
            _taskRepository.DeleteAsync(id);
        }
    }
}
