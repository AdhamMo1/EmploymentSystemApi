﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmploymentSystemApplication.DTOs.Authentication
{
    public class AuthenticationResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public bool IsAuthenticated { get; set; } = false;
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
        public DateTime ExpirationOn { get; set; }
    }
}
