using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Entities.LinkModels
{
    public class LinkResourceBase
    {
        public LinkResourceBase()
        {
        }

        public List<Link> Links { get; set; } = new List<Link>();
    }
}