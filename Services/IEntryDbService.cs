using MyDiaryApp.Models;

namespace MyDiaryApp;

// Interface for CRUD operations on generic objects
public interface IEntryDbService
{
	public Entry Create(Entry entry);
	public Entry Read(Guid guid);
	public IEnumerable<Entry> ReadAll();
	public Entry Update(Entry entry, Guid guid);
	public Entry Delete(Guid guid);
}