using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoloDevApp.Api.Filters;
using SoloDevApp.Service.Services;
using SoloDevApp.Service.Utilities;
using SoloDevApp.Service.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoloDevApp.Api.Controllers
{
    [Route("api/dat-phong")]
    [ApiController]
    [ApiKeyAuth("")]
    public class DatPhongController : ControllerBase
    {
        private readonly IDatPhongService _datPhongService;

        public DatPhongController(IDatPhongService datPhongService)
        {
            _datPhongService = datPhongService;
        }

       [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await _datPhongService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await _datPhongService.GetByIdAsync(id);
        }

        [HttpGet("lay-theo-nguoi-dung/{MaNguoiDung}")]
        public async Task<IActionResult> GetDatPhongTheoUser(string MaNguoiDung)
        {
            return await _datPhongService.GetDatPhongTheoUser(MaNguoiDung);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DatPhongViewModel model)
        {
   
            return await _datPhongService.InsertAsync(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DatPhongViewModel model)
        {


            return await _datPhongService.UpdateAsync(id, model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete( int id)
        {

            return await _datPhongService.DeleteByIdAsync(id);
        }
    }
}