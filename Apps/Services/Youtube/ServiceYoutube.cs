using DStutz.Apps.Services.Base.Configs;
using DStutz.Apps.Services.Base.SQL;
using DStutz.Data;
using DStutz.Data.Cruders;
using DStutz.Data.Cruders.Youtube;
using DStutz.Data.Efcos.Youtube;
using DStutz.Data.Pocos;
using DStutz.Data.Pocos.Youtube;

using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DStutz.Apps.Services.Youtube
{
    public interface IServiceYoutube
        : IService
    {
        public ICruderVideo Videos { get; }
        public ICruderWebsite Websites { get; }
        public IReaderTree<TopicPE> Topics { get; }


        public void AddChannel(string abbr, string name, string website, string person);
        public IReadOnlyList<ChannelMPE> FindChannels();

        public IReadOnlyList<PlaylistMPE> FindPlaylists();

        public void AddProducer(string abbr, string name, string website, string country);
        public IReadOnlyList<ProducerMPE> FindProducers();

        public void AddTags(IEnumerable<TagMPE> pocos);
        public IReadOnlyList<TagMPE> FindTags();
    }

    public class ServiceYoutube
        : ServiceEFC<ServiceConfigSQLMySql>, IServiceYoutube
    {
        #region Properties
        /***********************************************************/
        public ICruderVideo Videos { get { return CruderVideo; } }
        public ICruderWebsite Websites { get { return CruderWebsite; } }
        public IReaderTree<TopicPE> Topics { get { return ReaderTopic; } }
        #endregion

        #region Properties (cruders)
        /***********************************************************/
        private CruderVideo CruderVideo { get; }
        private CruderWebsite CruderWebsite { get; }
        private ReaderPocoTree<TopicMEE, TopicPE> ReaderTopic { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        private static bool init = false; // TODO !!!

        public ServiceYoutube(
            IAppContext appContext)
            : base(
                  appContext.GetServiceContext("Youtube"),
                  init)
        {
            CruderVideo =
                new CruderVideo(this);

            CruderWebsite =
                new CruderWebsite(this);

            ReaderTopic =
                new ReaderPocoTree<TopicMEE, TopicPE>(this);

            AppLogger.LogEntities(
                this,
                CruderVideo,
                CruderWebsite);
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
                .Entity<CommentMEE>()
                .HasKey(e => new { e.Pk1, e.OrderBy });

            modelBuilder
                .Entity<ProducerMEE>();

            modelBuilder
                .Entity<ProductMEE>();

            modelBuilder
                .Entity<TopicMEE>()
                .OwnsOne(e => e.Data);

            modelBuilder
                .Entity<WebsiteMEE>();

            // Channel data
            modelBuilder
                .Entity<ChannelMEE>();

            // Playlist data
            modelBuilder
                .Entity<PlaylistMEE>();

            // Video data
            modelBuilder
                .Entity<VideoMEE>();

            modelBuilder
                .Entity<VideoProductRel>()
                .HasKey(e => new { e.OwnerPk1, e.OrderBy });

            modelBuilder
                .Entity<VideoTagRel>()
                .HasKey(e => new { e.OwnerPk1, e.OrderBy });
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

        #region Methods handling channels
        /***********************************************************/
        public void AddChannel(
            string abbr,
            string name,
            string website,
            string person)
        {
            Set<ChannelMEE>().Add(new ChannelMEE()
            {
                Pk1 = Abc.GetNumber(abbr, 3),
                Abbr = abbr,
                Name = name,
                Website = website,
                Person = person,
                Identifier = "?",
            });

            SaveChanges();
        }

        public IReadOnlyList<ChannelMPE> FindChannels()
        {
            return Set<ChannelMEE>().ToList()
                .Select(e => e.Map())
                .ToList();
        }
        #endregion

        #region Methods handling playlists
        /***********************************************************/
        public IReadOnlyList<PlaylistMPE> FindPlaylists()
        {
            return Set<PlaylistMEE>().ToList()
                .Select(e => e.Map())
                .ToList();
        }
        #endregion

        #region Methods handling producers
        /***********************************************************/
        public void AddProducer(
            string abbr,
            string name,
            string website,
            string country)
        {
            Set<ProducerMEE>().Add(new ProducerMEE()
            {
                Pk1 = Abc.GetNumber(abbr, 3),
                Abbr = abbr,
                Name = name,
                Website = website,
                Country = country,
            });

            SaveChanges();
        }

        public IReadOnlyList<ProducerMPE> FindProducers()
        {
            return Set<ProducerMEE>().ToList()
                .Select(e => e.Map())
                .ToList();
        }
        #endregion

        #region Methods handling tags
        /***********************************************************/
        public void AddTags(
            IEnumerable<TagMPE> pocos)
        {
            var set = Set<TagMEE>();

            foreach (var poco in pocos)
            {
                if (poco.DE != null &&
                    set.FirstOrDefault(e => e.DE == poco.DE) == null)
                {
                    var pkBase = Abc.GetPrimaryKey(poco.DE) + 1;

                    for (long pk = pkBase; true; pk++)
                    {
                        if (set.Find(pk) == null)
                        {
                            poco.Pk1 = pk;
                            set.Add(poco.Map<TagMEE>());
                            break;
                        }
                    }
                }
            }

            SaveChanges();
        }

        public IReadOnlyList<TagMPE> FindTags()
        {
            return Set<TagMEE>().ToList()
                .Select(e => e.Map())
                .ToList();
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        private void TestVideo()
        {
            var v = new VideoMPE()
            {
                Pk1 = 1,
                Date = "",
                Title = "Testing",
                Remark = "Bemerkung",
                Identifier = "abc",
                Comments = new List<CommentMPE>()
                {
                    new CommentMPE()
                    {
                        Pk1 = 1,
                        OrderBy = 0,
                        DE = "Einleitung 0",
                    },
                    new CommentMPE()
                    {
                        Pk1 = 1,
                        OrderBy = 10,
                        DE = "Whatever...",
                    },
                },
                ChannelPk1 = 131014,
                PlaylistPk1 = 131014001,
                ProductRels = new List<RelPEAny<ProductMPE, IProduct>>()
                {
                    new RelPEAny<ProductMPE, IProduct>()
                    {
                        OwnerPk1 = 1,
                        OrderBy = 20,
                        RelatedPk1 = 2,
                        Related = new ProductMPE()
                        {
                            Pk1 = 2,
                            Type = "AA",
                            Name = "Armaflex",
                            ProducerPk1 = 102923,
                        },
                    }
                },
                TagRels = new List<RelPEAny<TagMPE, ITag>>()
                {
                    new RelPEAny<TagMPE, ITag>()
                    {
                        OwnerPk1 = 1,
                        OrderBy = 30,
                        RelatedPk1 = 3,
                        Related = new TagMPE()
                        {
                            Pk1 = 3,
                            DE = "Abc",
                        },
                    }
                },
            };

            // TODO Use CruderVideo
            //FinderVideo.Add(v.Map<VideoMEE>(), true);
        }
        #endregion
    }
}
