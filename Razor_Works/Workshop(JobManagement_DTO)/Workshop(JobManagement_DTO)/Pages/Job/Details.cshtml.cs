using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Workshop_JobManagement_DTO_.Dto;
using Workshop_JobManagement_DTO_.Interface;

namespace Workshop_JobManagement_DTO_.Pages.Job
{
    public class DetailsModel : PageModel
    {

        private readonly IJobService _service;

        public DetailsModel(IJobService service)
        {
            _service = service;
        }

        public JobDto jobdetails { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            jobdetails=await _service.GetJobByIdAsync(id);

            if(jobdetails==null)
            {
                return NotFound();

            }
            return Page();

        }
    }
}
