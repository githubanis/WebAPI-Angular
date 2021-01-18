﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configaration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configaration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select EmployeeId, EmployeeName, Department,
                convert(varchar(10),DateOfJoining,120) as DateOfJoining, PhotoFileName from dbo.Employee";

            DataTable table = new DataTable();

            string sqlDataSource = _configaration.GetConnectionString("EmployeeAppCon");

            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            string query = @"
                insert into dbo.Employee 
                (EmployeeName, Department, DateOfJoining, PhotoFileName)
                values
                (
                '" + emp.EmployeeName + @"'
                ,'" + emp.Department + @"'
                ,'" + emp.DateOfJoining + @"'
                ,'" + emp.PhotoFileName + @"'
                )
                ";

            DataTable table = new DataTable();

            string sqlDataSource = _configaration.GetConnectionString("EmployeeAppCon");

            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully!");
        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string query = @"
                update dbo.Employee set
                EmployeeName = '" + emp.EmployeeName + @"'
                ,Department = '" + emp.Department + @"'
                ,DateOfJoining = '" + emp.DateOfJoining + @"'
                where EmployeeId = " + emp.EmployeeId + @"
                ";

            DataTable table = new DataTable();

            string sqlDataSource = _configaration.GetConnectionString("EmployeeAppCon");

            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Update Successfully!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                delete from dbo.Employee
                where EmployeeId = " + id + @"
                ";

            DataTable table = new DataTable();

            string sqlDataSource = _configaration.GetConnectionString("EmployeeAppCon");

            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Delete Successfully!");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _webHostEnvironment.ContentRootPath + "/Photos/" + fileName;

                using(var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(fileName);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }
        }

        [Route("GetAllDepartmentName")]
        [HttpGet]
        public JsonResult GetAllDepartmentName()
        {
            string query = @"
                select DepartmentName from dbo.Department";

            DataTable table = new DataTable();

            string sqlDataSource = _configaration.GetConnectionString("EmployeeAppCon");

            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
    }
}
