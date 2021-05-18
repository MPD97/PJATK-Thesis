using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thesis.Application.Common.Routes.Queries
{
    public class GetRoutesQuery : IRequest<GetRoutesVM>
    {
        public decimal TopLeftLat { get; }
        public decimal TopLeftLon { get; }

        public decimal BottomRightLat { get; }
        public decimal BottomRightLon { get; }
    }

    public class GetRoutesQueryHandler : IRequestHandler<GetRoutesQuery, GetRoutesVM>
    {
        private readonly IMapper _mapper;

        public GetRoutesQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<GetRoutesVM> Handle(GetRoutesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
