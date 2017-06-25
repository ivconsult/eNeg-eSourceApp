
#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.DomainServices.Client;
using System.ComponentModel;
using citPOINT.eNeg.Common;
using citPOINT.eSourceApp.Common;
using citPOINT.eSourceApp.Data.Web;
using citPOINT.eSourceApp.MVVM.UnitTest.Helpers;
using citPOINT.eSourceApp.Data.eSource;
using citPOINT.eSourceApp.Data;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 29.01.12     M.Wahab         • creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion


namespace citPOINT.eSourceApp.MVVM.UnitTest
{
    public class MockManageeSourceModel : IManageeSourceModel
    {
        #region → Fields         .

        private eSourceAppContext mContext;
        private string meSourceBaseAddress = "https://eneg.negoint-solution.com/";
        private eSourceUser mCurrenteSourceUser;
        private List<NegotiationBid> mNegotiationBidSource;
        private UserMapping mCurrentMappedUser;
        private List<TenderInfo> mTenderInfosSource;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the e source service address.
        /// </summary>
        /// <value>The e source service address.</value>
        public string eSourceServiceAddress { get; set; }

        /// <summary>
        /// True if mLoginContext.HasChanges is true; otherwise, false
        /// </summary>
        /// <value></value>
        public bool HasChanges
        {
            get { return true; }
        }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        /// <value></value>
        public bool IsBusy
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        public eSourceAppContext Context
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
        /// Gets the e source base address.
        /// </summary>
        /// <value>The e source base address.</value>
        public string eSourceBaseAddress
        {
            get
            {
                return meSourceBaseAddress;
            }
            set
            {
                meSourceBaseAddress = value;
            }
        }

        /// <summary>
        /// Gets the tender infos source.
        /// </summary>
        /// <value>The tender infos source.</value>
        public List<TenderInfo> TenderInfosSource
        {
            get
            {
                if (mTenderInfosSource == null)
                {
                    mTenderInfosSource = new List<TenderInfo>()
                    {
                        new TenderInfo()
                        {
                            bidID = "C7CAD9E5-FA25-4EB9-82E6-E4D66D2D0301",
                            endTime = DateTime.Now.AddDays(7),
                            name= "New TenderInfo 1",
                            type = ObjectType.Tender
                        },
                        new TenderInfo()
                        {
                            bidID = "C7CAD9E5-FA25-4EB9-82E6-E4D66D2D0302",
                            endTime = DateTime.Now.AddDays(7),
                            name = "New TenderInfo 2",
                            type = ObjectType.Tender
                        },
                        new TenderInfo()
                        {
                            bidID = "C7CAD9E5-FA25-4EB9-82E6-E4D66D2D0303",
                            endTime = DateTime.Now.AddDays(7),
                            name = "New Auction 1",
                            type = ObjectType.Auction
                        },
                        new TenderInfo()
                        {
                            bidID = "C7CAD9E5-FA25-4EB9-82E6-E4D66D2D0304",
                            endTime = DateTime.Now.AddDays(7),
                            name = "New Auction 2",
                            type = ObjectType.Auction
                        },
                    };
                }
                return mTenderInfosSource;
            }
        }

        /// <summary>
        /// Gets the currente source user.
        /// </summary>
        /// <value>The currente source user.</value>
        public eSourceUser CurrenteSourceUser
        {
            get
            {
                if (mCurrenteSourceUser == null)
                {
                    mCurrenteSourceUser = new eSourceUser()
                    {
                        FirstName = "Yousra",
                        LastName = "Reda",
                        UserName = "yousra.reda@citpoint.com",
                        Email = "yousra.reda@citpoint.com",
                        Company = "citPoint",
                        Gender = false,
                        Password = "123456789"
                    };
                }
                return mCurrenteSourceUser;
            }
        }

        /// <summary>
        /// Gets the current mapped user.
        /// </summary>
        /// <value>The current mapped user.</value>
        public UserMapping CurrentMappedUser
        {
            get
            {
                if (mCurrentMappedUser == null)
                {
                    mCurrentMappedUser = new UserMapping()
                    {
                        eSourceUserID = new Guid("C7CAD9E5-FA25-4EB9-82E6-E4D66D2D03AA"),
                        eNegUserID = new Guid("C7CAD9E5-FA25-4EB9-82E6-E4D66D2D03BB"),
                        Deleted = false,
                        DeletedOn = DateTime.Now
                    };
                }
                return mCurrentMappedUser;
            }
        }

        /// <summary>
        /// Gets the negotiation bid source.
        /// </summary>
        /// <value>The negotiation bid source.</value>
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
                            NegotiationBidID = Guid.NewGuid(),
                            NegotiationID = SharedTestContext.CarNegotiation,
                            BidID = Guid.NewGuid(),
                            IsClosed = false,
                            eNegUserID = new Guid("C7CAD9E5-FA25-4EB9-82E6-E4D66D2D03BB"),
                            Deleted= false,
                            DeletedOn = DateTime.Now
                        },
                        new NegotiationBid()
                        {
                            NegotiationBidID = Guid.NewGuid(),
                            BidID = Guid.NewGuid(),
                            IsClosed = false,
                            eNegUserID = new Guid("C7CAD9E5-FA25-4EB9-82E6-E4D66D2D03BB"),
                            Deleted= false,
                            DeletedOn = DateTime.Now
                        },
                        new NegotiationBid()
                        {
                            NegotiationBidID = Guid.NewGuid(),
                            BidID = Guid.NewGuid(),
                            IsClosed = false,
                            eNegUserID = new Guid("C7CAD9E5-FA25-4EB9-82E6-E4D66D2D03BB"),
                            Deleted= false,
                            DeletedOn = DateTime.Now
                        }
                    };
                }
                return mNegotiationBidSource;
            }
        }

        /// <summary>
        /// Gets the report.
        /// </summary>
        /// <value>The report.</value>
        public ReportInfo Report
        {
            get
            {
                return new ReportInfo()
                {
                    downloadName = "eSource Report",
                    binaryData = new byte[] { }
                };
            }
        }
        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="MockManageeSourceModel"/> class.
        /// </summary>
        public MockManageeSourceModel()
        {
        }

        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when [test service completed].
        /// </summary>
        public event Action<eSourceArgs<bool>> TestServiceCompleted;

        /// <summary>
        /// Occurs when [gete source service URL completed].
        /// </summary>
        public event Action<InvokeOperation<string>> GeteSourceServiceUrlCompleted;

        /// <summary>
        /// Occurs when [get settings complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<eSourceServicesSetting>> GetSettingsComplete;

        /// <summary>
        /// event occured after creation of Auction and return the Auction id
        /// </summary>
        public event Action<eSourceArgs<string>> CreateAuctionCompleted;

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
        /// event occured after creation of tender and return the tender id
        /// </summary>
        public event Action<eSourceArgs<string>> CreateTenderCompleted;

        /// <summary>
        /// Occurs when [gete source base address completed].
        /// </summary>
        public event Action<InvokeOperation<string>> GeteSourceBaseAddressCompleted;

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
        /// Private Method used to perform query on certain entity of eNeg Entities
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

        #endregion

        #region → Protected      .

        #region INotifyPropertyChanged Interface implementation



        /// <summary>
        /// Handle changes in any Property
        /// </summary>
        /// <param name="propertyName">Property Name</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #endregion

        #region → Public         .
        
        /// <summary>
        /// Tests the servic async.
        /// </summary>
        public void TestServiceAsync()
        {
            if (TestServiceCompleted != null)
            {
                TestServiceCompleted(new eSourceArgs<bool>(true, null));

            }
        }

        /// <summary>
        /// Getes the source service URL.
        /// </summary>
        public void GeteSourceServiceUrl()
        {

        }

        /// <summary>
        /// Getes the source setting.
        /// </summary>
        public void GeteSourceSetting()
        {
            if (GetSettingsComplete != null)
            {
                GetSettingsComplete(this,
                    new eNegEntityResultArgs<eSourceServicesSetting>(
                        new List<eSourceServicesSetting>(){
                            new eSourceServicesSetting(){ 
                                EncryptionIV="5C7k9L1f3G4h2N6@", 
                                EncryptionKey ="sWv07@iDT5xderfv", 
                                ID=1
                                                        }
                                                          })
                                  );
            }
        }

        /// <summary>
        /// Getes the source base address async.
        /// </summary>
        public void GeteSourceBaseAddressAsync()
        {
            if (GeteSourceBaseAddressCompleted != null)
            {

            }
        }

        /// <summary>
        /// Creates the auction async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        public void CreateAuctionAsync(string eSourceUserID)
        {
            if (CreateAuctionCompleted != null)
            {
                CreateAuctionCompleted(new eSourceArgs<string>(Guid.NewGuid().ToString(), null));
            }
        }

        /// <summary>
        /// Creates the tender async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        public void CreateTenderAsync(string eSourceUserID)
        {
            if (CreateTenderCompleted != null)
            {
                CreateTenderCompleted(new eSourceArgs<string>(Guid.NewGuid().ToString(), null));
            }
        }

        /// <summary>
        /// Creates the user async.
        /// </summary>
        /// <param name="eSourceUser">The e source user.</param>
        public void CreateUserAsync(eSourceUser eSourceUser)
        {
            if (CreateUserCompleted != null)
            {
                CreateUserCompleted(new eSourceArgs<string>(Guid.NewGuid().ToString(), null));
            }
        }

        /// <summary>
        /// Logins the async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        public void LoginAsync(string eSourceUserID)
        {
            if (LoginCompleted != null)
            {
                LoginCompleted(new eSourceArgs<bool>(true, null));
            }
        }

        /// <summary>
        /// Tests the user login async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        public void TestUserLoginAsync(string eSourceUserID)
        {
            if (TestUserLoginCompleted != null)
            {
                TestUserLoginCompleted(new eSourceArgs<bool>(true, null));
            }
        }

        /// <summary>
        /// Gets the bid report async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        /// <param name="bidID">The bid ID.</param>
        public void GetBidReportAsync(string eSourceUserID, string bidID, ObjectType type)
        {
            if (GetBidReportComplete != null)
            {
                GetBidReportComplete(new eSourceArgs<ReportInfo>(Report, null));
            }
        }

        /// <summary>
        /// Updates the user ine neg async.
        /// </summary>
        /// <param name="CurrenteSourceUser">The currente source user.</param>
        public void UpdateUserIneNegAsync(eSourceUser CurrenteSourceUser)
        {

        }

        /// <summary>
        /// Sends the apps statisticals messages.
        /// </summary>
        /// <param name="messageSubject">The message subject.</param>
        /// <param name="messageContent">Content of the message.</param>
        /// <returns></returns>
        public bool SendAppsStatisticalsMessages(string messageSubject, string messageContent)
        {
            if (SendAppsStatisticalsMessageCompleted != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the negotiation bid async.
        /// </summary>
        public void GetNegotiationBidAsync()
        {
            if (GetNegotiationBidComplete != null)
            {
                GetNegotiationBidComplete(this, new eNegEntityResultArgs<NegotiationBid>(NegotiationBidSource));
            }
        }

        /// <summary>
        /// Gets the user mapping async.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        public void GetUserMappingAsync(Guid userID)
        {
            if (GetUserMappingComplete != null)
            {
                GetUserMappingComplete(this, new eNegEntityResultArgs<UserMapping>(new List<UserMapping>() { CurrentMappedUser }));
            }
        }

        /// <summary>
        /// Addes the source user.
        /// </summary>
        /// <returns></returns>
        public eSourceUser AddeSourceUser()
        {
            eSourceUser user = new eSourceUser()
            {
                FirstName = "Yousra",
                LastName = "Reda",
                UserName = "yousra.reda@gmail.com",
                Email = "yousra.reda@gmail.com",
                Company = "citPoint",
                Gender = false,
                Password = "123456789"
            };
            return user;
        }

        /// <summary>
        /// Adds the user mapping.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <returns></returns>
        public UserMapping AddUserMapping(bool SetInContext)
        {
            UserMapping mappedUser = new UserMapping()
            {
                eSourceUserID = Guid.NewGuid(),
                eNegUserID = Guid.NewGuid(),
                Deleted = false,
                DeletedOn = DateTime.Now
            };
            return mappedUser;
        }

        /// <summary>
        /// Adds the negotiation bid.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="bidID">The bid ID.</param>
        /// <returns></returns>
        public NegotiationBid AddNegotiationBid(bool SetInContext, string bidID)
        {
            NegotiationBid negBid = new NegotiationBid()
            {
                NegotiationBidID = Guid.NewGuid(),
                BidID = Guid.Parse(bidID),
                IsClosed = false,
                eNegUserID = Guid.NewGuid(),
                Deleted = false,
                DeletedOn = DateTime.Now
            };
            return negBid;
        }

        /// <summary>
        /// Gets the tender async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        /// <param name="bidIDs">The bid I ds.</param>
        public void GetTenderAsync(string eSourceUserID, string[] bidIDs)
        {
            if (GetTenderComplete != null)
            {
                GetTenderComplete(new eSourceArgs<IEnumerable<TenderInfo>>(TenderInfosSource, null));
            }
        }

        /// <summary>
        /// Save changes asynchronously
        /// </summary>
        public void SaveChangesAsync()
        {
            if (SaveChangesComplete != null)
            {
                SaveChangesComplete(this, new SubmitOperationEventArgs(null, null));
            }
        }

        /// <summary>
        /// Reject any pending changes
        /// </summary>
        public void RejectChanges()
        {

        }

        #endregion  Public

        #endregion Methods








    }
}