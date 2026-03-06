using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogapibackend.Models;
using blogapibackend.Services.Context;
using Microsoft.EntityFrameworkCore;

namespace blogapibackend.Services
{
    public class BlogItemService
    {
        private readonly DataContext _context;
        public BlogItemService (DataContext context)
        {
            _context = context;
        }
        public bool AddBlogItems(BlogItemModel newBlogItem)
        {
            bool result;

            _context.Add(newBlogItem);
            result = _context.SaveChanges() != 0;
            return result;
        }

        public IEnumerable<BlogItemModel> GetAllBlogItems()
        {
            return _context.BlogInfo;
        }

        public IEnumerable<BlogItemModel> GetBlogItemsByCategory(string category)
        {
            //Built in method from IEnumerable, filters and finds what you enter, iterates through and finds for it and returns back what you need (item are looking for)
            return _context.BlogInfo.Where(item => item.Category == category);
        }

        public List<BlogItemModel> GetItemsByTag(string tag)
        {
            List<BlogItemModel> AllBlogItemsWithTag = new List<BlogItemModel>();
            var allItems = GetAllBlogItems().ToList();
            
            for(int i = 0; i < allItems.Count; i++)
            {
                BlogItemModel Item = allItems[i];
                var itemArr = Item.Tags.Split(",");
                for(int j = 0; j < itemArr.Length; j++)
                {
                    if (itemArr.Contains(tag))
                    {
                        AllBlogItemsWithTag.Add(Item);
                        break;
                    }
                }
            }
            return AllBlogItemsWithTag;
        }

        public IEnumerable<BlogItemModel> GetItemsByDate(string date)
        {
            return _context.BlogInfo.Where(item => item.Date == date);
        }

        public bool UpdateBlogItems(BlogItemModel blogUpdate)
        {
            _context.Update(blogUpdate);
            return _context.SaveChanges() != 0;
        }

        public bool DeleteBlogItem(BlogItemModel blogItemToDelete)
        {
            _context.Update(blogItemToDelete);
            return _context.SaveChanges() != 0;
        }
        public IEnumerable<BlogItemModel> GetPublishedBlogItems()
        {
            return _context.BlogInfo.Where(item => item.isPublished);
            //^^^ We don't need to compare with anything since isPublished from our BlogInfo is a bool so it will tell us if it's there or not.
        }
    }
}