using NaijaQuickFix.Models;
using NaijaQuickFix.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static NaijaQuickFix.Helper.Helper;

namespace NaijaQuickFix.Controllers.Api
{
    public class ArtisansController : ApiController
    {
        public NaijaQuickFixxEntities naijaQuickFixEntities;

        public ArtisansController()
        {
            naijaQuickFixEntities = new NaijaQuickFixxEntities();
        }

        [HttpGet]
        public IHttpActionResult GetArtisans(int id)
        {
            var service = new ArtisanService();
            var item = service.SelectByID(id);
            if (item != null)
            {
                //item.Status = (int)Entity_Status.Public;
                //service.Update(item);
                return Ok(item);
            }
            return NotFound();
        }
    }
}
