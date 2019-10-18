using System.ComponentModel;

namespace AspCoreMicroservice.Core
{
    public class Enums
    {
        public enum Status
        {
            Active = 1,
            Inactive = 2,
            Deleted = 3,
            Block = 4
        }

        public enum TokenAlgorithm
        {
            [Description("HS256")]
            HmacSha256,
            [Description("HS384")]
            HmacSha384,
            [Description("HS512")]
            HmacSha512,
            [Description("RS256")]
            RsaSha256,
            [Description("RS384")]
            RsaSha384,
            [Description("RS512")]
            RsaSha512,
            [Description("ES256")]
            EcdsaSha256,
            [Description("ES384")]
            EcdsaSha384,
            [Description("ES512")]
            EcdsaSha512,
            [Description("PS256")]
            RsaSsaPssSha256,
            [Description("PS384")]
            RsaSsaPssSha384
        }

        public enum Action
        {
            Insert = 1,
            Update,
            Delete,
            View,
            Other
        }

        public enum Module
        {
            Web = 1,
            Desktop,
            Mobile
        }

        public enum LogType
        {
            SecurityLog = 1,
            ErrorLog,
            SystemLog,
            DbQuery,
            Other
        }

        public enum UserRoles
        {
            Guide = 1,
            User = 2
        }

        public enum UpdateUserUrlLastSegment
        {
            location = 1,
            comment = 2
        }

        public enum PaymentGateway
        {
            Stripe = 1,
            Paypal,
            Both,
            None
        }

        public enum Occurrence
        {
            once = 1,
            monthly,
            weekly,
            daily,
            weekdays,
            free
        }

        public enum OccurrenceDays
        {


        }
    }
}
