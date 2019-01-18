using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pigeon.WebApi.Models
{
    public enum ErrorCode
    {
        InvalidInput,        
        RecordNotFound,
        CouldNotCreateItem,
        CouldNotUpdateItem,
        CouldNotDeleteItem
    }
}
