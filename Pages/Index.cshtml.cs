using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyDiaryApp.Models;

namespace MyDiaryApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IEntryDbService _databaseService;
    public List<Entry> Entries { get; set; } = new List<Entry>();
    public List<Entry> FilteredEntries { get; set; } = new List<Entry>();
    [BindProperty]
    public Entry NewEntry { get; set; } = new Entry();
    [BindProperty]
    public DateTime SelectedDate { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IEntryDbService databaseService)
    {
        _logger = logger;
        _databaseService = databaseService;
    }

    public void OnGet(DateTime? date)
    {
        SelectedDate = date ?? DateTime.Now;
        Entries = _databaseService.ReadAll().ToList();
        FilteredEntries = Entries.Where(e => e.Created.Date == SelectedDate.Date).ToList();
    }

    public IActionResult OnPostAddEntry()
    {
        if (!ModelState.IsValid)
        {
            Page();
        }

        NewEntry.Created = DateTime.Now;
        NewEntry.Edited = NewEntry.Created;

        _databaseService.Create(NewEntry);

        return RedirectToPage("Index", NewEntry.Created);
    }
    public IActionResult OnPostChooseDate()
    {
        // Filter entries based on the selected date
        FilteredEntries = Entries.Where(e => e.Created.Date == SelectedDate.Date).ToList();

        return RedirectToPage("Index", SelectedDate);
    }
}
