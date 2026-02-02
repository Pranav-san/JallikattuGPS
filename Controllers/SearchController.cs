using Jallikattu.Data.ViewModels;
using Jallikattu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jallikattu.Controllers
{
    public class SearchController : Controller
    {
        private readonly JallikattuGPSEntities db = new JallikattuGPSEntities();

        public ActionResult Index(string SearchString)
        {
            var vm = new SearchResultsVM
            {
                Query = SearchString
            };

            if (string.IsNullOrWhiteSpace(SearchString))
                return View(vm);

            SearchString = SearchString.ToLower();

            vm.CattleBreeds = db.CattleBreedsTables
                .Where(x =>
                    x.BreedName.ToLower().Contains(SearchString) ||
                    x.Description.ToLower().Contains(SearchString))
                .ToList();

            vm.BlogPosts = db.BlogPostsTables
                .Where(x =>
                    x.BlogTitle.ToLower().Contains(SearchString) ||
                    x.BlogContent.ToLower().Contains(SearchString))
                .ToList();

            vm.Products = db.Products
                .Where(x =>
                    x.ProductName.ToLower().Contains(SearchString) ||
                    x.Description.ToLower().Contains(SearchString))
                .ToList();

            return View(vm);
        }
    }
}
