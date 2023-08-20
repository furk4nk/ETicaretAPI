using ETicaretAPI.Application.Services.Repositories.Customers;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerWriteRepository _customerWriteRepository;

        public CustomerController(ICustomerWriteRepository customerWriteRepository)
        {
            _customerWriteRepository=customerWriteRepository;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody]string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));
            Customer customer = new() { Name = name, CreatedDate = DateTime.UtcNow };
            customer = await _customerWriteRepository.AddAsync(customer);
            return Created("",customer);
        }
    }
}
