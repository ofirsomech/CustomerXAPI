using CustomerXAPI.Dtos;
using CustomerXAPI.Interfaces;
using CustomerXAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerXAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractsController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContracts()
        {
            var contracts = await _contractService.GetAllContractsAsync();
            return Ok(contracts);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContractById(int id)
        {
            var contract = await _contractService.GetContractByIdAsync(id);

            if (contract == null)
            {
                return NotFound();
            }

            return Ok(contract);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContract(ContractCreateDto contractCreateDto)
        {
            var contract = await _contractService.CreateContractAsync(contractCreateDto);
            return CreatedAtAction(nameof(GetContractById), new { id = contract.ID }, contract);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContract(int id, ContractUpdateDto contractUpdateDto)
        {
            var result = await _contractService.UpdateContractAsync(id, contractUpdateDto);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(int id)
        {
            var result = await _contractService.DeleteContractAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
