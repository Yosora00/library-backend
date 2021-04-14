using System.Threading.Tasks;
using library_backend.Entities;
using library_backend.Results;

namespace library_backend.Services
{
    public interface ILabelService
    {
        public ResultBase AddLabel(label l);
        public ResultBase DeleteLabel(label l);
        public ResultBase UpdateLabel(label l);
        public string GetLabelId(string name);
    }
}