using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesis.Application.Common.Interfaces
{
    public interface IDataSeederService
    {
        Task CreateTestUser();

        Task CreateTestRoute();

    }
}
