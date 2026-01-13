using CRUD_Studuent.Models;
using CRUD_Studuent.Repository;

namespace CRUD_Studuent.Services
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository studentRepository)
        {
            _repository = studentRepository;
        }

        public List<Student> GetAllStudents()
        {
            return _repository.GetAll();
        }
        public void Add(Student student)
        {
            _repository.Add(student);
        }

        public Student GetStudentById(int id)
        {
            return _repository.GetStudentById(id);
        }
        public void Update(Student student)
        {
            _repository.Update(student);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
