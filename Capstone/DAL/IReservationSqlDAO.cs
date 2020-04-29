using Capstone.Models;
using System;
using System.Collections.Generic;

namespace Capstone.DAL
{
    public interface IReservationSqlDAO
    {
        int CreateReservation(string site_id, string name, string from_date, string to_date);
        IList<Reservation> Search(int siteId, DateTime fromDate, DateTime endDate);
    }
}