using TodoApp.Entities.Models;

namespace TodoApp.Repository.Extensions
{
    public static class RepositoryTaskItemExtension
    {
        public static IQueryable<TaskItem> FilterTaskItems(this IQueryable<TaskItem> taskItems, DateTime fromDate, DateTime toDate)
        {
            return taskItems.Where(t => t.DueDate >= fromDate && t.DueDate <= toDate);
        }

        public static IQueryable<TaskItem> Search(this IQueryable<TaskItem> taskItems, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return taskItems;

            var lowerSearchTerm = searchTerm.ToLower();
            return taskItems.Where(t => t.Name.ToLower().Contains(lowerSearchTerm));
        }
    }
}