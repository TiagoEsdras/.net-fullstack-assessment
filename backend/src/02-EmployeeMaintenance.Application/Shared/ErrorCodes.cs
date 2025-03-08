namespace EmployeeMaintenance.Application.Shared
{
    public static class ErrorCodes
    {
        #region Results Codes

        public const string UnknownErrorCode = "00.00";

        public const string AnErrorOccurOnCreatingEntityCode = "00.01";

        public const string NotFoundEntityByTermCode = "00.02";

        #endregion Results Codes

        public const string InternalServerErrorCode = "50.00";

        #region Validations Codes

        public const string FieldCannotBeNullOrEmptyCode = "00.03";
        public const string FieldMustHaveLengthBetweenCode = "00.04";
        public const string FieldContainInvalidValue = "Field(s) {0} contain(s) invalid value(s)";
        public const string FieldMustBeGreaterThan = "Field {0} must be greater than {1}";
        public const string FieldMustBeLowerOrEqualTo = "Field {0} must be lower or equal to {1}";
        public const string DuplicatedProductIds = "The following ProductId(s) is/are duplicated: {0}";
        public const string GuidCannotBeEmptyGuid = "Field {0} cannot be empty guid";
        public const string SaleHasAlreadyBeenCancelled = "Sale with Id {0} has already been cancelled";

        #endregion Validations Codes
    }
}