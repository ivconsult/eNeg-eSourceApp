#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eSourceApp.Common;
using citPOINT.eSourceApp.Data.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eSourceApp.Data.eSource;
using citPOINT.eSourceApp.Data;
using System.ServiceModel;
using System.Windows;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 26.01.12     M.Wahab           • Creation
 * 
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eSourceApp.Model
{

    #region Using MEF to export Manage eSource Model.

    /// <summary>
    /// Manage eSource Model
    /// </summary>
    [Export(typeof(IManageeSourceModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class ManageeSourceModel : IManageeSourceModel
    {

        #region → Fields         .

        private eSourceAppContext meSourceAppContext;
        private eSourceAppSoapClient meSourceLoader;
        private Boolean mHasChanges = false;
        private Boolean mIsBusy = false;
        private bool mIsLastAuction = false;
        private string meSourceServiceAddress;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Context of Service eNegService
        /// </summary>
        public eSourceAppContext Context
        {
            get
            {
                if (meSourceAppContext == null)
                {
                    meSourceAppContext = new eSourceAppContext(eSourceAppConfigurations.MainServiceUri);
                    meSourceAppContext.PropertyChanged += new PropertyChangedEventHandler(meSourceAppContext_PropertyChanged);
                }
                return meSourceAppContext;
            }
        }

        /// <summary>
        /// Gets the e source loader.
        /// </summary>
        /// <value>The e source loader.</value>
        private eSourceAppSoapClient eSourceLoader
        {
            get
            {
                if (meSourceLoader == null)
                {

                    //                <system.serviceModel>
                    //<bindings>
                    //  <basicHttpBinding>
                    //    <binding name="eSourceAppSoap"
                    //             maxBufferSize="2147483647"
                    //             maxReceivedMessageSize="2147483647">
                    //      <security mode="None" />
                    //    </binding>
                    //  </basicHttpBinding>
                    //</bindings>


                    //<client>
                    //  <endpoint address="http://eneg.negpoint-test.com:10010/WebServices/eNeg/eSourceApp.asmx"
                    //            binding="basicHttpBinding"
                    //            bindingConfiguration="eSourceAppSoap"
                    //            contract="eSource.eSourceAppSoap"
                    //            name="eSourceAppSoap" />
                    //</client>

                    bool isHttps = false;

                    isHttps = !string.IsNullOrEmpty(this.eSourceServiceAddress) && this.eSourceServiceAddress.ToLower().Contains("https://");

                    BasicHttpBinding basicHttpBinding = new BasicHttpBinding();

                    basicHttpBinding.MaxBufferSize = 2147483647;

                    basicHttpBinding.MaxReceivedMessageSize = 2147483647;

                    basicHttpBinding.OpenTimeout = new TimeSpan(0, 0, 15);
                    basicHttpBinding.ReceiveTimeout = new TimeSpan(0, 0, 15);
                    basicHttpBinding.SendTimeout = new TimeSpan(0, 0, 15);

                    basicHttpBinding.Security.Mode = isHttps ? BasicHttpSecurityMode.Transport : BasicHttpSecurityMode.None;

                    EndpointAddress endpointAddress = new EndpointAddress(new Uri(this.eSourceServiceAddress, UriKind.Absolute));

                    meSourceLoader = new eSourceAppSoapClient(basicHttpBinding, endpointAddress);

                }

                return meSourceLoader;
            }
        }

        /// <summary>
        /// Gets the e source base address.
        /// </summary>
        /// <value>The e source base address.</value>
        public string eSourceBaseAddress
        {
            get
            {
                string SoapEndpoint = eSourceLoader.Endpoint.Address.ToString();
                int index = SoapEndpoint.IndexOf("/WebServices/", StringComparison.CurrentCultureIgnoreCase);
                SoapEndpoint = SoapEndpoint.Substring(0, index) + "/Default.aspx";
                return SoapEndpoint;
            }
        }

        /// <summary>
        /// Gets or sets the e source service address.
        /// </summary>
        /// <value>The e source service address.</value>
        public string eSourceServiceAddress
        {
            get
            {
                return meSourceServiceAddress;
            }
            set
            {
                meSourceServiceAddress = value;
                this.InitializeeSourceServices();
            }
        }

        /// <summary>
        /// True if Domain context Has Changes ;otherwise false
        /// </summary>
        public Boolean HasChanges
        {
            get
            {
                return this.mHasChanges;
            }
            private set
            {
                this.mHasChanges = value;
                this.OnPropertyChanged("HasChanges");
            }
        }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        public Boolean IsBusy
        {
            get
            {
                return this.mIsBusy;
            }

            private set
            {
                this.mIsBusy = value;
                this.OnPropertyChanged("IsBusy");

            }
        }

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageeSourceModel"/> class.
        /// </summary>
        public ManageeSourceModel()
        {
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the getTendersCompleted event of the eSourceLoader control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="citPOINT.eSourceApp.Data.eSource.getTendersCompletedEventArgs"/> instance containing the event data.</param>
        private void eSourceLoader_getTendersCompleted(object sender, getTendersCompletedEventArgs e)
        {
            var result = e.Result;
            if (result == null)
            {
                result = new System.Collections.ObjectModel.ObservableCollection<TenderInfo>();
            }

            this.GetTenderComplete(new eSourceArgs<IEnumerable<TenderInfo>>(result, e.Error));
        }

        /// <summary>
        /// Handles the getReportCompleted event of the eSourceLoader control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="citPOINT.eSourceApp.Data.eSource.getReportCompletedEventArgs"/> instance containing the event data.</param>
        private void eSourceLoader_getReportCompleted(object sender, getReportCompletedEventArgs e)
        {
            this.GetBidReportComplete(new eSourceArgs<ReportInfo>(e.Result, e.Error));
        }

        /// <summary>
        /// Handles the TestUserLoginCompleted event of the eSourceLoader control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="citPOINT.eSourceApp.Data.eSource.TestUserLoginCompletedEventArgs"/> instance containing the event data.</param>
        private void eSourceLoader_TestUserLoginCompleted(object sender, TestUserLoginCompletedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Result) && e.Result.IndexOf("successfull", StringComparison.InvariantCultureIgnoreCase) != -1)
            {
                this.TestUserLoginCompleted(new eSourceArgs<bool>(true, e.Error));
            }
            else
            {
                this.TestUserLoginCompleted(new eSourceArgs<bool>(false, e.Error));
            }
        }

        /// <summary>
        /// Handles the createUserCompleted event of the eSourceLoader control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="citPOINT.eSourceApp.Data.eSource.createUserCompletedEventArgs"/> instance containing the event data.</param>
        private void eSourceLoader_createUserCompleted(object sender, createUserCompletedEventArgs e)
        {
            this.CreateUserCompleted(new eSourceArgs<string>(e.Result, e.Error));
        }

        /// <summary>
        /// Handles the CreateTenderCompleted event of the eSourceLoader control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="citPOINT.eSourceApp.Data.eSource.CreateTenderCompletedEventArgs"/> instance containing the event data.</param>
        private void eSourceLoader_CreateTenderCompleted(object sender, CreateTenderCompletedEventArgs e)
        {
            if (mIsLastAuction)
            {
                this.CreateAuctionCompleted(new eSourceArgs<string>(e.Result, e.Error));
            }
            else
            {
                this.CreateTenderCompleted(new eSourceArgs<string>(e.Result, e.Error));
            }
        }

        /// <summary>
        /// Handles the LoginCompleted event of the eSourceLoader control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="citPOINT.eSourceApp.Data.eSource.LoginCompletedEventArgs"/> instance containing the event data.</param>
        private void eSourceLoader_LoginCompleted(object sender, LoginCompletedEventArgs e)
        {
            LoginCompleted(new eSourceArgs<bool>(e.Result, e.Error));
        }

        /// <summary>
        /// Handles the testMEthodCompleted event of the eSourceLoader control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="citPOINT.eSourceApp.Data.eSource.testMEthodCompletedEventArgs"/> instance containing the event data.</param>
        private void eSourceLoader_testMEthodCompleted(object sender, testMEthodCompletedEventArgs e)
        {
            TestServiceCompleted(new eSourceArgs<bool>(e.Error == null, e.Error));
        }

        /// <summary>
        /// Executed when any property of Domain context is changed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        private void meSourceAppContext_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "HasChanges":
                    this.HasChanges = meSourceAppContext.HasChanges;
                    break;
                case "IsLoading":
                    this.IsBusy = meSourceAppContext.IsLoading;
                    break;
                case "IsSubmitting":
                    this.IsBusy = meSourceAppContext.IsSubmitting;
                    break;
            }
        }
        #endregion

        #region → Events         .

        #region → From eSource .

        /// <summary>
        /// Occurs when [test services completed].
        /// </summary>
        public event Action<eSourceArgs<bool>> TestServiceCompleted;

        /// <summary>
        /// Occurs when [gete source service URL completed].
        /// </summary>
        public event Action<InvokeOperation<string>> GeteSourceServiceUrlCompleted;

        /// <summary>
        /// event occured after creation of Auction and return the Auction id
        /// </summary>
        public event Action<eSourceArgs<string>> CreateAuctionCompleted;

        /// <summary>
        /// event occured after creation of tender and return the tender id
        /// </summary>
        public event Action<eSourceArgs<string>> CreateTenderCompleted;

        /// <summary>
        /// Event Occurs when esource user creation finished and return the eSource UserID.
        /// </summary>
        public event Action<eSourceArgs<string>> CreateUserCompleted;

        /// <summary>
        /// if current user login success return true other return false (in eSource).
        /// </summary>
        public event Action<eSourceArgs<bool>> LoginCompleted;



        /// <summary>
        /// if current user login success return true other return false (in eSource).
        /// </summary>
        public event Action<eSourceArgs<bool>> TestUserLoginCompleted;

        /// <summary>
        /// Occurs when [get bid report complete].
        /// </summary>
        public event Action<eSourceArgs<ReportInfo>> GetBidReportComplete;

        /// <summary>
        /// Occurs when [get tender complete].
        /// </summary>
        public event Action<eSourceArgs<IEnumerable<TenderInfo>>> GetTenderComplete;

        /// <summary>
        /// Occurs when [get settings complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<eSourceServicesSetting>> GetSettingsComplete;

        #endregion

        /// <summary>
        /// Occurs when [get negotiation bid complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NegotiationBid>> GetNegotiationBidComplete;

        /// <summary>
        /// Occurs when [get user mapping complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<UserMapping>> GetUserMappingComplete;

        /// <summary>
        /// Occurs when [send apps statisticals message completed].
        /// </summary>
        public event Action<InvokeOperation<bool>> SendAppsStatisticalsMessageCompleted;

        /// <summary>
        /// PropertyChanged Callback
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// SaveChangesComplete
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Private Method used to perform query on certain entity of eSourceApp Entities
        /// </summary>
        /// <typeparam name="T">Value Of T</typeparam>
        /// <param name="qry">Value Of qry</param>
        /// <param name="evt">Value Of evt</param>
        private void PerformQuery<T>(EntityQuery<T> qry, EventHandler<eNegEntityResultArgs<T>> evt) where T : Entity
        {

            Context.Load<T>(qry, LoadBehavior.RefreshCurrent, r =>
            {
                if (evt != null)
                {
                    try
                    {
                        if (r.HasError)
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Error));
                            r.MarkErrorAsHandled();
                        }
                        else
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Entities));
                        }
                    }
                    catch (Exception ex)
                    {
                        evt(this, new eNegEntityResultArgs<T>(ex));
                    }
                }
            }, null);
        }

        /// <summary>
        /// Initializees the source services.
        /// </summary>
        private void InitializeeSourceServices()
        {
            eSourceLoader.testMEthodCompleted += new EventHandler<testMEthodCompletedEventArgs>(eSourceLoader_testMEthodCompleted);
            eSourceLoader.LoginCompleted += new EventHandler<LoginCompletedEventArgs>(eSourceLoader_LoginCompleted);
            eSourceLoader.CreateTenderCompleted += new EventHandler<CreateTenderCompletedEventArgs>(eSourceLoader_CreateTenderCompleted);
            eSourceLoader.createUserCompleted += new EventHandler<createUserCompletedEventArgs>(eSourceLoader_createUserCompleted);
            eSourceLoader.TestUserLoginCompleted += new EventHandler<TestUserLoginCompletedEventArgs>(eSourceLoader_TestUserLoginCompleted);
            eSourceLoader.getReportCompleted += new EventHandler<getReportCompletedEventArgs>(eSourceLoader_getReportCompleted);
            eSourceLoader.getTendersCompleted += new EventHandler<getTendersCompletedEventArgs>(eSourceLoader_getTendersCompleted);
        }



        #endregion

        #region → Protected      .


        #region "INotifyPropertyChanged Interface implementation"

        /// <summary>
        /// called On Property Changed
        /// </summary>
        /// <param name="propertyName">property name</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion "INotifyPropertyChanged Interface implementation"

        #endregion

        #region → Public         .

        #region IManageeSourceModel Interface Implementation

        #region → From eSource .

        /// <summary>
        /// Tests the servic async.
        /// </summary>
        public void TestServiceAsync()
        {
            eSourceLoader.testMEthodAsync();
        }

        /// <summary>
        /// Creates the auction async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        public void CreateAuctionAsync(string eSourceUserID)
        {
            mIsLastAuction = true;
            eSourceLoader.CreateTenderAsync(eSourceUserID, ObjectType.Auction);
        }

        /// <summary>
        /// Creates the tender async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        public void CreateTenderAsync(string eSourceUserID)
        {
            mIsLastAuction = false;
            eSourceLoader.CreateTenderAsync(eSourceUserID, ObjectType.Tender);
        }

        /// <summary>
        /// Creates the user async.
        /// </summary>
        /// <param name="eSourceUser">The e source user.</param>
        public void CreateUserAsync(eSourceUser eSourceUser)
        {
            eSourceLoader.createUserAsync(
                                    eSourceUser.UserName,
                                    eSourceUser.Password,
                                    eSourceUser.Email,
                                    eSourceUser.FirstName,
                                    eSourceUser.LastName,
                                    eSourceUser.Company,
                                    eSourceUser.IsMale); //in eNeg gender male false and in eSource male true
        }

        /// <summary>
        /// Logins the async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        public void LoginAsync(string eSourceUserID)
        {
            eSourceLoader.LoginAsync(eSourceUserID);
        }

        /// <summary>
        /// Tests the user login async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        public void TestUserLoginAsync(string eSourceUserID)
        {
            eSourceLoader.TestUserLoginAsync(eSourceUserID);
        }

        /// <summary>
        /// Gets the tender async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        /// <param name="bidIDs">The bid I ds.</param>
        public void GetTenderAsync(string eSourceUserID, string[] bidIDs)
        {
            #region → Praiparing Query        .

            ArrayOfString tmpbidIDs = new ArrayOfString();

            if (bidIDs != null)
            {
                foreach (var bidItem in bidIDs)
                {
                    tmpbidIDs.Add(bidItem);
                }
            }

            #endregion

            eSourceLoader.getTendersAsync(eSourceUserID, tmpbidIDs);
        }

        /// <summary>
        /// Gets the bid report async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        /// <param name="bidID">The bid ID.</param>
        /// <param name="type">The type.</param>
        public void GetBidReportAsync(string eSourceUserID, string bidID, ObjectType type)
        {
            eSourceLoader.getReportAsync(eSourceUserID, bidID, type);
        }

        /// <summary>
        /// Getes the source setting.
        /// </summary>
        public void GeteSourceSetting()
        {
            PerformQuery<eSourceServicesSetting>(Context.GetEncryptionSettingsQuery(), GetSettingsComplete);
        }

        #endregion

        /// <summary>
        /// Getes the source service URL.
        /// </summary>
        public void GeteSourceServiceUrl()
        {
            this.Context.GeteSourceServiceUrl(GeteSourceServiceUrlCompleted, null);
        }

        /// <summary>
        /// Updates the user ine neg async.
        /// </summary>
        /// <param name="CurrenteSourceUser">The currente source user.</param>
        public void UpdateUserIneNegAsync(eSourceUser CurrenteSourceUser)
        {
            this.Context.UpdateUserIneNeg(eSourceAppConfigurations.CurrentLoginUser.UserID,
                CurrenteSourceUser.FirstName, CurrenteSourceUser.LastName,
                CurrenteSourceUser.Gender, CurrenteSourceUser.Company);
        }

        /// <summary>
        /// Sends the apps statisticals messages.
        /// </summary>
        /// <param name="messageSubject">The message subject.</param>
        /// <param name="messageContent">Content of the message.</param>
        /// <returns></returns>
        public bool SendAppsStatisticalsMessages(string messageSubject, string messageContent)
        {
            this.Context
                .SendAppsStatisticalsMessages(
                                              eSourceAppConfigurations.AppName,
                                              eSourceAppConfigurations.CurrentLoginUser.UserID,
                                              eSourceAppConfigurations.NegotiationIDParameter.Value,
                                              messageContent,
                                              messageSubject,
                                              "eSource App",
                                              "eNeg System",
                                              SendAppsStatisticalsMessageCompleted,
                                              null);

            return true;
        }

        /// <summary>
        /// Gets the negotiation bid async.
        /// </summary>
        public void GetNegotiationBidAsync()
        {
            PerformQuery<NegotiationBid>(Context.GetNegotiationBidsForNegotiationQuery(eSourceAppConfigurations.NegotiationIDParameter.Value),
                                         GetNegotiationBidComplete);
        }

        /// <summary>
        /// Gets the user mapping async.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        public void GetUserMappingAsync(Guid userID)
        {
            PerformQuery<UserMapping>(Context.GetUserMappingsForUserQuery(userID),
                                      GetUserMappingComplete);
        }

        /// <summary>
        /// Addes the source user.
        /// </summary>
        /// <returns></returns>
        public eSourceUser AddeSourceUser()
        {
            eSourceUser eSourceUser = new eSourceUser()
            {
                Company = eSourceAppConfigurations.CurrentLoginUser.CompanyName,
                Email = eSourceAppConfigurations.CurrentLoginUser.EmailAddress,
                FirstName = eSourceAppConfigurations.CurrentLoginUser.FirstName,
                LastName = eSourceAppConfigurations.CurrentLoginUser.LastName,
                Gender = eSourceAppConfigurations.CurrentLoginUser.Gender.Value,
                IsMale = !eSourceAppConfigurations.CurrentLoginUser.Gender.Value,
                IsFemale = eSourceAppConfigurations.CurrentLoginUser.Gender.Value,
                UserName = eSourceAppConfigurations.CurrentLoginUser.EmailAddress
            };

            return eSourceUser;
        }

        /// <summary>
        /// Adds the user mapping.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <returns></returns>
        public UserMapping AddUserMapping(bool SetInContext)
        {
            UserMapping userMapping = new UserMapping()
            {
                //eSourceUserID = Guid.Parse(eSourceUserID),
                eNegUserID = eSourceAppConfigurations.CurrentLoginUser.UserID,
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = eSourceAppConfigurations.CurrentLoginUser.UserID,
            };

            if (SetInContext)
                this.Context.UserMappings.Add(userMapping);

            return userMapping;
        }

        /// <summary>
        /// Adds the negotiation bid.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="bidID">The bid ID.</param>
        /// <returns></returns>
        public NegotiationBid AddNegotiationBid(bool SetInContext, string bidID)
        {
            NegotiationBid negotiationBid = new NegotiationBid()
            {
                BidID = Guid.Parse(bidID),
                NegotiationBidID = Guid.NewGuid(),
                NegotiationID = eSourceAppConfigurations.NegotiationIDParameter.Value,
                IsClosed = false,
                eNegUserID = eSourceAppConfigurations.CurrentLoginUser.UserID,
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = eSourceAppConfigurations.CurrentLoginUser.UserID,
            };


            if (SetInContext)
                this.Context.NegotiationBids.Add(negotiationBid);

            return negotiationBid;
        }

        /// <summary>
        /// Save changes asynchronously
        /// </summary>
        public void SaveChangesAsync()
        {
            this.Context.SubmitChanges(s =>
            {
                if (SaveChangesComplete != null)
                {
                    try
                    {
                        Exception ex = null;
                        if (s.HasError)
                        {
                            ex = s.Error;
                            s.MarkErrorAsHandled();
                        }
                        SaveChangesComplete(this, new SubmitOperationEventArgs(s, ex));
                    }
                    catch (Exception ex)
                    {
                        SaveChangesComplete(this, new SubmitOperationEventArgs(ex));
                    }
                }
            }, null);
        }

        /// <summary>
        /// Reject any pending changes
        /// </summary>
        public void RejectChanges()
        {
            this.Context.RejectChanges();
        }

        #endregion

        #endregion

        #endregion
    }
}




