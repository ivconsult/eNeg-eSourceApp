

#region → Usings   .

using System.ServiceModel.DomainServices.Client;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System;

#endregion

#region → History  .

/* Date         User        Change
 * 
 * 25.01.12     M.Wahab     • creation
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
    /// Class represent eSourceUser entity loaded from eSource.
    /// </summary>
    public partial class eSourceUser : Entity
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [Required]
        public string UserName
        {
            get { return mUserName; }
            set
            {
                mUserName = value;
                this.RaisePropertyChanged("UserName");
            }
        }

        public string mUserName;
        private string mPassword;
        private string mEmail;
        private string mFirstName;
        private string mLastName;
        private string mCompany;
        private bool mGender;

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [Required]
        public string Password
        {
            get { return mPassword; }
            set
            {
                mPassword = value;
                this.RaisePropertyChanged("Password");
                this.RaiseDataMemberChanging("Password");
            }
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [DataMember()]
        [Required()]
        public string Email
        {
            get { return mEmail; }
            set
            {
                mEmail = value;
                this.RaisePropertyChanged("Email");
            }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [DataMember()]
        [Required()]
        public string FirstName
        {
            get { return mFirstName; }
            set
            {
                mFirstName = value;
                this.RaisePropertyChanged("FirstName");
            }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [DataMember()]
        [Required()]
        public string LastName
        {
            get { return mLastName; }
            set
            {
                mLastName = value;
                this.RaisePropertyChanged("LastName");
            }
        }

        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        /// <value>The company.</value>
        [Required(AllowEmptyStrings = false)]
        public string Company
        {
            get { return mCompany; }
            set
            {
                mCompany = value;
                this.RaisePropertyChanged("Company");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="eSourceUser"/> is gender.
        /// </summary>
        /// <value><c>true</c> if gender; otherwise, <c>false</c>.</value>
        public bool Gender
        {
            get { return mGender; }
            set
            {
                mGender = value;
                this.RaisePropertyChanged("Gender");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is male.
        /// </summary>
        /// <value><c>true</c> if this instance is male; otherwise, <c>false</c>.</value>
        public bool IsMale { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is female.
        /// </summary>
        /// <value><c>true</c> if this instance is female; otherwise, <c>false</c>.</value>
        public bool IsFemale { get; set; }

        #endregion

        #region → Methods        .

        /// <summary>
        /// Try validate for the NegotiationStrategySettings class
        /// </summary>
        /// <returns>True Or False </returns>
        public bool TryValidateObject()
        {
            ValidationContext context = new ValidationContext(this, null, null);
            var validationResults = new Collection<ValidationResult>();

            if (Validator.TryValidateObject(this, context, validationResults, false) == false)
            {
                foreach (ValidationResult error in validationResults)
                {
                    this.ValidationErrors.Add(error);
                }
            }

            IsValidEmail();

            return this.ValidationErrors.Count == 0;
        }

        /// <summary>    
        /// Try Try Validate by Property name  
        /// </summary> 
        /// <returns>True Or False </returns> 
        public bool TryValidateProperty(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return true;
                //throw new ArgumentNullException("propertyName");
            }
            if (propertyName == "UserName"
             || propertyName == "Password"
             || propertyName == "Email"
             || propertyName == "FirstName"
             || propertyName == "LastName"
             || propertyName == "Company"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "UserName")
                    return Validator.TryValidateProperty(this.UserName, context, validationResults);
                if (propertyName == "Password")
                    return Validator.TryValidateProperty(this.Password, context, validationResults);
                if (propertyName == "Email")
                    return Validator.TryValidateProperty(this.Email, context, validationResults);
                if (propertyName == "FirstName")
                    return Validator.TryValidateProperty(this.FirstName, context, validationResults);
                if (propertyName == "LastName")
                    return Validator.TryValidateProperty(this.LastName, context, validationResults);
                if (propertyName == "Company")
                    return Validator.TryValidateProperty(this.Company, context, validationResults);


            }
            return true;
        }



        /// <summary>
        /// Determines whether [is valid email] [the specified email address].
        /// </summary>
        /// <param name="EmailAddress">The email address.</param>
        /// <returns>
        /// 	<c>true</c> if [is valid email] [the specified email address]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValidEmail(string EmailAddress)
        {
            return Regex.IsMatch(EmailAddress,
                 @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }


        /// <summary>
        /// Determines whether [is valid email].
        /// </summary>
        private void IsValidEmail()
        {
            // user Email can be null
            if (string.IsNullOrEmpty(Email))
                return;

            // Return true if strIn is in valid e-mail format.
            if (!IsValidEmail(Email))
                this.ValidationErrors.Add(new ValidationResult(ErrorResource.ValidationErrorInvalidEmail, new string[] { "Email" }));
        }
        #endregion Methods

    }
}
