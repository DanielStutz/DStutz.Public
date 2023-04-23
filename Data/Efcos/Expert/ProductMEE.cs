using DStutz.Data.Pocos.Expert;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Expert
{
    [Table("product")]
    public class ProductMEE
        : IEfco<ProductMPE>, IProduct
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("type")]
        public string Type { get; set; }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        [Column("producer_pk1")]
        public long? ProducerPk1 { get; set; }

        [ForeignKey("ProducerPk1")]
        public ProducerMEE? Producer { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ProductMapper.New.Joiner(this); }
        }

        public ProductMPE Map()
        {
            return ProductMapper.New.Map<ProductMPE>(this);
        }
        #endregion
    }

    public class ProductMapper
        : IMapper<IProduct>
    {
        public static ProductMapper New { get; } = new ProductMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IProduct e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 80, e1.Name),
                ('L', 40, e1.Type),
                ('L', 20, e1.ProducerPk1)
            ).Add(data);
        }

        public E Map<E>(
            IProduct e1) where E : IProduct, new()
        {
            var e2 = new E()
            {
                Pk1 = e1.Pk1,
                Name = e1.Name,
                Type = e1.Type,
                ProducerPk1 = e1.ProducerPk1,
            };

            if (typeof(E) == typeof(ProductMEE))
            {
                ProductMPE poco = (ProductMPE)(object)e1;
                ProductMEE efco = (ProductMEE)(object)e2;

                efco.Producer =
                    Mapper.MapOptional(
                        poco.Producer,
                        e => e.Map<ProducerMEE>());
            }
            else if (typeof(E) == typeof(ProductMPE))
            {
                ProductMEE efco = (ProductMEE)(object)e1;
                ProductMPE poco = (ProductMPE)(object)e2;

                poco.Producer =
                    Mapper.MapOptional(
                        efco.Producer,
                        e => e.Map());
            }
            else
            {
                throw new NotImplementedException();
            }

            return e2;
        }
        #endregion
    }
}
