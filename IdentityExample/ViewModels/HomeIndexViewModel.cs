using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityExample.Models;
using PagedList;
using PagedList.Core;
namespace IdentityExample.ViewModels
{
    public class HomeIndexViewModel
    {
        public IPagedList<Product> Products { get; set; }

        public string CurrentCategory { get; set; }

        public int PageNumber { get; set; }
    }
}
