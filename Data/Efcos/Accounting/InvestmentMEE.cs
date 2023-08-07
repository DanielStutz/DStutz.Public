using DStutz.Data.DAO.Logistics;
using DStutz.Data.Pocos.Accounting;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Accounting
{
    [Table("investment")]
    public class InvestmentMEE
        : IEfco<InvestmentMPE>, IInvestment
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public int Pk1 { get; set; } // Account number

        [Column("order_by"), Key]
        public int OrderBy { get; set; }

        [Column("date_1")]
        public DateTime Date1 { get; set; } // First payment

        [Column("date_2")]
        public DateTime? Date2 { get; set; } // Last payment

        [Column("cre_name")]
        public string CreditorName { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        [Column("cre_add_pk1")]
        public long? CreditorAddressPk1 { get; set; }

        [ForeignKey("CreditorAddressPk1")]
        public AddressDAO? CreditorAddress { get; set; }

        [Column("cre_con_pk1")]
        public long? CreditorContactPk1 { get; set; }

        [ForeignKey("CreditorContactPk1")]
        public ContactDAO? CreditorContact { get; set; }

        [Column("deb_add_pk1")]
        public long? DebitorAddressPk1 { get; set; }

        [ForeignKey("DebitorAddressPk1")]
        public AddressDAO? DebitorAddress { get; set; }

        [Column("deb_con_pk1")]
        public long? DebitorContactPk1 { get; set; }

        [ForeignKey("DebitorContactPk1")]
        public ContactDAO? DebitorContact { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return InvestmentMapper.New.Joiner(this); }
        }

        public InvestmentMPE Map()
        {
            return InvestmentMapper.New.Map<InvestmentMPE>(this);
        }
        #endregion
    }

    public class InvestmentMapper
        : IMapper<IInvestment>
    {
        public static InvestmentMapper New { get; } = new InvestmentMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IInvestment e1,
            params IJoinableOld?[] data)
        {
            // Asymmetric code
            var date1 = e1.Date1.ToShortDateString();
            var date2 = "";

            if (e1.Date2 != null)
                date2 = ((DateTime)e1.Date2).ToShortDateString();

            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 4, e1.Pk1),
                ('R', 2, e1.OrderBy),
                ('R', 10, date1),
                ('R', 10, date2),
                ('L', 20, e1.CreditorName),
                ('L', 40, e1.Remark),
                ('R', 3, e1.CreditorAddressPk1),
                ('R', 3, e1.CreditorContactPk1),
                ('R', 3, e1.DebitorAddressPk1),
                ('R', 3, e1.DebitorContactPk1)
            ).Add(data);
        }

        public E Map<E>(
            IInvestment e1) where E : IInvestment, new()
        {
            var e2 = new E()
            {
                Pk1 = e1.Pk1,
                OrderBy = e1.OrderBy,
                Date1 = e1.Date1,
                Date2 = e1.Date2,
                CreditorName = e1.CreditorName,
                Remark = e1.Remark,
                CreditorAddressPk1 = e1.CreditorAddressPk1,
                CreditorContactPk1 = e1.CreditorContactPk1,
                DebitorAddressPk1 = e1.DebitorAddressPk1,
                DebitorContactPk1 = e1.DebitorContactPk1,
            };

            if (typeof(E) == typeof(InvestmentMEE))
            {
                InvestmentMPE poco = (InvestmentMPE)(object)e1;
                InvestmentMEE efco = (InvestmentMEE)(object)e2;

                //efco.CreditorAddress =
                //    Mapper.MapOptional(
                //        poco.CreditorAddress,
                //        e => e.Map<AddressDAO>());

                //efco.CreditorContact =
                //    Mapper.MapOptional(
                //        poco.CreditorContact,
                //        e => e.Map<ContactDAO>());

                //efco.DebitorAddress =
                //    Mapper.MapOptional(
                //        poco.DebitorAddress,
                //        e => e.Map<AddressDAO>());

                //efco.DebitorContact =
                //    Mapper.MapOptional(
                //        poco.DebitorContact,
                //        e => e.Map<ContactDAO>());
            }
            else if (typeof(E) == typeof(InvestmentMPE))
            {
                InvestmentMEE efco = (InvestmentMEE)(object)e1;
                InvestmentMPE poco = (InvestmentMPE)(object)e2;

                //poco.CreditorAddress =
                //    Mapper.MapOptional(
                //        efco.CreditorAddress,
                //        e => e.Map());

                //poco.CreditorContact =
                //    Mapper.MapOptional(
                //        efco.CreditorContact,
                //        e => e.Map());

                //poco.DebitorAddress =
                //    Mapper.MapOptional(
                //        efco.DebitorAddress,
                //        e => e.Map());

                //poco.DebitorContact =
                //    Mapper.MapOptional(
                //        efco.DebitorContact,
                //        e => e.Map());
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
