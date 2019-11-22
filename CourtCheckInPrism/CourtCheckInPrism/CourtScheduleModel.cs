using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace CourtCheckInPrism
{
    public class CourtScheduleModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Occ/PON# should not be empty")]
        [StringLength(11, ErrorMessage = "Occ/PON# should not exceed 11 characters")]
        public string OccurenceNo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name should not be empty")]
        public string NameOfAccused { get; set; }
        [Required]
        public DateTime DateOfCourtAppearence { get; set; }
        [Required]
        public string CourtAppearenceTime { get; set; }
        [Required]
        public string DateOfOffence { get; set; }
        [Required]
        public string CourtHouseAddress { get; set; }
        [Required]
        public string ReasonForAppearence { get; set; }
        
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string Testify { get; set; }
        public string TimeCalledIn { get; set; }

        public string NoTestifyReason { get; set; }

    }
}
