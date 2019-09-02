namespace FightCore.Backend.ViewModels.Errors
{
    public class NotFoundErrorViewModel : BaseErrorViewModel
    {
        private const string NOT_FOUND_ERROR_CODE = "EntityNotFound";
        private NotFoundErrorViewModel()
        {
        }

        public override string ErrorCode => NOT_FOUND_ERROR_CODE;

        public static NotFoundErrorViewModel Create(string entity, long id)
        {
            return new NotFoundErrorViewModel()
            {
                Message = $"{entity} with id \"{id}\" has not been found."
            };
        }
    }
}