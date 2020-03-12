using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace SSPWorld.Models
{
    public class Update
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("_id")]
        public string Id { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("_courseId")]
        public string CourseId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("deadline")]
        public DateTime Deadline { get; set; }
    }
}
