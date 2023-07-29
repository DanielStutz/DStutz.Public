namespace DStutz.Data.CRUD
{
    public partial class CruderPoco<E, P, I>
    {
        #region Methods reading many entities (sql statement)
        /***********************************************************/
        public async Task<List<PO>> ReadMany<EO, PO, IO>(
            string searchFor,
            string ISOCode639 = "de")
        where EO : class, IO, IEfco<PO>, new()
        where PO : class, IO, IPoco<IO>
        {
            var efcos = await FindMany<EO>(searchFor, ISOCode639);

            return efcos.Select(e => e.Map()).ToList();
        }
        #endregion
    }
}
