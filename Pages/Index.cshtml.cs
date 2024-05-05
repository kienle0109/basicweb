using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razorweb.models;

namespace Razorweb.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private readonly MyBlogContext myBlogContext;
    public IndexModel(ILogger<IndexModel> logger, MyBlogContext _myblogcontext)
    {
        _logger = logger;
        myBlogContext = _myblogcontext;
    }

    public void OnGet()
    {
        var posts = (from a in myBlogContext.articles 
                    orderby a.Created descending
                    select a).ToList();
        ViewData["posts"] = posts;
    }
}
