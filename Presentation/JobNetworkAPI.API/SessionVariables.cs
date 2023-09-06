namespace JobNetworkAPI.API
{
    public class SessionVariables
    {
        public const string SessionKeyEmail = "SessionKeyEmail";
            public const string SessionKeyPassword = "SessionKeyPassword";
    }

    public enum SessionKeyEnum
    {
        SessionKeyEmail=0,
        SessionKeyPassword=1
    }
}
