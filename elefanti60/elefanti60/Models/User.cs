namespace elefanti60.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get
            {
                return DateTime.Now.Subtract(DOB).Days / 365;
            }
        }
        public string Address { get; set; }
        public decimal CardNumber { get; set; }
        public decimal Amount { get; set; }
        
    }
}
