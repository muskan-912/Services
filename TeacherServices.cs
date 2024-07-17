using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityAPI.Models;

namespace UniversityAPI.Services
{
    public class TeacherService
    {
        private readonly string _connectionString;

        public TeacherService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Teacher>("SELECT * FROM Teachers");
            }
        }

        public async Task<Teacher> GetTeacherByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Teacher>("SELECT * FROM Teachers WHERE TeacherId = @Id", new { Id = id });
            }
        }

        public async Task<int> CreateTeacherAsync(Teacher teacher)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO Teachers (FirstName, LastName, Email, DepartmentId) VALUES (@FirstName, @LastName, @Email, @DepartmentId)";
                return await connection.ExecuteAsync(query, teacher);
            }
        }

        public async Task<int> UpdateTeacherAsync(Teacher teacher)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "UPDATE Teachers SET FirstName = @FirstName, LastName = @LastName, Email = @Email, DepartmentId = @DepartmentId WHERE TeacherId = @TeacherId";
                return await connection.ExecuteAsync(query, teacher);
            }
        }

        public async Task<int> DeleteTeacherAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteAsync("DELETE FROM Teachers WHERE TeacherId = @Id", new { Id = id });
            }
        }
    }
}
