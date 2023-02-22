namespace TodoApp.Entities.Exceptions
{
    public class DateRangeBadRequestException : BadRequestException
    {
        public DateRangeBadRequestException() : base("Date range is not valid. FromDate must be less than or equal to ToDate")
        {
        }
    }
}