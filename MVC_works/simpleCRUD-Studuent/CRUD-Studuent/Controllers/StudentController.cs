using CRUD_Studuent.Models;
using CRUD_Studuent.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Studuent.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var students = _service.GetAllStudents();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            _service.Add(student);
             return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var student=_service.GetStudentById(id);
            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            _service.Update(student);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var student = _service.GetStudentById(id);
            return View(student);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
