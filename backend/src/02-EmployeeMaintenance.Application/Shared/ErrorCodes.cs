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
        public const string PhotoBase64MustBeAnPngOrJpegFormatCode = "00.05";
        public const string PhotoBase64InvalidFormatCode = "00.06";

        #endregion Validations Codes
    }
}