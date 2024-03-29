using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyDiaryApp.Models;

namespace MyDiaryApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public Entry? Entry { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        Entry = new Entry(){
            Id = Guid.NewGuid(),
            Title = "My First Entry",
            Body = "This is my first entry and I'm very happy with it.",
            Created = DateTime.Now,
            Edited = DateTime.Now
        };
    }
}
