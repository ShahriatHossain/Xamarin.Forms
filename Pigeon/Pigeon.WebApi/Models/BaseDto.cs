using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pigeon.WebApi.Models
{
    public abstract class BaseDto<TEntity>
    {        
        public int Id { get; protected set; }

        public abstract void Map(TEntity entity);     
    }
}
