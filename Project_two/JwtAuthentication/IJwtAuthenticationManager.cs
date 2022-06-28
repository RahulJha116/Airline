using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_two.IJwtAuthentication
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string AdminEmail, string AdminPasskey);
    }
}
