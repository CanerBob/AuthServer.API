namespace SharedLibrary.DTOs;
public class ErrorDTO
{
    public List<string>? Errors { get;private set; }
    public bool IsShow { get; private set; }
    public ErrorDTO() 
    {
    Errors = new List<string>();    
    }
    public ErrorDTO(string Error,bool isShow) 
    {
        Errors= new List<string>();
    Errors.Add(Error);
        isShow = true;
    }
    public ErrorDTO(List<string> errors,bool isShow) 
    {
    Errors=Errors;
        IsShow = isShow;
    }
}
