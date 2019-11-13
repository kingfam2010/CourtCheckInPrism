﻿using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace CourtCheckInPrism
{
    public class CourtScheduleModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string OccurenceNo { get; set; }
        public string NameOfAccused { get; set; }
        public DateTime DateOfCourtAppearence { get; set; }
        public string CourtAppearenceTime { get; set; }

        public string DateOfOffence { get; set; }

        public string CourtHouseAddress { get; set; }
        public string ReasonForAppearence { get; set; }
    }
}