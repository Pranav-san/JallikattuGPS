using Jallikattu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jallikattu.Data.ViewModels
{
    public class SearchResultsVM
    {
        public string Query { get; set; }

        public List<CattleBreedsTable> CattleBreeds { get; set; }
        public List<BlogPostsTable> BlogPosts { get; set; }
        public List<Product> Products { get; set; }
    }
}