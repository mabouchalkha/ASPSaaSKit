namespace StarterKit.Utils
{
    public static class MessageUtil
    {
        public static string GenerateUpdateSuccessfull(string model)
        {
            return string.Format("{0} {1}", model, App_GlobalResources.lang.modelUpdateSuccess);
        }
    }
}
