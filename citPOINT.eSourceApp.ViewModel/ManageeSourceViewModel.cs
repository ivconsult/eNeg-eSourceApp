
#region → Usings   .
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Browser;
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using citPOINT.eSourceApp.Common;
using citPOINT.eSourceApp.Data;
using citPOINT.eSourceApp.Data.eSource;
using citPOINT.eSourceApp.Data.Web;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 26.01.12     Yousra Reda       Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/
# endregion


namespace citPOINT.eSourceApp.ViewModel
{
    #region  Using MEF to export ManageeSourceViewModel
    /// <summary>
    /// Class For managing all action will be achieved through eSource App
    /// </summary>
    [Export(eSourceAppViewModelTypes.ManageeSourceViewModel)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    #endregion
    public class ManageeSourceViewModel : ViewModelBase
    {
        #region → Enums          .

        /// <summary>
        /// Navigation Purpose Options
        /// </summary>
        private enum NavigationPurposeOptions
        {
            /// <summary>
            /// Open Created Bid
            /// </summary>
            OpenCreatedBid,
            /// <summary>
            /// Opene Source
            /// </summary>
            OpeneSource
        }

        #endregion

        #region → Fields         .

        private IManageeSourceModel mManageeSourceModel;

        private bool mIsBusy;
        private bool mIsCreatingUserBusy;
        private bool mIsLoginSuccess = false;
        private bool RunQueueForApplyChanges;

        private string meSourceBaseAddress = string.Empty;

        private eSourceUser mCurrenteSourceUser;

        private List<NegotiationBid> mNegotiationBidSource;
        private List<TenderInfo> mTendersSource;
        private UserMapping mCurrentMappedUser;

        private RelayCommand mCreateeSourceUserCommand;
        private RelayCommand<string> mCreateBidCommand;
        private RelayCommand mNavigateToeSourceCommand;
        private RelayCommand<TenderInfo> mOpenBidIneSourceCommand;
        private RelayCommand<TenderInfo> mDownloadReportCommand;
        private RelayCommand mSaveDownloadReportCommand;

        private List<string> BidIDs = new List<string>();
        private bool IsNewTenderAdded;
        private eSourceServicesSetting meSourceServicesSetting;
        private NavigationPurposeOptions NavigationPurpose;
        private TenderInfo NavigationBidItem;

        public ReportInfo lastReprot = null;

        public bool IsUnitTest = false;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is login success.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is login success; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoginSuccess
        {
            get
            {
                return mIsLoginSuccess;
            }
            set
            {
                mIsLoginSuccess = value;
                this.RaisePropertyChanged("IsLoginSuccess");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get { return mIsBusy; }
            set
            {
                mIsBusy = value;
                this.RaisePropertyChanged("IsBusy");

                if (!this.IsBusy)
                {
                    if (RunQueueForApplyChanges)
                    {
                        this.ApplyChanges();
                    }
                }

                CreateeSourceUserCommand.RaiseCanExecuteChanged();

                this.RaisePropertyChanged("IsCreatingUserBusy");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is creating user busy.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is creating user busy; otherwise, <c>false</c>.
        /// </value>
        public bool IsCreatingUserBusy
        {
            get
            {
                return mIsCreatingUserBusy || this.IsBusy;
            }
            set
            {
                mIsCreatingUserBusy = value;
                this.RaisePropertyChanged("IsCreatingUserBusy");
                CreateeSourceUserCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the e source base address.
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
                this.mManageeSourceModel.eSourceServiceAddress = value;
                meSourceBaseAddress = this.mManageeSourceModel.eSourceBaseAddress;
                this.RaisePropertyChanged("eSourceBaseAddress");
            }
        }

        /// <summary>
        /// Gets or sets the e source services setting.
        /// </summary>
        /// <value>The e source services setting.</value>
        public eSourceServicesSetting eSourceServicesSetting
        {
            get { return meSourceServicesSetting; }
            set { meSourceServicesSetting = value; }
        }

        /// <summary>
        /// Gets the WS call query param.
        /// </summary>
        /// <value>The WS call query param.</value>
        public string WSCallQueryParam
        {
            get
            {
                return this.Encrypt(QueryParameters.WSCall.ToString()) + "=" + this.Encrypt("true");
            }
        }

        /// <summary>
        /// Gets or sets the tenders source.
        /// </summary>
        /// <value>The tenders source.</value>
        public List<TenderInfo> TendersSource
        {
            get
            {
                return mTendersSource;
            }
            set
            {
                mTendersSource = value;
                RaisePropertyChanged("TendersSource");
            }
        }

        /// <summary>
        /// Gets or sets the currente source user.
        /// </summary>
        /// <value>The currente source user.</value>
        public eSourceUser CurrenteSourceUser
        {
            get
            {
                return mCurrenteSourceUser;
            }

            set
            {
                mCurrenteSourceUser = value;

                this.RaisePropertyChanged("CurrenteSourceUser");

                if (CurrenteSourceUser != null)
                {
                    CurrenteSourceUser.PropertyChanged += new PropertyChangedEventHandler(CurrenteSourceUser_PropertyChanged);
                }
            }
        }

        /// <summary>
        /// Gets or sets the current mapped user.
        /// </summary>
        /// <value>The current mapped user.</value>
        public UserMapping CurrentMappedUser
        {
            get
            {
                return mCurrentMappedUser;
            }
            set
            {
                mCurrentMappedUser = value;
                RaisePropertyChanged("CurrentMappedUser");
            }
        }

        /// <summary>
        /// Gets or sets the negotiation bid source.
        /// </summary>
        /// <value>The negotiation bid source.</value>
        public List<NegotiationBid> NegotiationBidSource
        {
            get
            {
                return mNegotiationBidSource;
            }
            set
            {
                mNegotiationBidSource = value;
                this.RaisePropertyChanged("NegotiationBidSource");
            }
        }

        #endregion

        #region → Constructors   .
        /// <summary>
        /// Initializes a new instance of the <see cref="ManageeSourceViewModel"/> class.
        /// </summary>
        /// <param name="ManageeSourceModel">The managee source model.</param>
        [ImportingConstructor]
        public ManageeSourceViewModel(IManageeSourceModel ManageeSourceModel)
        {
            this.mManageeSourceModel = ManageeSourceModel;

            #region → Set up event handling       .
            this.mManageeSourceModel.TestServiceCompleted += new Action<eSourceArgs<bool>>(mManageeSourceModel_TestServiceCompleted);
            this.mManageeSourceModel.TestUserLoginCompleted += new Action<eSourceArgs<bool>>(mManageeSourceModel_TestUserLoginCompleted);
            this.mManageeSourceModel.LoginCompleted += new Action<eSourceArgs<bool>>(mManageeSourceModel_LoginCompleted);
            this.mManageeSourceModel.GetTenderComplete += new Action<eSourceArgs<IEnumerable<TenderInfo>>>(mManageeSourceModel_GetTenderComplete);
            this.mManageeSourceModel.GetBidReportComplete += new Action<eSourceArgs<ReportInfo>>(mManageeSourceModel_GetBidReportComplete);
            this.mManageeSourceModel.CreateAuctionCompleted += new Action<eSourceArgs<string>>(mManageeSourceModel_CreateAuctionCompleted);
            this.mManageeSourceModel.CreateTenderCompleted += new Action<eSourceArgs<string>>(mManageeSourceModel_CreateTenderCompleted);
            this.mManageeSourceModel.GetNegotiationBidComplete += new EventHandler<eNegEntityResultArgs<NegotiationBid>>(mManageeSourceModel_GetNegotiationBidComplete);
            this.mManageeSourceModel.CreateUserCompleted += new Action<eSourceArgs<string>>(mManageeSourceModel_CreateUserCompleted);
            this.mManageeSourceModel.GetUserMappingComplete += new EventHandler<eNegEntityResultArgs<UserMapping>>(mManageeSourceModel_GetUserMappingComplete);
            this.mManageeSourceModel.GetSettingsComplete += new EventHandler<eNegEntityResultArgs<Data.Web.eSourceServicesSetting>>(mManageeSourceModel_GetSettingsComplete);
            this.mManageeSourceModel.PropertyChanged += new PropertyChangedEventHandler(mManageeSourceModel_PropertyChanged);
            this.mManageeSourceModel.SaveChangesComplete += new EventHandler<SubmitOperationEventArgs>(mManageeSourceModel_SaveChangesComplete);
            this.mManageeSourceModel.GeteSourceServiceUrlCompleted += new Action<System.ServiceModel.DomainServices.Client.InvokeOperation<string>>(mManageeSourceModel_GeteSourceServiceUrlCompleted);
            #endregion

            #region → Load Lookup tables          .

            this.mManageeSourceModel.GeteSourceServiceUrl();

            this.GeteSourceSetting();

            #endregion
        }


        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the PropertyChanged event of the CurrenteSourceUser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void CurrenteSourceUser_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (CurrenteSourceUser != null)
            {
                if (!CurrenteSourceUser.TryValidateProperty(e.PropertyName))
                {
                    CurrenteSourceUser.ValidationErrors.Add(new System.ComponentModel.DataAnnotations.ValidationResult("This Field is required", new List<string> { e.PropertyName }));
                }
                else
                {
                    var error = CurrenteSourceUser.ValidationErrors.Where(s => s.MemberNames.Contains(e.PropertyName)).FirstOrDefault();

                    if (error != null)
                    {
                        CurrenteSourceUser.ValidationErrors.Remove(error);
                    }

                }
            }
        }

        /// <summary>
        ///  Call back of Gete source service URL.
        /// </summary>
        /// <param name="e">The e.</param>
        private void mManageeSourceModel_GeteSourceServiceUrlCompleted(System.ServiceModel.DomainServices.Client.InvokeOperation<string> e)
        {
            if (!e.HasError)
            {
                if (!string.IsNullOrEmpty(e.Value))
                {

                    #region → Load Lookup tables          .

                    //Get Address of web site
                    this.eSourceBaseAddress = e.Value;

                    this.mManageeSourceModel.TestServiceAsync();

                    #endregion
                }
                else
                {
                    eSourceAppMessanger.RaiseErrorMessage.Send(new Exception("Can not get the service url"));
                }
            }
            else
            {
                eSourceAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back of Test service completed.
        /// </summary>
        /// <param name="e">The e.</param>
        private void mManageeSourceModel_TestServiceCompleted(eSourceArgs<bool> e)
        {
            if (!e.HasError)
            {
                if (e.Value)
                {
                    if (eSourceAppConfigurations.NegotiationIDParameter.HasValue)
                    {
                        GetUserMappingAsync();
                    }
                }
            }
            else
            {
                //eSourceAppMessanger.RaiseErrorMessage.Send(e.Error);

                eSourceAppMessanger.ChangeScreenMessage.Send(eSourceAppViewTypes.ServiceAlarmView);
            }

        }

        /// <summary>
        /// Call back of Get settings complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mManageeSourceModel_GetSettingsComplete(object sender, eNegEntityResultArgs<eSourceServicesSetting> e)
        {
            if (!e.HasError)
            {
                if (e.Results != null &&
                    e.Results.Count() > 0 &&
                    !string.IsNullOrEmpty(e.Results.First().EncryptionIV) &&
                    !string.IsNullOrEmpty(e.Results.First().EncryptionKey))
                {
                    meSourceServicesSetting = e.Results.First();
                }
                else
                {
                    ////eSourceAppMessanger.ChangeScreenMessage.Send(eSourceAppViewTypes.BidsView);
                }
            }
            else
            {
                eSourceAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Ms the managee source model_ test user login completed.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void mManageeSourceModel_TestUserLoginCompleted(eSourceArgs<bool> obj)
        {
            if (!obj.HasError)
            {
                //case the eSourceUserID we have tested is still a valid one.
                if (obj.Value)
                {
                    this.IsLoginSuccess = true;

                    if (IsUnitTest)
                    {
                        return;
                    }

                    //Get all negotiations bids available for this negotiation.
                    GetNegotiationBidAsync();

                    //Change screen to main view which contain the grid.
                    eSourceAppMessanger.ChangeScreenMessage.Send(eSourceAppViewTypes.BidsView);
                }
                //case the eSourceUserID we have tested is not valid anymore.
                else
                {
                    CurrenteSourceUser = AddeSourceUser();


                    eSourceAppMessanger.ChangeScreenMessage.Send(eSourceAppViewTypes.FillDataView);
                }
            }
            else
            {
                eSourceAppMessanger.RaiseErrorMessage.Send(obj.Error);
            }
        }

        /// <summary>
        /// Call back of login.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void mManageeSourceModel_LoginCompleted(eSourceArgs<bool> obj)
        {
            if (!obj.HasError)
            {
                if (obj.Value)
                {
                    this.IsLoginSuccess = true;

                    if (IsUnitTest)
                    {
                        return;
                    }

                    if (NavigationPurpose == NavigationPurposeOptions.OpeneSource)
                    {
                        eNegNavigation.NavigateToUrl(this.eSourceBaseAddress + "?" + WSCallQueryParam, true);
                    }
                    else if (NavigationPurpose == NavigationPurposeOptions.OpenCreatedBid)
                    {
                        //htts://eSource.com/Default.aspx?wsCall&BidID=356-356-8787-03213&Type=Tender

                        StringBuilder url = EncryptUrl(NavigationBidItem.bidID, NavigationBidItem.type.ToString());

                        //Navigate to certain bid in eSource
                        eNegNavigation.NavigateToUrl(url.ToString(), true);
                    }
                }
                else
                {
                    eNeg.Common.eNegMessanger.SendCustomMessage.Send(
                        new eNegMessage("login for eSource failed", ImageType.Error, eSourceAppConfigurations.ApplicationID));
                }
            }
            else
            {
                eSourceAppMessanger.RaiseErrorMessage.Send(obj.Error);
            }
        }

        /// <summary>
        /// Handles the SaveChangesComplete event of the mManageeSourceModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="citPOINT.eNeg.Common.SubmitOperationEventArgs"/> instance containing the event data.</param>
        private void mManageeSourceModel_SaveChangesComplete(object sender, SubmitOperationEventArgs e)
        {
            if (!e.HasError)
            {
                #region → In case changes in user Mapping .

                if (((e.SubmitOp.ChangeSet.AddedEntities.Count >= 1) &&
                   (e.SubmitOp.ChangeSet.AddedEntities.Where(s => s.GetType().Equals(typeof(UserMapping))).FirstOrDefault() is UserMapping))
                        ||
                   ((e.SubmitOp.ChangeSet.ModifiedEntities.Count >= 1) &&
                   (e.SubmitOp.ChangeSet.ModifiedEntities.Where(s => s.GetType().Equals(typeof(UserMapping))).FirstOrDefault() is UserMapping)))
                {
                    this.IsLoginSuccess = true;

                    eSourceAppMessanger.ChangeScreenMessage.Send(eSourceAppViewTypes.BidsView);
                }
                #endregion
            }
            else
            {
                eSourceAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back of Get tender completed.
        /// </summary>
        /// <param name="e">The e.</param>
        private void mManageeSourceModel_GetTenderComplete(eSourceArgs<IEnumerable<TenderInfo>> e)
        {
            if (!e.HasError)
            {
                if (IsNewTenderAdded)
                {
                    TendersSource.AddRange(e.Value.ToList());
                    IsNewTenderAdded = false;
                    TendersSource = new List<TenderInfo>(TendersSource);

                    //Navigate to proper page in esource
                    OpenBidIneSourceCommand.Execute(e.Value.FirstOrDefault());
                }
                else
                {
                    TendersSource = e.Value.ToList();

                    StringBuilder ClosedTenders = new StringBuilder("");

                    foreach (var tender in TendersSource)
                    {
                        if (NegotiationBidSource.Where(s => s.BidID.ToString() == tender.bidID
                            && s.IsClosed == false).FirstOrDefault() != null
                            && tender.isclosed)
                        {
                            var negBid = NegotiationBidSource.Where(s => s.BidID.ToString() == tender.bidID).FirstOrDefault();
                            if (negBid != null)
                            {
                                negBid.IsClosed = true;
                            }
                            ClosedTenders.Append(tender.type.ToString() + " - " + tender.name + " - Closed in: " + tender.endTime + "\r\n");
                        }
                    }

                    if (!string.IsNullOrEmpty(ClosedTenders.ToString()))
                    {
                        SaveChangesAsync();

                        SendAppsStatisticalsMessages("RFxs/Auctions has been closed", string.Format(Resources.TenderClosedReport, ClosedTenders.ToString(), eSourceBaseAddress));
                    }

                    //To hide load window in case of apply change loading window rasied
                    eSourceAppMessanger.ChangeScreenMessage.Send(eSourceAppViewTypes.BidsView);
                }
            }
            else
            {
                eSourceAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back of Get bid report complete.
        /// </summary>
        /// <param name="e">The e.</param>
        private void mManageeSourceModel_GetBidReportComplete(eSourceArgs<ReportInfo> e)
        {
            if (!e.HasError)
            {
                try
                {
                    if (e.Value != null)
                    {
                        lastReprot = e.Value;

                        eSourceAppMessanger.ChangeScreenMessage.Send(eSourceAppViewTypes.SaveReportView.ToString());
                    }
                }
                catch (Exception ex)
                {
                    eSourceAppMessanger.RaiseErrorMessage.Send(ex);
                }
            }
            else
            {
                eSourceAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back of Create tender.
        /// </summary>
        /// <param name="e">The e.</param>
        private void mManageeSourceModel_CreateTenderCompleted(eSourceArgs<string> e)
        {
            if (!e.HasError)
            {
                if (!string.IsNullOrEmpty(e.Value))
                {
                    FinishCreatingTender(e.Value, "Tender");
                }
                else
                {
                    //show notification message that creating tender has failed.
                    eNeg.Common.eNegMessanger.SendCustomMessage.Send(new eNegMessage("RFx creation failed", ImageType.Error, eSourceAppConfigurations.ApplicationID));
                }
            }
            else
            {
                eSourceAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back of Create auction.
        /// </summary>
        /// <param name="e">The e.</param>
        private void mManageeSourceModel_CreateAuctionCompleted(eSourceArgs<string> e)
        {
            if (!e.HasError)
            {
                if (!string.IsNullOrEmpty(e.Value))
                {
                    FinishCreatingTender(e.Value, "Auction");
                }
                else
                {
                    eNegMessanger.SendCustomMessage.Send(new eNegMessage("Auction creation failed", ImageType.Error, eSourceAppConfigurations.ApplicationID));
                }
            }
            else
            {
                eSourceAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back of Get negotiation bid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mManageeSourceModel_GetNegotiationBidComplete(object sender, eNegEntityResultArgs<NegotiationBid> e)
        {
            if (!e.HasError)
            {
                NegotiationBidSource = e.Results.AsEnumerable().ToList();

                BidIDs = new List<string>();

                foreach (var Bid in NegotiationBidSource)
                {
                    BidIDs.Add(Bid.BidID.ToString());
                }

                GetTendersAsync(CurrentMappedUser.eSourceUserID.ToString(), BidIDs.ToArray());
            }
            else
            {
                eSourceAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back of Create user completed.
        /// </summary>
        /// <param name="e">The e.</param>
        private void mManageeSourceModel_CreateUserCompleted(eSourceArgs<string> e)
        {
            if (!e.HasError)
            {
                IsCreatingUserBusy = false;

                if (String.IsNullOrEmpty(e.Value))
                {
                    //Show message to the user indicating that creating user in eSource has failed :(
                    eNegMessanger.SendCustomMessage.Send(new eNegMessage(Resources.eSourceUserCreationFailed, ImageType.Error, eSourceAppConfigurations.ApplicationID));
                }
                else
                {
                    CurrentMappedUser.eSourceUserID = Guid.Parse(e.Value);

                    SaveChangesAsync();

                    GetNegotiationBidAsync();
                }
            }
            else
            {
                eSourceAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back of Get user mapping complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mManageeSourceModel_GetUserMappingComplete(object sender, eNegEntityResultArgs<UserMapping> e)
        {
            if (!e.HasError)
            {
                CurrentMappedUser = e.Results.FirstOrDefault();

                //Check if this is the first time to make linking between 
                //eNeg & eSource or it have been done before.
                if (CurrentMappedUser == null)
                {
                    CurrentMappedUser = AddUserMapping(true);

                    CurrenteSourceUser = AddeSourceUser();


                    //Change screen to request the user data that will be saved in eNeg & eSource
                    eSourceAppMessanger.ChangeScreenMessage.Send(eSourceAppViewTypes.FillDataView);
                }
                //if the mapping process have been done but no linking happened between eNeg & eSource.
                else if (CurrentMappedUser != null &&
                    (CurrentMappedUser.eSourceUserID == null ||
                    CurrentMappedUser.eSourceUserID == Guid.Empty))
                {
                    CurrenteSourceUser = AddeSourceUser();

                    //Change screen to request the user data that will be saved in eNeg & eSource
                    eSourceAppMessanger.ChangeScreenMessage.Send(eSourceAppViewTypes.FillDataView);
                }
                //if the linking process have been done before.
                else
                {
                    //Validate the saved eSourceUserID is valid in eSource or not any more. 
                    TestLoginAsync();
                }
            }
            else
            {
                eSourceAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Handles the PropertyChanged event of the mIssueHistoryModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void mManageeSourceModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsBusy"))
            {
                this.IsBusy = this.mManageeSourceModel.IsBusy;
                this.CreateBidCommand.RaiseCanExecuteChanged();
                this.CreateeSourceUserCommand.RaiseCanExecuteChanged();
                this.DownloadReportCommand.RaiseCanExecuteChanged();
                this.NavigateToeSourceCommand.RaiseCanExecuteChanged();
                this.OpenBidIneSourceCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region → Commands       .

        /// <summary>
        /// Gets the createe source user command.
        /// </summary>
        /// <value>The createe source user command.</value>
        public RelayCommand CreateeSourceUserCommand
        {
            get
            {
                if (mCreateeSourceUserCommand == null)
                {
                    mCreateeSourceUserCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            CurrenteSourceUser.ValidationErrors.Clear();

                            if (CurrenteSourceUser.TryValidateObject())
                            {
                                IsCreatingUserBusy = true;

                                CreateUserAsync(CurrenteSourceUser);
                                UpdateUserIneNegAsync(CurrenteSourceUser);
                            }

                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eSourceAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !IsCreatingUserBusy);
                }
                return mCreateeSourceUserCommand;
            }
        }

        /// <summary>
        /// Gets the create bid command.
        /// </summary>
        /// <value>The create bid command.</value>
        public RelayCommand<string> CreateBidCommand
        {
            get
            {
                if (mCreateBidCommand == null)
                {
                    mCreateBidCommand = new RelayCommand<string>((BidName) =>
                    {
                        try
                        {
                            //Case Tender: user clicked on create tender link
                            if (BidName.ToLower() == "tender")
                            {
                                CreateTenderAsync(CurrentMappedUser.eSourceUserID.ToString());
                            }
                            //Case Auction: user clicked on create auction link
                            else
                            {
                                CreateAuctionAsync(CurrentMappedUser.eSourceUserID.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eSourceAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , (BidName) => !mManageeSourceModel.IsBusy);
                }
                return mCreateBidCommand;
            }
        }

        /// <summary>
        /// Gets the navigate toe source command.
        /// </summary>
        /// <value>The navigate toe source command.</value>
        public RelayCommand NavigateToeSourceCommand
        {
            get
            {
                if (mNavigateToeSourceCommand == null)
                {
                    mNavigateToeSourceCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            NavigationPurpose = NavigationPurposeOptions.OpeneSource;
                            LoginAsync();
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eSourceAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !mManageeSourceModel.IsBusy);
                }
                return mNavigateToeSourceCommand;
            }
        }

        /// <summary>
        /// Gets the open bid ine source command.
        /// </summary>
        /// <value>The open bid ine source command.</value>
        public RelayCommand<TenderInfo> OpenBidIneSourceCommand
        {
            get
            {
                if (mOpenBidIneSourceCommand == null)
                {
                    mOpenBidIneSourceCommand = new RelayCommand<TenderInfo>((BidItem) =>
                    {
                        try
                        {
                            NavigationPurpose = NavigationPurposeOptions.OpenCreatedBid;
                            NavigationBidItem = BidItem;
                            LoginAsync();
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eSourceAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , (BidItem) => !mManageeSourceModel.IsBusy);
                }
                return mOpenBidIneSourceCommand;
            }
        }

        /// <summary>
        /// Gets the download report command.
        /// </summary>
        /// <value>The download report command.</value>
        public RelayCommand<TenderInfo> DownloadReportCommand
        {
            get
            {
                if (mDownloadReportCommand == null)
                {
                    mDownloadReportCommand = new RelayCommand<TenderInfo>((tender) =>
                    {
                        try
                        {
                            GetBidReportAsync(
                                CurrentMappedUser.eSourceUserID.ToString(), tender.bidID, tender.type);
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eSourceAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , (tender) => !mManageeSourceModel.IsBusy);
                }
                return mDownloadReportCommand;
            }
        }

        /// <summary>
        /// Gets the save download report command.
        /// </summary>
        /// <value>The save download report command.</value>
        public RelayCommand SaveDownloadReportCommand
        {
            get
            {
                if (mSaveDownloadReportCommand == null)
                {
                    mSaveDownloadReportCommand = new RelayCommand(() =>
                    {
                        try
                        {

                            SaveFileDialog dialog = new SaveFileDialog();

                            dialog.DefaultExt = GetFileExtension(lastReprot.downloadName);

                            dialog.Filter = string.Format("{1} File (*.{0}) | *.{0}", dialog.DefaultExt, dialog.DefaultExt);

                            if (!(bool)dialog.ShowDialog())
                                return;

                            Stream fileStream = dialog.OpenFile();
                            fileStream.Write(lastReprot.binaryData, 0, lastReprot.binaryData.Length);
                            fileStream.Close();

                            //Close popup
                            eSourceAppMessanger.ChangeScreenMessage.Send(eSourceAppViewTypes.ClosePopupView);

                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eSourceAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => true);
                }
                return mSaveDownloadReportCommand;
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Gets the file extension.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        private string GetFileExtension(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                Match match = Regex.Match(fileName, @"^.*\.(\w*)$");

                if (match.Groups.Count > 0)
                {
                    return match.Groups[match.Groups.Count - 1].Value;
                }
            }
            return "*";
        }

        /// <summary>
        /// Finishes the creating tender.
        /// </summary>
        /// <param name="bidID">The bid ID.</param>
        /// <param name="type">The type.</param>
        private void FinishCreatingTender(string bidID, string type)
        {
            StringBuilder url = EncryptUrl(bidID, type);

            string tenderAddress = url.ToString();

            //Navigate to certain bid in eSource
            //eNegNavigation.NavigateToUrl(url.ToString(), true);

            if (type.ToLower() == "Tender".ToLower())
            {
                type = "RFx";
            }

            SendAppsStatisticalsMessages(string.Format("New {0} Created", type), string.Format(Resources.eNegMessage, type, tenderAddress));

            var newTender = AddNegotiationBid(true, bidID);

            this.NegotiationBidSource.Add(newTender);

            GetTendersAsync(CurrentMappedUser.eSourceUserID.ToString(), new string[] { bidID });

            IsNewTenderAdded = true;

            this.SaveChangesAsync();
        }

        /// <summary>
        /// Encrypts the specified value string.
        /// </summary>
        /// <param name="valueString">The value string.</param>
        /// <returns></returns>
        private string Encrypt(string valueString)
        {
            if (meSourceServicesSetting != null)
            {
                return HttpUtility.UrlEncode(
                                        Hashing.Encrypt(
                                                 UTF8Encoding.UTF8.GetBytes(meSourceServicesSetting.EncryptionKey),
                                                 UTF8Encoding.UTF8.GetBytes(meSourceServicesSetting.EncryptionIV),
                                                 valueString)
                                       );
            }
            return string.Empty;
        }

        /// <summary>
        /// Encrypts the URL.
        /// http://eSource.com/Default.aspx?wsCall&BidID=356-356-8787-03213&Type=Tender
        /// </summary>
        /// <param name="bidID">The bid ID.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private StringBuilder EncryptUrl(string bidID, string type)
        {
            StringBuilder url = new StringBuilder();
            url.Append(this.eSourceBaseAddress + "?" + WSCallQueryParam);
            url.Append("&");
            url.Append(Encrypt(QueryParameters.BidID.ToString()) + "=" + Encrypt(bidID));
            url.Append("&");
            url.Append(Encrypt(QueryParameters.Type.ToString()) + "=" + Encrypt(type));
            return url;
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Tests the login async.
        /// </summary>
        public void TestLoginAsync()
        {
            mManageeSourceModel.TestUserLoginAsync(CurrentMappedUser.eSourceUserID.ToString());
        }

        /// <summary>
        /// Logins the async.
        /// </summary>
        public void LoginAsync()
        {
            mManageeSourceModel.LoginAsync(CurrentMappedUser.eSourceUserID.ToString());
        }

        /// <summary>
        /// Sends the apps statisticals messages.
        /// </summary>
        /// <param name="messageSubject">The message subject.</param>
        /// <param name="messageContent">Content of the message.</param>
        public void SendAppsStatisticalsMessages(string messageSubject, string messageContent)
        {
            mManageeSourceModel.SendAppsStatisticalsMessages(messageSubject, messageContent);
        }

        /// <summary>
        /// Gets the tenders async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        /// <param name="BidIDs">The bid I ds.</param>
        public void GetTendersAsync(string eSourceUserID, string[] BidIDs)
        {
            mManageeSourceModel.GetTenderAsync(eSourceUserID, BidIDs);
        }

        /// <summary>
        /// Addes the source user.
        /// </summary>
        /// <returns></returns>
        public eSourceUser AddeSourceUser()
        {
            return mManageeSourceModel.AddeSourceUser();
        }

        /// <summary>
        /// Adds the user mapping.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <returns></returns>
        public UserMapping AddUserMapping(bool SetInContext)
        {
            return mManageeSourceModel.AddUserMapping(SetInContext);
        }

        /// <summary>
        /// Gets the negotiation bid async.
        /// </summary>
        public void GetNegotiationBidAsync()
        {
            mManageeSourceModel.GetNegotiationBidAsync();
        }

        /// <summary>
        /// Adds the negotiation bid.
        /// </summary>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="BidID">The bid ID.</param>
        /// <returns></returns>
        public NegotiationBid AddNegotiationBid(bool SetInContext, string BidID)
        {
            return mManageeSourceModel.AddNegotiationBid(SetInContext, BidID);
        }

        /// <summary>
        /// gets the user mapped object from eSource DB
        /// </summary>
        public void GetUserMappingAsync()
        {
            mManageeSourceModel.GetUserMappingAsync(eSourceAppConfigurations.CurrentLoginUser.UserID);
        }

        /// <summary>
        /// Getes the source setting.
        /// </summary>
        public void GeteSourceSetting()
        {
            this.mManageeSourceModel.GeteSourceSetting();
        }

        /// <summary>
        /// Creates the user async.
        /// </summary>
        /// <param name="User">The user.</param>
        public void CreateUserAsync(eSourceUser User)
        {
            mManageeSourceModel.CreateUserAsync(User);
        }

        /// <summary>
        /// Updates the user ine neg async.
        /// </summary>
        /// <param name="User">The user.</param>
        public void UpdateUserIneNegAsync(eSourceUser User)
        {
            mManageeSourceModel.UpdateUserIneNegAsync(User);
        }

        /// <summary>
        /// Creates the tender async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        public void CreateTenderAsync(string eSourceUserID)
        {
            mManageeSourceModel.CreateTenderAsync(eSourceUserID);
        }

        /// <summary>
        /// Creates the auction async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        public void CreateAuctionAsync(string eSourceUserID)
        {
            mManageeSourceModel.CreateAuctionAsync(eSourceUserID);
        }

        /// <summary>
        /// Gets the bid report async.
        /// </summary>
        /// <param name="eSourceUserID">The e source user ID.</param>
        /// <param name="BidID">The bid ID.</param>
        /// <param name="objType">Type of the obj.</param>
        public void GetBidReportAsync(string eSourceUserID, string BidID, ObjectType objType)
        {
            mManageeSourceModel.GetBidReportAsync(eSourceUserID, BidID, objType);
        }

        /// <summary>
        /// Saves the changes async.
        /// </summary>
        public void SaveChangesAsync()
        {
            if (IsUnitTest)
                return;
            mManageeSourceModel.SaveChangesAsync();
        }

        /// <summary>
        /// Applies the changes.
        /// </summary>
        public void ApplyChanges()
        {
            RunQueueForApplyChanges = true;

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (!this.IsBusy && this.IsLoginSuccess)
                {
                    RunQueueForApplyChanges = false;

                    this.mManageeSourceModel.RejectChanges();

                    this.NegotiationBidSource = new List<NegotiationBid>();
                    this.TendersSource = new List<TenderInfo>();

                    //Get all negotiations bids available for this negotiation.
                    this.GetNegotiationBidAsync();
                }

            });

        }
        #endregion

        #endregion


    }
}
