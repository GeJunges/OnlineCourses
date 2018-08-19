using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OnlineCourses.API.Controllers;
using OnlineCourses.Domain.Layer.Entities;
using OnlineCourses.Domain.Layer.Interfaces;
using OnlineCourses.Domain.Layer.Model;
using Newtonsoft.Json;

namespace OnlineCourses.Unit.Tests.API.Controllers {

    public class SubscriptionsControllerTests {

        private SubscriptionsController _controller;
        private Mock<IMapper> _mapperMock;
        private Mock<IQueueService<Student>> _queueServiceMock;
        private Mock<IReadRepository<Course>> _readRepositoryMock;
        private Guid _id;
        private StudentDto _studentDto;
        private Course _course;

        [SetUp]
        public void SetUp() {
            _id = Guid.NewGuid();
            _course = CreateCourse();
            _studentDto = CreateStudentDto();
            _mapperMock = new Mock<IMapper>();
            _queueServiceMock = new Mock<IQueueService<Student>>();
            _readRepositoryMock = new Mock<IReadRepository<Course>>();
            _controller = new SubscriptionsController(_mapperMock.Object, _readRepositoryMock.Object, _queueServiceMock.Object);

            _readRepositoryMock.Setup(mock => mock.FindSingleBy(It.IsAny<Expression<Func<Course, bool>>>(), It.IsAny<string>())).Returns(_course);
            _readRepositoryMock.Setup(mock => mock.FindSingleByAsync(It.IsAny<Expression<Func<Course, bool>>>(), It.IsAny<string>())).ReturnsAsync(_course);
        }

        [Test]
        public void Post_ShouldSubscribeStudent() {
            var expected = StatusCodes.Status200OK;
            _mapperMock.Setup(mock => mock.Map<Student>(It.IsAny<StudentDto>())).Returns(MapDtoToEntity(_studentDto));

            var actual = (JsonResult)_controller.Post(_studentDto);

            Assert.AreEqual(expected, actual.StatusCode);
        }

        [Test]
        public void Post_ShouldCallQueueServiceOnce() {
            var expected = MapDtoToEntity(_studentDto);
            _mapperMock.Setup(mock => mock.Map<Student>(It.IsAny<StudentDto>())).Returns(expected);

            var actual = (JsonResult)_controller.Post(_studentDto);

            _queueServiceMock.Verify(mock => mock.Save(It.IsAny<Student>()), Times.Once());
            _queueServiceMock.Verify(mock => mock.Save(expected));
        }

        [Test]
        public void Post_ShouldNotSubscribeStudentIfMaximumSignaturesExceeded() {
            var expected = StatusCodes.Status406NotAcceptable;
            _course.MaximumSignatures = 1;
            _readRepositoryMock.Setup(mock => mock.FindSingleBy(It.IsAny<Expression<Func<Course, bool>>>(), It.IsAny<string>())).Returns(_course);
            _mapperMock.Setup(mock => mock.Map<Student>(It.IsAny<StudentDto>())).Returns(MapDtoToEntity(_studentDto));

            var actual = (JsonResult)_controller.Post(_studentDto);

            Assert.AreEqual(expected, actual.StatusCode);
        }

        [Test]
        public void Post_ShouldNotSubscribeStudentIfCourseNotFound() {
            var expected = StatusCodes.Status404NotFound;
            _readRepositoryMock.Setup(mock => mock.FindSingleBy(It.IsAny<Expression<Func<Course, bool>>>(), It.IsAny<string>())).Returns(default(Course));
            _mapperMock.Setup(mock => mock.Map<Student>(It.IsAny<StudentDto>())).Returns(MapDtoToEntity(_studentDto));

            var actual = (ObjectResult)_controller.Post(_studentDto);

            Assert.AreEqual(expected, actual.StatusCode);
        }


        [Test]
        public async Task PostAsync_ShouldNotifyUserThatSubscriptionWillBeProcessedInBackground() {
            var expected = JsonConvert.SerializeObject(new JsonResult(
                    new { notification = "Your signature is being processed. When completed, you'll receive an email notification." }) {
                StatusCode = StatusCodes.Status200OK
            });
            _mapperMock.Setup(mock => mock.Map<Student>(It.IsAny<StudentDto>())).Returns(MapDtoToEntity(_studentDto));

            var result = await _controller.PostAsync(_studentDto) as JsonResult;

            var actual = JsonConvert.SerializeObject(result);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task PostAsync_ShouldCallQueueServiceOnce() {
            var expected = MapDtoToEntity(_studentDto);
            _mapperMock.Setup(mock => mock.Map<Student>(It.IsAny<StudentDto>())).Returns(expected);

            var actual = await _controller.PostAsync(_studentDto);

            _queueServiceMock.Verify(mock => mock.SaveAsync(It.IsAny<Student>()), Times.Once());
            _queueServiceMock.Verify(mock => mock.SaveAsync(expected));
        }

        [Test]
        public async Task PostAsync_ShouldNotSubscribeStudentIfMaximumSignaturesExceeded() {
            var expected = StatusCodes.Status406NotAcceptable;
            _course.MaximumSignatures = 1;
            _readRepositoryMock.Setup(mock => mock.FindSingleByAsync(It.IsAny<Expression<Func<Course, bool>>>(), It.IsAny<string>())).ReturnsAsync(_course);
            _mapperMock.Setup(mock => mock.Map<Student>(It.IsAny<StudentDto>())).Returns(MapDtoToEntity(_studentDto));

            var actual = await _controller.PostAsync(_studentDto) as JsonResult;

            Assert.AreEqual(expected, actual.StatusCode);
        }

        [Test]
        public async Task PostAsync_ShouldNotSubscribeStudentIfCourseNotFound() {
            var expected = StatusCodes.Status404NotFound;
            _readRepositoryMock.Setup(mock => mock.FindSingleByAsync(It.IsAny<Expression<Func<Course, bool>>>(), It.IsAny<string>())).ReturnsAsync(default(Course));
            _mapperMock.Setup(mock => mock.Map<Student>(It.IsAny<StudentDto>())).Returns(MapDtoToEntity(_studentDto));

            var actual = await _controller.PostAsync(_studentDto) as ObjectResult;

            Assert.AreEqual(expected, actual.StatusCode);
        }

        private Course CreateCourse() {
            return new Course {
                Name = "Azure",
                MaximumSignatures = 2,
                Students = new List<Student> { new Student() }
            };
        }

        private StudentDto CreateStudentDto() {
            return new StudentDto {
                Name = "Anna",
                Age = 30,
                CourseDto = new CourseDto {
                    Name = "Azure"
                }
            };
        }

        private Student MapDtoToEntity(StudentDto studentDto) {
            return new Student {
                Id = _id,
                Name = studentDto.Name,
                Age = studentDto.Age,
                Course = _course
            };
        }
    }
}
