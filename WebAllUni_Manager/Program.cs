using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using WebAllUni_Manager.DataModel;

namespace WebAllUni_Manager
{
    public class Program
    {

        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            StudentsService studentService = new StudentsService(DBConnection.ConnectionString);

            builder.Services.AddDbContext<StudentDBContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("UniversityDB")));

            builder.Services.Configure<DBConnectionMongo>(builder.Configuration.GetSection("UniversityMongoDB"));
            builder.Services.AddSingleton<DBConnectionMongo>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

        }

    }

}
