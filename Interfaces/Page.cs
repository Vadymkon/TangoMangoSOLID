using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TangoMangoSOLID.Interfaces
{
    interface Page
    {
        void Refresh();
        void Auth();
        void Close();
        Task<bool> Check();
    }
}
