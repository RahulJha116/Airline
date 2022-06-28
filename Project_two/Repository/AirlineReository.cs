using Microsoft.EntityFrameworkCore;
using Project_two.DbContexts;
using Project_two.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Project_two.Repository
{
    public class AirlineReository : IAirlineReository
    {
        private readonly AirlineContext _dbContext;
        public IHostingEnvironment HostingEnviornment;

        public AirlineReository(AirlineContext dbContext , IHostingEnvironment hostingEnviornment)
        {
            _dbContext = dbContext;
            HostingEnviornment = hostingEnviornment;

        }

        public void DeleteAirline(int airlineId)
        {

            var airline = _dbContext.Airlines.Find(airlineId);
            _dbContext.Airlines.Remove(airline);
        }

        public Airline GetAirlineByID(int airlineId)
        {
            return _dbContext.Airlines.Find(airlineId);
        }

        public IEnumerable<Airline> GetAirlines()
        {
            return _dbContext.Airlines.ToList();
        }

        public void RegisterAirline(Airline airline)
        {
            //Airline a = new Airline
            //{
            //    airlineName = airline.airlineName,
            //    airlineId = airline.airlineId,
            //    airlineContactNumber = airline.airlineContactNumber,
            //    airlineAddress = airline.airlineAddress,
            //    airlineLogo = UploadFile(airline.airlineLogo)
            //};



            _dbContext.Add(airline);
            Save();
        }


        //public async Task<string> UploadFile(Airline airline)
        //{
        //    try
        //    {
        //        if (airline.airlineLogo != null)
        //        {
        //            if (!Directory.Exists(HostingEnviornment.WebRootPath + "\\AirlineLogo\\"))
        //            {
        //                Directory.CreateDirectory(HostingEnviornment.WebRootPath + "\\AirlineLogo\\");
        //            }
        //            using (FileStream fileStream = System.IO.File.Create(HostingEnviornment.WebRootPath + "\\AirlineLogo\\" + airline.airlineLogo.FileName))
        //            {
        //                airline.airlineLogo.CopyTo(fileStream);
        //                fileStream.Flush();
        //                return "\\AirlineLogo\\" + airline.airlineLogo.FileName;
        //            }
        //        }
        //        else
        //        {
        //            return "Failed";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message.ToString();
        //    }

        //}

        public void Save()
    {
        _dbContext.SaveChanges();
    }

         public void UpdateAirline(Airline airline)
    {
        _dbContext.Entry(airline).State = EntityState.Modified;
        Save();
    }
    }
}
