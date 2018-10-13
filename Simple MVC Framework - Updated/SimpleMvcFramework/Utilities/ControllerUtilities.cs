using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvcFramework.Utilities
{
    public static class ControllerUtilities
    {
        public static string GetControllerName(object controller) => controller.GetType()
            .Name
            .Replace(MvcContext.Get.ControllersSuffix, string.Empty);


        public static string GetViewFullQualifiedName(
        string controller, string action)
        => string.Format("{0}\\{1}\\{2}.html",
        MvcContext.Get.ViewsFolder,
        controller,
        action);

        public static string CapitalizeFirstLetter(string word)
        {
            return Char.ToUpper(word[0]) + word.Substring(1);
        }

    }
}
