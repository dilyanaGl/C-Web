using System;
using SimpleMvcFramework.Controllers;

namespace SimpleMvcFramework.Helpers
{
    public static class ControllerHelpers
    {
        public static string GetFullQualifiedName(string controller, string action) => String.Format(
                "{0}.{1}.{2}.{3}, {0}",
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ViewsFolder,
                controller,
                action);

        public static string GetControllerName(Controller controller) => controller
            .GetType()
            .Name
            .Replace(MvcContext.Get.ControllersSuffix, String.Empty);
    }
}
