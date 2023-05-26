using DStutz.Data.Pocos.Food;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Food
{
    [Table("source")]
    public class SourceMEE
        : IEfco<SourceMPE>, ISource
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("name")]
        public string Name { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return SourceMapper.New.Joiner(this); }
        }

        public SourceMPE Map()
        {
            return SourceMapper.New.Map<SourceMPE>(this);
        }
        #endregion
    }

    public class SourceMapper
        : IMapper<ISource>
    {
        public static SourceMapper New { get; } = new SourceMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            ISource e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 100, e1.Name)
            ).Add(data);
        }

        public E Map<E>(
            ISource e1) where E : ISource, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                Name = e1.Name,
            };
        }
        #endregion
    }
}
