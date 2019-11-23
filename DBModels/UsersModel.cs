using System;
using System.Collections.Generic;
using System.Text;

namespace DBModels
{
    public class UsersModel
    {
        public string ID { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public string AdminEmail { get; set; }
        public string AdminUsername { get; set; }
        public string AdminPassword { get; set; }
        public string AdminRole { get; set; }
        public string Created_at { get; set; }
    }
}
