﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public EmployeeController(IConfiguration configuration)
        {
            _configaration = configuration;
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

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
