using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSPWorld.Models;

namespace SSPWorld.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task<Student> GetStudentBySSPIdAsync(int SSPId);

    }

    public class StudentService : IStudentService
    {
        private readonly List<Student> _students = new List<Student>()
        {
            new Student()
            {
                SSPId = 6294,
                FirstName = "Omar",
                LastName = "Alaa",
                Image = "https://scontent-hbe1-1.xx.fbcdn.net/v/t1.0-9/s960x960/68626985_3243241599024559_4162686666969972736_o.jpg?_nc_cat=102&_nc_ohc=4KyHe4owRncAQm8ohwSvy93J1NxA-_0PX2yo2tmMMc31TQzBsNxEcg7rQ&_nc_ht=scontent-hbe1-1.xx&oh=e0ef18289e90bbf3abd440d0ca7d78ca&oe=5E6DFC54",
                Year = Year.First,
                Department = Department.CCE
            },

            new Student()
            {
                SSPId = 6206,
                FirstName = "Yosra",
                LastName = "Emad",
                Image = "https://scontent-hbe1-1.xx.fbcdn.net/v/t1.0-9/66831937_2411318315770546_1689479936261226496_n.jpg?_nc_cat=101&_nc_ohc=47Ie0DUeTGIAQlgcQMM_hRs_6eMG68LGWuFQTXXPO8xvEfRnK2Y6ZWTRQ&_nc_ht=scontent-hbe1-1.xx&oh=529b44d5504aa9852fd19aee4c3c47cb&oe=5E3EC1B1",
                Year = Year.First,
                Department = Department.CCE
            },

            new Student()
            {
                SSPId = 5201,
                FirstName = "Nadine",
                LastName = "Emad",
                Image = "https://scontent-hbe1-1.xx.fbcdn.net/v/t1.0-9/73472584_2606895349331388_3210652366323843072_n.jpg?_nc_cat=108&_nc_ohc=-2Ask3zHBFsAQn0KlvYx2bF6-qe8UejQhA27PAM_YkMPws86WkWXiio2w&_nc_ht=scontent-hbe1-1.xx&oh=a7fdafc04e7a5264db4e27aa2c0df03d&oe=5E74E128",
                Year = Year.Second,
                Department = Department.CAE
            },

            new Student()
            {
                SSPId = 7132,
                FirstName = "Muhammed",
                LastName = "Saeed",
                Image = "https://scontent-hbe1-1.xx.fbcdn.net/v/t1.0-9/p960x960/76653007_2673994055955408_9198177080091607040_o.jpg?_nc_cat=111&_nc_ohc=xyM0Oydeyv8AQmZymEqT_bVXVBeYA-F8icG1fkL07ktTYuiC37zBQGy0g&_nc_ht=scontent-hbe1-1.xx&oh=0920a77c77f1cb835a971c6cf6c1585b&oe=5E83A811",
                Year = Year.Prep,
                Department = Department.General
            }

        };

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return _students;
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return _students.FirstOrDefault(x => x.SSPId == id);
        }

        public async Task<Student> GetStudentBySSPIdAsync(int SSPId)
        {
            return  _students.FirstOrDefault(x => x.SSPId == SSPId);
        }
    }
}
