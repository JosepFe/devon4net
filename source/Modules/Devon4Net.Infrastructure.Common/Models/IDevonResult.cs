using Devon4Net.Infrastructure.Common.Errors;

namespace Devon4Net.Infrastructure.Common.Models
{
    public interface IDevonResult
    {
        IEnumerable<DevonError> Errors { get; }
        Type ValueType { get; }
        object GetData();
    }
}
