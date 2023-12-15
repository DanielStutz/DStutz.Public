using DStutz.Data.Efcos.Expert.Youtube;
using DStutz.Data.Pocos.Expert.Youtube;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DStutz.Data.CRUD.Expert.Youtube
{
    public interface ICruderVideo
        : ICruderBLO<VideoMPE>
    {
        public ValueTask<List<VideoMPE>> ReadManyByChannel(long pk);
        public ValueTask<List<VideoMPE>> ReadManyByPlaylist(long pk);
        //public ValueTask<List<VideoMPE>> ReadManyByProduct(string partialProduct);
        //public ValueTask<List<VideoMPE>> ReadManyByRemark(string partialRemark);
        //public ValueTask<List<VideoMPE>> ReadManyByTag(string partialTag);
    }

    public class CruderVideo
        //: CruderPoco<VideoMEE, VideoMPE, IVideo>, ICruderVideo
    {
        #region Constructors
        /***********************************************************/
        public CruderVideo(
            DbContext context)
            //: base(context)
        { }
        #endregion

        #region Methods implementing
        /***********************************************************/
        //public async ValueTask<List<VideoMPE>> ReadManyByChannel(
        //    long pk)
        //{
        //    return await ReadMany(e =>
        //        e.ChannelPk1 == pk,
        //        null);
        //}

        //public async ValueTask<List<VideoMPE>> ReadManyByPlaylist(
        //    long pk)
        //{
        //    return await ReadMany(e =>
        //        e.PlaylistPk1 == pk,
        //        null);
        //}

        //public async ValueTask<List<VideoMPE>> ReadManyByProduct(
        //    string partialProduct)
        //{
        //    return await ReadMany<VideoProductRel>(e =>
        //        e.Related != null && (
        //            e.Related.Name.Contains(partialProduct) ||
        //            e.Related.Type.Contains(partialProduct)
        //        ),
        //        CInclude.All
        //    );
        //}

        //public async ValueTask<List<VideoMPE>> ReadManyByRemark(
        //    string partialRemark)
        //{
        //    return await ReadMany(e =>
        //        e.Remark != null &&
        //        e.Remark.Contains(partialRemark),
        //        CInclude.All);
        //}

        //public async ValueTask<List<VideoMPE>> ReadManyByTag(
        //    string partialTag)
        //{
        //    return await ReadMany<VideoTagRel>(e =>
        //        e.Related != null && (
        //            (e.Related.DE != null &&
        //            e.Related.DE.Contains(partialTag)) ||
        //            (e.Related.EN != null &&
        //            e.Related.EN.Contains(partialTag)) ||
        //            (e.Related.FR != null &&
        //            e.Related.FR.Contains(partialTag))
        //        ),
        //        CInclude.All
        //    );
        //}
        #endregion

        #region Methods loading
        /***********************************************************/
        protected VideoMEE Loading(
            EntityEntry<VideoMEE> entry,
            int includeType)
        {
            //switch (includeType)
            //{
            //    case 0:
            //        entry.Reference(e => e.Channel.Author)
            //            .Load();
            //        entry.Collection(e => e.Comments)
            //            .Load();
            //        entry.Reference(e => e.Playlist).Query()
            //            .Include(p => p.Channel)
            //            .Load();
            //        entry.Collection(e => e.ProductRels).Query()
            //            .Include(r => r.Related!.Producer)
            //            .Load();
            //        entry.Collection(e => e.TagRels).Query()
            //            .Include(r => r.Related)
            //            .Load();
            //        break;
            //    default:
            //        break;
            //}

            return entry.Entity;
        }
        #endregion
    }
}
