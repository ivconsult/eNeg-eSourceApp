#region → Usings   .
using System;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using citPOINT.eSourceApp.Common;
using citPOINT.eSourceApp.Data.Web;
using citPOINT.eNeg.Data.Web.Test;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 01.02.12     M.Wahab         creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
*/

# endregion

namespace citPOINT.eSourceApp.Data.Web.Test
{
    /// <summary>
    /// Class for testing [Insert - Update - Delete] 
    /// operations for eSourceApp Database
    /// </summary>
    [TestClass]
    public class eSourceAppServiceTest
    {

        #region → Fields         .

        eSourceAppContext mContext;

        List<UserMapping> mUserMappingSource;
        List<NegotiationBid> mNegotiationBidSource;

        int CountOfAllEntries = 0;
        private TestContext testContextInstance;
        #endregion

        #region → Properties     .

        #region Mock Objects

        #region → <1> UserMapping  .

        /// <summary>
        /// Gets the user mapping source.
        /// </summary>
        /// <value>The user mapping source.</value>
        public List<UserMapping> UserMappingSource
        {
            get
            {
                if (mUserMappingSource == null)
                {
                    mUserMappingSource = new List<UserMapping>()
                    {
                        new UserMapping()
                        {
                            eNegUserID=eSourceAppConfigurations.CurrentLoginUser.UserID,
                            eSourceUserID=Guid.NewGuid(),
                            Deleted=false,
                            DeletedBy=eSourceAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn=DateTime.Now
                        },
                        
                         new UserMapping()
                        {
                            eNegUserID=Guid.NewGuid(),
                            eSourceUserID=Guid.NewGuid(),
                            Deleted=false,
                            DeletedBy=eSourceAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn=DateTime.Now
                        }
                    };
                }
                return mUserMappingSource;
            }
        }
        #endregion

        #region → <2> NegotiationBid       .

        /// <summary>
        /// Gets the message types.
        /// </summary>
        /// <value>The message types.</value>
        public List<NegotiationBid> NegotiationBidSource
        {
            get
            {
                if (mNegotiationBidSource == null)
                {
                    mNegotiationBidSource = new List<NegotiationBid>()
                    {
                        new NegotiationBid()
                        {
                            BidID=Guid.NewGuid(),
                            eNegUserID=this.UserMappingSource[0].eNegUserID,
                            NegotiationID=Guid.NewGuid(),
                            IsClosed=false,
                            NegotiationBidID = Guid.NewGuid(),
                            Deleted = false,
                            DeletedBy = eSourceAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn = DateTime.Now
                        },
                        new NegotiationBid()
                        {
                            BidID=Guid.NewGuid(),
                            eNegUserID=this.UserMappingSource[0].eNegUserID,
                            NegotiationID=Guid.NewGuid(),
                            IsClosed=false,
                            NegotiationBidID = Guid.NewGuid(),
                            Deleted = false,
                            DeletedBy = eSourceAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn = DateTime.Now
                        }
                    };
                }
                return mNegotiationBidSource;
            }
        }
        #endregion

        #endregion

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        private eSourceAppContext Context
        {
            get
            {
                if (mContext == null)
                {
                    mContext = new eSourceAppContext(new Uri("http://localhost:9007/citPOINT-eSourceApp-Data-Web-eSourceAppService.svc", UriKind.Absolute));
                }
                return mContext;
            }
        }


        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #endregion Properties

        #region → Constructor    .
        public eSourceAppServiceTest()
        {
            eSourceAppConfigurations.CurrentLoginUser = new LoginUser() { UserID = Guid.NewGuid() };

            CountOfAllEntries = this.UserMappingSource.Count +
                                this.NegotiationBidSource.Count;
        }
        #endregion

        #region → Methods        .

        #region Test Insert All Entries
        /// <summary>
        ///A test for Insert All Entries
        ///</summary>
        [TestMethod]
        [Description(@"Test Insert Operations for all entries")]
        public void InsertAllEntries()
        {
            try
            {
                foreach (var item in this.UserMappingSource)
                {
                    this.Context.UserMappings.Add(item);
                }

                foreach (var item in this.NegotiationBidSource)
                {
                    this.Context.NegotiationBids.Add(item);
                }

                this.Context.SubmitChanges(new Action<SubmitOperation>(InsertAllEntriesComplete), null);
            }
            catch (Exception ex)
            {
                eNegMessageBox.ShowMessageBox(false, "InsertAllEntries", ex);
            }
        }

        /// <summary>
        /// Inserts all entries complete.
        /// </summary>
        /// <param name="subOp">The sub op.</param>
        private void InsertAllEntriesComplete(SubmitOperation subOp)
        {
            if (!subOp.HasError)
            {
                if (subOp.ChangeSet.AddedEntities.Count != this.CountOfAllEntries)
                {
                    eNegMessageBox.ShowMessageBox(false, "InsertAllEntriesComplete", "Number of Records Inserted is not right.");
                }
                else
                {
                    UpdateAllEntries();
                }
            }
            else
            {
                eNegMessageBox.ShowMessageBox(false, "InsertAllEntriesComplete", subOp.Error);
            }
        }

        #endregion

        #region Test Update All Entries

        /// <summary>
        ///A test for Update All Entries
        ///</summary>
        public void UpdateAllEntries()
        {
            try
            {
                this.Context.RejectChanges();

                foreach (var item in this.UserMappingSource)
                {
                    item.DeletedOn = item.DeletedOn.Value.AddDays(100);
                }

                foreach (var item in this.NegotiationBidSource)
                {
                    item.DeletedOn = item.DeletedOn.Value.AddDays(100);
                }


                this.Context.SubmitChanges(new Action<SubmitOperation>(UpdateAllEntriesComplete), null);
            }
            catch (Exception ex)
            {
                eNegMessageBox.ShowMessageBox(false, "UpdateAllEntries", ex);
            }
        }


        /// <summary>
        /// Event Complete of  Update All Entries
        /// </summary>
        /// <param name="subOp">Value of subOp</param>
        private void UpdateAllEntriesComplete(SubmitOperation subOp)
        {
            if (!subOp.HasError)
            {
                if (subOp.ChangeSet.AddedEntities.Count == 0 &&
                    subOp.ChangeSet.RemovedEntities.Count == 0 &&
                    subOp.ChangeSet.ModifiedEntities.Count != this.CountOfAllEntries)
                {
                    eNegMessageBox.ShowMessageBox(false, "UpdateAllEntriesComplete", "Number of Records updated is not right.");
                }
                else
                {
                    DeleteAllEntries();
                }
            }
            else
            {
                eNegMessageBox.ShowMessageBox(false, "UpdateAllEntriesComplete", subOp.Error);
            }
        }
        #endregion

        #region Test Delete All Entries


        /// <summary>
        ///A test for Delete All Entries
        ///only for Delete Flag
        ///</summary>
        public void DeleteAllEntries()
        {
            try
            {
                this.Context.RejectChanges();

                 
                while (this.NegotiationBidSource.Count > 0)
                {
                    this.Context.NegotiationBids.Remove(this.NegotiationBidSource[0]);
                    this.NegotiationBidSource.RemoveAt(0);
                }

                while (this.UserMappingSource.Count > 0)
                {
                    this.Context.UserMappings.Remove(this.UserMappingSource[0]);
                    this.UserMappingSource.RemoveAt(0);
                }


                this.Context.SubmitChanges(new Action<SubmitOperation>(DeleteAllEntriesComplete), null);
            }
            catch (Exception ex)
            {
                eNegMessageBox.ShowMessageBox(false, "DeleteAllEntries", ex);
            }
        }

        /// <summary>
        /// Event Complete of  Delete All Entries
        /// </summary>
        /// <param name="subOp">Value of subOp</param>
        private void DeleteAllEntriesComplete(SubmitOperation subOp)
        {
            if (!subOp.HasError)
            {

                if (subOp.ChangeSet.AddedEntities.Count == 0 &&
                    subOp.ChangeSet.ModifiedEntities.Count == 0 &&
                    subOp.ChangeSet.RemovedEntities.Count != this.CountOfAllEntries)
                {
                    eNegMessageBox.ShowMessageBox(false, "DeleteAllEntriesComplete", "Number of Records Inserted is not right.");
                }
                else
                {
                    eNegMessageBox.ShowMessageBox(true, "Inset - Update - Delete All Entries ", DeleteString);
                }
            }
            else
            {
                eNegMessageBox.ShowMessageBox(false, "DeleteAllEntriesComplete", subOp.Error);
            }
        }
        #endregion

        /// <summary>
        /// get SQL Statement to Clear Database
        /// </summary>
        private string DeleteString
        {
            get
            {
                return @"
---------------------------------------------------
You must run these SQL commands Before retest again
---------------------------------------------------
DELETE [NegotiationBid];
DELETE [UserMapping];
";
            }
        }
        #endregion Methods
    }
}
