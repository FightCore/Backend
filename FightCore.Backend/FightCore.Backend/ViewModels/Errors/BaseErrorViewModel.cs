namespace FightCore.Backend.ViewModels.Errors
{
    public abstract class BaseErrorViewModel
    {
        public abstract string ErrorCode { get; }
        
        public virtual string Message { get; set; }
    }
}