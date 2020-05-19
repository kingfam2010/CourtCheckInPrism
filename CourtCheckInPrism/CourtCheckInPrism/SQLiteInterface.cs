using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CourtCheckInPrism
{
    public interface SQLiteInterface
    {
        SQLiteConnection GetConnectionWithDatabase();
    }
}
