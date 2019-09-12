namespace FightCore.Backend.ViewModels.Errors
{
    /// <summary>
    /// The view model to display errors.
    /// </summary>
    public abstract class BaseErrorViewModel
    {
        /// <summary>
        /// The code that is unique to the error.
        /// </summary>
        public abstract string ErrorCode { get; }
        
        /// <summary>
        /// The error message to displayed for the user.
        /// </summary>
        public virtual string Message { get; set; }
    }
}