using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Workshop_JobManagement_DTO_.Dto;
using Workshop_JobManagement_DTO_.Interface;

namespace Workshop_JobManagement_DTO_.Pages.Job
{
    public class CreateModel : PageModel

        
    {

        private readonly IJobService _service;

        public CreateModel(IJobService service)
        {
            _service = service;
        }

        [BindProperty]
        public JobDto JobPost { get; set; }
          
      public async Task<IActionResult> OnpostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            await _service.AddJobAsync(JobPost);
            return RedirectToPage("Index");
        }
    }
}
