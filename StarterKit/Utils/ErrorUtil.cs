using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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

        public static string GenerateModelStateError(ModelStateDictionary modelState)
        {
            string errors = string.Empty;

            if (modelState != null)
            {
                List<string> errorList = modelState.Keys.SelectMany(key => modelState[key].Errors.Select(x => x.ErrorMessage)).ToList();
                return ErrorUtil.JoinErrors(errorList);
            }

            return errors;
        }

        public static string JoinErrors(IEnumerable<string> errors)
        {
            List<string> newErrors = new List<string>();

            foreach (string error in errors)
            {
                newErrors.Add(error.Replace(".", "<br />"));
            }

            return string.Join("<br />", newErrors);
        }
    }
}
