namespace EmployeeMaintenance.Application.Shared
{
    public static class ErrorMessages
    {
        public const string UnknownError = "Something unexpected went wrong";
        public const string InternalServerError = "Internal server error";
        public const string ContactSupport = "Please contact support for more details.";

        #region Results Messages

        public const string AnErrorOccurOnCreatingEntity = "An error occur when try to create {0}";
        public const string NotFoundEntity = "{0} not found";
        public const string NotFoundEntityByTerm = "{0} with {1} {2} was not found";
        public const string OperationCannotBeProcessed = "The operation {0} cannot be processed";
        public const string FailOnCreatingEntity = "Fail on creating entity";

        #endregion Results Messages
    }
}