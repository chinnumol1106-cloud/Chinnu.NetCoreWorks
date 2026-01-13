using CRUD_Studuent.Models;

namespace CRUD_Studuent.Repository
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        void Add(Student student);
        Student GetStudentById(int id);
        void Update(Student student);
        void Delete(int  id);
    }
}
