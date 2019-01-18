using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pigeon.DataAccess.Entities;
using Pigeon.DataAccess.Repository;
using Pigeon.Framework.Exceptions;
using Pigeon.WebApi.Models;
using Pigeon.WebApi.WebApi;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pigeon.WebApi.Controllers
{
    [ValidateModel]
    [Authorize]
    public abstract class CrudController<TEntity, TDto> : BaseController
        where TDto : BaseDto<TEntity>, new()
        where TEntity : BaseModel
    {
        protected abstract IRepository<TEntity> Repository { get; }

        [HttpGet]
        public virtual IActionResult Get()
        {   
            return Ok(this.Repository.Get().Select(ToDto));
        }
        [HttpGet("{id}")]
        public virtual IActionResult Get(int id)
        {
            return Ok(ToDto(this.GetById(id)));
        }

        protected TEntity GetById(int id)
        {
            var entity = this.Repository.GetById(id);
            if (entity == null)
            {
                throw new ResourceObjectNotFoundException(typeof(TEntity).Name, id.ToString());
            }
            return entity;
        }

        [HttpPost]
        [ValidateModel]
        public virtual IActionResult Post([FromBody]TEntity item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest(ErrorCode.InvalidInput.ToString());
            }
            item = this.Repository.Save(item);
            return Ok(ToDto(item));
        }

        [HttpPut("{id}")]
        public virtual IActionResult Put(int id, [FromBody]TEntity item)
        {                  
            //item.Id = id;
            //this.Repository.Save(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual IActionResult Delete(int id)
        {
            var item = this.GetById(id);
            this.Repository.Delete(item);
            return NoContent();
        }

        protected virtual TDto ToDto(TEntity entity)
        {
            var dto = new TDto();
            dto.Map(entity);
            return dto;
        }
    }
}
