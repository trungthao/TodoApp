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

    public ListTaskDto CreateListTask(ListTaskForCreationDto listTaskForCreationDto)
    {
        var listTaskEntity = _mapper.Map<ListTask>(listTaskForCreationDto);
        _repositoryManager.ListTask.CreateListTask(listTaskEntity);
        _repositoryManager.Save();

        var listTaskToReturn = _mapper.Map<ListTaskDto>(listTaskEntity);
        return listTaskToReturn;
    }

    public (IEnumerable<ListTaskDto> listTasks, string ids) CreateListTaskCollection(IEnumerable<ListTaskForCreationDto> listTaskCollection)
    {
        if (listTaskCollection is null)
            throw new ListTaskCollectionBadRequest();

        var listTaskEntities = _mapper.Map<IEnumerable<ListTask>>(listTaskCollection);
        foreach (var listTaskEntity in listTaskEntities)
        {
            _repositoryManager.ListTask.CreateListTask(listTaskEntity);
        }
        _repositoryManager.Save();

        var listTaskCollectionToReturn = _mapper.Map<IEnumerable<ListTaskDto>>(listTaskEntities);
        var ids = string.Join(",", listTaskCollectionToReturn.Select(x => x.Id));

        return (listTaskCollectionToReturn, ids);
    }

    public IEnumerable<ListTaskDto> GetAllListTask(bool trackChanges)
    {
        var listTask = _repositoryManager.ListTask.GetAllListTask(trackChanges);
        var listTaskDto = _mapper.Map<IEnumerable<ListTaskDto>>(listTask);
        return listTaskDto;
    }

    public ListTaskDto GetListTask(Guid listTaskId, bool trackChanges)
    {
        var listTask = _repositoryManager.ListTask.GetListTask(listTaskId, trackChanges);
        if (listTask == null)
            throw new ListTaskNotFoundException(listTaskId);

        var listTaskDto = _mapper.Map<ListTaskDto>(listTask);

        return listTaskDto;
    }

    public IEnumerable<ListTaskDto> GetListTaskByIds(IEnumerable<Guid> ids)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();

        var listTaskEntities = _repositoryManager.ListTask.GetListTaskByIds(ids, trackChanges: false);
        if (ids.Count() != listTaskEntities.Count())
            throw new CollectionByIdsBadRequestException();

        var listTaskToReturn = _mapper.Map<IEnumerable<ListTaskDto>>(listTaskEntities);

        return listTaskToReturn;
    }
}

