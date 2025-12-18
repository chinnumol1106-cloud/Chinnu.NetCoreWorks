using AutoMapper;
using Workshop_JobManagement_DTO_.Dto;
using Workshop_JobManagement_DTO_.Interface;
using Workshop_JobManagement_DTO_.Model;

namespace Workshop_JobManagement_DTO_.Service
{
    public class JobService: IJobService
    {
        private readonly IJobRepository _jobRepository;

        private readonly IMapper _mapper;

        public JobService(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }


        public async Task<List<JobDto>> GetAllJobsAsync()
        {
            var job = await _jobRepository.GetAlljobsAsync();

            return _mapper.Map<List<JobDto>>(job);

        }

        public async Task AddJobAsync(JobDto jobDto)
        {
            var job=_mapper.Map<Job>(jobDto);
            await _jobRepository.AddJobAsync(job);
        }
        


        public async Task<JobDto> GetJobByIdAsync(int id)
        {
            var job=await _jobRepository.GetJobByIdAsync(id);
            return _mapper.Map<JobDto>(job);
        }


        public async Task DeleteJobAsync(int id)
        {
            await _jobRepository.DeleteJobAsync(id);
        }

        public async Task UpdateJobAsync(int id,JobDto jobDto)
        {
            var job = _mapper.Map<Job>(jobDto);
            await _jobRepository.UpdateJobAsync(id,job);
        }
    }
}
