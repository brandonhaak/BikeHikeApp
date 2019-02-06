using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeHikeApp
{
    class Customer
    {
        public int CID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }

        public Customer(int id, string last, string first, string email)
        {
            CID = id;
            LastName = last;
            FirstName = first;
            Email = email;
        }

        public string printName()
        {
            return string.Format("{0}, {1}", LastName, FirstName);
        }
    }

    class Bike
    {
        public int BID { get; set; }
        public int TID { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public decimal HourlyPrice { get; set; }
        public bool Rented { get; set; }

        public Bike(int id, int tid, int year, string desc, decimal hrprice, bool rented)
        {
            BID = id;
            TID = tid;
            Year = year;
            Description = desc;
            HourlyPrice = hrprice;
            Rented = rented;
        }

        public string printName()
        {
            return string.Format("{0} - {1}", BID, Description);
        }
    }

    class Rental
    {
        public int BID { get; set; }
        public int RID { get; set; }
        public double Duration { get; set; }
        public string Description { get; set; }
        public string StartTime { get; set; }
        public decimal Price { get; set; }

        public Rental(int bid, int rid, double duration, string desc, string start, decimal price)
        {
            BID = bid;
            RID = rid;
            Duration = duration;
            Description = desc;
            StartTime = start;
            Price = price;
        }
    }
}
