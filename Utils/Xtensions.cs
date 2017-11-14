namespace Webshop.Utils.Xtensions
{
    using Microsoft.EntityFrameworkCore;

    using Webshop.Utils.Paginator;
    using Webshop.Utils.Xtratypes;

    using System;
    using System.Linq;
    using System.Collections.Generic;
    public static class Xtensions
    {
        public static Page<T> GetPage<T>(this IEnumerable<T> list, int page_index, int page_size, Func<T, object> order_by_selector)
        {
            
            var res = list.OrderBy(order_by_selector).Skip(page_index*page_size).Take(page_size).ToArray();

            if(res == null || res.Length == 0)return null;

            var tot_items = list.Count();
            var tot_pages = tot_items / page_size;
            if(tot_items < page_size) tot_pages =1;

            return new Page<T>{Index = page_index,Items = res,TotalPages = tot_pages};
        }

        public static string GenderToString(this Gender gender)
        {
            switch(gender)
            {
                case Gender.MAN:
                     return "MAN";
                case Gender.WOMAN:
                     return "WOMAN";
                case Gender.UNISEX:
                     return "UNISEX";
                case Gender.UNSPECIFIED:
                     return "UNSPECIFIED";
                default: 
                      return "None";
                   
            }
        }

        public static string FormatPrice(this double price)
        {
            //100000 => 1000.000.000
            return String.Format("{0}",price);
        }
    }
}