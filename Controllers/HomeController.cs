using Microsoft.AspNetCore.Mvc;
using StepMediaAssignment.Models;
using StepMediaAssignment.Models.Service;
using System.Diagnostics;

namespace StepMediaAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStepmediaService _stepmediaService;

        public HomeController(ILogger<HomeController> logger, 
            IStepmediaService stepmediaService)
        {
            _logger = logger;
            _stepmediaService = stepmediaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Rearrange(UserInputView input)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }


            var splitInput = input.CommaSeparatedNumbers.Split(',').ToList();
            var numbers = splitInput.Select(i => Convert.ToInt64(i)).ToList();

            var rearrangedResult = _stepmediaService.Rearrange(numbers);

            var results = rearrangedResult.Select(i => i.ToString()).ToList();
            return View("Index", new UserInputView()
            {
                CommaSeparatedNumbers = string.Join(" , ", results)
            });
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
}