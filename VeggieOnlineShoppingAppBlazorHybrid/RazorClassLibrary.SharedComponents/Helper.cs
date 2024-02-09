using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.SharedComponents
{
    public static class Helper
    {
        public static string GetImage(string url)
        {
            var newFile = $"https://veggie-image.static.domains/Images/{url}";
            return newFile;
        }
    }
}
