using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyDiaryApp.Models;

namespace MyDiaryApp.Pages;

public class EntryPageModel : PageModel
{
    private readonly ILogger<EntryPageModel> _logger;
    private readonly IEntryDbService _dbService;
    public Entry Entry { get; set; }

    public EntryPageModel(ILogger<EntryPageModel> logger, 
        IEntryDbService dbService)
    {
        _logger = logger;
        _dbService = dbService;
        Entry = new Entry();
    }

    public void OnGet(Guid id)
    {
        Entry = _dbService.Read(id);

        if (Entry is null)
        {
            NotFound();
        }
    }
    
    public IActionResult OnPostDeleteEntry(Guid Id)
    {
        var entry = _dbService.Read(Id);
        _dbService.Delete(Id);
        return RedirectToPage("Index", entry.Created);
    }
}