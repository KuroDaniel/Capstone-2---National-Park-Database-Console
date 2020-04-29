using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAL
{
    public interface ISiteSqlDAO
    {
        IList<Site> GetAllSites(int site_id);
        IList<Site> ViewAvailableReservations(string campground_id, string from_date, string to_date);
    }
}