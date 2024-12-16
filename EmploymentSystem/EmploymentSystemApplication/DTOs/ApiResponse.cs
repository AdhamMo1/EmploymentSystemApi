namespace EmploymentSystemApplication.DTOs
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
