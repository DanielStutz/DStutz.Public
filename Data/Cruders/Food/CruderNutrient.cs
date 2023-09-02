﻿using DStutz.Data.Efcos.Food;
using DStutz.Data.Pocos.Food;

using Microsoft.EntityFrameworkCore;

namespace DStutz.Data.CRUD.Food
{
    public interface ICruderNutrient
        : ICruderBLO<INutrientMPE>
    {
        public Task<INutrientMPE> ReadByName(string name);
        //public Task<List<INutrientMPE>> ReadManyByName(string partialName);
    }

    public class CruderNutrient
        : CruderPoco<NutrientMEE, INutrientMPE, INutrient>, ICruderNutrient
    {
        #region Constructors
        /***********************************************************/
        public CruderNutrient(
            DbContext context)
            : base(context)
        { }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public async Task<INutrientMPE> ReadByName(
            string name)
        {
            return await ReadFirstOrThrow(e =>
                e.DE.Equals(name),
                CInclude.All);
        }

        //public async Task<List<INutrientMPE>> ReadManyByName(
        //    string partialName)
        //{
        //    return await ReadMany(e =>
        //        e.DE.Contains(partialName),
        //        CInclude.All);
        //}
        #endregion
    }
}
