using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace fast_notes_app
{
    public class DatabaseHelper
    {
        private readonly string connectionString;

        public DatabaseHelper()
        {
            connectionString = "Server=186.19.152.34;Port=3306;Database=notes_test;User Id=root;Password=Luna2020@;";
        }

        // Test database connection
        public async Task<bool> TestConnectionAsync()
        {
            DebugPasswordHash();

            try
            {
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
                return false;
            }
        }

        public static string HashPasswordWithSalt(string password, string base64Salt)
        {
            byte[] saltBytes = Convert.FromBase64String(base64Salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(32);

            return Convert.ToBase64String(hashBytes);
        }


        public (string hash, string salt) HashPassword(string password)
        {
            byte[] saltBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);
            string salt = Convert.ToBase64String(saltBytes);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(32);
            string hash = Convert.ToBase64String(hashBytes);
            return (hash, salt);
        }

        // Verify password
        public bool VerifyPassword(string password, string hash, string salt)
        {
            try
            {
                byte[] saltBytes = Convert.FromBase64String(salt);
                using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256);
                byte[] hashBytes = pbkdf2.GetBytes(32);
                string computedHash = Convert.ToBase64String(hashBytes);
                return computedHash == hash;
            }
            catch
            {
                return false;
            }
        }

        public void DebugPasswordHash()
        {
            string password = "TESTPASW";

            var (hash, salt) = HashPassword(password);
            System.Diagnostics.Debug.WriteLine($"Password: {password}");
            System.Diagnostics.Debug.WriteLine($"Salt: {salt}");
            System.Diagnostics.Debug.WriteLine("Hash: " + hash);

            bool isValid = VerifyPassword(password, hash, salt);
            Console.WriteLine($"Verificación correcta? {isValid}");

            bool isInvalid = VerifyPassword("otra_contraseña", hash, salt);
            Console.WriteLine($"Verificación con contraseña incorrecta: {isInvalid}");
        }

        public async Task<(bool success, string message)> RegisterUserAsync(string username, string email, string password)
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                // Check if username or email exists
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @username OR Email = @email";
                using var checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@username", username);
                checkCommand.Parameters.AddWithValue("@email", email);

                long existingCount = (long)await checkCommand.ExecuteScalarAsync();
                if (existingCount > 0)
                {
                    return (false, "Username or email already exists");
                }

                var (hash, salt) = HashPassword(password);

                string insertQuery = @"
                    INSERT INTO Users (Username, Email, PasswordHash, Salt, CreatedAt) 
                    VALUES (@username, @email, @hash, @salt, NOW())";

                using var insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@username", username);
                insertCommand.Parameters.AddWithValue("@email", email);
                insertCommand.Parameters.AddWithValue("@hash", hash);
                insertCommand.Parameters.AddWithValue("@salt", salt);

                await insertCommand.ExecuteNonQueryAsync();
                return (true, "User registered successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Registration failed: {ex.Message}");
            }
        }

        public async Task<(bool success, string message, UserInfo? user)> LoginUserAsync(string username, string password)
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                string query = @"
                    SELECT Id, Username, Email, PasswordHash, Salt, IsActive 
                    FROM Users 
                    WHERE Username = @username AND IsActive = 1";

                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    string storedHash = reader.GetString("PasswordHash");
                    string storedSalt = reader.GetString("Salt");

                    if (VerifyPassword(password, storedHash, storedSalt))
                    {
                        var user = new UserInfo
                        {
                            Id = reader.GetInt32("Id"),
                            Username = reader.GetString("Username"),
                            Email = reader.GetString("Email")
                        };

                        _ = UpdateLastLoginAsync(user.Id);

                        return (true, "Login successful", user);
                    }
                    else
                    {
                        return (false, "Invalid password", null);
                    }
                }
                else
                {
                    return (false, "User not found", null);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Login failed: {ex.Message}", null);
            }
        }

        public async Task<(bool success, string message, UserInfo? user)> ValidateUserByIdAsync(int userId, string username)
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                string query = @"
                    SELECT Id, Username, Email, IsActive 
                    FROM Users 
                    WHERE Id = @userId AND Username = @username AND IsActive = 1";

                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@username", username);

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var user = new UserInfo
                    {
                        Id = reader.GetInt32("Id"),
                        Username = reader.GetString("Username"),
                        Email = reader.GetString("Email")
                    };

                    _ = UpdateLastLoginAsync(user.Id);

                    return (true, "User validated successfully", user);
                }
                else
                {
                    return (false, "User not found or inactive", null);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Validation failed: {ex.Message}", null);
            }
        }

        private async Task UpdateLastLoginAsync(int userId)
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                string updateQuery = "UPDATE Users SET LastLogin = NOW() WHERE Id = @userId";

                using var command = new MySqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@userId", userId);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating last login: {ex.Message}");
            }
        }
    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
