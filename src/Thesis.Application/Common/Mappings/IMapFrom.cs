using AutoMapper;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Thesis.Application.Common.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
