using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Traningsdagbok.Models;

namespace Traningsdagbok
{
    public class Database
    {
        MongoClient dbClient = new MongoClient();

        private IMongoDatabase GetDb()
        {
            return dbClient.GetDatabase("TraningsdagbokDB");
        }
        public async Task<List<Exercise>> GetExerciseAsync()
        {
            return await GetDb().GetCollection<Exercise>("Exercises")
                 .Find(e => true)
                 .ToListAsync();

        }

        public async Task SaveExercise(Exercise exercise)
        {
            await GetDb().GetCollection<Exercise>("Exercises")
                 .InsertOneAsync(exercise);
        }



        public async Task<Exercise> GetExercise(string id)
        {
            ObjectId _id = new ObjectId(id);

            var exercise = await GetDb().GetCollection<Exercise>("Exercises")
                .Find(e => e.Id == _id)
                .SingleOrDefaultAsync();
            return exercise;
        }

        public async Task DeleteExercise(string id)
        {
            ObjectId _id = new ObjectId(id);

            await GetDb().GetCollection<Exercise>("Exercises")
                .DeleteOneAsync(e => e.Id == _id);
        }

        public async Task UpdateExercise(Exercise updatedExercise)
        {
            var filter = Builders<Exercise>.Filter.Eq(e => e.Id, updatedExercise.Id);
            var update = Builders<Exercise>.Update
                .Set(e => e.Name, updatedExercise.Name)
                .Set(e => e.Description, updatedExercise.Description);

            await GetDb().GetCollection<Exercise>("Exercises")
           .UpdateOneAsync(filter, update);
        }

        public async Task<List<ExerciseSession>> GetExerciseSessionsAsync()
        {
            return await GetDb().GetCollection<ExerciseSession>("ExerciseSessions")
                .Find(es => true)
                .ToListAsync();
        }

        public async Task SaveExerciseSession(ExerciseSession exerciseSession)
        {
            await GetDb().GetCollection<ExerciseSession>("ExerciseSessions")
                .InsertOneAsync(exerciseSession);
        }


        public async Task DeleteExerciseSession(string id)
        {
            ObjectId _id = new ObjectId(id);

            await GetDb().GetCollection<ExerciseSession>("ExerciseSessions")
                .DeleteOneAsync(es => es.Id == _id);
        }


    }

}

