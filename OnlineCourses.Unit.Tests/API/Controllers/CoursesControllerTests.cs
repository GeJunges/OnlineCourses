﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OnlineCourses.API.Controllers;
using OnlineCourses.Domain.Layer.Entities;
using OnlineCourses.Domain.Layer.Interfaces;
using OnlineCourses.Domain.Layer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourses.Unit.Tests.API.Controllers {
    public class CoursesControllerTests {

        private CoursesController _controller;
        private Mock<IMapper> _mapperMock;
        private Mock<IReadRepository<Course>> _readRepositoryMock;
        private IEnumerable<Course> _courses;

        [SetUp]
        public void SetUp() {
            _mapperMock = new Mock<IMapper>();
            _readRepositoryMock = new Mock<IReadRepository<Course>>();

            _controller = new CoursesController(_mapperMock.Object, _readRepositoryMock.Object);
            _courses = CreateCoursesList();
        }

        [Test]
        public async Task Get_ShouldReturnAllCourses() {
            var expected = CrieCourseListDto().ToList();
            _readRepositoryMock.Setup(mock => mock.FindAll(It.IsAny<string>())).ReturnsAsync(_courses);
            _mapperMock.Setup(mock => mock.Map<IEnumerable<Course>, List<CourseListDto>>(It.IsAny<List<Course>>())).Returns(expected);

            var result = await _controller.Get() as JsonResult;
            var actual = (List<CourseListDto>)result.Value;

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test]
        public async Task Get_ShouldReturnEmptyListIfNoCourses() {
            _readRepositoryMock.Setup(mock => mock.FindAll(It.IsAny<string>())).ReturnsAsync(new List<Course>());
            _mapperMock.Setup(mock => mock.Map<IEnumerable<Course>, List<CourseListDto>>(It.IsAny<List<Course>>())).Returns(new List<CourseListDto>());

            var result = await _controller.Get() as JsonResult;
            var actual = (List<CourseListDto>)result.Value;

            CollectionAssert.IsEmpty(actual);
        }

        [Test]
        public async Task Get_ShouldReturnCourseById() {
            var expected = CrieCourseDetailDto().First();

            _readRepositoryMock.Setup(mock => mock.FindById(It.IsAny<Guid>(), It.IsAny<string[]>())).ReturnsAsync(_courses.First());
            _mapperMock.Setup(mock => mock.Map<CourseDetailDto>(It.IsAny<Course>())).Returns(expected);

            var result = await _controller.Get(It.IsAny<Guid>()) as JsonResult;
            var actual = (CourseDetailDto)result.Value;

            Assert.AreEqual(expected, actual);
        }


        [Test]
        public async Task Get_ShouldReturnNotFoundIfCouseNotFounded() {
            var expected = StatusCodes.Status404NotFound;
            _readRepositoryMock.Setup(mock => mock.FindById(It.IsAny<Guid>(), It.IsAny<string[]>())).ReturnsAsync(default(Course));

            var actual = await _controller.Get(It.IsAny<Guid>()) as ObjectResult;

            Assert.AreEqual(expected, actual.StatusCode);
        }

        private IEnumerable<CourseDetailDto> CrieCourseDetailDto() {
            foreach (var course in _courses) {
                yield return new CourseDetailDto {
                    Name = course.Name,
                    MaximumSignatures = course.MaximumSignatures,
                    TotalSignatures = course.TotalSignatures,
                    MinimumAge = course.MinimumAge,
                    MaximumAge = course.MaximumAge,
                    AverageAge = course.AverageAge,
                    TeacherDto =new TeacherDto { Name = course.Teacher.Name }
                };
            }
        }

        private IEnumerable<CourseListDto> CrieCourseListDto() {
            foreach (var course in _courses) {
                yield return new CourseListDto {
                    Name = course.Name,
                    MaximumSignatures = course.MaximumSignatures,
                    TotalSignatures = course.TotalSignatures,
                    MinimumAge = course.MinimumAge,
                    MaximumAge = course.MaximumAge,
                    AverageAge = course.AverageAge
                };
            }
        }

        private IEnumerable<Course> CreateCoursesList() {
            return new List<Course>{
                    new Course {
                        Id = Guid.NewGuid(),
                        Name = "Azure",
                        MaximumSignatures = 2,
                        Students = new List<Student> { new Student() },
                        Teacher = new Teacher()
                    },
                    new Course {
                        Id = Guid.NewGuid(),
                        Name = ".Net Core",
                        MaximumSignatures = 1,
                        Students = new List<Student> { new Student() },
                        Teacher = new Teacher()
                    }
                };
        }
    }
}
