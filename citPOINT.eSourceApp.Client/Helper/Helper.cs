using System;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using System.Windows.Controls;
using citPOINT.eNeg.Apps.Common;
using citPOINT.eNeg.Apps.Common.Enums;
using citPOINT.eNeg.Apps.Common.Interfaces;
using citPOINT.eNeg.Common.eNegApps;
using citPOINT.eSourceApp.Common;
using citPOINT.eSourceApp.Data.Web;
using citPOINT.eSourceApp.Model;
using citPOINT.eSourceApp.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace citPOINT.eSourceApp.Client
{
    /// <summary>
    /// Helper just to help in test a lone without eNeg plat form
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is actives.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is actives; otherwise, <c>false</c>.
        /// </value>
        public static bool IsActives { get; set; }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public static void Run()
        {
            MainPlatformInfo.Instance.CurrentNegotiation = new XNegotiation() { NegotiationID = Guid.Parse("1399864A-2334-4F09-8243-E7CADADA02F7"), NegotiationName = "Any Negotiation" };

            MainPlatformInfo.Instance.CurrentConversation = new XConverstaion() { ConversationID = Guid.Parse("46E89FCB-97FB-4DE7-B5CB-11BC8AA3CC31"), ConversationName = "Any Conv" };

            MainPlatformInfo.Instance.IsRunningOutOfBrowser = false;

            MainPlatformInfo.Instance.UserInfo = new XUserInfo()
            {
                UserID = Guid.Parse("A93ADDE4-A72D-41B1-9467-FF195E9BE283"),
                EmailAddress = "Zoovar@Yahoo.Com",
                FirstName = "eNeg",
                LastName = "2010",
                Gender=false
            };


            eSourceAppConfigurations.MainPlatformInfo = MainPlatformInfo.Instance;

            eSourceAppConfigurations.MainPlatformInfo.HostRegionSizeDetails = new eNeg.Apps.Common.ApplicationHostBounds() { Left = -10, Top = 30 };

            if (eSourceAppConfigurations.MainPlatformInfo.CurrentNegotiation != null)
            {
                eSourceAppConfigurations.NegotiationIDParameter = eSourceAppConfigurations.MainPlatformInfo.CurrentNegotiation.NegotiationID;
            }
            else
            {
                eSourceAppConfigurations.NegotiationIDParameter = null;
            }

            eSourceAppConfigurations.CurrentLoginUser = eSourceAppConfigurations.MainPlatformInfo.UserInfo;

            //if (eSourceAppConfigurations.MainPlatformInfo.CurrentConversation != null)
            //{
            //    eSourceAppConfigurations.ConversationIDParameter = eSourceAppConfigurations.MainPlatformInfo.CurrentConversation.ConversationID;
            //}
            //else
            //{
            //    eSourceAppConfigurations.ConversationIDParameter = null;
            //}

            //eSourceAppConfigurations.ActionTypeParameter = eSourceAppConfigurations.ActionTypes.Report.ToString();


            eSourceAppConfigurations.MainPlatformInfo.eNegApplicationList = new System.Collections.Generic.List<IeNegApplication>()
            {
                new XeNegApplication(){
                     ApplicationID=Guid.Parse("67cb25f2-acbd-4b2b-9917-ba5ea62150fe"),
                     ApplicationTitle="eSource App",
                     ApplicationMainServicePath="http://localhost:9007/citPOINT-eSourceApp-Data-Web-eSourceAppService.svc",
                     ApplicationBaseAddress="http://localhost:9007/",
                     ApplicationRank=3
                }
            };


            eSourceAppMessanger.RaiseErrorMessage.Register(l, OnRaiseErrorMessage);

            eSourceAppMessanger.ConfirmMessage.Register(l, OnConfirmMessage);


            MainPlatformInfo.Instance.HostRegionSizeDetails
                        = new ApplicationHostBounds()
                        {
                            Height = App.Current.Host.Content.ActualHeight,
                            Left = 0,
                            Top = 0,
                            Width = App.Current.Host.Content.ActualWidth
                        };

            eSourceAppConfigurations.MainPlatformInfo.CurrentPlatform = PlatformTypes.MainPlatform;

            IsActives = true;

            Helper.IntializeContainer();
        }

        static ContentControl l = new ContentControl();

        /// <summary>
        /// Raise error message if there is any layer send RaiseErrorMessage
        /// </summary>
        /// <param name="ex">exception to raise</param>
        private static void OnRaiseErrorMessage(Exception ex)
        {
            MessageBox.Show("Pops"+ex.Message + Environment.NewLine + ex.StackTrace);

            // eSourceAppConfigurations.MainPlatformInfo.HandleException.HandleException(ex, eSourceAppConfigurations.AppName);

            //ExceptionHandlingResult exceptionHandlingResult = ExceptionManager.Instance.HandleException(ex, "Policy1");
            //ClientExceptionHandlerProvider.ShowMessageErrorWindow(exceptionHandlingResult.Message, ex);
        }

        /// <summary>
        /// Display Confirmation Message and resent back the result choosen
        /// </summary>
        /// <param name="dialogMessage">dialogMessage</param>
        private static void OnConfirmMessage(DialogMessage dialogMessage)
        {
            if (dialogMessage != null)
            {
                MessageBoxResult result = MessageBox.Show(dialogMessage.Content,
                    dialogMessage.Caption, dialogMessage.Button);

                dialogMessage.ProcessCallback(result);
            }
        }

        /// <summary>
        /// Intializes the container.
        /// </summary>
        private static void IntializeContainer()
        {  //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();

            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(App).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(eSourceAppConfigurations).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ManageeSourceViewModel).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ManageeSourceModel).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(LoginUser).Assembly));

            //Create the CompositionContainer with the parts in the catalog
            eSourceAppModule.Container = new CompositionContainer(catalog);
        }

        /// <summary>
        /// Changes this instance.
        /// </summary>
        public static void Change()
        {
            MainPlatformInfo.Instance.CurrentNegotiation = new XNegotiation() { NegotiationID = Guid.Parse("1399864A-2334-4F09-8243-E7CADADA02F7"), NegotiationName = "Any Negotiation" };

            MainPlatformInfo.Instance.CurrentConversation = new XConverstaion() { ConversationID = Guid.Parse("46E89FCB-97FB-4DE7-B5CB-11BC8AA3CC31"), ConversationName = "Any Conv" };

            IsActives = true;

            MainPlatformInfo.Instance.ApplyChanges();

            // IsActives = false;
        }

        /// <summary>
        /// Change1s this instance.
        /// </summary>
        public static void Change1()
        {

            MainPlatformInfo.Instance.CurrentNegotiation = new XNegotiation() { NegotiationID = Guid.Parse("9068BD62-7800-4CAD-BC14-4F20E6EE0384"), NegotiationName = "Any Negotiation" };

            MainPlatformInfo.Instance.CurrentConversation = null;// new XConverstaion() { ConversationID = Guid.Parse("d40c734a-eae2-4758-a592-1f6e35def23d"), ConversationName = "Any Conv" };

            IsActives = true;

            MainPlatformInfo.Instance.ApplyChanges();

            //IsActives = false;

            //new XConverstaion() { ConversationID = Guid.Parse("46E89FCB-97FB-4DE7-B5CB-11BC8AA3CC31"), ConversationName = "Any Conv" };

        }
    }

    #region → Helper Class .

    /// <summary>
    /// X Converstaion
    /// </summary>
    public class XConverstaion : IConversation
    {
        /// <summary>
        /// Gets or sets the conversation ID.
        /// </summary>
        /// <value>The conversation ID.</value>
        public Guid ConversationID { get; set; }

        /// <summary>
        /// Gets or sets the name of the conversation.
        /// </summary>
        /// <value>The name of the conversation.</value>
        public string ConversationName { get; set; }
    }

    /// <summary>
    /// X Negotiation
    /// </summary>
    public class XNegotiation : INegotiation
    {
        /// <summary>
        /// Gets or sets the negotiation ID.
        /// </summary>
        /// <value>The negotiation ID.</value>
        public Guid NegotiationID { get; set; }

        /// <summary>
        /// Gets or sets the name of the negotiation.
        /// </summary>
        /// <value>The name of the negotiation.</value>
        public string NegotiationName { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is closed.
        /// </summary>
        /// <value><c>true</c> if this instance is closed; otherwise, <c>false</c>.</value>
        public bool IsClosed
        {
            get { return false; }
        }
    }

    /// <summary>
    /// X eNeg Application
    /// </summary>
    public class XeNegApplication : IeNegApplication
    {
        /// <summary>
        /// Gets or sets the application base address.
        /// </summary>
        /// <value>The application base address.</value>
        public string ApplicationBaseAddress { get; set; }

        /// <summary>
        /// Gets or sets the application ID.
        /// </summary>
        /// <value>The application ID.</value>
        public Guid ApplicationID { get; set; }

        /// <summary>
        /// Gets or sets the application main service path.
        /// </summary>
        /// <value>The application main service path.</value>
        public string ApplicationMainServicePath { get; set; }

        /// <summary>
        /// Gets or sets the application rank.
        /// </summary>
        /// <value>The application rank.</value>
        public int ApplicationRank { get; set; }

        /// <summary>
        /// Gets or sets the application title.
        /// </summary>
        /// <value>The application title.</value>
        public string ApplicationTitle { get; set; }

        /// <summary>
        /// Gets or sets the name of the assembly.
        /// </summary>
        /// <value>The name of the assembly.</value>
        public string AssemblyName { get; set; }

        /// <summary>
        /// Gets or sets the downloading percentage.
        /// </summary>
        /// <value>The downloading percentage.</value>
        public int DownloadingPercentage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is static app.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is static app; otherwise, <c>false</c>.
        /// </value>
        public bool IsStaticApp { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <value>The name of the module.</value>
        public string ModuleName { get; set; }

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the name of the region.
        /// </summary>
        /// <value>The name of the region.</value>
        public string RegionName { get; set; }

        /// <summary>
        /// Gets a value indicating whether [show download progress].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show download progress]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowDownloadProgress
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets or sets the xap file path.
        /// </summary>
        /// <value>The xap file path.</value>
        public string XapFilePath { get; set; }
        
        /// <summary>
        /// Gets or sets the default view.
        /// </summary>
        /// <value>The default view.</value>
        public ViewsTypes DefaultView{ get; set; }
       
    }

    /// <summary>
    /// X User Info
    /// </summary>
    public class XUserInfo : IUserInfo
    {
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the country ID.
        /// </summary>
        /// <value>The country ID.</value>
        public Guid? CountryID { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>The create date.</value>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the culture ID.
        /// </summary>
        /// <value>The culture ID.</value>
        public int? CultureID { get; set; }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The email address.</value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        public bool? Gender { get; set; }

        /// <summary>
        /// Gets or sets the has public profile.
        /// </summary>
        /// <value>The has public profile.</value>
        public bool? HasPublicProfile { get; set; }

        /// <summary>
        /// Gets or sets the LCID.
        /// </summary>
        /// <value>The LCID.</value>
        public int? LCID { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        /// <value>The mobile.</value>
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the profile description.
        /// </summary>
        /// <value>The profile description.</value>
        public string ProfileDescription { get; set; }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        /// <value>The user ID.</value>
        public Guid UserID { get; set; }
    }

    #endregion

}
