using System;
using TodoApp.Service.Contracts;

namespace TodoApp.Service.Contracts
{
	public interface IServiceManager
	{
		IListTaskService ListTaskService { get; }

		ITaskItemService TaskItemService { get; }
	}
}

