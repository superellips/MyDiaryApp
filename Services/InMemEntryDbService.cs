using MyDiaryApp.Models;

namespace MyDiaryApp.Services;

public class InMemEntryDbService : IEntryDbService
{
    private List<Entry> _entries = new List<Entry>()
    {
        new Entry(){
            Id = Guid.NewGuid(),
            Title = "My First Entry",
            Body = "This is my first entry and I'm very happy with it.",
            Created = DateTime.Now,
            Edited = DateTime.Now
        }
    };

    public InMemEntryDbService()
    {

    }

    public Entry Create(Entry entry)
    {
        var newEntry = new Entry()
        {
            Id = Guid.NewGuid(),
            Title = entry.Title,
            Body = entry.Body,
            Created = entry.Created,
            Edited = entry.Created
        };
        _entries.Add(newEntry);
        return newEntry;
    }

    public Entry Delete(Guid guid)
    {
        var entry = Read(guid);
        if (entry is not null) _entries.Remove(entry);
        return entry;
    }

    public Entry Read(Guid guid)
    {
        return _entries.FirstOrDefault(e => e.Id == guid) ??
            throw new KeyNotFoundException($"Entry with Id {guid} not found");
    }

    public IEnumerable<Entry> ReadAll()
    {
        var entriesList = new List<Entry>();
        foreach (var e in _entries)
        {
            entriesList.Add(new Entry(){ Id = e.Id, Body = e.Body, Title = e.Title, Created = e.Created, Edited = e.Edited });
        }
        return entriesList;
    }

    public Entry Update(Entry entry, Guid guid)
    {
        var originalEntry = _entries.FirstOrDefault(e => e.Id == guid) ?? throw new KeyNotFoundException($"Entry with Id {guid} not found");
        originalEntry.Body = entry.Body;
        originalEntry.Title = entry.Title;
        originalEntry.Edited = DateTime.Now;

        return Read(guid);
    }
}