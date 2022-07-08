using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CloudGroup_WebApp.Models;
using CloudGroup_WebApp.RequestModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudGroup_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly cloudgroupContext _cloudgroupcontext;
        private readonly IHostingEnvironment _env;

        public DocumentsController(cloudgroupContext context, IHostingEnvironment environment)
        {
            _cloudgroupcontext = context;
            _env = environment;
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

        [HttpPost]
        [Route("upload/{createdBy}/{createdAt}/{folderId}")]
        public IActionResult post(int createdBy, DateTime createdAt, int folderId)
        {
            
            if (Request.Form.Files.Count() > 0)
            {
                string abc = "aaa";
            }
            IFormFile file = Request.Form.Files[0];



            var RootPath = Path.Combine(_env.ContentRootPath, "Resources", "Documents");
            if (!Directory.Exists(RootPath))
                Directory.CreateDirectory(RootPath);
            for (var i = 0; i < Request.Form.Files.Count(); i++)
            {
                var filePath = Path.Combine(RootPath, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {



                    Documents obj = new Documents();
                    {
                        obj.DName = file.FileName;
                        obj.ContentType = file.ContentType;
                        obj.Size = (int)file.Length;
                        obj.CreatedBy = createdBy;
                        obj.CreatedAt = createdAt;
                        obj.FolderId = folderId;
                        obj.IsDeleted = false;

                    };
                    file.CopyTo(stream);
                    _cloudgroupcontext.Documents.Add(obj);
                    _cloudgroupcontext.SaveChanges();
                }
            }
           
            return Ok();

        }

        //soft Delete of folders from drive
        [HttpPut("SoftDelete/{id}")]
        public IActionResult SoftDelete(int id)
        {
            int a = 0;
            try
            {
                var newobj = _cloudgroupcontext.Documents.First(obj => obj.FolderId == id);
                newobj.IsDeleted = true;
                _cloudgroupcontext.Documents.Update(newobj);
                _cloudgroupcontext.SaveChanges();
                a = 200;   
            }
            catch(Exception e)
            {
                a = 404;
            }
            return StatusCode(a);
        }
        //soft deleted folder get undelete
        [HttpPut("Undelete/{id}")]
        public IActionResult Undelete(int id)
        {
            int a = 0;
            try
            {
                var newobj = _cloudgroupcontext.Documents.First(obj => obj.FolderId == id);
                newobj.IsDeleted = false;
                _cloudgroupcontext.Documents.Update(newobj);
                _cloudgroupcontext.SaveChanges();
                a = 200;
            }
            catch(Exception e)
            {
                a = 404;
            }
            return StatusCode(a);
        }
        //Starred the file
        [HttpPut("Starred/{id}")]
        public IActionResult Starred(int id)
        {
            int a = 0;
            try
            {
                var newobj = _cloudgroupcontext.Documents.First(obj => obj.FolderId == id);
                newobj.IsFavourite = true;
                _cloudgroupcontext.Documents.Update(newobj);
                _cloudgroupcontext.SaveChanges();
                a = 200;
            }
            catch(Exception e)
            {
                a = 404;
            }
            return StatusCode(a);
        }
        //UnStarred the file
        [HttpPut("UnStarred/{id}")]
        public IActionResult UnStarred(int id)
        {
            int a = 0;
            try
            {
                var newobj = _cloudgroupcontext.Documents.First(obj => obj.FolderId == id);
                newobj.IsFavourite = false;
                _cloudgroupcontext.Documents.Update(newobj);
                _cloudgroupcontext.SaveChanges();
                a = 200;
            }
            catch (Exception e)
            {
                a = 404;
            }
            return StatusCode(a);
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
