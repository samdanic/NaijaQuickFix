﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaijaQuickFix.Helper
{
    public class PagedData<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
    }
}