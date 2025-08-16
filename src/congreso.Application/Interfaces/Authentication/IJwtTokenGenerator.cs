using congreso.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Application.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
