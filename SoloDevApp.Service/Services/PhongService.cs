using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SoloDevApp.Repository.Models;
using SoloDevApp.Repository.Repositories;
using SoloDevApp.Service.Constants;
using SoloDevApp.Service.Helpers;
using SoloDevApp.Service.Infrastructure;
using SoloDevApp.Service.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SoloDevApp.Service.Services
{
    public interface IPhongService : IService<Phong, PhongViewModel>
    {
        Task<ResponseEntity> UploadAnhPhong(string maPhong, Photo file);

        Task<ResponseEntity> GetTheoViTri(string maViTri);

    }

    public class PhongService : ServiceBase<Phong, PhongViewModel>, IPhongService
    {
        private readonly IPhongRepository _phongRepository;
        private readonly IFileService _fileService;
        private readonly IAppSettings _appSettings;


        public PhongService(IPhongRepository phongRepository, 
            IMapper mapper,
            IAppSettings appSettings,
            IFileService fileService
            )
            : base(phongRepository, mapper)
        {
            _phongRepository = phongRepository;
            _fileService = fileService;
            _appSettings = appSettings;
        }
        public async Task<ResponseEntity> UploadAnhPhong(string maPhong, Photo file)
        {
            try
            {
                Phong phong = await _phongRepository.GetByIdAsync(int.Parse(maPhong));

                if (phong == null)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Không tìm thấy phòng");

                if (file.formFile == null)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Không tìm thấy hình để thêm");

                //kiem tra dugn luong hinh chi cho phep hinh < 1Mb
                if (file.formFile.Length > 1000000)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Dung lượng hình phải dưới 1Mb");

                if (file.formFile.ContentType != "image/jpg" && file.formFile.ContentType != "image/jpeg" && file.formFile.ContentType != "image/png" && file.formFile.ContentType != "image/gif")
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Chỉ cho phép dịnh dạng (jpg, jpeg, png, gif)");

                string filePath = "";

                filePath = await _fileService.SaveFileAsync(file.formFile, "avatar");

                phong.HinhAnh = _appSettings.UrlMain + filePath;
                await _phongRepository.UpdateAsync(phong.Id, phong);

                return new ResponseEntity(StatusCodeConstants.OK, phong);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ResponseEntity> GetTheoViTri(string maViTri)
        {
            try {
                IEnumerable<Phong> dsPhong = await _phongRepository.GetMultiByConditionAsync("MaViTri", int.Parse(maViTri));

                return new ResponseEntity(StatusCodeConstants.OK, dsPhong);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

    }
}