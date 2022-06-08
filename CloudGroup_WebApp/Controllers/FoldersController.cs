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
    public class FoldersController : ControllerBase
    {
        private readonly cloudgroupContext _cloudgroupcontext;
        public FoldersController(cloudgroupContext context)
        {
            _cloudgroupcontext = context;
        }
        // GET: api/Folders
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_cloudgroupcontext.Folders.ToList());
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
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
        }
        // GET: api/Folders/5
        /*[HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }*/


        // POST: api/Folders
        [HttpPost]
        public void Post([FromBody] FolderRequest value)
        {
            Folders obj = new Folders();

            obj.FName = value.FName;
            obj.CreatedBy = value.CreatedBy;
            obj.CreatedAt = value.CreatedAt;
            obj.IsDeleted = value.IsDeleted;
            _cloudgroupcontext.Folders.Add(obj);
            _cloudgroupcontext.SaveChanges();

        }

        // PUT: api/Folders/5
        [HttpGet("{id}/{value}")]
        public IActionResult Get(int id, string value)
        {
            var result = _cloudgroupcontext.Folders.Where(e => e.CreatedBy == id).Where(o => o.FName.Contains(value));
            return Ok(result);
        }



        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var delete = _cloudgroupcontext.Documents.Where(res => res.FolderId == id).ToList();
            delete.ForEach(res => _cloudgroupcontext.Documents.Remove(res));
            var del = _cloudgroupcontext.Folders.Where(res => res.FId == id).ToList();
            del.ForEach(res => _cloudgroupcontext.Folders.Remove(res));
            _cloudgroupcontext.SaveChanges();
        }
    }
}
