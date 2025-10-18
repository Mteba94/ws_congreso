using congreso.Application.Interfaces.Persistence;
using congreso.Domain.Entities;
using congreso.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Http;

namespace congreso.Infrastructure.Persistence.Repositories;

public class DiplomaRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : GenericRepository<Diploma>(context, httpContextAccessor), IDiplomaRepository
{
    // No specific methods needed for Diploma yet, as it inherits from GenericRepository
}