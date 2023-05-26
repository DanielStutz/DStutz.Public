using DStutz.Apps.Services.Base.Configs;
using DStutz.Apps.Services.Base.SQL;
using DStutz.Data.Cruders.Expert;
using DStutz.Data.Cruders.Expert.Notes;
using DStutz.Data.Cruders.Expert.Websites;
using DStutz.Data.DAO.Expert;
using DStutz.Data.DAO.Expert.Notes;
using DStutz.Data.DAO.Expert.Websites;

using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DStutz.Apps.Services.Expert
{
    public interface IServiceExpert
        : IService
    {
        public ICruderAuthor Authors { get; }
        public ICruderArticle Articles { get; }
        public ICruderNote Notes { get; }
        public ICruderSeries Series { get; }
        //public ICruderVideo Videos { get; }
        //public IReaderTree<TopicPE> Topics { get; }

        //public void AddChannel(string abbr, string name, string website, string person);
        //public IReadOnlyList<ChannelMPE> FindChannels();

        //public IReadOnlyList<PlaylistMPE> FindPlaylists();

        //public void AddProducer(string abbr, string name, string website, string country);
        //public IReadOnlyList<ProducerMPE> FindProducers();

        //public void AddTags(IEnumerable<TagMPE> pocos);
        //public IReadOnlyList<TagMPE> FindTags();
    }

    public class ServiceExpert
        : ServiceEFC<ServiceConfigSQLMySql>, IServiceExpert
    {
        #region Properties
        /***********************************************************/
        public ICruderAuthor Authors { get { return CruderAuthor; } }
        public ICruderArticle Articles { get { return CruderArticle; } }
        public ICruderNote Notes { get { return CruderNote; } }
        public ICruderSeries Series { get { return CruderSeries; } }
        //public ICruderVideo Videos { get { return CruderVideo; } }
        //public IReaderTree<TopicPE> Topics { get { return ReaderTopic; } }
        #endregion

        #region Properties (cruders)
        /***********************************************************/
        private CruderAuthor CruderAuthor { get; }
        private CruderArticle CruderArticle { get; }
        private CruderNote CruderNote { get; }
        private CruderSeries CruderSeries { get; }
        //private CruderVideo CruderVideo { get; }
        //private ReaderPocoTree<TopicMEE, TopicPE> ReaderTopic { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        private static bool init = false; // TODO !!!

        public ServiceExpert(
            IAppContext appContext)
            : base(
                  appContext.GetServiceContext("Expert"),
                  init)
        {
            CruderAuthor =
                new CruderAuthor(this);

            CruderArticle =
                new CruderArticle(this);

            CruderNote =
                new CruderNote(this);

            CruderSeries =
                new CruderSeries(this);

            // TODO Factory ?!
            CruderArticle.c = CruderAuthor;
            CruderNote.c = CruderAuthor;
            CruderSeries.c = CruderAuthor;

            //CruderVideo =
            //    new CruderVideo(this);

            //ReaderTopic =
            //    new ReaderPocoTree<TopicMEE, TopicPE>(this);

            AppLogger.LogEntities(
                this,
                CruderAuthor,
                CruderSeries,
                CruderArticle,
                CruderNote);
        }
        #endregion

        #region Methods - Config / Init (executed in this order)
        /***********************************************************/
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            // General data
            modelBuilder
                .Entity<AuthorDAO>();

            //modelBuilder
            //    .Entity<ProducerMEE>();

            //modelBuilder
            //    .Entity<ProductMEE>();

            //modelBuilder
            //    .Entity<TopicMEE>()
            //    .OwnsOne(e => e.Data);

            // Website article data
            modelBuilder
                .Entity<SeriesDAO>();

            modelBuilder
                .Entity<ArticleDAO>();

            //modelBuilder
            //    .Entity<ArticleComment>()
            //    .HasKey(e => new { e.Pk1, e.OrderBy });

            //modelBuilder
            //    .Entity<ArticleProductRel>()
            //    .HasKey(e => new { e.OwnerPk1, e.OrderBy });

            //modelBuilder
            //    .Entity<ArticleTagRel>()
            //    .HasKey(e => new { e.OwnerPk1, e.OrderBy });

            // Notes data
            modelBuilder
                .Entity<NoteDAO>();

            // Youtube video data
            //modelBuilder
            //    .Entity<ChannelMEE>();

            //modelBuilder
            //    .Entity<PlaylistMEE>();

            //modelBuilder
            //    .Entity<VideoMEE>();

            //modelBuilder
            //    .Entity<VideoComment>()
            //    .HasKey(e => new { e.Pk1, e.OrderBy });

            //modelBuilder
            //    .Entity<VideoProductRel>()
            //    .HasKey(e => new { e.OwnerPk1, e.OrderBy });

            //modelBuilder
            //    .Entity<VideoTagRel>()
            //    .HasKey(e => new { e.OwnerPk1, e.OrderBy });
        }

        protected override void Init()
        {
            // In Textpad make sure all files below are
            // saved with line ending 'UNIX' and encoding
            // 'UTF8' and the box 'UNICODE BOM' checked.
            var files = new string[] {
                //"APP/sqlite/init/01_APP_Whatever.sql"
            };

            Init(Encoding.UTF8, files);
        }
        #endregion

        #region Methods testing whatever
        /***********************************************************/
        public override void Test()
        {
            //AddChannel("FRS", "Freundship", "https://freundship.de", "?");
            //AddProducer("Ford", "https://www.ford.com/", "US");
            //AddProducer("Iveco", "https://www.iveco.com", "IT");
        }
        #endregion

        #region Methods handling general data
        /***********************************************************/
        #endregion

        //#region Methods handling channels
        ///***********************************************************/
        //public void AddChannel(
        //    string abbr,
        //    string name,
        //    string website,
        //    string person)
        //{
        //    Set<ChannelMEE>().Add(new ChannelMEE()
        //    {
        //        Pk1 = PK.Assign6D(abbr),
        //        Name = name,
        //        Href = website,
        //        Identifier = "?",
        //    });

        //    SaveChanges();
        //}

        //public IReadOnlyList<ChannelMPE> FindChannels()
        //{
        //    return Set<ChannelMEE>().ToList()
        //        .Select(e => e.Map())
        //        .ToList();
        //}
        //#endregion

        //#region Methods handling playlists
        ///***********************************************************/
        //public IReadOnlyList<PlaylistMPE> FindPlaylists()
        //{
        //    return Set<PlaylistMEE>().ToList()
        //        .Select(e => e.Map())
        //        .ToList();
        //}
        //#endregion

        //#region Methods handling producers
        ///***********************************************************/
        //public void AddProducer(
        //    string abbr,
        //    string name,
        //    string href,
        //    string country)
        //{
        //    Set<ProducerMEE>().Add(new ProducerMEE()
        //    {
        //        Pk1 = PK.Assign6D(abbr),
        //        Abbr = abbr,
        //        Name = name,
        //        Href = href,
        //        Country = country,
        //    });

        //    SaveChanges();
        //}

        //public IReadOnlyList<ProducerMPE> FindProducers()
        //{
        //    return Set<ProducerMEE>().ToList()
        //        .Select(e => e.Map())
        //        .ToList();
        //}
        //#endregion

        //#region Methods handling tags
        ///***********************************************************/
        //public void AddTags(
        //    IEnumerable<TagMPE> pocos)
        //{
        //    var set = Set<TagMEE>();

        //    foreach (var poco in pocos)
        //    {
        //        if (poco.DE != null &&
        //            set.FirstOrDefault(e => e.DE == poco.DE) == null)
        //        {
        //            var pkBase = PK.Assign14D5Z(poco.DE) + 1;

        //            for (long pk = pkBase; true; pk++)
        //            {
        //                if (set.Find(pk) == null)
        //                {
        //                    poco.Pk1 = pk;
        //                    set.Add(poco.Map<TagMEE>());
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    SaveChanges();
        //}

        //public IReadOnlyList<TagMPE> FindTags()
        //{
        //    return Set<TagMEE>().ToList()
        //        .Select(e => e.Map())
        //        .ToList();
        //}
        //#endregion

        //#region Miscellaneous
        ///***********************************************************/
        //private void TestVideo()
        //{
        //    var v = new VideoMPE()
        //    {
        //        Pk1 = 1,
        //        Date = "",
        //        Title = "Testing",
        //        Remark = "Bemerkung",
        //        Identifier = "abc",
        //        Comments = new List<CommentMPE>()
        //        {
        //            new CommentMPE()
        //            {
        //                Pk1 = 1,
        //                OrderBy = 0,
        //                DE = "Einleitung 0",
        //            },
        //            new CommentMPE()
        //            {
        //                Pk1 = 1,
        //                OrderBy = 10,
        //                DE = "Whatever...",
        //            },
        //        },
        //        ChannelPk1 = 131014,
        //        PlaylistPk1 = 131014001,
        //        ProductRels = new List<RelPEAny<ProductMPE, IProduct>>()
        //        {
        //            new RelPEAny<ProductMPE, IProduct>()
        //            {
        //                OwnerPk1 = 1,
        //                OrderBy = 20,
        //                RelatedPk1 = 2,
        //                Related = new ProductMPE()
        //                {
        //                    Pk1 = 2,
        //                    Type = "AA",
        //                    Name = "Armaflex",
        //                    ProducerPk1 = 102923,
        //                },
        //            }
        //        },
        //        TagRels = new List<RelPEAny<TagMPE, ITag>>()
        //        {
        //            new RelPEAny<TagMPE, ITag>()
        //            {
        //                OwnerPk1 = 1,
        //                OrderBy = 30,
        //                RelatedPk1 = 3,
        //                Related = new TagMPE()
        //                {
        //                    Pk1 = 3,
        //                    DE = "Abc",
        //                },
        //            }
        //        },
        //    };

        //    // TODO Use CruderVideo
        //    //FinderVideo.Add(v.Map<VideoMEE>(), true);
        //}
        //#endregion
    }
}
