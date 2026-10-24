using Microsoft.Data.SqlClient;
using StudentAdoNet.Models;

namespace StudentAdoNet.Data;


public class StudentRepository
{
    private readonly string _connectionString;

    public StudentRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");
    }

    private SqlConnection CreateConnection() => new(_connectionString);


    public async Task<List<Student>> GetAllAsync()
    {
        var students = new List<Student>();
        await using var connection = CreateConnection();
        const string sql = "SELECT Id, FirstName, LastName, Email, Course FROM Students ORDER BY Id";
        await using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            students.Add(MapStudent(reader));
        }
        return students;
    }


    public async Task<Student?> GetByIdAsync(int id)
    {
        await using var connection = CreateConnection();
        const string sql = "SELECT Id, FirstName, LastName, Email, Course FROM Students WHERE Id = @Id";
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapStudent(reader);
        }
        return null;
    }


    public async Task<int> AddAsync(Student student)
    {
        await using var connection = CreateConnection();
        const string sql = """
            INSERT INTO Students (FirstName, LastName, Email, Course)
            VALUES (@FirstName, @LastName, @Email, @Course);
            SELECT CAST(SCOPE_IDENTITY() AS int);
            """;
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@FirstName", student.FirstName);
        command.Parameters.AddWithValue("@LastName", student.LastName);
        command.Parameters.AddWithValue("@Email", student.Email);
        command.Parameters.AddWithValue("@Course", student.Course);
        await connection.OpenAsync();
        var newId = await command.ExecuteScalarAsync();
        return Convert.ToInt32(newId);
    }

    public async Task<bool> UpdateAsync(Student student)
    {
        await using var connection = CreateConnection();
        const string sql = """
            UPDATE Students
            SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Course = @Course
            WHERE Id = @Id
            """;
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@FirstName", student.FirstName);
        command.Parameters.AddWithValue("@LastName", student.LastName);
        command.Parameters.AddWithValue("@Email", student.Email);
        command.Parameters.AddWithValue("@Course", student.Course);
        command.Parameters.AddWithValue("@Id", student.Id);
        await connection.OpenAsync();
        var rows = await command.ExecuteNonQueryAsync();
        return rows > 0;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        await using var connection = CreateConnection();
        const string sql = "DELETE FROM Students WHERE Id = @Id";
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);
        await connection.OpenAsync();
        var rows = await command.ExecuteNonQueryAsync();
        return rows > 0;
    }


    public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
    {
        await using var connection = CreateConnection();
        var sql = "SELECT COUNT(*) FROM Students WHERE LOWER(Email) = LOWER(@Email)";
        if (excludeId.HasValue)
        {
            sql += " AND Id <> @ExcludeId";
        }
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Email", email);
        if (excludeId.HasValue)
        {
            command.Parameters.AddWithValue("@ExcludeId", excludeId.Value);
        }
        await connection.OpenAsync();
        var count = await command.ExecuteScalarAsync();
        return Convert.ToInt32(count) > 0;
    }


    private static Student MapStudent(SqlDataReader reader) => new()
    {
        Id = reader.GetInt32(0),
        FirstName = reader.GetString(1),
        LastName = reader.GetString(2),
        Email = reader.GetString(3),
        Course = reader.GetString(4)
    };
}
