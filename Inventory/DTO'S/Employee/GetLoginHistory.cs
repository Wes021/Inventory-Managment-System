namespace Inventory.DTO_S.Employee
{
    public class GetLoginHistory
    {
        public int id { get; set; }

        public string EmployeeName { get; set; }



        public string email { get; set; }

        public DateTime loginTime { get; set; }

        public string IPAddress { get; set; }

        public string UserAgent { get; set; }
    }
}
