﻿using AutoMapper;
using AspNetMVCFileUpload.Models;
using AspNetMVCFileUpload.Entities;

namespace AspNetMVCFileUpload
{
    public class MapperConfig
    {
        public static void RegisterObjectMappings()
        {
            Mapper.CreateMap<FileUploadViewModel, FileUpload>().ReverseMap();
            Mapper.CreateMap<TestViewModel, Test>().ReverseMap();
        }
    }
}