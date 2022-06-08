using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudGroup_WebApp.Models;
using CloudGroup_WebApp.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudGroup_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly cloudgroupContext _cloudgroupcontext;
        public DocumentsController(cloudgroupContext context)
        {
            _cloudgroupcontext = context;
        }
        // GET: api/Documents
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_cloudgroupcontext.Documents.ToList());
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var response = _cloudgroupcontext.Documents.Where(obj => obj.CreatedBy == id);
                if (response == null)
                    return NotFound();
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error!");
            }
        }

        // GET: api/Documents/5
        /* [HttpGet("{id:int}")]
         public string Get(int id)
         {
             try
             {
                 var response = _cloudgroupcontext.Folders.Where(obj => obj.CreatedBy == id);
                 if (response == null)
                     return NotFound();
                 return Ok(response);
             }
             catch (Exception)
             {
                 return StatusCode(StatusCodes.Status500InternalServerError, "Error!");
             }
         }*/

        // POST: api/Documents
        [HttpPost]
        public void Post([FromBody] DocumentRequest value)
        {
            Documents obj = new Documents();

            obj.DName = value.DName;
            obj.ContentType = value.ContentType;
            obj.Size = value.Size;
            obj.CreatedBy = value.CreatedBy;
            obj.CreatedAt = value.CreatedAt;
           // obj.FolderId = value.FolderId;
            obj.IsDeleted = value.IsDeleted;
            _cloudgroupcontext.Documents.Add(obj);
            _cloudgroupcontext.SaveChanges();
        }

        // PUT: api/Documents/5
        [HttpGet("{id}/{value}")]
        public IActionResult Get(int id, string value)
        {
            var result = _cloudgroupcontext.Documents.Where(e => e.CreatedBy == id).Where(o => o.DName.Contains(value));
            return Ok(result);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var delete = _cloudgroupcontext.Documents.Where(res => res.DId == id).ToList();
            delete.ForEach(res => _cloudgroupcontext.Documents.Remove(res));
            _cloudgroupcontext.SaveChanges();
        }
    }
}
