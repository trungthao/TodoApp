using System.Dynamic;
using System.Reflection;
using TodoApp.Contracts;
using TodoApp.Entities.Models;

namespace TodoApp.Service.DataShaping
{
    public class DataShaper<T> : IDataShaper<T> where T : class
    {
        public PropertyInfo[] Properties { get; set; }
        public DataShaper()
        {
            Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        public IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);
            return FetchData(entities, requiredProperties);
        }

        public ShapedEntity ShapeData(T entity, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);
            return FetDataForEntity(entity, requiredProperties);
        }

        private ShapedEntity FetDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new ShapedEntity();
            foreach (var property in requiredProperties)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapedData.Entity.TryAdd(property.Name, objectPropertyValue);
            }
            var idProperty = entity.GetType().GetProperty("Id");
            shapedData.Id = (Guid)idProperty.GetValue(entity);

            return shapedData;
        }

        private IEnumerable<ShapedEntity> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<ShapedEntity>();

            foreach (var entity in entities)
            {
                var shapedObject = FetDataForEntity(entity, requiredProperties);
                shapedData.Add(shapedObject);
            }

            return shapedData;
        }

        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
        {
            var requiredProperties = new List<PropertyInfo>();
            if (!string.IsNullOrWhiteSpace(fieldsString))
            {
                var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var field in fields)
                {
                    var property = Properties.FirstOrDefault(pi => pi.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));

                    if (property == null)
                        continue;

                    requiredProperties.Add(property);
                }
            }
            else
            {
                requiredProperties = Properties.ToList();
            }

            return requiredProperties;
        }
    }
}