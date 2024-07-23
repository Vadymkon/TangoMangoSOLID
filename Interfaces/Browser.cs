using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TangoMangoSOLID.Interfaces
{
    interface Browser
    {
        void Close();
        void Init(bool headless);
        void OpenPage(string URL);
        void ClosePage(string URL);
        void UpdatePageList();
    }
}
