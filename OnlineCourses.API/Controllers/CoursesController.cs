﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Domain.Layer.Constants;
using OnlineCourses.Domain.Layer.Entities;
using OnlineCourses.Domain.Layer.Interfaces;
using OnlineCourses.Domain.Layer.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineCourses.API.Controllers {
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly IReadRepository<Course> _readRepository;

        public CoursesController(IMapper mapper, IReadRepository<Course> readRepository) {
            _mapper = mapper;
            _readRepository = readRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var entities = await _readRepository.FindAll(Includes.Students());
            var dtos = _mapper.Map<IEnumerable<Course>, List<CourseListDto>>(entities);

            return new JsonResult(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id) {
            var entity = await _readRepository.FindById(id, Includes.StudentsAndTeacher());
            if (entity == null) {
                return NotFound("Course not found");
            }

            var dto = _mapper.Map<CourseDetailDto>(entity);

            return new JsonResult(dto);
        }
    }
}
