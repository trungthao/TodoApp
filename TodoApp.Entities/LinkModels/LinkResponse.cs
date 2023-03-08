using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Entities.Models;

namespace TodoApp.Entities.LinkModels
{
    public class LinkResponse
    {
        public bool HasLinks { get; set; }

        public List<Entity> ShapedEntities { get; set; }

        public LinkCollectionWrapper<Entity> LinkedEntities { get; set; }

        public LinkResponse()
        {
            LinkedEntities = new LinkCollectionWrapper<Entity>();
            ShapedEntities = new List<Entity>();
        }
    }
}