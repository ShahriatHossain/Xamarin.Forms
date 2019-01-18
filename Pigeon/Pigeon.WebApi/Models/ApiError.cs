using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Pigeon.WebApi.Models
{
    public class ApiError
    {
        internal ApiError(HttpStatusCode status, string title, string details = null)
        {            
            this.StatusCode = status;
            this.Title = title;
            this.Details = details;
        }        
        

        public HttpStatusCode StatusCode { get; }
        
        public string Title { get; }        
        
        public string Details { get; }       
        
    }
}
