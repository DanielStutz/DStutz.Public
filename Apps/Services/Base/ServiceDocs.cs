using DStutz.Apps.Services.Base.Configs;
using DStutz.Data.Pocos.Documents;
using DStutz.System.Commands;
using DStutz.View.Docs;

namespace DStutz.Apps.Services.Base
{
    public interface IServiceDoc
        : IService
    {
        public IDocInfo FindDocInfo(string docId);
        public IDocInfo FindDocInfo(string docId, string docType);
    }

    public interface IServiceDocs
        : IServiceDoc
    {
        public ICollection<IDocInfo> FindDocInfos();
        public ICollection<IDocInfo> FindDocInfosByDocType(string docType);
    }

    public abstract class ServiceDocs<C>
        : Service<C>, IServiceDocs
        where C : ServiceConfig
    {
        #region Constructors
        /***********************************************************/
        protected ServiceDocs(
            ServiceContext context)
            : base(context)
        {
            AppLogger.LogStart(this);
        }

        protected ServiceDocs(
            ServiceContext context,
            C config)
            : base(
                  context,
                  config)
        {
            AppLogger.LogStart(this);
        }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public virtual IDocInfo FindDocInfo(string docId)
        {
            throw new NotImplementedException();
        }

        public virtual IDocInfo FindDocInfo(string docId, string docType)
        {
            throw new NotImplementedException();
        }

        public virtual ICollection<IDocInfo> FindDocInfos()
        {
            throw new NotImplementedException();
        }

        public virtual ICollection<IDocInfo> FindDocInfosByDocType(string docType)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public abstract class ServiceDocs
    {
        public static string Combine(string dir1, string dir2)
        {
            if (dir2 == null)
                return dir1;

            return Path.Combine(dir1, dir2);
        }

        public static Doc HandleDoc(
            Doc doc,
            CommandBrowser browser)
        {
            if (browser == null)
                doc.Save();
            else
                doc.SaveAndOpen(browser);

            return doc;
        }
    }
}
