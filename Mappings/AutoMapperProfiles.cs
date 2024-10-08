﻿using AutoMapper;
using Test.Models.Domain;
using Test.Models.DTO;

namespace Test.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>()
                .ReverseMap();
            CreateMap<AddRegionRequestDto, Region>();
            CreateMap<UpdateRegionRequestDto, Region>();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<WalkDTO, Walk>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>();
            CreateMap<DifficultyDto, Difficulty>().ReverseMap();
        }
    }

}
