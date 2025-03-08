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
    }
}