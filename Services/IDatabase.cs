namespace MyDiaryApp;

// Interface for CRUD operations on generic objects
public interface IDatabase
{
	public bool Create<T>(T item) where T : class;
	public List<T>? Read<T>(T? filter = null) where T : class;
	public bool Update<T>(T item) where T : class;
	public bool Delete<T>(T filter) where T : class;
}