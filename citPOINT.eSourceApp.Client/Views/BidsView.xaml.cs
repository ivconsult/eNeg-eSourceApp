
#region → Usings   .
using System;
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using citPOINT.eSourceApp.Data.eSource;
using citPOINT.eSourceApp.ViewModel;
using citPOINT.eSourceApp.Common;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 29.01.12     M.Wahab           Creation
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
    /// Bids View.
    /// </summary>
    public partial class BidsView : UserControl
    {

        #region → Properties     .

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public ManageeSourceViewModel ViewModel
        {
            get
            {
                return DataContext as ManageeSourceViewModel;
            }
        }

        #endregion
        
        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="BidsView"/> class.
        /// </summary>
        public BidsView()
        {
            InitializeComponent();

            #region → Registeration for needed messages in eNegMessenger    .

            eNegMessanger.SendCustomMessage.Register(this, OnUpdateMessage);

            #endregion
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the Click event of the uxlnkDownloadReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void uxlnkDownloadReport_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            TenderInfo tender = null;

            if ((sender as HyperlinkButton) != null && (sender as HyperlinkButton).CommandParameter != null)
            {
                tender = ((sender as HyperlinkButton).CommandParameter as TenderInfo);
            }

            if (tender != null)
            {
                this.ViewModel.DownloadReportCommand.Execute(tender);
            }

 
        }
        
        /// <summary>
        /// Handles the Click event of the uxlnkOpenTender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void uxlnkOpenTender_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TenderInfo tender = null;

            if ((sender as HyperlinkButton) != null && (sender as HyperlinkButton).CommandParameter != null)
            {
                tender = ((sender as HyperlinkButton).CommandParameter as TenderInfo);
            }

            if (tender != null)
            {
                this.ViewModel.OpenBidIneSourceCommand.Execute(tender);
            }

        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [update message].
        /// </summary>
        /// <param name="Message">The message.</param>
        private void OnUpdateMessage(eNegMessage Message)
        {
            if (Message.ReceiverApplicationID == eSourceAppConfigurations.ApplicationID)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    uxSPSucessMessage.MessageText = Message.Message;
                    uxSPSucessMessage.Completed = Message.ShowMessageCompleted;
                    uxSPSucessMessage.Show();
                });
            }
        }

        #endregion
        #endregion

    }
}
