using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace SSPWorld.Models
{
    public enum Term { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Last}
    public enum CourseType { Core, Elective, Humanity }

    public class Course
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("_id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public int CreditHours { get; set; }
        public CourseType CourseType { get; set; }

        [JsonProperty("term")]
        public Term CourseTerm { get; set; }
    }
}
