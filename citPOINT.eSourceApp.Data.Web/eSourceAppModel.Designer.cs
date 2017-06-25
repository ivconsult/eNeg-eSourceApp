﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("eSourceAppModel", "FK__Negotiati__eNegU__0519C6AF", "UserMapping", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(citPOINT.eSourceApp.Data.Web.UserMapping), "NegotiationBid", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(citPOINT.eSourceApp.Data.Web.NegotiationBid), true)]

#endregion

namespace citPOINT.eSourceApp.Data.Web
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class eSourceAppEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new eSourceAppEntities object using the connection string found in the 'eSourceAppEntities' section of the application configuration file.
        /// </summary>
        public eSourceAppEntities() : base("name=eSourceAppEntities", "eSourceAppEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new eSourceAppEntities object.
        /// </summary>
        public eSourceAppEntities(string connectionString) : base(connectionString, "eSourceAppEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new eSourceAppEntities object.
        /// </summary>
        public eSourceAppEntities(EntityConnection connection) : base(connection, "eSourceAppEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<NegotiationBid> NegotiationBids
        {
            get
            {
                if ((_NegotiationBids == null))
                {
                    _NegotiationBids = base.CreateObjectSet<NegotiationBid>("NegotiationBids");
                }
                return _NegotiationBids;
            }
        }
        private ObjectSet<NegotiationBid> _NegotiationBids;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<UserMapping> UserMappings
        {
            get
            {
                if ((_UserMappings == null))
                {
                    _UserMappings = base.CreateObjectSet<UserMapping>("UserMappings");
                }
                return _UserMappings;
            }
        }
        private ObjectSet<UserMapping> _UserMappings;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the NegotiationBids EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToNegotiationBids(NegotiationBid negotiationBid)
        {
            base.AddObject("NegotiationBids", negotiationBid);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the UserMappings EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToUserMappings(UserMapping userMapping)
        {
            base.AddObject("UserMappings", userMapping);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="eSourceAppModel", Name="NegotiationBid")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class NegotiationBid : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new NegotiationBid object.
        /// </summary>
        /// <param name="negotiationBidID">Initial value of the NegotiationBidID property.</param>
        /// <param name="negotiationID">Initial value of the NegotiationID property.</param>
        /// <param name="bidID">Initial value of the BidID property.</param>
        /// <param name="eNegUserID">Initial value of the eNegUserID property.</param>
        public static NegotiationBid CreateNegotiationBid(global::System.Guid negotiationBidID, global::System.Guid negotiationID, global::System.Guid bidID, global::System.Guid eNegUserID)
        {
            NegotiationBid negotiationBid = new NegotiationBid();
            negotiationBid.NegotiationBidID = negotiationBidID;
            negotiationBid.NegotiationID = negotiationID;
            negotiationBid.BidID = bidID;
            negotiationBid.eNegUserID = eNegUserID;
            return negotiationBid;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid NegotiationBidID
        {
            get
            {
                return _NegotiationBidID;
            }
            set
            {
                if (_NegotiationBidID != value)
                {
                    OnNegotiationBidIDChanging(value);
                    ReportPropertyChanging("NegotiationBidID");
                    _NegotiationBidID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("NegotiationBidID");
                    OnNegotiationBidIDChanged();
                }
            }
        }
        private global::System.Guid _NegotiationBidID;
        partial void OnNegotiationBidIDChanging(global::System.Guid value);
        partial void OnNegotiationBidIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid NegotiationID
        {
            get
            {
                return _NegotiationID;
            }
            set
            {
                OnNegotiationIDChanging(value);
                ReportPropertyChanging("NegotiationID");
                _NegotiationID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("NegotiationID");
                OnNegotiationIDChanged();
            }
        }
        private global::System.Guid _NegotiationID;
        partial void OnNegotiationIDChanging(global::System.Guid value);
        partial void OnNegotiationIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid BidID
        {
            get
            {
                return _BidID;
            }
            set
            {
                OnBidIDChanging(value);
                ReportPropertyChanging("BidID");
                _BidID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("BidID");
                OnBidIDChanged();
            }
        }
        private global::System.Guid _BidID;
        partial void OnBidIDChanging(global::System.Guid value);
        partial void OnBidIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Boolean> IsClosed
        {
            get
            {
                return _IsClosed;
            }
            set
            {
                OnIsClosedChanging(value);
                ReportPropertyChanging("IsClosed");
                _IsClosed = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("IsClosed");
                OnIsClosedChanged();
            }
        }
        private Nullable<global::System.Boolean> _IsClosed;
        partial void OnIsClosedChanging(Nullable<global::System.Boolean> value);
        partial void OnIsClosedChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid eNegUserID
        {
            get
            {
                return _eNegUserID;
            }
            set
            {
                OneNegUserIDChanging(value);
                ReportPropertyChanging("eNegUserID");
                _eNegUserID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("eNegUserID");
                OneNegUserIDChanged();
            }
        }
        private global::System.Guid _eNegUserID;
        partial void OneNegUserIDChanging(global::System.Guid value);
        partial void OneNegUserIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Boolean> Deleted
        {
            get
            {
                return _Deleted;
            }
            set
            {
                OnDeletedChanging(value);
                ReportPropertyChanging("Deleted");
                _Deleted = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Deleted");
                OnDeletedChanged();
            }
        }
        private Nullable<global::System.Boolean> _Deleted;
        partial void OnDeletedChanging(Nullable<global::System.Boolean> value);
        partial void OnDeletedChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Guid> DeletedBy
        {
            get
            {
                return _DeletedBy;
            }
            set
            {
                OnDeletedByChanging(value);
                ReportPropertyChanging("DeletedBy");
                _DeletedBy = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("DeletedBy");
                OnDeletedByChanged();
            }
        }
        private Nullable<global::System.Guid> _DeletedBy;
        partial void OnDeletedByChanging(Nullable<global::System.Guid> value);
        partial void OnDeletedByChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> DeletedOn
        {
            get
            {
                return _DeletedOn;
            }
            set
            {
                OnDeletedOnChanging(value);
                ReportPropertyChanging("DeletedOn");
                _DeletedOn = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("DeletedOn");
                OnDeletedOnChanged();
            }
        }
        private Nullable<global::System.DateTime> _DeletedOn;
        partial void OnDeletedOnChanging(Nullable<global::System.DateTime> value);
        partial void OnDeletedOnChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("eSourceAppModel", "FK__Negotiati__eNegU__0519C6AF", "UserMapping")]
        public UserMapping UserMapping
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<UserMapping>("eSourceAppModel.FK__Negotiati__eNegU__0519C6AF", "UserMapping").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<UserMapping>("eSourceAppModel.FK__Negotiati__eNegU__0519C6AF", "UserMapping").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<UserMapping> UserMappingReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<UserMapping>("eSourceAppModel.FK__Negotiati__eNegU__0519C6AF", "UserMapping");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<UserMapping>("eSourceAppModel.FK__Negotiati__eNegU__0519C6AF", "UserMapping", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="eSourceAppModel", Name="UserMapping")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class UserMapping : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new UserMapping object.
        /// </summary>
        /// <param name="eNegUserID">Initial value of the eNegUserID property.</param>
        /// <param name="eSourceUserID">Initial value of the eSourceUserID property.</param>
        public static UserMapping CreateUserMapping(global::System.Guid eNegUserID, global::System.Guid eSourceUserID)
        {
            UserMapping userMapping = new UserMapping();
            userMapping.eNegUserID = eNegUserID;
            userMapping.eSourceUserID = eSourceUserID;
            return userMapping;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid eNegUserID
        {
            get
            {
                return _eNegUserID;
            }
            set
            {
                if (_eNegUserID != value)
                {
                    OneNegUserIDChanging(value);
                    ReportPropertyChanging("eNegUserID");
                    _eNegUserID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("eNegUserID");
                    OneNegUserIDChanged();
                }
            }
        }
        private global::System.Guid _eNegUserID;
        partial void OneNegUserIDChanging(global::System.Guid value);
        partial void OneNegUserIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid eSourceUserID
        {
            get
            {
                return _eSourceUserID;
            }
            set
            {
                OneSourceUserIDChanging(value);
                ReportPropertyChanging("eSourceUserID");
                _eSourceUserID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("eSourceUserID");
                OneSourceUserIDChanged();
            }
        }
        private global::System.Guid _eSourceUserID;
        partial void OneSourceUserIDChanging(global::System.Guid value);
        partial void OneSourceUserIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Boolean> Deleted
        {
            get
            {
                return _Deleted;
            }
            set
            {
                OnDeletedChanging(value);
                ReportPropertyChanging("Deleted");
                _Deleted = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Deleted");
                OnDeletedChanged();
            }
        }
        private Nullable<global::System.Boolean> _Deleted;
        partial void OnDeletedChanging(Nullable<global::System.Boolean> value);
        partial void OnDeletedChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Guid> DeletedBy
        {
            get
            {
                return _DeletedBy;
            }
            set
            {
                OnDeletedByChanging(value);
                ReportPropertyChanging("DeletedBy");
                _DeletedBy = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("DeletedBy");
                OnDeletedByChanged();
            }
        }
        private Nullable<global::System.Guid> _DeletedBy;
        partial void OnDeletedByChanging(Nullable<global::System.Guid> value);
        partial void OnDeletedByChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> DeletedOn
        {
            get
            {
                return _DeletedOn;
            }
            set
            {
                OnDeletedOnChanging(value);
                ReportPropertyChanging("DeletedOn");
                _DeletedOn = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("DeletedOn");
                OnDeletedOnChanged();
            }
        }
        private Nullable<global::System.DateTime> _DeletedOn;
        partial void OnDeletedOnChanging(Nullable<global::System.DateTime> value);
        partial void OnDeletedOnChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("eSourceAppModel", "FK__Negotiati__eNegU__0519C6AF", "NegotiationBid")]
        public EntityCollection<NegotiationBid> NegotiationBids
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<NegotiationBid>("eSourceAppModel.FK__Negotiati__eNegU__0519C6AF", "NegotiationBid");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<NegotiationBid>("eSourceAppModel.FK__Negotiati__eNegU__0519C6AF", "NegotiationBid", value);
                }
            }
        }

        #endregion
    }

    #endregion
    
}
