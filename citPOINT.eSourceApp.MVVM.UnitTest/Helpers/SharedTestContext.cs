
#region → Usings   .
using System;

#endregion

#region → History  .

/* 
 * Date           User            Change
 * *********************************************
 * 31.01.12       Yousra Reda           • creation
 * **********************************************
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eSourceApp.MVVM.UnitTest.Helpers
{
    /// <summary>
    /// Shared Test Context
    /// </summary>
    public static class SharedTestContext
    {

        #region → Properties     .

        /// <summary>
        /// Gets the car negotiation.
        /// </summary>
        /// <value>The car negotiation.</value>
        public static Guid CarNegotiation
        {
            get
            {
                return Guid.Parse("747D37FC-6969-4649-9B7B-DE1A0ADB08BD");
            }
        }

        /// <summary>
        /// Gets the laptop negotiation.
        /// </summary>
        /// <value>The laptop negotiation.</value>
        public static Guid LaptopNegotiation
        {
            get
            {
                return Guid.Parse("747D37FC-6969-4649-6969-DE1A0ADB08BD");
            }
        }
        

        #endregion

    }
}
