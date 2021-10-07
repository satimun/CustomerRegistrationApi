using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CustomerRegistrationAPI.Engine;
using CustomerRegistrationAPI.Engine.DBase.BWC;
using CustomerRegistrationADO.Connect.Mssql.CRMDB;
using CustomerRegistrationADO.Connect.Mssql.CRMDB.Table;

namespace CustomerRegistrationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string GET()
        {
            return pdrga_s_memberAdo.GetInstant().GetServerAddr();
            //return "GetServerAddr";
        }

        [HttpGet("GetWeatherForecast")]
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            string jsonString = JsonSerializer.Serialize(Summaries);

            var rng = new Random();

             

          //  string FilePath = "\\191.20.2.45\\Vol\\BKKF\\DB";//";//D:\\backup";

            //  var currentDirectory = System.IO.Directory.GetCurrentDirectory();

            // string conString = $"Provider=vfpoledb;Data Source={FilePath};Collating Sequence=machine;";


            //   conString = "Provider=vfpoledb;Data Source=" + FilePath + ";Collating Sequence=machine;"; //machine
            //  string DBF_FileName = "COLOR";
            // DBF_FileName = "PRODDM";

            //  OleDbConnection conn = new OleDbConnection(conString);

            // OleDbDataAdapter DA = new OleDbDataAdapter();
            // conn.Open();

            // OleDbCommand cmd3 = new OleDbCommand("insert into COLOR(COLOR_ID,COLOR_NAME ) values ('9999','9999')", conn);
            // cmd3.ExecuteNonQuery();
            /*
             OleDbCommand cmd3 = new OleDbCommand("UPDATE COLOR SET COLOR_NAME = 'EDIT 01' WHERE COLOR_ID = '9999' ", conn);
               cmd3.ExecuteNonQuery();
             */
            //   OleDbCommand cmd3 = new OleDbCommand("delete from COLOR WHERE COLOR_ID = '9999' ", conn);
            //    cmd3.ExecuteNonQuery();


            // OleDbCommand command = new OleDbCommand("SELECT * FROM RUN_OUS;", conn);
            // conn.Open();


            /*


                        DataTable dt1 = new DataTable();
                        dt1.Load(command.ExecuteReader());

                        DataRow dr = dt1.NewRow();
                        dr["COLOR_ID"] = "9999";
                        dr["COLOR_NAME"] = "9999";
                        // CustomerRegistrationDBase.Class1.GetInstant().getDbase();

                        dt1.Rows.Add(dr);

                        */

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // GET api/values/5
        [HttpGet("{tb}")]
        public ActionResult<string> Get(string tb)
        {
            var model = CustomerRegistrationADO.Connect.Mssql.CRMDB.Common.TableStructureAdo.GetInstant().GenModelClass(tb).ToString();

            return model;
        }



    }
}
