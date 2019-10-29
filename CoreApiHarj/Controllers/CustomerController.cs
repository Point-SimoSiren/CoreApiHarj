﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiHarj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiHarj.Controllers
{
    [Route("nw/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public List<Customers> GetAllCustomers() // KAIKKI RIVIT
        {
            NorthwindContext db = new NorthwindContext();
            List<Customers> asiakkaat = db.Customers.ToList();
            return asiakkaat;
        }

        [HttpGet]
        [Route("{id}")]
        public Customers GetCustomerById(string id) // FIND hakee pääavaimella yhden rivin
        {
            NorthwindContext db = new NorthwindContext();
            try
            {
                Customers Asiakas = db.Customers.Find(id);
                return Asiakas;
            }
            finally
            {
                db.Dispose();
            }
        }

        [HttpGet]
        [Route("{country}/{key}")]
        public List<Customers> GetCustomerByCountry(string key) // LINQ kysely key itse nimetty. Polkuun nw/customer/country/<haluttu maa>
        {
            NorthwindContext db = new NorthwindContext();
            try
            {
                var seulottu = from a in db.Customers
                               where a.Country == key
                               select a;

                return seulottu.ToList();
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}