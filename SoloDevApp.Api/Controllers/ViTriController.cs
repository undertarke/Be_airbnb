using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoloDevApp.Api.Filters;
using SoloDevApp.Repository.Models;
using SoloDevApp.Service.Services;
using SoloDevApp.Service.Utilities;
using SoloDevApp.Service.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoloDevApp.Api.Controllers
{
    [Route("api/vi-tri")]
    [ApiController]
    [ApiKeyAuth("")]
    public class ViTriController : ControllerBase
    {
        private readonly IViTriService _viTriService;

        public ViTriController(IViTriService viTriService)
        {
            _viTriService = viTriService;
        }

       [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await _viTriService.GetAllAsync();
        }

        [HttpGet("phan-trang-tim-kiem")]
        public async Task<IActionResult> GetPaging(int pageIndex, int pageSize, string keyword)
        {
            if(pageIndex <= 0 || pageSize <= 0)
            {
                return new ResponseEntity(400, "Số trang và số lượng phần tử phải lớn hơn 0");
            }

            return await _viTriService.GetPagingAsync(pageIndex, pageSize, keyword == null ? keyword : " TenViTri LIKE N'%" + keyword + "%'");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await _viTriService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] string token,[FromBody] ViTriViewModel model)
        {
            string nguoiDungId = FuncUtilities.CheckToken(token, true);
            string sMess = FuncUtilities.TokenMessage(nguoiDungId,true);
            if (sMess != "")
                return new ResponseEntity(403, sMess);

            return await _viTriService.InsertAsync(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromHeader] string token, int id, [FromBody] ViTriViewModel model)
        {
            string nguoiDungId = FuncUtilities.CheckToken(token, true);
            string sMess = FuncUtilities.TokenMessage(nguoiDungId,true);
            if (sMess != "")
                return new ResponseEntity(403, sMess);

            List<int> lstCheck = JsonConvert.DeserializeObject<List<int>>("[1,2,3,4,5,6,7,8]");

            if (lstCheck.Find(n=> n == id) != 0)
            {
                return new ResponseEntity(403, "Không có quyền");

            }

            return await _viTriService.UpdateAsync(id, model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromHeader] string token, int id)
        {
            string nguoiDungId = FuncUtilities.CheckToken(token, true);
            string sMess = FuncUtilities.TokenMessage(nguoiDungId,true);
            if (sMess != "")
                return new ResponseEntity(403, sMess);

            List<int> lstCheck = JsonConvert.DeserializeObject<List<int>>("[1,2,3,4,5,6,7,8]");
            if (lstCheck.Find(n => n == id) != 0)
            {
                return new ResponseEntity(403, "Không có quyền");

            }

            return await _viTriService.DeleteByIdAsync(id);
        }

        [HttpPost("upload-hinh-vitri")]
        public async Task<IActionResult> UploadAnhViTri([FromHeader] string token, string maViTri, [FromForm] Photo files)
        {

            string nguoiDungId = FuncUtilities.CheckToken(token, true);
            string sMess = FuncUtilities.TokenMessage(nguoiDungId, true);
            if (sMess != "")
                return new ResponseEntity(403, sMess);

            List<int> lstCheck = JsonConvert.DeserializeObject<List<int>>("[1,2,3,4,5,6,7,8]");
            if (lstCheck.Find(n => n == int.Parse(maViTri)) != 0)
            {
                return new ResponseEntity(403, "Không có quyền");

            }

            return await _viTriService.UploadAnhViTri(maViTri, files);

        }
    }
}