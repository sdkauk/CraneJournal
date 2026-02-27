using MongoDB.Driver;
using CraneJournal.Common.Models;

namespace CraneJournal.DataAccess
{
    public interface IDbConnection
    {
        IMongoCollection<JournalEntry> JournalEntryCollection { get; }
        string JournalEntryCollectionName { get; }
        MongoClient Client { get; }
    }
}