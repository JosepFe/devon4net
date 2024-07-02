using System;
using Devon4Net.Infrastructure.Common.Errors;

namespace Devon4Net.Infrastructure.Common.Helpers
{
    public static class DevonErrorHelper
    {
        public static int ToHigherHttpStatusCode(this IEnumerable<DevonError> errors)
            => (int)errors.Max(x => x.HttpStatus);
    }
}
