using System.Text.Json.Serialization;

namespace SharedLibrary.DTOs;
public class ResponseDTO<T> where T : class
{
    public T Data { get;private set; }
    public int StatusCode { get; private set; }
    [JsonIgnore]
    public bool IsSuccesful { get; private  set; }
    public ErrorDTO Error { get; private set; }
    public static ResponseDTO<T> Succes(T data,int statusCode) 
    {
        return new ResponseDTO<T> { Data = data, StatusCode = statusCode,IsSuccesful=true };
    }
    public static ResponseDTO<T> Succes(int statusCode)
    {
        return new ResponseDTO<T> { Data = default, StatusCode = statusCode, IsSuccesful = true };
    }
    public static ResponseDTO<T> Fail(ErrorDTO errorDTO,int statusCode) 
    {
        return new ResponseDTO<T>(){Error = errorDTO,StatusCode = statusCode,IsSuccesful= false};
    }
    public static ResponseDTO<T> Fail(string errorMessage,int statusCode,bool isShow ) 
    {
        var  errorDto=new ErrorDTO(errorMessage,isShow);
        return new ResponseDTO<T>(){ Error=errorDto,StatusCode=statusCode,IsSuccesful=false};
    }
}