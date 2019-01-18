using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Pigeon.DataAccess.Entities
{
    public class BaseModel 
    {      
        [Key]
        public virtual int Id { get; set; }
    }
}
