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
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly StudentService _studentService = new StudentService();

        public async Task<Student> GetStudentProfile()
        {
            return await _studentService.GetStudentProfileAsync();
        }
    }
}
