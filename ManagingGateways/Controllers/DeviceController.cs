using AutoMapper;
using GatewaysDomain.Models;
using GatewaysDomain.Repository;
using ManagingGateways.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagingGateways.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly Repository<Device> _deviceRepository;
        private readonly Repository<Gateway> _gatewayRepository;
        public DeviceController(IMapper mapper, IUnitOfWork unitofwork) : base(unitofwork)
        {
            _mapper = mapper;
            _unitOfWork = unitofwork;
            _deviceRepository = _unitOfWork.DeviceRepository;
            _gatewayRepository = _unitOfWork.GatewayRepository;
        }

        [HttpGet]
        public IEnumerable<DeviceViewModel> Get()
        {
            var model = _mapper.Map<List<DeviceViewModel>>(_deviceRepository.List());

            return model;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice([FromRoute] int id)
        {
            var model = _mapper.Map<DeviceViewModel>(await _deviceRepository.FindAsync(id));

            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice([FromRoute] int id, [FromBody] DeviceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var device = _deviceRepository.Find(id);
                var gateway = _gatewayRepository.Find(model.DeviceGatewayId);
                device.Gateway = gateway;
                device.Status = model.DeviceStatus;
                device.UID = model.DeviceUID;
                device.Vendor = model.DeviceVendor;
                _deviceRepository.Update(device);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostDevice([FromBody] DeviceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var device = _mapper.Map<Device>(model);
                device.CreateAt = DateTime.Now;
                var gateway = _gatewayRepository.Find(model.DeviceGatewayId);
                device.Gateway = gateway;
                _deviceRepository.Create(device);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice([FromRoute] int id)
        {
            var gateway = await _deviceRepository.FindAsync(id);
            if (gateway == null)
            {
                return NotFound();
            }
            try
            {
                _deviceRepository.Delete(gateway);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

    }
}
