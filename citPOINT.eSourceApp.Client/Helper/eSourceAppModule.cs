#region → Usings   .

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using citPOINT.eSourceApp.Common;
using citPOINT.eSourceApp.Data.Web;
using citPOINT.eSourceApp.ViewModel;
using citPOINT.eNeg.Apps.Common.Interfaces;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using citPOINT.eSourceApp.Model;
using citPOINT.eSourceApp.Data.eSource;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 21.03.12     M.Wahab       Creation
 */

# endregion History

#region → ToDos    .
/*
 * Date         set by User     Description
 * 
 * 
*/
# endregion ToDos

namespace citPOINT.eSourceApp.Client
{
    /// <summary>
    /// Preference App Module.
    /// </summary>
    [ModuleExport(typeof(eSourceAppModule))]
    public class eSourceAppModule : IModule
    {
        #region → Fields         .

        private readonly IRegionManager regionManager;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>The container.</value>
        public static CompositionContainer Container { get; set; }

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="eSourceAppModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="MainPlatformInfo">The main platform info.</param>
        [ImportingConstructor()]
        public eSourceAppModule(IRegionManager regionManager, IMainPlatformInfo MainPlatformInfo)
        {
            this.regionManager = regionManager;

            eSourceAppConfigurations.MainPlatformInfo = MainPlatformInfo;

            //eSourceAppConfigurations.ActionTypeParameter = eSourceAppConfigurations.ActionTypes.Report.ToString();

            this.IntializeContainer();
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Intializes the container.
        /// </summary>
        private void IntializeContainer()
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();

            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(App).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(eSourceAppConfigurations).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ManageeSourceViewModel).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ManageeSourceModel).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(LoginUser).Assembly));

            //catalog.Catalogs.Add(new AssemblyCatalog(typeof(PreferenceSetNeg).Assembly));

            //Create the CompositionContainer with the parts in the catalog
            Container = new CompositionContainer(catalog);
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            try
            {
                regionManager.RegisterViewWithRegion
                    (eSourceAppConfigurations.AppName.Replace(" ", "") + "Region",
                     typeof(MainPageView));
            }
            catch (System.Exception ex)
            {
                eSourceAppConfigurations.MainPlatformInfo.HandleException.HandleException(ex, eSourceAppConfigurations.AppName);
            }

        }

        #endregion

        #endregion

    }
}
