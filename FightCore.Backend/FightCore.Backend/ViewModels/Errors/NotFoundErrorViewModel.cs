namespace FightCore.Backend.ViewModels.Errors
{
    /// <summary>
    /// ViewModel to display that the not found error occured.
    /// </summary>
    public class NotFoundErrorViewModel : BaseErrorViewModel
    {
        private const string NotFoundErrorCode = "EntityNotFound";

        private NotFoundErrorViewModel()
        {
        }

        /// <inheritdoc />
        public override string ErrorCode => NotFoundErrorCode;

        /// <summary>
        /// Creates an instance of the <see cref="NotFoundErrorViewModel"/> object.
        /// </summary>
        /// <param name="entity">The name of the entity which was not found.</param>
        /// <param name="id">The id for which it wasn't found.</param>
        /// <returns>The created view model.</returns>
        public static NotFoundErrorViewModel Create(string entity, long id)
        {
            return new NotFoundErrorViewModel()
            {
                Message = $"{entity} with id \"{id}\" has not been found."
            };
        }
    }
}