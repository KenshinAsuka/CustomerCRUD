using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerCRUD.Repositories;
using CustomerCRUD.Models;
using System.Transactions;
using CustomerCRUD.DTO;
using Microsoft.AspNetCore.Cors;

namespace CustomerCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customer
        [HttpGet]
        [EnableCors("AllowAllOrigins")]
        public IActionResult Get()
        {
            var customers = _customerRepository.GetAll();
            return new OkObjectResult(customers);
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        [EnableCors("AllowAllOrigins")]
        public IActionResult Get(Guid id)
        {
            var customer = _customerRepository.Get(id);
            return new OkObjectResult(customer);
        }

        // PUT: api/Customer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [EnableCors("AllowAllOrigins")]
        public IActionResult Put(string id, [FromBody] UpdateCustomerModel customer)
        {
            if (customer != null)
            {
                using (var scope = new TransactionScope())
                {
                    _customerRepository.Update(Guid.Parse(id), customer);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // POST: api/Customer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [EnableCors("AllowAllOrigins")]
        public IActionResult Post([FromBody] CreateCustomerModel customer)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var customerId = _customerRepository.Insert(customer);
                    scope.Complete();
                    Customer newCustomer = new Customer();
                    newCustomer.Id = customerId;

                    if(customer == null)
                        return new NoContentResult();

                    newCustomer.Name = customer.Name;
                    newCustomer.Email = customer.Email;
                    newCustomer.Phone = customer.Phone;
                    newCustomer.Address = customer.Address;
                    newCustomer.DateOfBirth = customer.DateOfBirth.Value;
                    return CreatedAtAction(nameof(Get), new { id = customerId }, newCustomer);
                }
            }
            catch(Exception ex)
            {
                return new NoContentResult();
            }
            
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        [EnableCors("AllowAllOrigins")]
        public IActionResult Delete(Guid id)
        {
            _customerRepository.Delete(id);
            return new OkResult();
        }
    }
}
