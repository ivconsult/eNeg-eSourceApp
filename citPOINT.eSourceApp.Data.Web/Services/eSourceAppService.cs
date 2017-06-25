
namespace citPOINT.eSourceApp.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using citPOINT.eSourceApp.Data.Web;


    // Implements application logic using the eSourceAppEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public partial class eSourceAppService : LinqToEntitiesDomainService<eSourceAppEntities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'NegotiationBids' query.
        [Query(IsDefault = true)]
        public IQueryable<NegotiationBid> GetNegotiationBids()
        {
            return this.ObjectContext.NegotiationBids;
        }

        public void InsertNegotiationBid(NegotiationBid negotiationBid)
        {
            if ((negotiationBid.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(negotiationBid, EntityState.Added);
            }
            else
            {
                this.ObjectContext.NegotiationBids.AddObject(negotiationBid);
            }
        }

        public void UpdateNegotiationBid(NegotiationBid currentNegotiationBid)
        {
            this.ObjectContext.NegotiationBids.AttachAsModified(currentNegotiationBid, this.ChangeSet.GetOriginal(currentNegotiationBid));
        }

        public void DeleteNegotiationBid(NegotiationBid negotiationBid)
        {
            if ((negotiationBid.EntityState == EntityState.Detached))
            {
                this.ObjectContext.NegotiationBids.Attach(negotiationBid);
            }
            this.ObjectContext.NegotiationBids.DeleteObject(negotiationBid);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'UserMappings' query.
        [Query(IsDefault = true)]
        public IQueryable<UserMapping> GetUserMappings()
        {
            return this.ObjectContext.UserMappings;
        }

        public void InsertUserMapping(UserMapping userMapping)
        {
            if ((userMapping.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(userMapping, EntityState.Added);
            }
            else
            {
                this.ObjectContext.UserMappings.AddObject(userMapping);
            }
        }

        public void UpdateUserMapping(UserMapping currentUserMapping)
        {
            this.ObjectContext.UserMappings.AttachAsModified(currentUserMapping, this.ChangeSet.GetOriginal(currentUserMapping));
        }

        public void DeleteUserMapping(UserMapping userMapping)
        {
            if ((userMapping.EntityState == EntityState.Detached))
            {
                this.ObjectContext.UserMappings.Attach(userMapping);
            }
            this.ObjectContext.UserMappings.DeleteObject(userMapping);
        }
    }
}


