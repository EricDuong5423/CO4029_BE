using Microsoft.AspNetCore.Mvc;

namespace CO4029_BE.Facades;

public class CampusManagementFacade : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}