using System.Threading.Tasks;
using library_backend.Entities;
using library_backend.Results;

namespace library_backend.Services
{
    public interface ILabelService
    {
        public Task<ResultBase> AddLabelAsync(label l);
        public Task<ResultBase> DeleteLabelAsync(label l);
        public Task<ResultBase> UpdateLabelAsync(label l);
        public Task<string> GetLabelIdAsync(string name);
    }
}
