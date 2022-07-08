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


        //Show in Dashboard
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var response = _cloudgroupcontext.Folders.Where(obj => obj.CreatedBy == id && obj.IsDeleted == false);
                if (response == null)
                    return NotFound();
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error!");
            }
        }


        //Show in trash
        [HttpGet("Trash/{id}")]
        public IActionResult Trash(int id)
        {
            try
            {
                var response = _cloudgroupcontext.Folders.Where(obj => obj.CreatedBy == id && obj.IsDeleted == true);

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error!");
            }
        }

        //Show in starred 
        [HttpGet("IsStarred/{id}")]
        public IActionResult IsStarred(int id)
        {
            try
            {
                var response = _cloudgroupcontext.Folders.Where(obj => obj.CreatedBy == id && obj.IsFavourite == true && obj.IsDeleted == false);

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
            obj.CreatedAt = DateTime.Now;
            obj.IsDeleted = value.IsDeleted;
            _cloudgroupcontext.Folders.Add(obj);
            _cloudgroupcontext.SaveChanges();

        }

        // PUT: api/Folders/5 
        //Searching
        [HttpGet("{id}/{value}")]
        public IActionResult Get(int id, string value)
        {
            var result = _cloudgroupcontext.Folders.Where(e => e.CreatedBy == id).Where(o => o.FName.Contains(value));
            return Ok(result);
        }
        //soft Delete a Folder
        [HttpPut("SoftDelete/{id}")]
        public IActionResult SoftDelete(int id)
        {
            int a = 0;
            try
            {
                var res = _cloudgroupcontext.Documents.Where(obj => obj.FolderId == id);
                foreach (var x in res)
                {
                    x.IsDeleted = true;
                    _cloudgroupcontext.Documents.Update(x);
                    _cloudgroupcontext.SaveChanges();
                }
                var newobj = _cloudgroupcontext.Folders.First(obj => obj.FId == id);
                newobj.IsDeleted = true;
                _cloudgroupcontext.Folders.Update(newobj);
                _cloudgroupcontext.SaveChanges();
                a = 200;
            }
            catch (Exception e)
            {
                a = 404;
            }
            return StatusCode(a);

        }

        //Starred
        [HttpPut("Starred/{id}")]
        public IActionResult Starred(int id)
        {
            int a = 0;
            try
            {
                var newobj = _cloudgroupcontext.Folders.First(obj => obj.FId == id);
                newobj.IsFavourite = true;
                _cloudgroupcontext.Folders.Update(newobj);
                _cloudgroupcontext.SaveChanges();
                a = 200;
            }
            catch (Exception e)
            {
                a = 404;
            }
            return StatusCode(a);
        }
        //UnStarred
        [HttpPut("UnStarred/{id}")]
        public IActionResult UnStarred(int id)
        {
            int a = 0;
            try
            {
                var newobj = _cloudgroupcontext.Folders.First(obj => obj.FId == id);
                newobj.IsFavourite = false;
                _cloudgroupcontext.Folders.Update(newobj);
                _cloudgroupcontext.SaveChanges();
                a = 200;
            }
            catch (Exception e)
            {
                a = 404;
            }
            return StatusCode(a);
        }

        // Undelete a folder
        [HttpPut("Undelete/{id}")]
        public IActionResult Undelete(int id)
        {
            int a = 0;
            try
            {
                var res = _cloudgroupcontext.Documents.Where(o => o.FolderId == id).ToList();
                foreach (var x in res)
                {
                    x.IsDeleted = false;
                    _cloudgroupcontext.Documents.Update(x);
                    _cloudgroupcontext.SaveChanges();
                }

                var newObj = _cloudgroupcontext.Folders.First(obj => obj.FId == id);
                newObj.IsDeleted = false;
                _cloudgroupcontext.Folders.Update(newObj);
                _cloudgroupcontext.SaveChanges();
                a = 200;
            }
            catch (Exception e)
            {
                a = 404;
            }

            return StatusCode(a);
        }


        [HttpGet("Recent/{userId}/{time}")]
        public IActionResult showRecentFolders(int userId, int time)
        {
            int m = 0;
            try
            {
                if (time == 6)
                {
                    var createdAt = DateTime.Now.AddHours(-6);
                    var res = _cloudgroupcontext.Folders.Where(o => o.CreatedAt >= createdAt && o.CreatedBy == userId && o.IsDeleted == false);
                    return Ok(res);
                }
                else if (time == 1)
                {
                    var createdAt = DateTime.Now.AddHours(-1);
                    var res = _cloudgroupcontext.Folders.Where(o => o.CreatedAt >= createdAt && o.CreatedBy == userId && o.IsDeleted == false);
                    return Ok(res);
                }
                else if (time == 30)
                {
                    var createdAt = DateTime.Now.AddMinutes(-30);
                    var res = _cloudgroupcontext.Folders.Where(o => o.CreatedAt >= createdAt && o.CreatedBy == userId && o.IsDeleted == false);
                    return Ok(res);
                }
                else if (time == 12)
                {
                    var createdAt = DateTime.Now.AddHours(-12);
                    var res = _cloudgroupcontext.Folders.Where(o => o.CreatedAt >= createdAt && o.CreatedBy == userId && o.IsDeleted == false);
                    return Ok(res);
                }
                else
                {
                    var createdAt = DateTime.Now.AddHours(-24);
                    var res = _cloudgroupcontext.Folders.Where(o => o.CreatedAt >= createdAt && o.CreatedBy == userId && o.IsDeleted == false);
                    return Ok(res);
                }
            }
            catch (Exception e)
            {
                m = 404;
                return StatusCode(m);
            }
        }




            // DELETE: api/ApiWithActions/5
            //Hard delete folder
            [HttpDelete("Delete/{id}")]
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

