using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Data;

namespace fast_notes_app
{
    public class NotesManager
    {
        private readonly string connectionString;

        public NotesManager()
        {
            connectionString = "Server=186.19.152.34;Port=3306;Database=notes_test;User Id=root;Password=Luna2020@;";
        }

        public async Task<List<NoteInfo>> GetUserNotesAsync(int userId)
        {
            var notes = new List<NoteInfo>();

            try
            {
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                string query = @"
                    SELECT Id, Title, Content, CreatedAt, UpdatedAt, SortOrder 
                    FROM Notes 
                    WHERE UserId = @userId AND IsDeleted = FALSE 
                    ORDER BY SortOrder ASC, CreatedAt DESC";

                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    notes.Add(new NoteInfo
                    {
                        Id = reader.GetInt32("Id"),
                        Title = reader.GetString("Title"),
                        Content = reader.IsDBNull("Content") ? "" : reader.GetString("Content"),
                        CreatedAt = reader.GetDateTime("CreatedAt"),
                        UpdatedAt = reader.GetDateTime("UpdatedAt"),
                        SortOrder = reader.GetInt32("SortOrder")
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get notes: {ex.Message}");
            }

            return notes;
        }

        public async Task<(bool success, string message, int noteId)> CreateNoteAsync(int userId, string title, string content = "")
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                string sortQuery = "SELECT COALESCE(MAX(SortOrder), 0) + 1 FROM Notes WHERE UserId = @userId AND IsDeleted = FALSE";
                using var sortCommand = new MySqlCommand(sortQuery, connection);
                sortCommand.Parameters.AddWithValue("@userId", userId);
                int nextSortOrder = Convert.ToInt32(await sortCommand.ExecuteScalarAsync());

                string insertQuery = @"
                    INSERT INTO Notes (UserId, Title, Content, SortOrder) 
                    VALUES (@userId, @title, @content, @sortOrder);
                    SELECT LAST_INSERT_ID();";

                using var command = new MySqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@content", content);
                command.Parameters.AddWithValue("@sortOrder", nextSortOrder);

                var result = await command.ExecuteScalarAsync();
                int noteId = Convert.ToInt32(result);

                return (true, "Note created successfully", noteId);
            }
            catch (Exception ex)
            {
                return (false, $"Failed to create note: {ex.Message}", 0);
            }
        }

        public async Task<(bool success, string message)> UpdateNoteAsync(int noteId, int userId, string title, string content)
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                string updateQuery = @"
                    UPDATE Notes 
                    SET Title = @title, Content = @content, UpdatedAt = CURRENT_TIMESTAMP 
                    WHERE Id = @noteId AND UserId = @userId AND IsDeleted = FALSE";

                using var command = new MySqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@noteId", noteId);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@content", content);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                    return (true, "Note updated successfully");
                else
                    return (false, "Note not found or access denied");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to update note: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> DeleteNoteAsync(int noteId, int userId)
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                string deleteQuery = @"
                    UPDATE Notes 
                    SET IsDeleted = TRUE, UpdatedAt = CURRENT_TIMESTAMP 
                    WHERE Id = @noteId AND UserId = @userId";

                using var command = new MySqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@noteId", noteId);
                command.Parameters.AddWithValue("@userId", userId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                    return (true, "Note deleted successfully");
                else
                    return (false, "Note not found or access denied");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to delete note: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> UpdateNoteSortOrderAsync(int noteId, int userId, int newSortOrder)
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                string updateQuery = @"
                    UPDATE Notes 
                    SET SortOrder = @sortOrder, UpdatedAt = CURRENT_TIMESTAMP 
                    WHERE Id = @noteId AND UserId = @userId AND IsDeleted = FALSE";

                using var command = new MySqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@noteId", noteId);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@sortOrder", newSortOrder);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                    return (true, "Sort order updated successfully");
                else
                    return (false, "Note not found or access denied");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to update sort order: {ex.Message}");
            }
        }
    }

    public class NoteInfo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int SortOrder { get; set; }
    }
}
