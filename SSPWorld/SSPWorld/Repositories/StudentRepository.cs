using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SSPWorld.Models;
using SSPWorld.Services;

namespace SSPWorld.Repositories
{
    public class StudentRepository
    {
        private readonly StudentService _studentService = new StudentService();

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _studentService.GetStudentsAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _studentService.GetStudentByIdAsync(id);
        }

        public async Task<Student> GetStudentBySSPId(int SSPid)
        {
            return await _studentService.GetStudentBySSPIdAsync(SSPid);
        }

    }
}
