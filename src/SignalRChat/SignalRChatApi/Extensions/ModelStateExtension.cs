using System.Web.Http.ModelBinding;

namespace SignalRChatApi.Extensions
{
    public static class ModelStateExtension
    {
        /// <summary>
        /// Get Error Message
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static string ToErrorMessage(this ModelStateDictionary modelState)
        {
            if (modelState == null || modelState.IsValid) return string.Empty;
            string result = string.Empty;
            foreach (var item in modelState.Values)
            {
                foreach (var modelError in item.Errors)
                {
                    result += modelError.ErrorMessage + System.Environment.NewLine;
                }
            }
            return result.TrimEnd(System.Environment.NewLine.ToCharArray());
        }
    }
}