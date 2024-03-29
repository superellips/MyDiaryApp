using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyDiaryApp.Models;

namespace MyDiaryApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IEntryDbService _databaseService;
    public List<Entry> Entries { get; set; } = new List<Entry>();
    [BindProperty]
    public Entry NewEntry { get; set; } = new Entry();

    public IndexModel(ILogger<IndexModel> logger, IEntryDbService databaseService)
    {
        _logger = logger;
        _databaseService = databaseService;
    }

    public void OnGet()
    {
        Entries = _databaseService.ReadAll().ToList();
    }

    public void OnPostAddEntry()
    {
        if (!ModelState.IsValid)
        {
            Page();
        }

        NewEntry.Created = DateTime.Now;

        _databaseService.Create(NewEntry);

        RedirectToPage();
    }
}
