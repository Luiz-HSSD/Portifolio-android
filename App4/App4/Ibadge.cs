using System;
using System.Collections.Generic;
using System.Text;

namespace App4
{
    public interface Ibadge
    {
        void SetBadge(int badgeNumber, string title = null);
        void ClearBadge();
    }
}
