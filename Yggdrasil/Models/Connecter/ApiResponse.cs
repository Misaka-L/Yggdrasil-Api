namespace Yggdrasil.Models.Connecter {
    public class ApiResponse {
        public bool success { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public string data { get; set; }
        public object extras { get; set; }
        public long timestamp { get; set; }
    }

    public class ApiResponse<T> : ApiResponse {
        public T data { get; set; }
    }
}
