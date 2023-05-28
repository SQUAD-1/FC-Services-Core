namespace backend_squad1.Models;

public class ServiceResult
{
    public bool Success { get; private set; }
    public string Message { get; private set; }

    private ServiceResult(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public static ServiceResult CreateSuccess()
    {
        return new ServiceResult(true, string.Empty);
    }

    public static ServiceResult Failure(string message)
    {
        return new ServiceResult(false, message);
    }
}
