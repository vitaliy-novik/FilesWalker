using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public interface IRole : IEntity
    {
        string Name { get; set; }
    }
}
