

#region → Usings   .
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

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

namespace citPOINT.eSourceApp.Data.eSource
{
    /// <summary>
    /// Class representTender entity loaded from eSource.
    /// </summary>
    public partial class TenderInfo
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string TypeShortCut
        {
            get
            {
                if (!string.IsNullOrEmpty(this.type.ToString()))
                {
                    if (this.type == ObjectType.Tender)
                    {
                        return "R";
                    }
                    else
                    {
                        return this.type.ToString().Substring(0, 1);
                    }

                }
                return "_";
            }
        }

        /// <summary>
        /// Gets or sets the link caption.
        /// </summary>
        /// <value>The link caption.</value>
        public string LinkCaption
        {
            get
            {
                return this.ShowReport ? "Link" : "n.a.";
            }
        }

        /// <summary>
        /// Gets a value indicating whether [show report].
        /// </summary>
        /// <value><c>true</c> if [show report]; otherwise, <c>false</c>.</value>
        public bool ShowReport
        {
            get
            {
                return this.isclosed && this.published;
            }
        }




        #endregion

    }
}


