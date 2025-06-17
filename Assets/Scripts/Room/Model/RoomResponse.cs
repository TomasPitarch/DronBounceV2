public class RoomResponse
{
    public RoomStatus Status{get;}
    public string Message{get;}
    public int Code{get;}
    
    public RoomResponse(RoomStatus status, string message, int code)
    {
        Status = status;
        Message = message;
        Code = code;
    }
}