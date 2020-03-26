using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SSPWorld.Models
{

    public enum Year { Prep, First, Second, Third, Senior }
    public enum Department { General, CCE, EME, CAE, BME, GPE, OCE }

    public class Student
    {
        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("year")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Year Year { get; set; }

        [JsonProperty("department")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Department Department { get; set; }

    }
}
