using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_two.Model;

namespace Project_two.Repository
{
    public interface IAirlineReository
    {

        IEnumerable<Airline> GetAirlines();
        Airline GetAirlineByID(int UserId);

        void RegisterAirline(Airline airline);
        void DeleteAirline(int airlineId);
        void UpdateAirline(Airline airline);
        void Save();
    }
}
