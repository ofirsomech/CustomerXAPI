using AutoMapper;
using CustomerXAPI.Dtos;
using CustomerXAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerXAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly IContractService _contractService;
        private readonly IMapper _mapper;

        public ContractsController(IContractService contractService, IMapper mapper)
        {
            _contractService = contractService;
            _mapper = mapper;
        }

        // GET: api/Contracts/{subscriptionNumber}
        [HttpGet("{subscriptionNumber}")]
        public async Task<ActionResult<ContractReadDto>> GetContract(string subscriptionNumber)
        {
            var contract = await _contractService.GetContractAsync(subscriptionNumber);
            if (contract == null)
            {
                return NotFound();
            }

            return Ok(contract);
        }

        // GET: api/Contracts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractReadDto>>> GetAllContracts()
        {
            var contracts = await _contractService.GetAllContractsAsync();
            return Ok(contracts);
        }

        // POST: api/Contracts
        [HttpPost]
        public async Task<ActionResult<ContractReadDto>> CreateContract(ContractCreateDto contractCreateDto)
        {
            var contract = await _contractService.CreateContractAsync(contractCreateDto);

            return CreatedAtAction(nameof(GetContract), new { subscriptionNumber = contract.SubscriptionNumber }, contract);
        }

        // PUT: api/Contracts/{subscriptionNumber}
        [HttpPut("{subscriptionNumber}")]
        public async Task<IActionResult> UpdateContract(string subscriptionNumber, ContractUpdateDto contractUpdateDto)
        {
            var updatedContract = await _contractService.UpdateContractAsync(subscriptionNumber, contractUpdateDto);

            if (updatedContract == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Contracts/{subscriptionNumber}
        [HttpDelete("{subscriptionNumber}")]
        public async Task<IActionResult> DeleteContract(string subscriptionNumber)
        {
            var success = await _contractService.DeleteContractAsync(subscriptionNumber);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
