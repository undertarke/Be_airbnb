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
    public interface IViTriService : IService<ViTri, ViTriViewModel>
    {
        Task<ResponseEntity> UploadAnhViTri(string maViTri, Photo file);


    }

    public class ViTriService : ServiceBase<ViTri, ViTriViewModel>, IViTriService
    {
        private readonly IViTriRepository _viTriRepository;
        private readonly IFileService _fileService;
        private readonly IAppSettings _appSettings;

        public ViTriService(IViTriRepository viTriRepository, 
            IMapper mapper,
            IAppSettings appSettings,
            IFileService fileService
            )
            : base(viTriRepository, mapper)
        {
            _viTriRepository = viTriRepository;
            _fileService = fileService;
            _appSettings = appSettings;
        }
        public async Task<ResponseEntity> UploadAnhViTri(string maviTri, Photo file)
        {
            try
            {
                ViTri viTri = await _viTriRepository.GetByIdAsync(int.Parse(maviTri));

                if (viTri == null)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Không tìm thấy vị trí");

                if (file.formFile == null)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Không tìm thấy hình để thêm");

                //kiem tra dugn luong hinh chi cho phep hinh < 1Mb
                if (file.formFile.Length > 1000000)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Dung lượng hình phải dưới 1Mb");

                if (file.formFile.ContentType != "image/jpg" && file.formFile.ContentType != "image/jpeg" && file.formFile.ContentType != "image/png" && file.formFile.ContentType != "image/gif")
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Chỉ cho phép dịnh dạng (jpg, jpeg, png, gif)");

                string filePath = "";

                filePath = await _fileService.SaveFileAsync(file.formFile, "avatar");

                viTri.HinhAnh = _appSettings.UrlMain + filePath;
                await _viTriRepository.UpdateAsync(viTri.Id, viTri);

                return new ResponseEntity(StatusCodeConstants.OK, viTri);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}