﻿using AutoMapper;
using OnlineCourses.Domain.Layer.Entities;
using OnlineCourses.Domain.Layer.Model;
using System.Collections.Generic;

namespace OnlineCourses.API.AutoMapperConfiguration {
    public class AutoMapperProfile : Profile {

        public AutoMapperProfile() {

            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.CourseDto, memberOptions => memberOptions.MapFrom(source => source.Course));
            CreateMap<StudentDto, Student>()
                .ForMember(dest => dest.Course, memberOptions => memberOptions.MapFrom(source => source.CourseDto));

            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<IEnumerable<Course>, List<CourseListDto>>().ReverseMap();
            CreateMap<Course, CourseDetailDto>().ReverseMap();
            CreateMap<Teacher, Teacher>().ReverseMap();
        }
    }
}