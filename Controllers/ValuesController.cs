using System.Drawing;
using Blood_Banking_Management.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blood_Banking_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        static List<Data> donorList = new List<Data>
        {
            new Data
            {
        Id = 1,
        DonorName = "Rajesh Kumar",
        Age = 29,
        BloodType = "A+",
        ContactInfo = "+91-9876543210",
        Quantity = 500,
        CollectionDate = DateTime.Now.AddDays(-1),
        ExpirationDate = DateTime.Now.AddMonths(1),
        Status = "Available"
    },
    new Data
    {
        Id = 2,
        DonorName = "Priya Sharma",
        Age = 25,
        BloodType = "O+",
        ContactInfo = "+91-9823456789",
        Quantity = 350,
        CollectionDate = DateTime.Now.AddDays(-5),
        ExpirationDate = DateTime.Now.AddMonths(1),
        Status = "Available"
    },
    new Data
    {
        Id = 3,
        DonorName = "Amit Verma",
        Age = 35,
        BloodType = "B+",
        ContactInfo = "+91-9988776655",
        Quantity = 450,
        CollectionDate = DateTime.Now.AddDays(-10),
        ExpirationDate = DateTime.Now.AddMonths(1),
        Status = "Requested"
    },
    new Data
    {
        Id = 4,
        DonorName = "Sneha Gupta",
        Age = 27,
        BloodType = "AB+",
        ContactInfo = "+91-9812345678",
        Quantity = 600,
        CollectionDate = DateTime.Now.AddDays(-3),
        ExpirationDate = DateTime.Now.AddMonths(1),
        Status = "Available"
    },
    new Data
    {
        Id = 5,
        DonorName = "Rohit Singh",
        Age = 31,
        BloodType = "O-",
        ContactInfo = "+91-9871234567",
        Quantity = 300,
        CollectionDate = DateTime.Now.AddDays(-7),
        ExpirationDate = DateTime.Now.AddMonths(1),
        Status = "Expired"
    },
    new Data
    {
        Id = 6,
        DonorName = "Anjali Patel",
        Age = 24,
        BloodType = "A-",
        ContactInfo = "+91-9966554433",
        Quantity = 500,
        CollectionDate = DateTime.Now.AddDays(-2),
        ExpirationDate = DateTime.Now.AddMonths(1),
        Status = "Available"
    },
    new Data
    {
        Id = 7,
        DonorName = "Vikram Mehta",
        Age = 28,
        BloodType = "B-",
        ContactInfo = "+91-9834567890",
        Quantity = 400,
        CollectionDate = DateTime.Now.AddDays(-9),
        ExpirationDate = DateTime.Now.AddMonths(1),
        Status = "Requested"
    },
    new Data
    {
        Id = 8,
        DonorName = "Ritu Aggarwal",
        Age = 30,
        BloodType = "A+",
        ContactInfo = "+91-9876543221",
        Quantity = 550,
        CollectionDate = DateTime.Now.AddDays(-6),
        ExpirationDate = DateTime.Now.AddMonths(1),
        Status = "Available"
    },
    new Data
    {
        Id = 9,
        DonorName = "Arjun Desai",
        Age = 34,
        BloodType = "AB+",
        ContactInfo = "+91-9922334455",
        Quantity = 600,
        CollectionDate = DateTime.Now.AddDays(-8),
        ExpirationDate = DateTime.Now.AddMonths(1),
        Status = "Available"
    },
    new Data
    {
        Id = 10,
        DonorName = "Neha Joshi",
        Age = 26,
        BloodType = "O+",
        ContactInfo = "+91-9811122233",
        Quantity = 500,
        CollectionDate = DateTime.Now.AddDays(-4),
        ExpirationDate = DateTime.Now.AddMonths(1),
        Status = "Available"
    }
};
        private static int _nextId = 11;

        // getting all the Product
        [HttpGet]
        public ActionResult<IEnumerable<Data>>  GetAll()
        {
            return donorList;
        }
     // getting product by id
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Data>> GetById(int id)
        {
            var da=donorList.Find(x => x.Id == id);
            if (da == null)
            {
                return NotFound();
            }
            return new List<Data> { da };
        }

    // add a new entry
        [HttpPost]
        public IActionResult CreateBloodBankEntry(Data entry)
        {
            entry.Id = _nextId++; 
            donorList.Add(entry); 
            return CreatedAtAction(nameof(GetById), new { id = entry.Id }, entry);
        }


    // update using "id"
        [HttpPut("{id}")]
        public IActionResult UpdateBloodBankEntry(int id, Data updatedEntry)
        {
            var existingEntry = donorList.Find(x => x.Id == id);
            if (existingEntry == null)
            {
                return NotFound(); 
            }

            if (!string.IsNullOrEmpty(updatedEntry.DonorName))
            {
                existingEntry.DonorName = updatedEntry.DonorName;
            }
            if (updatedEntry.Age > 0)
            {
                existingEntry.Age = updatedEntry.Age;
            }
            if (!string.IsNullOrEmpty(updatedEntry.BloodType))
            {
                existingEntry.BloodType = updatedEntry.BloodType;
            }
            if (!string.IsNullOrEmpty(updatedEntry.ContactInfo))
            {
                existingEntry.ContactInfo = updatedEntry.ContactInfo;
            }
            if (updatedEntry.Quantity > 0)
            {
                existingEntry.Quantity = updatedEntry.Quantity;
            }
            if (updatedEntry.CollectionDate != default(DateTime))
            {
                existingEntry.CollectionDate = updatedEntry.CollectionDate;
            }
            if (updatedEntry.ExpirationDate != default(DateTime))
            {
                existingEntry.ExpirationDate = updatedEntry.ExpirationDate;
            }
            if (!(updatedEntry.Status=="string"))
            {
                existingEntry.Status = updatedEntry.Status;
            }

            return NoContent(); 
        }

        // delete the Blood Bank Data -- with ID
        [HttpDelete("{id}")]
        public IActionResult DeleteBloodBankENtry(int id)
        {
            var prod = donorList.Find(x => x.Id == id);
            if (prod == null)
            {
                return NotFound();
            }
            donorList.Remove(prod);
            return NoContent();
        }


        //Pagination
        [HttpGet("page")]
        public ActionResult<IEnumerable<Data>> GetPageData(int page = 1, int size = 5)
        {
            var res = donorList.Skip((page - 1) * size).Take(size).ToList();
            return res;
        }

        //Search
        [HttpGet("search")]
        public ActionResult<IEnumerable<Data>> Search(
            [FromQuery] string bloodType = null,
            [FromQuery] string status = null,
            [FromQuery] string donorName = null)
        {           
            var results = donorList.AsEnumerable();

            if (!string.IsNullOrEmpty(bloodType))
            {
                results = results.Where(x => x.BloodType.Equals(bloodType, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(status))
            {
                results = results.Where(x => x.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(donorName))
            {
                results = results.Where(x => x.DonorName.Contains(donorName, StringComparison.OrdinalIgnoreCase));
            }

            if (!results.Any())
            {
                return NotFound("No matching entries found for the provided search criteria.");
            }

            return Ok(results);
        }



    }
}
