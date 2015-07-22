using System;
namespace StarterKit.Utils
{
    public static class ErrorUtil
    {
        public static string DefaultError = "Something went wrong with your last action. Please try again or contact an administrator.";

        public static string GetInnerMessage(Exception e)
        {
            while (e.InnerException != null)
            {
                e = e.InnerException;
            }

            return e.Message;
        }
    }
}
