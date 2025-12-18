using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Workshop_JobManagement_DTO_.Dto;
using Workshop_JobManagement_DTO_.Interface;

namespace Workshop_JobManagement_DTO_.Pages.Job
{
    public class DeleteModel : PageModel

    {

        private readonly IJobService _service;

        public DeleteModel(IJobService service)
        {
            _service = service;
        }

        [BindProperty]
        public JobDto JobPost { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            JobPost=await _service.GetJobByIdAsync(id);

            if(JobPost==null)
            {
                return NotFound();
            }
            return Page();
        }



        public async Task<IActionResult> OnPost()
        {
            await _service.DeleteJobAsync(JobPost.Id);
            return RedirectToPage("Index");
        }
    }
}
