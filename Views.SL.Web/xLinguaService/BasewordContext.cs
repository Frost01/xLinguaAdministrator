
namespace Views.SL.Web.xLinguaService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using Models.EF;


    // Implements application logic using the xLingua_StagingEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class BasewordContext : LinqToEntitiesDomainService<xLingua_StagingEntities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Basewords1' query.
        [Query(ResultLimit = 100)]
        public IQueryable<Baseword> GetBasewords()
        {
            return this.ObjectContext.Basewords1.OrderBy("Text");
        }

        [Query(ResultLimit = 100)]
        public IQueryable<Baseword> GetBasewordsByLanguageIdAndWordtypeId(int languageId, int wordtypeId)
        {
            return this.ObjectContext.Basewords1.Where(b => b.LanguageId == languageId && b.WordtypeId == wordtypeId).OrderBy(b=>b.Text);
        }
 
        [Query(ResultLimit = 100)]
        public IQueryable<Baseword> GetBasewordByText(string text)
        {
            return this.ObjectContext.Basewords1.Where(b => b.Text.StartsWith(text)).OrderBy(b => b.Text);
        }
    }
}


