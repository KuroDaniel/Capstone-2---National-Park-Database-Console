using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAL
{
    public interface IParkSqlDAO
    {
        IList<Park> GetParks();
    }
}