﻿namespace CustomerCRUD.DTO
{
    public class UpdateCustomerModel
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }


        public string? Address { get; set; }


        public DateTime? DateOfBirth { get; set; }
    }
}
