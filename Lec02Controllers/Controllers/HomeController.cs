using Lec02Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Xml.Linq;

namespace Lec02Controllers.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Parameter(string? id)
    {
        ViewData["id"] = id;
        return View();
    }

    public IActionResult FourSegments(string code, string idNumber)
    {
        ViewData["code"] = code;
        ViewData["idNumber"] = idNumber;
        return View();
    }

    public IActionResult Bind([Bind(Prefix = "id")] string? name)
    {
        ViewData["name"] = name;
        return View();
    }

    public IActionResult Details(string? enumber)
    {
        ViewData["enumber"] = enumber;
        return View();
    }

    public IActionResult Variable(string? name, string? values)
    {
        ViewData["result"] = $"name = {name} values = {values}";
        string[] itemArray = (values != null)?values.Split("/"):[];
        ViewData["itemStr"] = String.Join(" ", itemArray);
        return View();
    }

    public IActionResult InputAgeForm()
    {
        return View();
    }

    [HttpPost]
    public IActionResult FormProcessor(int age)
    {
        return RedirectToAction("Parameter", new { id = $"Your age is {age}" });
    }

    public IActionResult InputNameForm()
    {
        return View();
    }

    [HttpPost]
    public IActionResult NameProcessor(string firstName)
    {
        ViewData["firstName"] = firstName;
        return View("ShowName");
    }

    public IActionResult ContentDemo()
    {
        return Content("This is a raw string");
    }

    public IActionResult FileDemo()
    {
        return File("myfile.pdf", "application/pdf");
    }

    public IActionResult JsonDemo()
    {
        return Json(new { id = 1, name = "Jeff", type = "person" });
    }

    public IActionResult FileSaveDemo()
    {
        // Create a memory stream to stream the data
        string stringToSave = "This is the data to save!\n";
        byte[] bytesToSave = Encoding.UTF8.GetBytes(stringToSave);
        MemoryStream ms = new();
        ms.Write(bytesToSave, 0, bytesToSave.Length);   
        ms.Position = 0;
        // Download the memory stream
        string path = "somefile.txt";
        return File(ms, "application/octet-stream", Path.GetFileName(path));
    }

    public IActionResult FileUploadDemo()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> DoUpload(IList<IFormFile> files)
    {
        IFormFile? fileToImport = files.FirstOrDefault();
        if (fileToImport == null)
        {
            return RedirectToAction("ShowFileContent", 
                new { id = "No file to upload!" });
        }

        MemoryStream ms = new();
        await fileToImport.OpenReadStream().CopyToAsync(ms);
        var bytes = ms.ToArray();
        var fileContent = Encoding.UTF8.GetString(bytes);
        return RedirectToAction("ShowFileContent", 
            new { id = fileContent });
    }

    public IActionResult ShowFileContent([Bind(Prefix ="id")]string fileContent)
    {
        ViewData["fileContent"] = fileContent;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
