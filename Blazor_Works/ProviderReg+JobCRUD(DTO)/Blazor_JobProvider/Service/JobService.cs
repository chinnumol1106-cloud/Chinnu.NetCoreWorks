using AutoMapper;
using Blazor_JobProvider.Dto;
using Blazor_JobProvider.Interface;
using Blazor_JobProvider.Model;
using System.Collections.Generic;

namespace Blazor_JobProvider.Service
{
    public class JobService:IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public JobService(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        public async Task<List<JobDto>> GetJobByProviderIdAsync(int id)
        {
            var existjobs = await _jobRepository.GetJobByProviderIdAsync(id);

            return _mapper.Map<List<JobDto>>(existjobs);
        }

        public async Task<bool> AddJobAsync(JobDto jobDto, int providerId)
        {
            var newjob=_mapper.Map<Job>(jobDto);
            newjob.JobProviderId = providerId;
            await _jobRepository.AddAsync(newjob);
            return true;
        }

        public async Task<bool> UpdateJobAsync(JobDto jobDto)
        {
            var updatejob = await _jobRepository.GetByIdAsync(jobDto.Id);

            if(updatejob != null)
            {
                _mapper.Map(jobDto,updatejob);
                await _jobRepository.UpdateAsync(updatejob);
                return true;
            }
            return false;

        }

        public async Task<bool> DeleteJobAsync(int jobId)
        {
            await _jobRepository.DeleteAsync(jobId);
            return true;
        }

        public async Task<List<JobDto>> GetJobBySearchTextAsync(string searchtext)
        {
            var jobssearch=await _jobRepository.GetJobBySearchTextAsync(searchtext);
            return  _mapper.Map<List<JobDto>>(jobssearch);
        }
    }
}
