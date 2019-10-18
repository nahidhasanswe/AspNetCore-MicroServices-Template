namespace AspCoreMicroservice.Core.Constants
{
    public class AppConstant
    {
        public const string DISPLAY_DATE_FORMAT = "yyyy-MM-dd";
    }

    public class HttpHeaders
    {
        public const string Token = "Authorization";
        public const string AuthenticationSchema = "Bearer ";

    }

    public class DiscountType
    {
        public const string Percentage = "percentage";
        public const string FixedAmount = "fixedAmount";
    }

    public class UseCount
    {
        public const string Increase = "increase";
        public const string Decrease = "decrease";
    }
}
