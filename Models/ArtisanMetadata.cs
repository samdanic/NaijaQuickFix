using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NaijaQuickFix.Models
{
    public class ArtisanMetadata
    {
        public string FullName { get; set; }
        public string JobDescription { get; set; }
        public string Phone { get; set; }

    }
    [MetadataType(typeof(ArtisanMetadata))]
    public partial class Artisan
    {
        public HttpPostedFileBase[] files { get; set; }
        public List<int> pastfiles { get; set; }
    }
}