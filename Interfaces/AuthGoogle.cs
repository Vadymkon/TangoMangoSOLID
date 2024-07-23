using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TangoMangoSOLID.Interfaces;

namespace TangoMangoSOLID.Classes.Auth
{
    interface AuthGoogle
    {
        void GAuth();
        void justPressContinue();
        void passLoginForm();
    }
}
