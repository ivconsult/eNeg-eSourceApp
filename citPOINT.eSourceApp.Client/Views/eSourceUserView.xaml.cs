
#region → Usings   .
using citPOINT.eNeg.Common;
using System.Windows.Controls;
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
    /// eSource User View
    /// </summary>
    public partial class eSourceUserView : UserControl
    {
        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="eSourceUserView"/> class.
        /// </summary>
        public eSourceUserView()
        {
            InitializeComponent();

            #region → Registeration for needed messages in eNegMessenger    .

            eNegMessanger.SendCustomMessage.Register(this, OnUpdateMessage);

            #endregion
        }

        #endregion

        #region → Event handlers .

        /// <summary>
        /// Handles the KeyDown event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void UserControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                uxcmdCreateeSourceUser.Focus();
                Dispatcher.BeginInvoke(() => { uxcmdCreateeSourceUser.Command.Execute(uxcmdCreateeSourceUser.CommandParameter); });
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
