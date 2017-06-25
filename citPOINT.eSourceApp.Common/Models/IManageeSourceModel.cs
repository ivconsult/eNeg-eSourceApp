
#region → Usings   .
using System;
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Common;
using citPOINT.eSourceApp.Data.Web;
using citPOINT.eSourceApp.Data;
using citPOINT.eSourceApp.Data.eSource;
using System.Collections.Generic;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 26.01.12     M.Wahab           Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eSourceApp.Common
{
    /// <summary>
    /// Interface for Manage eSource Model
    /// </summary>
    public interface IManageeSourceModel : INotifyPropertyChanged
    {
        #region → Properties     .

        /// <summary>
        /// True if mLoginContext.HasChanges is true; otherwise, false
        /// </summary>
        bool HasChanges { get; }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        bool IsBusy { get; }

        /// <summary>
        /// Gets the e source base address.
        /// </summary>
        /// <value>The e source base address.</value>
        string eSourceBaseAddress { get; }

        /// <summary>
        /// Gets or sets the e source service address.
        /// </summary>
        /// <value>The e source service address.</value>
        string eSourceServiceAddress { get; set; }

        #endregion

        #region → Events         .

        #region → From eSource .

        /// <summary>
        /// Occurs when [test service completed].
        /// </summary>
        event Action<eSourceArgs<bool>> TestServiceCompleted;

        /// <summary>
        /// event occured after creation of Auction and return the Auction id
        /// </summary>
        event Action<eSourceArgs<string>> CreateAuctionCompleted;

        /// <summary>
        /// event occured after creation of tender and return the tender id
        /// </summary>
        event Action<eSourceArgs<string>> CreateTenderCompleted;

        /// <summary>
        /// Event Occurs when esource user creation finished and return the eSource UserID.
        /// </summary>
        event Action<eSourceArgs<string>> CreateUserCompleted;

        /// <summary>
        /// if current user login success return true other return false (in eSource).
        /// </summary>
        event Action<eSourceArgs<bool>> LoginCompleted;

        /// <summary>
        /// if current user login success return true other return false (in eSource).
        /// </summary>
        event Action<eSourceArgs<bool>> TestUserLoginCompleted;

        /// <summary>
        /// Occurs when [get tender complete].
        /// </summary>
        event Action<eSourceArgs<IEnumerable<TenderInfo>>> GetTenderComplete;

        /// <summary>
        /// Occurs when [get bid report complete].
        /// </summary>
        event Action<eSourceArgs<ReportInfo>> GetBidReportComplete;

        /// <summary>
        /// Occurs when [get settings complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<eSourceServicesSetting>> GetSettingsComplete;

        #endregion

        /// <summary>
        /// Occurs when [gete source service URL completed].
        /// </summary>
        event Action<InvokeOperation<string>> GeteSourceServiceUrlCompleted;

        /// <summary>
        /// Occurs when [get negotiation bid complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<NegotiationBid>> GetNegotiationBidComplete;

        /// <summary>
        /// Occurs when [get user mapping complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<UserMapping>> GetUserMappingComplete;

        /// <summary>
        /// Occurs when [send apps statisticals message completed].
        /// </summary>
        event Action<InvokeOperation<bool>> SendAppsStatisticalsMessageCompleted;

        ///// <summary>
        ///// PropertyChanged Callback
        ///// </summary>
        //event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// SaveChangesComplete
        /// </summary>
        event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        #endregion

        #region → Methods        .

        #region → Public         .

        #region → From eSource .
        
        /// <summary>
        /// Tests the servic async.
        /// </summary>
        void TestServiceAsync();
        
        /// <summary>
        /// Creates the auction async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        void CreateAuctionAsync(string eSourceUserID);

        /// <summary>
        /// Creates the tender async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        void CreateTenderAsync(string eSourceUserID);

        /// <summary>
        /// Creates the user async.
        /// </summary>
        /// <param name="eSourceUser">The e source user.</param>
        void CreateUserAsync(eSourceUser eSourceUser);

        /// <summary>
        /// Logins the async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        void LoginAsync(string eSourceUserID);

        /// <summary>
        /// Tests the user login async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        void TestUserLoginAsync(string eSourceUserID);

        /// <summary>
        /// Gets the tender async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        /// <param name="bidIDs">The bid I ds.</param>
        void GetTenderAsync(string eSourceUserID, string[] bidIDs);

        /// <summary>
        /// Gets the bid report async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        /// <param name="bidID">The bid ID.</param>
        /// <param name="type">The type.</param>
        void GetBidReportAsync(string eSourceUserID, string bidID, ObjectType type);

        /// <summary>
        /// Getes the source setting.
        /// </summary>
        void GeteSourceSetting();

        #endregion

        /// <summary>
        /// Getes the source service URL.
        /// </summary>
        void GeteSourceServiceUrl();

        /// <summary>
        /// Updates the user ine neg async.
        /// </summary>
        /// <param name="CurrenteSourceUser">The currente source user.</param>
        void UpdateUserIneNegAsync(eSourceUser CurrenteSourceUser);

        /// <summary>
        /// Sends the apps statisticals messages.
        /// </summary>
        /// <param name="messageSubject">The message subject.</param>
        /// <param name="messageContent">Content of the message.</param>
        /// <returns></returns>
        bool SendAppsStatisticalsMessages(string messageSubject, string messageContent);

        /// <summary>
        /// Gets the negotiation bid async.
        /// </summary>
        void GetNegotiationBidAsync();

        /// <summary>
        /// Gets the user mapping async.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        void GetUserMappingAsync(Guid userID);

        /// <summary>
        /// Addes the source user.
        /// </summary>
        /// <returns></returns>
        eSourceUser AddeSourceUser();

        /// <summary>
        /// Adds the user mapping.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <returns></returns>
        UserMapping AddUserMapping(bool SetInContext);

        /// <summary>
        /// Adds the negotiation bid.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="bidID">The bid ID.</param>
        /// <returns></returns>
        NegotiationBid AddNegotiationBid(bool SetInContext, string bidID);

        /// <summary>
        /// Save changes asynchronously
        /// </summary>
        void SaveChangesAsync();

        /// <summary>
        /// Reject any pending changes
        /// </summary>
        void RejectChanges();

        #endregion

        #endregion
    }
}
