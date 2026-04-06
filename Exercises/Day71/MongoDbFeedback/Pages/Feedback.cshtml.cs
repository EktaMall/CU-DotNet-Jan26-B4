using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDbFeedback.Models;
using MongoDbFeedBack.Services;

//using SmartBank.MVC.Models;
//using SmartBank.MVC.Services;

namespace MongoDbFeedback.Pages
{
    public class FeedbackModel : PageModel
    {
        private readonly FeedBackService _service;

        public FeedbackModel(FeedBackService service)
        {
            _service = service;
        }

        [BindProperty]
        public Feedback Feedback { get; set; } = new Feedback();

        public string SuccessMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {

            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"{state.Key}: {error.ErrorMessage}");
                }
            }

            if (!ModelState.IsValid)
                return Page();

            await _service.CreateAsync(Feedback);

            SuccessMessage = "Feedback submitted successfully!";
            ModelState.Clear();

            return Page();
        }
    }
}
