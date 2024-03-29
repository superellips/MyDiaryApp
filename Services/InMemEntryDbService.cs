using MyDiaryApp.Models;

namespace MyDiaryApp.Services;

public class InMemEntryDbService : IEntryDbService
{
    private List<Entry> _entries = new List<Entry>();

    public InMemEntryDbService()
    {
        _entries.Add(new Entry(){
            Id = Guid.NewGuid(),
            Title = "My First Entry",
            Body = "This is my first entry and I'm very happy with it.",
            Created = DateTime.Now,
            Edited = DateTime.Now
        });
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
        var entry = _entries.FirstOrDefault(e => e.Id == guid) ??
            throw new Exception();
        _entries.Remove(entry);
        return entry;
    }

    public Entry Read(Guid guid)
    {
        return _entries.FirstOrDefault(e => e.Id == guid) ??
            throw new Exception();
    }

    public IEnumerable<Entry> ReadAll()
    {
        return _entries;
    }

    public Entry Update(Entry entry, Guid guid)
    {
        Delete(guid);
        return Create(entry);
    }
}