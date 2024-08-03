using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;

namespace StoreControl.Application.Features.PermissionFeatures.Queries.GetAllPermisions
{
    public class GetAllPermisionsQueryHandler : IRequestHandler<GetAllPermisionsQuery, IEnumerable<GetAllPermisionsResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllPermisionsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllPermisionsResponse>> Handle(GetAllPermisionsQuery request, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var permissions = await _dbContext.Permissions
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return _mapper.Map<IEnumerable<GetAllPermisionsResponse>>(permissions);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
