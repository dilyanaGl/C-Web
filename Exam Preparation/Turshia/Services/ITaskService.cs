using Turshia.Models;

namespace Turshia.Services
{
    public interface ITaskService
    {
        IndexViewModel All();
        TaskDetailsModel Details(int taskId);
        void Report(int taskId, string username);
        void Create(CreateTaskModel model);
    }
}