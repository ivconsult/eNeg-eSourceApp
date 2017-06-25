#region → Usings   .
using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using citPOINT.eSourceApp.Data.Web;
using citPOINT.eNeg.Apps.Common.Interfaces;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 20.01.12     Yousra Reda       Creation
 * 20.01.12     Yousra Reda       Save current Login User
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
    /// Message App Configurations.
    /// </summary>
    public class eSourceAppConfigurations
    {
        #region → Properties     .

        #region Static

        /// <summary>
        /// Gets or sets the current login user.
        /// </summary>
        /// <value>The current login user.</value>
        public static IUserInfo CurrentLoginUser { get; set; }

        /// <summary>
        /// Gets the name of the app.
        /// </summary>
        /// <value>The name of the app.</value>
        public static string AppName { get { return "eSource App"; } }

        /// <summary>
        /// Gets or sets the negotiatio ID parameter.
        /// </summary>
        /// <value>The negotiatio ID parameter.</value>
        public static Guid? NegotiationIDParameter { get; set; }


        /// <summary>
        /// Gets or sets the main platform info.
        /// </summary>
        /// <value>The main platform info.</value>
        public static IMainPlatformInfo MainPlatformInfo { get; set; }

        /// <summary>
        /// Gets the main service URI.
        /// </summary>
        /// <value>The main service URI.</value>
        public static Uri MainServiceUri
        {
            get
            {
                if (eSourceAppConfigurations.MainPlatformInfo != null)
                {

                    var app = eSourceAppConfigurations
                                    .MainPlatformInfo
                                    .GetApplicationInfo(eSourceAppConfigurations.AppName);

                    if (app != null && !string.IsNullOrEmpty(app.ApplicationMainServicePath))
                    {
                        return new Uri(app.ApplicationMainServicePath, UriKind.Absolute);
                    }
                }

                return new Uri(string.Empty, UriKind.Absolute);
            }
        }

        /// <summary>
        /// Gets the application ID.
        /// </summary>
        /// <value>The application ID.</value>
        public static Guid ApplicationID
        {
            get
            {
                if (eSourceAppConfigurations.MainPlatformInfo != null)
                {

                    var app = eSourceAppConfigurations
                                    .MainPlatformInfo
                                    .GetApplicationInfo(eSourceAppConfigurations.AppName);

                    if (app != null)
                    {
                        return app.ApplicationID;
                    }
                }

                return Guid.Empty;
            }
        }

        #endregion

        #endregion
    }
}

