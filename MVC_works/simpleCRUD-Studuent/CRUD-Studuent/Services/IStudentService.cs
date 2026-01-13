using CRUD_Studuent.Models;

namespace CRUD_Studuent.Services
{
    public interface IStudentService
    {
        List<Student> GetAllStudents();
        void Add(Student student);
        Student GetStudentById(int id);
        void Update(Student student);
        void Delete(int id);

           
    }
}
