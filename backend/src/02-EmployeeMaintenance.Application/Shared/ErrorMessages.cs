namespace EmployeeMaintenance.Application.Shared
{
    public static class ErrorMessages
    {
        public const string UnknownError = "Something unexpected went wrong";
        public const string InternalServerError = "Internal server error";
        public const string ContactSupport = "Please contact support for more details.";

        #region Results Messages

        public const string AnErrorOccurOnProcessRequest = "An error occur when try to {0} {1}";
        public const string NotFoundEntity = "{0} not found";
        public const string NotFoundEntityByTerm = "{0} with {1} {2} was not found";
        public const string OperationCannotBeProcessed = "The operation {0} cannot be processed";
        public const string FailOnCreatingEntity = "Fail on creating entity";

        #endregion Results Messages

        #region Validations Messages

        public const string FieldContainInvalidValue = "Field(s) {0} contain(s) invalid value(s)";
        public const string FieldCannotBeNullOrEmpty = "Field {0} cannot be null, empty or default value";
        public const string FieldMustHaveLengthBetween = "Field {0} must have length between {1} and {2}";
        public const string FieldMustBeGreaterThan = "Field {0} must be greater than {1}";
        public const string FieldMustBeLowerOrEqualTo = "Field {0} must be lower or equal to {1}";
        public const string GuidCannotBeEmptyGuid = "Field {0} cannot be empty guid";
        public const string PhotoBase64MustBeAnPngOrJpegFormat = "Photo must be an png or jpeg format";
        public const string PhotoBase64InvalidFormat = "Photo has invalid base 64 format";

        #endregion Validations Messages
    }
}