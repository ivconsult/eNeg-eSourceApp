

#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using citPOINT.eSourceApp.Data.Web.ServiceReference1;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Configuration;
using System.ServiceModel.DomainServices.Server;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 23.01.12     Yousra Reda       Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eSourceApp.Data.Web
{
    /// <summary>
    /// Provide additional eSource App services wrapping some eNeg services 
    /// and original eSoucrce service.
    /// </summary>
    public partial class eSourceAppService
    {
        #region → Fields         .

        eNegServiceSoapClient mLoader;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the loader.
        /// </summary>
        /// <value>The loader.</value>
        public eNegServiceSoapClient Loader
        {
            get
            {
                if (mLoader == null)
                {
                    mLoader = new eNegServiceSoapClient();
                    InjectCredentials();
                }
                return mLoader;
            }
        }

        #endregion Properties

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Injects the credentials into message header.
        /// </summary>
        private void InjectCredentials()
        {
            OperationContextScope scope = new OperationContextScope((IContextChannel)Loader.InnerChannel);

            MessageHeaders messageHeadersElement = OperationContext.Current.OutgoingMessageHeaders;
            messageHeadersElement.Add(MessageHeader.CreateHeader("username", "http://tempori.org", ConfigurationManager.AppSettings["username"]));
            messageHeadersElement.Add(MessageHeader.CreateHeader("password", "http://tempori.org", ConfigurationManager.AppSettings["password"]));
        }

        #endregion

        #region → Public         .

        #region → Normal Services       .

        /// <summary>
        /// Gets the negotiation bids.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <returns></returns>
        public IQueryable<NegotiationBid> GetNegotiationBidsForNegotiation(Guid negotiationID)
        {
            return this.ObjectContext
                       .NegotiationBids
                       .Where(s => s.NegotiationID == negotiationID &&
                                   s.Deleted == false)
                       .AsQueryable();
        }

        /// <summary>
        /// Gets the user mappings.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns></returns>
        public IQueryable<UserMapping> GetUserMappingsForUser(Guid userID)
        {
            return this.ObjectContext
                       .UserMappings
                       .Where(s => s.eNegUserID == userID &&
                                   s.Deleted == false)
                       .AsQueryable();
        }

        /// <summary>
        /// Sends the apps statisticals messages.
        /// </summary>
        /// <param name="appName">Name of the app.</param>
        /// <param name="userID">The user ID.</param>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="messageContent">Content of the message.</param>
        /// <param name="messageSubject">The message subject.</param>
        /// <param name="messageSender">The message sender.</param>
        /// <param name="messageReceiver">The message receiver.</param>
        /// <returns></returns>
        public bool SendAppsStatisticalsMessages(string appName, Guid userID, Guid negotiationID, string messageContent, string messageSubject, string messageSender, string messageReceiver)
        {
            if (ServiceAuthentication.IsValid())
            {
                //get All conversation in that negotiation
                Conversation[] negConversations = Loader.GetConversationsByNegotiationID(new List<Guid>() { negotiationID }.ToArray()).RootResults;
                if (negConversations != null)
                {
                    foreach (var conv in negConversations)
                    {
                        if (!Loader.UpdateAppsStatisticalsByMessages(appName,
                                                                     userID,
                                                                     conv.ConversationID,
                                                                     messageContent,
                                                                     messageSubject,
                                                                     messageSender,
                                                                     messageReceiver))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Updates the user ine neg.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="fName">Name of the f.</param>
        /// <param name="lName">Name of the l.</param>
        /// <param name="gender">if set to <c>true</c> [gender].</param>
        /// <param name="companyName">Name of the company.</param>
        /// <returns></returns>
        public bool UpdateUserIneNeg(Guid userID, string fName, string lName, bool gender, string companyName)
        {
            if (ServiceAuthentication.IsValid())
            {
                return Loader.UpdateUserFromeSource(userID, fName, lName, gender, companyName);
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the encryption settings.
        /// </summary>
        /// <returns></returns>
        public eSourceServicesSetting GetEncryptionSettings()
        {
            if (ServiceAuthentication.IsValid())
            {
                eSourceServicesSetting lst = new eSourceServicesSetting();

                lst.ID = 1;

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["EncryptionKey"]))
                {
                    lst.EncryptionKey = ConfigurationManager.AppSettings["EncryptionKey"].ToString();
                }

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["EncryptionIV"]))
                {
                    lst.EncryptionIV = ConfigurationManager.AppSettings["EncryptionIV"].ToString();
                }

                return lst;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Getes the source service URL.
        /// </summary>
        /// <returns></returns>
        [Invoke]
        public string GeteSourceServiceUrl()
        {
            if (ServiceAuthentication.IsValid())
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["eSourceServiceUrl"]))
                {
                    return ConfigurationManager.AppSettings["eSourceServiceUrl"].ToString();
                }

                return string.Empty;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }


        #endregion

        #endregion

        #endregion
    }
}
