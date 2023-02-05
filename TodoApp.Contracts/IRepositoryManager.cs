using System;
namespace TodoApp.Contracts
{
	public interface IRepositoryManager
	{
		IListTaskRepository ListTask { get; }
		ITaskItemRepository TaskItem { get; }

		void Save();
	}
}

