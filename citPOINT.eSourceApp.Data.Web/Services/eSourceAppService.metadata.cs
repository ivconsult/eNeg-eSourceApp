
namespace citPOINT.eSourceApp.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies NegotiationBidMetadata as the class
    // that carries additional metadata for the NegotiationBid class.
    [MetadataTypeAttribute(typeof(NegotiationBid.NegotiationBidMetadata))]
    public partial class NegotiationBid
    {

        // This class allows you to attach custom attributes to properties
        // of the NegotiationBid class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class NegotiationBidMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private NegotiationBidMetadata()
            {
            }

            public Guid BidID { get; set; }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public Guid eNegUserID { get; set; }

            public Nullable<bool> IsClosed { get; set; }

            public Guid NegotiationBidID { get; set; }

            public Guid NegotiationID { get; set; }

            public UserMapping UserMapping { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies UserMappingMetadata as the class
    // that carries additional metadata for the UserMapping class.
    [MetadataTypeAttribute(typeof(UserMapping.UserMappingMetadata))]
    public partial class UserMapping
    {

        // This class allows you to attach custom attributes to properties
        // of the UserMapping class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class UserMappingMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private UserMappingMetadata()
            {
            }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public Guid eNegUserID { get; set; }

            public Guid eSourceUserID { get; set; }

            public EntityCollection<NegotiationBid> NegotiationBids { get; set; }
        }
    }
}
