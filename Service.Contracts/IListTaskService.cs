using TodoApp.Shared.DataTransferObjects;

namespace TodoApp.Service.Contracts
{
    public interface IListTaskService
    {
        Task<IEnumerable<ListTaskDto>> GetAllListTaskAsync(bool trackChanges);

        Task<ListTaskDto> GetListTaskAsync(Guid listTaskId, bool trackChanges);

        Task<ListTaskDto> CreateListTaskAsync(ListTaskForCreationDto listTaskDto);

        Task<IEnumerable<ListTaskDto>> GetListTaskByIdsAsync(IEnumerable<Guid> ids);

        Task<(IEnumerable<ListTaskDto> listTasks, string ids)> CreateListTaskCollectionAsync(IEnumerable<ListTaskForCreationDto> listTaskCollection);

        Task DeleteListTaskAsync(Guid listTaskId, bool trackChanges);

        Task UpdateListTaskAsync(Guid listTaskId, ListTaskForUpdateDto listTaskForUpdateDto, bool trackChanges);
    }
}