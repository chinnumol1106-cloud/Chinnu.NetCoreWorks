using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Workshop_JobManagement_DTO_.Dto;
using Workshop_JobManagement_DTO_.Interface;

namespace Workshop_JobManagement_DTO_.Pages.Job
{
    public class IndexModel : PageModel
    {

        private readonly IJobService _service;

        public IndexModel(IJobService service)
        {
            _service = service;
        }

        public List<JobDto> JobPosts { get; set; }
        public async Task OnGetAsync()
        {
            JobPosts = await _service.GetAllJobsAsync();
        }
    }   
}
