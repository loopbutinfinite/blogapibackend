using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogapibackend.Models;
using blogapibackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace blogapibackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly BlogItemService _data;
        public BlogController(BlogItemService dataFromService)
        {
            _data = dataFromService;
        }

        //AddBlogItems
        [HttpPost("AddBlogItems")]
        public bool AddBlogItems(BlogItemModel newBlogItem)
        {
            return _data.AddBlogItems(newBlogItem);
        }

        //GetBlogItems
        [HttpGet("GetBlogItems")]
        public IEnumerable<BlogItemModel> GetAllBlogItems()
        {
            return _data.GetAllBlogItems();
        }

        //GetBlogItemsByCategory
        [HttpGet("GetBlogItemsByCategory/{category}")]
        public IEnumerable<BlogItemModel> GetBlogItemsByCategory(string category)
        {
            return _data.GetBlogItemsByCategory(category);
        }

        //GetItemsByTag
        [HttpGet("GetItemsByTag/{tag}")]
        public List<BlogItemModel> GetItemsByTag(string tag)
        {
            return _data.GetItemsByTag(tag);
        }

        //GetItemsByDate
        [HttpGet("GetItemsByDate/{Date}")]
        public IEnumerable<BlogItemModel> GetItemsByDate(string date)
        {
            return _data.GetItemsByDate(date);
        }

        //UpdateBlogItems
        [HttpPut("UpdateBlog")]
        public bool UpdateBlogItems(BlogItemModel updateBlog)
        {
            return _data.UpdateBlogItems(updateBlog);
        }

        //DeleteBlogItems
        [HttpPut("DeleteBlogItem/{BlogToDelete}")]
        public bool DeleteBlogItem(BlogItemModel BlogToDelete)
        {
            return _data.DeleteBlogItem(BlogToDelete);
        }

        //GetPublishedBlogItems
        [HttpGet("GetPublishedBlogItems")]
        public IEnumerable<BlogItemModel> GetPublishedBlogItems()
        {
            return _data.GetPublishedBlogItems();
        }
    }
}