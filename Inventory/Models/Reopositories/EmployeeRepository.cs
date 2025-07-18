﻿using Inventory.DTO_S;
using Inventory.DTO_S.Employee;
using Inventory.DTO_S.Product;
using Inventory.Models.IRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Models.Reopositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly InventoryManagmentSystemContext _inventoryManagmentSystemContext;

        public EmployeeRepository(InventoryManagmentSystemContext inventoryManagmentSystemContext)
        {
            _inventoryManagmentSystemContext = inventoryManagmentSystemContext;
        }


        private async Task<IEnumerable<GetEmployeeDTO>> getEmployeeHelper(SqlCommand sc)
        {
            var Employees = new List<GetEmployeeDTO>();
            using (SqlConnection conn = new SqlConnection(_inventoryManagmentSystemContext.Database.GetConnectionString()))
            {
                await conn.OpenAsync();
                sc.Connection = conn;

                using (  SqlDataReader sdr = await sc.ExecuteReaderAsync())
                {
                    while (await sdr.ReadAsync())
                    {
                        var employees = new GetEmployeeDTO()
                        {
                            Id=sdr.GetString(sdr.GetOrdinal("Id")),
                            fisrtName=sdr.GetString(sdr.GetOrdinal("fisrtName")),
                            lastName=sdr.GetString(sdr.GetOrdinal("lastName")),
                            gender=sdr.GetString(sdr.GetOrdinal("gender")),
                            status=sdr.GetString(sdr.GetOrdinal("user_status")),
                            email=sdr.GetString(sdr.GetOrdinal("Email")),
                            phoneNumber=sdr.GetString(sdr.GetOrdinal("PhoneNumber"))
                        };

                        Employees.Add(employees);
                    }
                }
            }

            return Employees;
        }


        public async Task<IEnumerable<GetEmployeeDTO>> GetAllEmployeesAsync()
        {

            using (SqlCommand sc = new SqlCommand("GetEmployees"))
            {
                return await getEmployeeHelper(sc);
            }


        }

        public async Task<GetEmployeeDTO> GetEmployeeByIdAsync(string id)
        {
            using (SqlCommand sc = new SqlCommand("GetEmplpoyeeById"))
            {
                sc.CommandType = System.Data.CommandType.StoredProcedure;
                sc.Parameters.AddWithValue("@user_id", id);
                var result = await getEmployeeHelper(sc);
                return result.FirstOrDefault();

            }
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<GetLoginHistory>> GetAllLoginHistory()
        {
            

            var loginData =await _inventoryManagmentSystemContext.LoginHistories
                .Include(l => l.User)
                .OrderByDescending(l => l.loginTime)
                .Select(l => new GetLoginHistory
                {
                    id=l.id,
                    EmployeeName = l.User.fisrtName +" "+l.User.lastName,
                    email=l.User.Email,
                    loginTime=l.loginTime,
                    IPAddress=l.IPAddress,
                    UserAgent=l.UserAgent
                    
                })
                .ToListAsync();
            

            return loginData;
        }
    }
}
