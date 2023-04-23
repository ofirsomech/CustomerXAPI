using CustomerXAPI.Dtos;
using CustomerXAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerXAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerReadDto>> GetCustomerAsync(string id)
        {
            var customer = await _customerService.GetCustomerAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerReadDto>>> GetAllCustomersAsync()
        {
            var customers = await _customerService.GetAllCustomersAsync();

            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerReadDto>> CreateCustomerAsync(CustomerCreateDto customerCreateDto)
        {
            var customer = await _customerService.CreateCustomerAsync(customerCreateDto);

            return CreatedAtAction(nameof(GetCustomerAsync), new { id = customer.ID }, customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerReadDto>> UpdateCustomerAsync(string id, CustomerUpdateDto customerUpdateDto)
        {
            var customer = await _customerService.UpdateCustomerAsync(id, customerUpdateDto);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomerAsync(string id)
        {
            var deleted = await _customerService.DeleteCustomerAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
