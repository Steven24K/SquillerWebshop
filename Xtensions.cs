namespace Webshop.Xtensions
{
    using Microsoft.EntityFrameworkCore;
    using Webshop.Paginator;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    public static class Xtensions
    {
        //This extension method returns a new page and can be used for any DbSet, the other is exactly the
        //same but is for a IQueryable object.
        public static Page<T> GetPage<T>(this DbSet<T> list,int page_index, int page_size, Func<T, object> order_by_selector)
        where T : class
        {
            var res = list.OrderBy(order_by_selector).Skip(page_index * page_size).Take(page_size).ToArray();

            if(res == null || res.Length == 0)return null;

            var tot_items = list.Count();

            var tot_pages = tot_items / page_size;
            if(tot_items < page_size) tot_pages = 1;

            return new Page<T>{Index = page_index,Items = res, TotalPages = tot_pages };
        }
        
        public static Page<T> GetPage<T>(this IEnumerable<T> queryable, int page_index, int page_size, Func<T, object> order_by_selector)
        {
            
            var res = queryable.OrderBy(order_by_selector).Skip(page_index*page_size).Take(page_size).ToArray();

            if(res == null || res.Length == 0)return null;

            var tot_items = queryable.Count();
            var tot_pages = tot_items / page_size;
            if(tot_items < page_size) tot_pages =1;

            return new Page<T>{Index = page_index,Items = res,TotalPages = tot_pages};
        }
    }
}