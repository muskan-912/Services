using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityAPI.Models;

namespace UniversityAPI.Services
{
    public class DepartmentService
    {
        private readonly string _connectionString;

        public DepartmentService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Department>("SELECT * FROM Departments");
            }
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Department>("SELECT * FROM Departments WHERE DepartmentId = @Id", new { Id = id });
            }
        }

        public async Task<int> CreateDepartmentAsync(Department department)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO Departments (Name, UniversityId, DepartmentHeadId) VALUES (@Name, @UniversityId, @DepartmentHeadId)";
                return await connection.ExecuteAsync(query, department);
            }
        }

        public async Task<int> UpdateDepartmentAsync(Department department)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "UPDATE Departments SET Name = @Name, UniversityId = @UniversityId, DepartmentHeadId = @DepartmentHeadId WHERE DepartmentId = @DepartmentId";
                return await connection.ExecuteAsync(query, department);
            }
        }

        public async Task<int> DeleteDepartmentAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteAsync("DELETE FROM Departments WHERE DepartmentId = @Id", new { Id = id });
            }
        }
    }
}
