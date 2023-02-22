using System.Security;
using System.Net;
using AutoMapper;
using TodoApp.Contracts;
using TodoApp.Entities.Exceptions;
using TodoApp.Entities.Models;
using TodoApp.Service.Contracts;
using TodoApp.Shared.DataTransferObjects;

namespace TodoApp.Service;
public class ListTaskService : IListTaskService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public ListTaskService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ListTaskDto> CreateListTaskAsync(ListTaskForCreationDto listTaskForCreationDto)
    {
        var listTaskEntity = _mapper.Map<ListTask>(listTaskForCreationDto);
        _repositoryManager.ListTask.CreateListTask(listTaskEntity);
        await _repositoryManager.SaveAsync();

        var listTaskToReturn = _mapper.Map<ListTaskDto>(listTaskEntity);
        return listTaskToReturn;
    }

    public async Task<(IEnumerable<ListTaskDto> listTasks, string ids)> CreateListTaskCollectionAsync(IEnumerable<ListTaskForCreationDto> listTaskCollection)
    {
        if (listTaskCollection is null)
            throw new ListTaskCollectionBadRequest();

        var listTaskEntities = _mapper.Map<IEnumerable<ListTask>>(listTaskCollection);
        foreach (var listTaskEntity in listTaskEntities)
        {
            _repositoryManager.ListTask.CreateListTask(listTaskEntity);
        }
        await _repositoryManager.SaveAsync();

        var listTaskCollectionToReturn = _mapper.Map<IEnumerable<ListTaskDto>>(listTaskEntities);
        var ids = string.Join(",", listTaskCollectionToReturn.Select(x => x.Id));

        return (listTaskCollectionToReturn, ids);
    }

    public async Task DeleteListTaskAsync(Guid listTaskId, bool trackChanges)
    {
        var listTask = await GetListTaskAndCheckIfExists(listTaskId, trackChanges);

        _repositoryManager.ListTask.DeleteListTask(listTask);
        await _repositoryManager.SaveAsync();
    }

    public async Task<IEnumerable<ListTaskDto>> GetAllListTaskAsync(bool trackChanges)
    {
        var listTask = await _repositoryManager.ListTask.GetAllListTaskAsync(trackChanges);
        var listTaskDto = _mapper.Map<IEnumerable<ListTaskDto>>(listTask);
        return listTaskDto;
    }

    public async Task<ListTaskDto> GetListTaskAsync(Guid listTaskId, bool trackChanges)
    {
        var listTask = await GetListTaskAndCheckIfExists(listTaskId, trackChanges);

        var listTaskDto = _mapper.Map<ListTaskDto>(listTask);

        return listTaskDto;
    }

    public async Task<IEnumerable<ListTaskDto>> GetListTaskByIdsAsync(IEnumerable<Guid> ids)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();

        var listTaskEntities = await _repositoryManager.ListTask.GetListTaskByIdsAsync(ids, trackChanges: false);
        if (ids.Count() != listTaskEntities.Count())
            throw new CollectionByIdsBadRequestException();

        var listTaskToReturn = _mapper.Map<IEnumerable<ListTaskDto>>(listTaskEntities);

        return listTaskToReturn;
    }

    public async Task UpdateListTaskAsync(Guid listTaskId, ListTaskForUpdateDto listTaskForUpdateDto, bool trackChanges)
    {
        var listTaskEntity = await GetListTaskAndCheckIfExists(listTaskId, trackChanges);

        _mapper.Map(listTaskForUpdateDto, listTaskEntity);
        await _repositoryManager.SaveAsync();
    }

    private async Task<ListTask> GetListTaskAndCheckIfExists(Guid id, bool trackChanges)
    {
        var listTaskEntity = await _repositoryManager.ListTask.GetListTaskAsync(id, trackChanges);
        if (listTaskEntity is null)
            throw new ListTaskNotFoundException(id);

        return listTaskEntity;
    }
}

