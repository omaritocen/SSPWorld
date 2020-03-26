using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SSPWorld.Models
{
    public class RegisterModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("sspID")]
        public int SSPId { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("confirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}
