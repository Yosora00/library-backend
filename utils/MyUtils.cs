using System;

namespace library_backend.utils
{
    public static class MyUtils
    {
        public static string GenerateId()
        {
            var id = Guid.NewGuid();
            return id.ToString().Substring(0, 8);
        }
    }
}