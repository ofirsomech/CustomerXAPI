using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerXAPI.Dtos;
using CustomerXAPI.Interfaces;
using CustomerXAPI.Services;
using IsraeliHebrewNames;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerXAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerReadDto>>> GetAllCustomers()
        {
            return Ok(await _customerService.GetAllCustomersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerReadDto>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // GET api/customers/identity/{identityNumber}
        [HttpGet("identity/{identityNumber}")]
        public async Task<ActionResult> GetCustomerByIdentityNumberAsync(string identityNumber)
        {
            bool isValidID = IsraeliDataGenerator.IsValidIsraeliID(identityNumber);

            if (!isValidID)
            {
                return BadRequest("Invalid Israeli ID number");
            }

            var customer = await _customerService.GetCustomerByIdentityNumberAsync(identityNumber);
            if (customer == null)
            {
                return NotFound("לא נמצא לקוח עם תעודת זהות זאת.");
            }
            return Ok(customer);
        }

        // PUT api/customers/{id}/address
        [HttpPut("address/{id}")]
        public async Task<IActionResult> UpdateCustomerAddressAsync(int id, [FromBody] UpdateAddressDto addressUpdateDto)
        {
            var updatedCustomer = await _customerService.UpdateCustomerAddressAsync(id, addressUpdateDto);

            if (updatedCustomer == null)
            {
                return NotFound();
            }

            return Ok(updatedCustomer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerReadDto>> CreateCustomer(CustomerCreateDto customerCreateDto)
        {
            var customer = await _customerService.CreateCustomerAsync(customerCreateDto);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.ID }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerUpdateDto customerUpdateDto)
        {
            var success = await _customerService.UpdateCustomerAsync(id, customerUpdateDto);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var success = await _customerService.DeleteCustomerAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

