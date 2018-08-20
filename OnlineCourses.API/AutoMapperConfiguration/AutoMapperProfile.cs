using AutoMapper;
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
            CreateMap<Course, CourseDetailDto>().ReverseMap()
            .ForMember(dest => dest.Teacher, memberOptions => memberOptions.MapFrom(source => source.TeacherDto));
            CreateMap<CourseDetailDto, Course>().ReverseMap()
            .ForMember(dest => dest.TeacherDto, memberOptions => memberOptions.MapFrom(source => source.Teacher));
            CreateMap<Teacher, Teacher>().ReverseMap();
        }
    }
}
