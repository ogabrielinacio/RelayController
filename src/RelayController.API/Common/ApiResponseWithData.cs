namespace RelayController.API.Common;

public class ApiResponseWithData<T> : ApiResponse
{
    public T? Data { get; set; }
}
