
#region → Usings   .
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using citPOINT.eSourceApp.ViewModel;
using citPOINT.eSourceApp.Common;
using citPOINT.eSourceApp.MVVM.UnitTest;
using citPOINT.eSourceApp.MVVM.UnitTest.Helpers;
using citPOINT.eSourceApp.Data.Web;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 31.01.12    M.Wahab         • creation
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
    /// <summary>
    /// Manage eSource View Model Test class
    /// </summary>
    [TestClass]
    public class ManageeSourceViewModel_Test
    {
        #region → Fields         .
        private ManageeSourceViewModel ManageeSourcevm;
        private string ErrorMessage;
        string currentScreen = null;
        #endregion

        #region → Properties     .

        /// <summary>
        /// View Model Object
        /// </summary>
        /// <value>The VM.</value>
        public ManageeSourceViewModel TheVM
        {
            get { return ManageeSourcevm; }
            set { ManageeSourcevm = value; }
        }
        #endregion

        #region → Constructors   .
        /// <summary>
        /// Initializes a new instance of the <see cref="ManageeSourceViewModel_Test"/> class.
        /// </summary>
        [TestInitialize]
        public void BuildUp()
        {
            eSourceAppConfigurations.CurrentLoginUser = new LoginUser();
            eSourceAppConfigurations.CurrentLoginUser.UserID = new Guid("C7CAD9E5-FA25-4EB9-82E6-E4D66D2D03BB");
            eSourceAppConfigurations.NegotiationIDParameter = SharedTestContext.CarNegotiation;

            TheVM = new ManageeSourceViewModel(new MockManageeSourceModel());

            #region → Registeration for needed messages in eNegMessenger

            // register for RaiseErrorMessage
            eSourceAppMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);

            eSourceAppMessanger.ChangeScreenMessage.Register(this, OnChangeScreenMessage);

            #endregion
        }
        #endregion

        #region → Methods        .

        #region → Private        .

        #region → Raise Error Message   .

        /// <summary>
        /// Raise error message if there is any layer send RaiseErrorMessage
        /// </summary>
        /// <param name="ex">exception to raise</param>
        private void OnRaiseErrorMessage(Exception ex)
        {
            if (ex != null)
            {
                if (ex.InnerException != null)
                {
                    ErrorMessage = ex.Message + "\r\n" + ex.InnerException.Message;
                }
                else
                    ErrorMessage = ex.Message;
            }
        }

        #endregion

        #region → Change Screen Message .

        /// <summary>
        /// Called when [change screen message].
        /// </summary>
        /// <param name="screenName">Name of the screen.</param>
        private void OnChangeScreenMessage(string screenName)
        {
            this.currentScreen = screenName;
        }

        #endregion

        #endregion

        #region → Public         .

        /// <summary>
        /// Tests the basics.
        /// </summary>
        [TestMethod]
        public void TestVM_Existance_HaveInstance()
        {
            Assert.IsNotNull(TheVM, "Failed to retrieve the View Model");
        }

        [TestMethod]
        public void GetNegotiationBid_WithoutCondition_ReturnCollection()
        {
            #region → Arrange .


            #endregion

            #region → Act     .

            TheVM.GetNegotiationBidAsync();

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.NegotiationBidSource != null, "No Negotiation Bid Found");

            #endregion

        }

        [TestMethod]
        public void GetUserMapping_WithoutCondition_ReturnCollection()
        {
            #region → Arrange .


            #endregion

            #region → Act     .

            TheVM.GetUserMappingAsync();

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.CurrentMappedUser != null, "No mapped user found");

            #endregion

        }

        [TestMethod]
        public void GetTenders_WithoutCondition_ReturnCollection()
        {
            #region → Arrange .

            #endregion

            #region → Act     .

            TheVM.GetTendersAsync("C7CAD9E5-FA25-4EB9-82E6-E4D66D2D03AA",
                new string[] { "C7CAD9E5-FA25-4EB9-82E6-E4D66D2D0301", "C7CAD9E5-FA25-4EB9-82E6-E4D66D2D0302",
                "C7CAD9E5-FA25-4EB9-82E6-E4D66D2D0303","C7CAD9E5-FA25-4EB9-82E6-E4D66D2D0304"});

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.TendersSource != null && TheVM.TendersSource.Count() == 4, "No Tenders Found");

            #endregion
        }

        [TestMethod]
        public void CreateTender_WithoutCondition_AddNewItemToCollection()
        {
            #region → Arrange .
            int NegotiationBidCountBefore = TheVM.NegotiationBidSource.Count;
            #endregion

            #region → Act     .
            TheVM.IsUnitTest = true;
            TheVM.CreateTenderAsync("C7CAD9E5-FA25-4EB9-82E6-E4D66D2D03AA");

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.NegotiationBidSource.Count == NegotiationBidCountBefore + 1, "No Tender has been Created");
            TheVM.IsUnitTest = false;
            #endregion
        }

        [TestMethod]
        public void CreateAuction_WithoutCondition_AddNewItemToCollection()
        {
            #region → Arrange .
            int NegotiationBidCountBefore = TheVM.NegotiationBidSource.Count;
            #endregion

            #region → Act     .
            TheVM.IsUnitTest = true;
            TheVM.CreateAuctionAsync("C7CAD9E5-FA25-4EB9-82E6-E4D66D2D03AA");

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.NegotiationBidSource.Count == NegotiationBidCountBefore + 1, "No Auction has been Created");
            TheVM.IsUnitTest = false;
            #endregion
        }

        [TestMethod]
        public void CreateUser_WithoutCondition_SeteSourCeUserID()
        {
            #region → Arrange .

            #endregion

            #region → Act     .
            TheVM.IsUnitTest = true;
            TheVM.CreateUserAsync(TheVM.CurrenteSourceUser);
            
            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.CurrentMappedUser != null && !string.IsNullOrEmpty(TheVM.CurrentMappedUser.eSourceUserID.ToString()), "No User has been Created");
            TheVM.IsUnitTest = false;
            #endregion
        }

        [TestMethod]
        public void LoginIneSource_WithoutCondition_Login()
        {
            #region → Arrange .

            #endregion

            #region → Act     .
            TheVM.IsUnitTest = true;
            TheVM.IsLoginSuccess = false;
            TheVM.LoginAsync();

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.IsLoginSuccess, "Cannot login to eSource");
            TheVM.IsUnitTest = false;
            #endregion
        }

        [TestMethod]
        public void TestLoginIneSource_WithoutCondition_TestLogin()
        {
            #region → Arrange .

            #endregion

            #region → Act     .
            TheVM.IsLoginSuccess = false;
            TheVM.IsUnitTest = true;
            TheVM.TestLoginAsync();

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.IsLoginSuccess, "Cannot test login to eSource");
            TheVM.IsUnitTest = false;
            #endregion
        }

        [TestMethod]
        public void GetBidReport_WithoutCondition_RetuenReportItem()
        {
            #region → Arrange .

            #endregion

            #region → Act     .

            TheVM.GetBidReportAsync("C7CAD9E5-FA25-4EB9-82E6-E4D66D2D03AA", "C7CAD9E5-FA25-4EB9-82E6-E4D66D2D0301", Data.eSource.ObjectType.Auction);

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.lastReprot != null, "No report has been found");

            #endregion
        }
        #endregion

        #endregion
    }
}
