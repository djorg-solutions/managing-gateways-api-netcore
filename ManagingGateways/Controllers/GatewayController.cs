using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using AutoMapper;
using GatewaysDomain.Models;
using GatewaysDomain.Repository;
using ManagingGateways.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ManagingGateways.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GatewayController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly Repository<Gateway> _gatewayRepository;
        public GatewayController(IMapper mapper, IUnitOfWork unitofwork) : base(unitofwork)
        {
            _mapper = mapper;
            _unitOfWork = unitofwork;
            _gatewayRepository = _unitOfWork.GatewayRepository;
        }

        [HttpGet]
        public IEnumerable<GatewayViewModel> Get()
        {
            var model = _mapper.Map<List<GatewayViewModel>>(_gatewayRepository.Queryable().Include(d => d.Devices).ToList());

            return model;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGateway([FromRoute] int id)
        {
            var model = new GatewayViewModel();
            try
            {
                 model = _mapper.Map<GatewayViewModel>(await _gatewayRepository.Queryable().Include(d => d.Devices).Where(d => d.Id == id).FirstAsync());
            }
            catch (Exception e)
            {
                return NotFound();
            }
          
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGateway([FromRoute] int id, [FromBody] GatewayViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                model.GatewayId = id;
                var gateway = _mapper.Map<Gateway>(model);
                _gatewayRepository.Update(gateway);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostGateway([FromBody] GatewayViewModel model)
        {
          
            if (!ModelState.IsValid || !IsIPv4(model.GatewayIpAddress))
            {
                return BadRequest("Invalid model");
            }

            try
            {
                var gateway = _mapper.Map<Gateway>(model);
                _gatewayRepository.Create(gateway);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGateway([FromRoute] int id)
        {
            var gateway = await _gatewayRepository.FindAsync(id);
            if (gateway == null)
            {
                return NotFound();
            }
            try
            {
                _gatewayRepository.Delete(gateway);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        private bool IsIPv4(string value)
        {
            IPAddress address;

            if (IPAddress.TryParse(value, out address))
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
