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
    [Route("api/phong-thue")]
    [ApiController]
    [ApiKeyAuth("")]
    public class PhongController : ControllerBase
    {
        private readonly IPhongService _phongService;

        public PhongController(IPhongService phongService)
        {
            _phongService = phongService;
        }

       [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await _phongService.GetAllAsync();
        }
        [HttpGet("lay-phong-theo-vi-tri")]
        public async Task<IActionResult> GetTheoViTri(string maViTri)
        {
            return await _phongService.GetTheoViTri(maViTri);
        }

        [HttpGet("phan-trang-tim-kiem")]
        public async Task<IActionResult> GetPaging(int pageIndex, int pageSize, string keyword)
        {
            if(pageIndex <= 0 || pageSize <= 0)
            {
                return new ResponseEntity(400, "Số trang và số lượng phần tử phải lớn hơn 0");
            }

            return await _phongService.GetPagingAsync(pageIndex, pageSize, keyword == null ? keyword : " TenPhong LIKE N'%" + keyword + "%'");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await _phongService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] string token,[FromBody] PhongViewModel model)
        {
            string nguoiDungId = FuncUtilities.CheckToken(token, true);
            string sMess = FuncUtilities.TokenMessage(nguoiDungId,true);
            if (sMess != "")
                return new ResponseEntity(403, sMess);

            return await _phongService.InsertAsync(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromHeader] string token, int id, [FromBody] PhongViewModel model)
        {
            string nguoiDungId = FuncUtilities.CheckToken(token, true);
            string sMess = FuncUtilities.TokenMessage(nguoiDungId,true);
            if (sMess != "")
                return new ResponseEntity(403, sMess);

            List<int> lstCheck = JsonConvert.DeserializeObject<List<int>>("[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21]");
            if (lstCheck.Find(n => n == id) != 0)
            {
                return new ResponseEntity(403, "Không có quyền");

            }

            return await _phongService.UpdateAsync(id, model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromHeader] string token, int id)
        {
            string nguoiDungId = FuncUtilities.CheckToken(token, true);
            string sMess = FuncUtilities.TokenMessage(nguoiDungId,true);
            if (sMess != "")
                return new ResponseEntity(403, sMess);

            List<int> lstCheck = JsonConvert.DeserializeObject<List<int>>("[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21]");
            if (lstCheck.Find(n => n == id) != 0)
            {
                return new ResponseEntity(403, "Không có quyền");

            }

            return await _phongService.DeleteByIdAsync(id);
        }

        [HttpPost("upload-hinh-phong")]
        public async Task<IActionResult> UploadAnhPhong([FromHeader] string token,string maPhong, [FromForm] Photo files)
        {

            string nguoiDungId = FuncUtilities.CheckToken(token, true);
            string sMess = FuncUtilities.TokenMessage(nguoiDungId, true);
            if (sMess != "")
                return new ResponseEntity(403, sMess);
            List<int> lstCheck = JsonConvert.DeserializeObject<List<int>>("[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21]");
            if (lstCheck.Find(n => n == int.Parse(maPhong)) != 0)
            {
                return new ResponseEntity(403, "Không có quyền");

            }

            return await _phongService.UploadAnhPhong(maPhong, files);

        }
    }
}