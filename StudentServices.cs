using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityAPI.Models;

namespace UniversityAPI.Services
{
    public class StudentService
    {
        private readonly string _connectionString;

        public StudentService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Student>("SELECT * FROM Students");
            }
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Student>("SELECT * FROM Students WHERE StudentId = @Id", new { Id = id });
            }
        }

        public async Task<int> CreateStudentAsync(Student student)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO Students (FirstName, LastName, DepartmentId, IsActive) VALUES (@FirstName, @LastName, @DepartmentId, @IsActive)";
                return await connection.ExecuteAsync(query, student);
            }
        }

        public async Task<int> UpdateStudentAsync(Student student)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "UPDATE Students SET FirstName = @FirstName, LastName = @LastName, DepartmentId = @DepartmentId, IsActive = @IsActive WHERE StudentId = @StudentId";
                return await connection.ExecuteAsync(query, student);
            }
        }

        public async Task<int> DeleteStudentAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteAsync("DELETE FROM Students WHERE StudentId = @Id", new { Id = id });
            }
        }
    }
}
