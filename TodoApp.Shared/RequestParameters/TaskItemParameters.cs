namespace TodoApp.Shared.RequestParameters
{
    public class TaskItemParameters : RequestParameters
    {
        public TaskItemParameters() => OrderBy = "Name";

        public DateTime FromDate { get; set; } = DateTime.MinValue;

        public DateTime ToDate { get; set; } = DateTime.MaxValue;

        public bool ValidDateRange => FromDate <= ToDate;

        public string? SearchTerm { get; set; }
    }
}