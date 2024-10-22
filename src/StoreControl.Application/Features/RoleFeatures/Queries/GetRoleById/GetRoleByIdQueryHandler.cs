using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;
using StoreControl.Domain.Exceptions;

namespace StoreControl.Application.Features.RoleFeatures.Queries.GetRoleById
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDetailedDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<RoleDetailedDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _dbContext.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (role == null)
            {
                throw new NotFoundException("Role not found.");
            }

            return _mapper.Map<RoleDetailedDto>(role);
        }
    }
}
