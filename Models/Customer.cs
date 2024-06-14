namespace CustomerCRUD.Models
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public string Phone { get; set; } = String.Empty;


        public string Address { get; set; } = String.Empty;


        public DateTime DateOfBirth { get; set; }
    }
}
