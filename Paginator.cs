using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Webshop.Paginator
{
    public class Page<T>
    {
        public int Index{get;set;}
        public T[] Items{get;set;}
        public int TotalPages{get;set;}
    }
}
