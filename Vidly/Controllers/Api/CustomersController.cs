using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }




        // GET /api/customers
        public IHttpActionResult GetCustomers()  // Restituisce una lista di Customers
        {
            return Ok(_context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>));
        }




        // GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)  // Restituisce un singolo Customer
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();  // E' una convenzione RESTfull:  Se la risorsa non viene trovata, viene restituita la response standard NOT FOUND

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        


        // POST  /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer (CustomerDto customerDto)  // Creo un Customer. Volendo, avrei potuto chiamare il metodo PostCustomer (invece di CreateCustomer). 
        {                                                   // PostCustomer è un nome usato da Microsoft per convenzione, e nel caso usassimo questo, non servirebbe definire l'attributo [HttpPost] al metodo
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id),customerDto);
        }




        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto CustomerDto)  // Aggiorno un Customer
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);


            Mapper.Map(CustomerDto, customerInDb);

            //customerInDb.Name = CustomerDto.Name;
            //customerInDb.BirthDate = CustomerDto.BirthDate;
            //customerInDb.IsSubscribedToNewsletter = CustomerDto.IsSubscribedToNewsletter;
            //customerInDb.MembershipTypeId= CustomerDto.MembershipTypeId;

            _context.SaveChanges();

        }




        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id) // Cancellazione di un Customer
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}
