using WebAllUni_Manager.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ClassLibrary;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAllUni_Manager.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class MongoDBController : Controller
    {

        private IMongoCollection<Student> mongoCollection;

        public MongoDBController(IOptions<DBConnectionMongo> options)
        {

            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDB = mongoClient.GetDatabase(options.Value.DatabaseName);

            mongoCollection = mongoDB.GetCollection<Student>(options.Value.CollectionName);

        }

        [HttpGet]

        public async Task<List<Student>> Get()
        {

            return await mongoCollection.Find(_ => true).ToListAsync();

        }

        [HttpGet("{matricola}")]

        public async Task<List<Student>> GetMatricola(string matricola)
        {

            return await mongoCollection.Find(std => std.Matricola == matricola).ToListAsync();

        }

        [HttpDelete("{matricola}")]

        public async Task<DeleteResult> Delete(string matricola)
        {

            return await mongoCollection.DeleteOneAsync(std => std.Matricola == matricola);
        
        }


        [HttpPost]

        public async Task<ActionResult> InsertStudent(Student student)
        {

            await mongoCollection.InsertOneAsync(student);
            return CreatedAtAction(nameof(Get), new { id = student.Matricola }, student);

            //return CreatedAtAction(nameof(stdloyee), new {id = stdloyee.Id}, stdloyee);

        }

        [HttpPut("{matricola}")]

        public async Task<ActionResult> UpdateStudent(string matricola, Student student)
        {

            var search = Builders<Student>.Filter.Eq(std => std.Matricola, matricola);

            var update = Builders<Student>.Update
                .Set(std => std.Matricola, student.Matricola)
                .Set(std => std.Department, student.Department);

            await mongoCollection.UpdateOneAsync(search, update);

            return NoContent();

        }

    }

}
