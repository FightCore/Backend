namespace FightCore.Backend.ViewModels.Errors
{
    public abstract class BaseErrorViewModel
    {
        public abstract string ErrorCode { get; }
        
        public string Message { get; set; }
    }
}