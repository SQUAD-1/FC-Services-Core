namespace backend_squad1.Models
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Token { get; set; }
    }
}
