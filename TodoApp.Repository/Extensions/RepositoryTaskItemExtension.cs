using System.Reflection;
using System.Text;
using TodoApp.Entities.Models;
using System.Linq.Dynamic.Core;
using TodoApp.Repository.Extensions.Utility;

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

        public static IQueryable<TaskItem> Sort(this IQueryable<TaskItem> taskItems, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return taskItems.OrderBy(t => t.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<TaskItem>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return taskItems.OrderBy(t => t.Name);

            return taskItems.OrderBy(orderQuery);
        }
    }
}