using ApiTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace ApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        string con = "Data Source=TestApiDB.mssql.somee.com;Initial Catalog=TestApiDB;User ID=DeadHatred_SQLLogin_2;Password=";

        [HttpPost("register")]
        public bool Register([FromBody] LoginData data)
        {
            SqlConnection connection = new SqlConnection(con);
            SqlCommand command = new SqlCommand(
                "INSERT INTO [User] (Login, Password) VALUES (@log, @pas);"
                , connection);
            command.Parameters.AddWithValue("@log", data.Login);
            command.Parameters.AddWithValue("@pas", data.Password);
            connection.Open();
            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e) { 
                return false; 
            }
        }

        [HttpPost("login")]
        public string Login([FromBody] LoginData data)
        {
            SqlConnection connection = new SqlConnection(con);
            SqlCommand command = new SqlCommand(
                "SELECT * FROM [User] WHERE LOGIN = @log AND PASSWORD = @pas;"
                , connection);
            command.Parameters.AddWithValue("@log", data.Login);
            command.Parameters.AddWithValue("@pas", data.Password);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            string result = string.Empty;
            try
            {
                while (reader.Read())
                {
                    result += reader["Login"];
                }
                reader.Close();
                return result != string.Empty ? result : string.Empty;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        private string generateNewCode()
        {
            List<string> codes = new List<string>();
            SqlConnection connection = new SqlConnection(con);
            SqlCommand command = new SqlCommand(
                "SELECT Code FROM [CONTROLLER]"
                , connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                codes.Add(reader["Code"].ToString());
            }
            reader.Close();
            Random random = new Random(DateTime.UtcNow.Millisecond);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            while (true)
            {
                string res = new string(Enumerable.Repeat(chars, 16)
                .Select(s => s[random.Next(s.Length)]).ToArray());
                if (!codes.Contains(res)) return res;
            }
            
        }

        private bool addNewCode(string code, int userId)
        {
            SqlConnection connection = new SqlConnection(con);
            SqlCommand command = new SqlCommand(
                "INSERT INTO [CONTROLLER] (User_ID, Code) VALUES (@uid, @code);"
                , connection);
            command.Parameters.AddWithValue("@uid", userId);
            command.Parameters.AddWithValue("@code", code);
            connection.Open();
            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpGet("testsite")]
        public string TestSite()
        {
            return "success";
        }


        [HttpPost("getcode")]
        public string GetCode([FromBody] LoginData data)
        {
            SqlConnection connection = new SqlConnection(con);
            SqlCommand command = new SqlCommand(
                "SELECT * FROM [User] WHERE LOGIN = @log AND PASSWORD = @pas;"
                , connection);
            command.Parameters.AddWithValue("@log", data.Login);
            command.Parameters.AddWithValue("@pas", data.Password);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            string result = string.Empty;
            try
            {
                while (reader.Read())
                {
                    result += reader["ID"].ToString();
                }
                reader.Close();
                if (result != string.Empty)
                {
                    string code = generateNewCode();
                    addNewCode(code, Convert.ToInt32(result));
                    return code;
                } else
                {
                    return string.Empty;
                }
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
    }
}
