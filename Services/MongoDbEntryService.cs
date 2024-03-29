namespace MyDiaryApp.Services;

using MyDiaryApp.Models;
using MyDiaryApp.Configuration;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

public class MongoDbEntryService : IEntryDbService
{
    private readonly IMongoCollection<Entry> _entryList;

    public MongoDbEntryService(IOptions<MongoDbEntrySettings> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        var database = client.GetDatabase(options.Value.DatabaseName);
        _entryList = database.GetCollection<Entry>(options.Value.CollectionName);
    }

    public Entry Create(Entry entry)
    {
        if (entry.Id == Guid.Empty)
        {
            entry.Id = Guid.NewGuid();
        }
        _entryList.InsertOne(entry);
        return entry;
    }

    public Entry Delete(Guid guid)
    {
        var entry = Read(guid);
        _entryList.DeleteOne(e => e.Id == guid);
        return entry;
    }

    public Entry Read(Guid guid)
    {
        return _entryList.Find(e => e.Id == guid).FirstOrDefault();
    }

    public IEnumerable<Entry> ReadAll()
    {
        return _entryList.Find(e => true).ToList();
    }

    public Entry Update(Entry entry, Guid guid)
    {
        _entryList.ReplaceOne(e => e.Id == guid, entry);
        return entry;
    }
}