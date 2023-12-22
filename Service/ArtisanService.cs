using NaijaQuickFix.Models;
using NaijaQuickFix.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaijaQuickFix.Service
{
    public class ArtisanService: GenericRepository<Artisan>
    {
        public Artisan FindByNIN(string Nin)
        {
            return table.Where(p => p.NIN == Nin).FirstOrDefault();
        }
        //public Artisan FindBothIdAndPhoto(int id, string photo)
        //{
        //    return table.Where(i=>i.Id == id && i.ArtisanProfilePhotoUrl==photo).FirstOrDefault();
        //}
        public IEnumerable<Artisan> SearchBothProfessionAndLocation(string searchProfession, string searchLocation)
        {
            return table.Where(l => l.Profession.Name.ToLower().Contains(searchProfession.ToLower()) && l.OfficeAddress.ToLower().Contains(searchLocation.ToLower())).ToList();
        }
        public IEnumerable<Artisan> SearchForProfession(string searchProfession)
        {
            return table.Where(s => s.Profession.Name.ToLower().Contains(searchProfession.ToLower())).ToList();
        }
    }
}