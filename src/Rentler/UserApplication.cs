using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Rentler
{
    /// <summary>
    /// The application a user will fill out in order 
    /// to submit to landlords.
    /// </summary>
    public class UserApplication
    {
        public int UserId { get; set; }

        [StringLength(250)]
        public string FirstName { get; set; }

        [StringLength(250)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Ssn { get; set; }

        [StringLength(50)]
        public string PresentPhone { get; set; }

        [StringLength(250)]
        public string PresentAddressLine1 { get; set; }

        [StringLength(250)]
        public string PresentAddressLine2 { get; set; }

        [StringLength(250)]
        public string PresentCity { get; set; }

        [StringLength(50)]
        public string PresentState { get; set; }

        [StringLength(10)]
        public string PresentZip { get; set; }

        [StringLength(50)]
        public string PresentLandlord { get; set; }

        [StringLength(50)]
        public string PresentLandlordPhone { get; set; }

        [StringLength(250)]
        public string PreviousAddressLine1 { get; set; }

        [StringLength(250)]
        public string PreviousAddressLine2 { get; set; }

        [StringLength(250)]
        public string PreviousCity { get; set; }

        [StringLength(50)]
        public string PreviousState { get; set; }

        [StringLength(10)]
        public string PreviousZip { get; set; }

        [StringLength(50)]
        public string PreviousLandlord { get; set; }

        [StringLength(50)]
        public string PreviousLandlordPhone { get; set; }

        [StringLength(50)]
        public string PresentEmployer { get; set; }

        [StringLength(50)]
        public string PresentEmployerPhone { get; set; }

        [StringLength(50)]
        public string EmergencyContact { get; set; }

        [StringLength(50)]
        public string PreviousEmployer { get; set; }

        [StringLength(50)]
        public string PreviousEmployerPhone { get; set; }

        [StringLength(50)]
        public string EmergencyContactPhone { get; set; }

        [StringLength(250)]
        public string EmergencyContactAddressLine1 { get; set; }

        [StringLength(250)]
        public string EmergencyContactAddressLine2 { get; set; }

        [StringLength(250)]
        public string EmergencyContactCity { get; set; }

        [StringLength(50)]
        public string EmergencyContactState { get; set; }

        [StringLength(10)]
        public string EmergencyContactZip { get; set; }

        public int? VehicleYear { get; set; }

        [StringLength(50)]
        public string VehicleMake { get; set; }

        [StringLength(50)]
        public string VehicleModel { get; set; }

        [StringLength(50)]
        public string VehicleColor { get; set; }

        [StringLength(50)]
        public string VehicleLicenseNumber { get; set; }

        [StringLength(50)]
        public string VehicleState { get; set; }

        public bool HasBeenConvicted { get; set; }

        [StringLength(500)]
        public string ConvictedExplaination { get; set; }

        public bool HasEverBeenUnlawfulDetainer { get; set; }

        public DateTime? CreateDateUtc { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? UpdateDateUtc { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public virtual User User { get; set; }
    }
}
