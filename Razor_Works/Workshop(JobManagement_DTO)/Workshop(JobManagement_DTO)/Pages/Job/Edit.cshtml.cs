using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Workshop_JobManagement_DTO_.Dto;
using Workshop_JobManagement_DTO_.Interface;

namespace Workshop_JobManagement_DTO_.Pages.Job
{
    public class EditModel : PageModel

 
    {

        private readonly IJobService _service;

        public EditModel(IJobService service)
        {
            _service = service;
        }

        [BindProperty]
        public JobDto JobPost { get; set; }


        public async Task<ActionResult> OnGetAsync(int id)
        {
            JobPost = await _service.GetJobByIdAsync(id);
            if (JobPost == null)
            {
                return NotFound();
            }
            return Page();
        }



        public async Task<IActionResult> OnPostAsync() 
        { 
         await _service.UpdateJobAsync(JobPost.Id,JobPost);
            return RedirectToPage("Index");
        }


    }
}
