
#region → Usings   .
using System.ComponentModel.Composition;
using GalaSoft.MvvmLight;
using citPOINT.eSourceApp.ViewModel;
using citPOINT.eSourceApp.Common;
#endregion

#region → History  .

/* Date         User          Change
 * 
 * 05.04.12    M.Wahab         Creation
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
    /// View Model Repository.
    /// Shared View Models forcing that all view models are intialized.
    /// </summary>
    public class ViewModelRepository : ICleanup
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the preference sets view model.
        /// </summary>
        /// <value>The preference sets view model.</value>
        [Import(eSourceAppViewModelTypes.ManageeSourceViewModel)]
        public ManageeSourceViewModel ManageeSourceViewModel { get; set; }

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelRepository"/> class.
        /// </summary>
        [ImportingConstructor]
        public ViewModelRepository()
        {
            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                eSourceAppModule.Container.SatisfyImportsOnce(this);

            }
        }

        #endregion

        #region → Methods        .

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            this.ManageeSourceViewModel.Cleanup();

            //Repository.Cleanup();
        }

        #endregion
    }
}
