using DStutz.Apps.Services.Base.Configs;
using DStutz.Apps.Services.Base.SQL;
using DStutz.Apps.Services.Food.Files;
using DStutz.Data.Cruders.Food;
using DStutz.Data.Efcos.Food;

using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DStutz.Apps.Services.Food
{
    public interface IServiceFood
        : IServiceEFC
    {
        public void InitFromCSV();

        public ICruderFoodCategory Categories { get; }
        public ICruderFoodItem Items { get; }
        public ICruderNutrient Nutrients { get; }
        public ICruderSource Sources { get; }
    }

    public class ServiceFood
        : ServiceEFC<ServiceConfigSQLSqlite>, IServiceFood
    {
        #region Properties
        /***********************************************************/
        public ICruderFoodCategory Categories { get { return CruderCategory; } }
        public ICruderFoodItem Items { get { return CruderItem; } }
        public ICruderNutrient Nutrients { get { return CruderNutrient; } }
        public ICruderSource Sources { get { return CruderSource; } }
        #endregion

        #region Properties (cruders)
        /***********************************************************/
        private CruderFoodCategory CruderCategory { get; }
        private CruderFoodItem CruderItem { get; }
        private CruderNutrient CruderNutrient { get; }
        private CruderSource CruderSource { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        private static bool init = false; // TODO !!!

        public ServiceFood(
            IAppContext appContext)
            : base(
                  appContext.GetServiceContext("Food"),
                  init)
        {
            CruderCategory =
                new CruderFoodCategory(this);

            CruderItem =
                new CruderFoodItem(this);

            CruderNutrient =
                new CruderNutrient(this);

            CruderSource =
                new CruderSource(this);

            AppLogger.LogEntities(
                this,
                CruderCategory,
                CruderItem,
                CruderNutrient,
                CruderSource);
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
            modelBuilder
                .Entity<FoodCategoryMEE>();

            modelBuilder
                .Entity<FoodItemMEE>();

            modelBuilder
                .Entity<FoodItemFoodCategoryRel>()
                .HasKey(e => new { e.OwnerPk1, e.OrderBy });

            modelBuilder
                .Entity<NutrientMEE>();

            modelBuilder
                .Entity<NutrientDataMEE>()
                .HasKey(e => new { e.Pk1, e.OrderBy });

            modelBuilder
                .Entity<SourceMEE>();
        }

        protected override void Init()
        {
            // In Textpad make sure all files below are
            // saved with line ending 'UNIX' and encoding
            // 'UTF8' and the box 'UNICODE BOM' checked.
            var files = new string[] {
                "APP/sqlite/init/01_APP_V64_Sources.sql",
                "APP/sqlite/init/02_APP_V64_Food.sql",
                "APP/sqlite/init/03_APP_V64_Members.sql"
            };

            Init(Encoding.UTF8, files);
        }
        #endregion

        #region Methods initializing from CSV
        /***********************************************************/
        public void InitFromCSV()
        {
            if (false)
            {
                var s = new SerializerFileCSVFood(
                    "GL", @"ABC\Data\APP\backup\Data_64_GL.csv");

                var items = s.ReadAll();

                // Remove categories and items
                var s11 = Set<FoodCategoryMEE>();
                s11.RemoveRange(s11);

                var s21 = Set<FoodItemFoodCategoryRel>();
                s21.RemoveRange(s21);

                var s22 = Set<FoodItemMEE>();
                s22.RemoveRange(s22);

                var s23 = Set<NutrientDataMEE>();
                s23.RemoveRange(s23);

                SaveChanges();

                // Init categories
                var cats = new SortedSet<string>();

                foreach (var item in items)
                    foreach (var category in item.Categories)
                        cats.Add(category);

                int i = 1;

                foreach (var category in cats.ToList())
                    _ = CruderCategory.Create(
                        new FoodCategoryMEE() { Pk1 = i++, Name = category }
                    );

                // Init items
                foreach (var item in items)
                    InitItem(item);

                SaveChanges();
            }
        }

        private void InitItem(
            FoodItemCSV itemCSV)
        {
            var itemMEE = new FoodItemMEE()
            {
                Pk1 = itemCSV.Pk1,
                IdV40 = itemCSV.IdV40,
                IdSwissFIR = itemCSV.IdSwissFIR,
                Name = itemCSV.Name,
                Synonyms = itemCSV.Synonyms,
                Energy = itemCSV.Energy1,
                ReferenceUnit = itemCSV.ReferenceUnit,
                ChangedEntry = itemCSV.ChangedEntry,
            };

            itemMEE.AddDensity(itemCSV.Density);

            foreach (var category in itemCSV.CategoriesSorted)
                itemMEE.AddCategoryRel(
                    CruderCategory.ReadByNameCached(category).Result.Pk1
                );

            foreach (var nutrient in itemCSV.Nutrients.Values)
            {
                if (nutrient.SourcesParsed != null)
                    foreach (var source in nutrient.SourcesParsed)
                        if (!CruderSource.Exists(source))
                            throw new Exception($"Source {source} missing");

                itemMEE.AddNutrient(nutrient);
            }

            _ = CruderItem.Create(itemMEE, true);
        }
        #endregion
    }
}
