using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Domain.Layer.Entities;
using OnlineCourses.Domain.Layer.Interfaces;
using OnlineCourses.Domain.Layer.Model;
using System.Threading.Tasks;

namespace OnlineCourses.API.Controllers {

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase {

        private readonly IMapper _mapper;
        private readonly IReadRepository<Course> _readRepository;
        private readonly IQueueService<Student> _queueService;

        public SubscriptionsController(IMapper mapper, IReadRepository<Course> readRepository, IQueueService<Student> queueService) {
            _mapper = mapper;
            _readRepository = readRepository;
            _queueService = queueService;
        }

        [HttpPost]
        [ActionName("PostSync")]
        public IActionResult Post([FromBody] StudentDto student) {

            var entity = _mapper.Map<Student>(student);
            var course = _readRepository.FindSingleBy(n => n.Name == student.CourseDto.Name, "Students");

            if (course == null) {
                return NotFound("Course Not Found");
            }
            entity.Course = course;

            if (!entity.Course.HasVacancy) {
                return new JsonResult(new { error = "The course has no more vacancies." }) {
                    StatusCode = StatusCodes.Status406NotAcceptable
                };
            }

            _queueService.Save(entity);

            return new JsonResult(new { sucess = "Subscription was successful!" }) {
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpPost]
        [ActionName("PostAsync")]
        public async Task<IActionResult> PostAsync([FromBody] StudentDto student) {

            var entity = _mapper.Map<Student>(student);
            var course = await _readRepository.FindSingleByAsync(n => n.Name == student.CourseDto.Name, "Students");

            if (course == null) {
                return NotFound("Course Not Found");
            }
            entity.Course = course;

            if (!entity.Course.HasVacancy) {
                return new JsonResult(new { error = "The course has no more vacancies." }) {
                    StatusCode = StatusCodes.Status406NotAcceptable
                };
            }

            _queueService.SaveAsync(entity);

            return new JsonResult(new { notification = "Your signature is being processed. When completed, you'll receive an email notification." }) {
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
