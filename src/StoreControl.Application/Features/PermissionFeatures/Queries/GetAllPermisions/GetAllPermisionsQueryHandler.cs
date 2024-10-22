using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;

namespace StoreControl.Application.Features.PermissionFeatures.Queries.GetAllPermisions
{
    public class GetAllPermisionsQueryHandler : IRequestHandler<GetAllPermisionsQuery, IEnumerable<PermissionDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllPermisionsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionDto>> Handle(GetAllPermisionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _dbContext.Permissions
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
        }
    }
}
