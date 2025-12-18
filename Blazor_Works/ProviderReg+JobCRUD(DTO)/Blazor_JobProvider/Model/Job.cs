namespace Blazor_JobProvider.Model
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public decimal salary {  get; set; }
        public int JobProviderId {  get; set; }
        public JobProvider JobProvider { get; set; }
    }
}
