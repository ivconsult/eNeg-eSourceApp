

#region → Usings   .
using System;


#endregion

#region → History  .

/* Date         User        Change
 * 
 * 31.01.12     M.Wahab     • creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion
namespace citPOINT.eSourceApp.Data
{
    /// <summary>
    /// eSource Args
    /// </summary>
    /// <typeparam name="T">any type</typeparam>
    public class eSourceArgs<T> : EventArgs
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public T Value { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>The error.</value>
        public Exception Error { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has error.
        /// </summary>
        /// <value><c>true</c> if this instance has error; otherwise, <c>false</c>.</value>
        public bool HasError { get { return this.Error != null; } }

        #endregion

        #region → Constructor    .
        
        /// <summary>
        /// Initializes a new instance of the <see cref="eSourceArgs&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="error">The error.</param>
        public eSourceArgs(T value,Exception error)
        {
            this.Value = value;
            this.Error = error;
        }

        #endregion
    }
}
