using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class NotifyAuthState
    {
        public event Action<string?> UserLoggedIn;

        public async Task UserLoggedInAsync(string? userName)
        {
            // Perform any necessary logic...
            UserLoggedIn?.Invoke(userName);
        }
    }
}
