using CraneJournal.BusinessLogic.JournalEntries;
using CraneJournal.DataAccess;
using CraneJournal.DataAccess.DataSeeder;
using CraneJournal.DataAccess.Repositories;

namespace CraneJournal.API
{
    public static class RegisterServices
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IDbConnection, DbConnection>();

            builder.Services.AddScoped<IJournalEntryRepository, JournalEntryRepository>();
            builder.Services.AddTransient<JournalEntrySeeder>();
            builder.Services.AddTransient<DataSeeder>();

            builder.Services.AddScoped<IJournalEntryService, JournalEntryService>();
        }
    }
}
