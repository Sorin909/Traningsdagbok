using MongoDB.Bson;

namespace Traningsdagbok.Models
{
    public class ExerciseSession
    {
        public ObjectId Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public ObjectId ExerciseId { get; set; }
        public string Quantity { get; set; }
      
        public Exercise Exercise { get; set; }
    }
}
