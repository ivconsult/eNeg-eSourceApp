
#region → Usings   .
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Web;
using System.Data.Objects.DataClasses;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 05.02.12     M.Wahab           Creation
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
    /// eSource Services Setting.
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class eSourceServicesSetting : EntityObject
    {

        #region → Properties     .

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        [DataMemberAttribute()]
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the encryption key.
        /// </summary>
        /// <value>The encryption key.</value>
        [DataMemberAttribute()]
        public string EncryptionKey { get; set; }

        /// <summary>
        /// Gets or sets the encryption IV.
        /// </summary>
        /// <value>The encryption IV.</value>
        [DataMemberAttribute()]
        public string EncryptionIV { get; set; }

        #endregion

    }
}
