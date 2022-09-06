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
    public interface IDatPhongService : IService<DatPhong, DatPhongViewModel>
    {
        Task<ResponseEntity> GetDatPhongTheoUser(string MaNguoiDung);

        
    }

    public class DatPhongService : ServiceBase<DatPhong, DatPhongViewModel>, IDatPhongService
    {
        private readonly IDatPhongRepository _datPhongRepository;
        private readonly IFileService _fileService;

        public DatPhongService(IDatPhongRepository datPhongRepository, 
            IMapper mapper,
            IAppSettings appSettings,
            IFileService fileService
            )
            : base(datPhongRepository, mapper)
        {
            _datPhongRepository = datPhongRepository;
            _fileService = fileService;
        }
        public async Task<ResponseEntity> GetDatPhongTheoUser(string MaNguoiDung)
        {
            try
            {
                IEnumerable<DatPhong> dsDatPhong = await _datPhongRepository.GetMultiByConditionAsync("MaNguoiDung", int.Parse(MaNguoiDung));

                return new ResponseEntity(StatusCodeConstants.OK, dsDatPhong);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}