#region → Usings   .
using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using citPOINT.eNeg.Apps.Common.Interfaces;
using citPOINT.eSourceApp.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 20.01.12     Yousra Reda         • creation
 * **********************************************
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eSourceApp.Client
{
    /// <summary>
    /// UI responsible for eSource App
    /// </summary>
    [Export]
    public partial class MainPageView : UserControl, ICleanup, IObserverApp
    {

        #region → Properties     .

        /// <summary>
        /// Gets or sets the view model repository.
        /// </summary>
        /// <value>The view model repository.</value>
        private ViewModelRepository ViewModelRepository { get; set; }

        /// <summary>
        /// Gets the name of the app.
        /// </summary>
        /// <value>The name of the app.</value>
        public string AppName
        {
            get { return eSourceAppConfigurations.AppName; }
        }

        private Guid? LastNegotiationID { get; set; }

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageView"/> class.
        /// </summary>
        public MainPageView()
        {
            InitializeComponent();

            this.LastNegotiationID = null;

            #region → Registration for needed messages in eSource App Messanger .

            eSourceAppMessanger.ChangeScreenMessage.Register(this, OnChangeScreen);
            eSourceAppMessanger.ConfirmMessage.Register(this, OnConfirmMessage);
            eSourceAppMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);

            #endregion

            try
            {
                this.ApplyChanges(false);

                eSourceAppConfigurations.MainPlatformInfo.TrackChanges.AddObserverApp(this);
            }
            catch (Exception ex)
            {
                eSourceAppConfigurations.MainPlatformInfo.HandleException.HandleException(ex, eSourceAppConfigurations.AppName);
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Raise error message if there is any layer send RaiseErrorMessage
        /// </summary>
        /// <param name="ex"></param>
        private void OnRaiseErrorMessage(Exception ex)
        {
            eSourceAppConfigurations.MainPlatformInfo.HandleException.HandleException(ex, eSourceAppConfigurations.AppName);
        }

        /// <summary>
        /// Called when [change screen].
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        private void OnChangeScreen(string pageName)
        {

            this.uxgrdLoading.Visibility = System.Windows.Visibility.Collapsed;

            switch (pageName)
            {
                case eSourceAppViewTypes.FillDataView:
                    this.uxMainContent.Content = new eSourceUserView();
                    break;

                case eSourceAppViewTypes.BidsView:
                    this.uxMainContent.Content = new BidsView();
                    break;


                case eSourceAppViewTypes.ServiceAlarmView:
                    this.uxMainContent.Content = new AlarmView();
                    break;

                case eSourceAppViewTypes.SaveReportView:
                    {
                        var saveReportView = new SaveReportView();
                        var sendMailWindow = new PopUpWindow("Save Report")
                                                 {
                                                     DataContext = this.DataContext,
                                                     Content = saveReportView
                                                 };
                        sendMailWindow.ShowDialog();
                        break;
                    }
            }
        }

        /// <summary>
        /// Display Confirmation Message and resent back the result choosen 
        /// </summary>
        /// <param name="dialogMessage">dialogMessage</param>
        private void OnConfirmMessage(DialogMessage dialogMessage)
        {
            if (dialogMessage != null)
            {
                MessageBoxResult result = MessageBox.Show(dialogMessage.Content,
                    dialogMessage.Caption, dialogMessage.Button);
                dialogMessage.ProcessCallback(result);
            }
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Applies the changes.
        /// </summary>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        public void ApplyChanges(bool isActive)
        {
            if (isActive || Helper.IsActives)
            {
                #region → Change Negotiation      .

                if (eSourceAppConfigurations.MainPlatformInfo.CurrentNegotiation != null)
                {
                    eSourceAppConfigurations.NegotiationIDParameter = eSourceAppConfigurations.MainPlatformInfo.CurrentNegotiation.NegotiationID;
                }
                else
                {
                    eSourceAppConfigurations.NegotiationIDParameter = Guid.Empty;
                }

                #endregion

                #region → Change Conversation     .

                //if (eSourceAppConfigurations.MainPlatformInfo.CurrentConversation != null)
                //{
                //    eSourceAppConfigurations.ConversationIDParameter = eSourceAppConfigurations.MainPlatformInfo.CurrentConversation.ConversationID;
                //}
                //else
                //{
                //    eSourceAppConfigurations.ConversationIDParameter = Guid.Empty;
                //}

                #endregion

                #region → Change User             .

                if (eSourceAppConfigurations.CurrentLoginUser != null && eSourceAppConfigurations.CurrentLoginUser.UserID != eSourceAppConfigurations.MainPlatformInfo.UserInfo.UserID)
                {
                    if (this.ViewModelRepository != null)
                    {
                        this.ViewModelRepository.Cleanup();

                        this.ViewModelRepository = null;
                    }
                }

                eSourceAppConfigurations.CurrentLoginUser = eSourceAppConfigurations.MainPlatformInfo.UserInfo;

                #endregion

                #region → View Model Repository   .

                if (ViewModelRepository != null && this.LastNegotiationID != eSourceAppConfigurations.NegotiationIDParameter)
                {
                    this.uxgrdLoading.Visibility = System.Windows.Visibility.Visible;

                    ViewModelRepository.ManageeSourceViewModel.ApplyChanges();
                }
                else if (ViewModelRepository == null)
                {
                    this.uxgrdLoading.Visibility = System.Windows.Visibility.Visible;

                    ViewModelRepository = new ViewModelRepository();
                }

                this.LastNegotiationID = eSourceAppConfigurations.NegotiationIDParameter;

                this.DataContext = ViewModelRepository.ManageeSourceViewModel;

                #endregion

                #region → Adjust Widht and Heihgt .

                this.uxMainContent.Width = eSourceAppConfigurations.MainPlatformInfo.HostRegionSizeDetails.Width;
                this.uxMainContent.MinWidth = this.uxMainContent.Width;
                this.uxMainContent.MaxWidth = this.uxMainContent.Width;

                this.uxMainContent.Height = eSourceAppConfigurations.MainPlatformInfo.HostRegionSizeDetails.Height;
                this.uxMainContent.MinHeight = this.uxMainContent.Height;
                this.uxMainContent.MaxHeight = this.uxMainContent.Height;

                #endregion

            }
            else
            {
                this.uxgrdLoading.Visibility = System.Windows.Visibility.Visible;
            }
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            Messenger.Default.Unregister(this);
        }

        #endregion  Public

      
        #endregion
    }
}