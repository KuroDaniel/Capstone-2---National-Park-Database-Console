using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAL
{
    public interface ICampgroundSqlDAO
    {
        IList<Campground> GetCampgroundsByParkId(int park_id);
        decimal GetCampgroundCost(string campground_id);
    }
}