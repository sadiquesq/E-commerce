namespace E_Commerce.DTOs
{
    public class ApiRespones<T>
    {
        public bool Success { get; set; }

        public int status_code { get; set; }

        public string Message { get; set; }

        public List<T> Data { get; set; }
    }
}
