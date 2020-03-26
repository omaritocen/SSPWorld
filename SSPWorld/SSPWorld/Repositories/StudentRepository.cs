using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SSPWorld.Models;
using SSPWorld.Services;

namespace SSPWorld.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> GetStudentProfile();
        Task<string> CreateStudentProfile(Student student);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly IStudentService _studentService = new StudentService();

        public async Task<Student> GetStudentProfile()
        {
            return await _studentService.GetStudentProfileAsync();
        }

        public async Task<string> CreateStudentProfile(Student student)
        {
            return await _studentService.CreateStudentProfileAsync(student);
        }
    }
}
