using System;
using System.Threading.Tasks;

namespace Thesis.Application.Common.Interfaces
{
    public interface IUserAgentService
    {
        Task Save(int userId, DateTime dateTime, string userAgent);
    }
}
