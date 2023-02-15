using TodoApp.Shared.DataTransferObjects;

namespace TodoApp.Service.Contracts
{
    public interface IListTaskService
    {
        IEnumerable<ListTaskDto> GetAllListTask(bool trackChanges);

        ListTaskDto GetListTask(Guid listTaskId, bool trackChanges);

        ListTaskDto CreateListTask(ListTaskForCreationDto listTaskDto);

        IEnumerable<ListTaskDto> GetListTaskByIds(IEnumerable<Guid> ids);

        (IEnumerable<ListTaskDto> listTasks, string ids) CreateListTaskCollection(IEnumerable<ListTaskForCreationDto> listTaskCollection);

        void DeleteListTask(Guid listTaskId, bool trackChanges);

        void UpdateListTask(Guid listTaskId, ListTaskForUpdateDto listTaskForUpdateDto, bool trackChanges);
    }
}