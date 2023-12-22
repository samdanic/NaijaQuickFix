using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaijaQuickFix.Models.Paging
{
    public class PageModel
    {
        public int CurrentPage { get; set; }
        public int NumberOfPages { get; set; }
        public string Url { get; set; }
        public PageModel(int CurrentPage, int NumberOfPages, string Url)
        {
            this.CurrentPage = CurrentPage;
            this.NumberOfPages = NumberOfPages;
            this.Url = Url;
        }
        public PageModel()
        {

        }
    }
}