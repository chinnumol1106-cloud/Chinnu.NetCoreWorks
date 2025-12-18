namespace Workshop_JobManagement_DTO_.Model
{
    public class Job
    {

        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string Company {  get; set; }
        public string Location {  get; set; }
        public string EmploymentType {  get; set; }
        public string SalaryRange {  get; set; }
        public string JobDescription {  get; set; }
        public string Requirements {  get; set; }
        public DateTime PostedDate {  get; set; }
        public DateTime ExpirationDate { get; set; }


    }
}
