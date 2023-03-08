using System.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Entities.Models;

namespace TodoApp.Contracts
{
    public interface IDataShaper<T>
    {
        IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string fieldsString);

        ShapedEntity ShapeData(T entity, string fieldsString);
    }
}