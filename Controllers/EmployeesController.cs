using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlClient;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public readonly IConfiguration configuration;
        public EmployeesController(IConfiguration configuration)
        {
            this.configuration = configuration; 
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public string GetEmployees()
        {
            SqlConnection con = new SqlConnection(configuration.GetConnectionString("EmployeeAppCon").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Employees;", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<Employee> employeeList = new List<Employee>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
               for(int i=0; i<dt.Rows.Count; i++)
                {
                    Employee employee = new Employee();
                    employee.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    employee.USERNAME = Convert.ToString(dt.Rows[i]["USERNAME"]);
                    employee.FNAME = Convert.ToString(dt.Rows[i]["FNAME"]);
                    employee.LNAME = Convert.ToString(dt.Rows[i]["LNAME"]);
                    employee.EMAIL = Convert.ToString(dt.Rows[i]["EMAIL"]);
                    employee.PHONE = Convert.ToInt32(dt.Rows[i]["PHONE"]);
                    employeeList.Add(employee);
                }
            }
            if (employeeList.Count > 0) { 
              return JsonConvert.SerializeObject(employeeList);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "NO DATA FOUND";
                return JsonConvert.SerializeObject(response);
            }
        
        }
    }
}
