using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Domain.Entities;
using UAParser;
using UserAgent = Thesis.Domain.Entities.UserAgent;

namespace Thesis.Infrastructure.Services
{
    public class UserAgentService : IUserAgentService
    {
        private readonly IRepository<UserAgent> _repository;
        private static readonly Parser UAParser = Parser.GetDefault();

        public UserAgentService(IRepository<UserAgent> repository)
        {
            _repository = repository;
        }

        public async Task Save(int userId, string userAgent)
        {
            ClientInfo clientInfo = UAParser.Parse(userAgent);
            var clientUserAgent = clientInfo.UA;
            var clientOS = clientInfo.OS;
            var clientDevice = clientInfo.Device;

            var ua = new UserAgent()
            {
                UserId = userId,

                BrowserFamily = clientUserAgent.Family,
                BrowserMajorVersion = clientUserAgent.Major,
                BrowserMinorVersion = clientUserAgent.Minor,

                OSFamily = clientOS.Family,
                OSMajorVersion = clientOS.Major,
                OSMinorVersion = clientOS.Minor,

                DeviceFamily = clientDevice.Family,
                DeviceBrand = clientDevice.Brand,
                DeviceModel = clientDevice.Model,
                DeviceIsSpider = clientDevice.IsSpider
            };

            await _repository.AddAsync(ua);
            await _repository.SaveChangesAsync();
        }
    }
}
