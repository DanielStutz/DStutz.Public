﻿using DStutz.Data.Efcos.Expert.Youtube;
using DStutz.Data.Pocos.Expert.Youtube;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DStutz.Data.Cruders.Expert.Youtube
{
    public interface ICruderVideo
        : ICruder<VideoMPE>
    {
        public Task<List<VideoMPE>> ReadManyByChannel(long pk);
        public Task<List<VideoMPE>> ReadManyByPlaylist(long pk);
        public Task<List<VideoMPE>> ReadManyByProduct(string partialProduct);
        public Task<List<VideoMPE>> ReadManyByTag(string partialTag);
        public Task<List<VideoMPE>> ReadManyByRemark(string partialRemark);
    }

    public class CruderVideo
        : CruderPoco<VideoMEE, VideoMPE, IVideo>, ICruderVideo
    {
        #region Constructors
        /***********************************************************/
        public CruderVideo(
            DbContext context)
            : base(context)
        { }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public async Task<List<VideoMPE>> ReadManyByChannel(
            long pk)
        {
            return await ReadMany(e =>
                e.ChannelPk1 == pk,
                ICruder.INCLUDE_ALL);
        }

        public async Task<List<VideoMPE>> ReadManyByPlaylist(
            long pk)
        {
            return await ReadMany(e =>
                e.PlaylistPk1 == pk,
                ICruder.INCLUDE_ALL);
        }

        public async Task<List<VideoMPE>> ReadManyByProduct(
            string partialProduct)
        {
            var keys =
                FindOwnerPrimaryKeys<VideoProductRel>(e =>
                    e.Related != null && (
                        (e.Related.Name != null &&
                        e.Related.Name.Contains(partialProduct)) ||
                        (e.Related.Type != null &&
                        e.Related.Type.Contains(partialProduct))
                    )
                );

            return await ReadMany(e =>
                keys.Contains(e.Pk1),
                ICruder.INCLUDE_ALL);
        }

        public async Task<List<VideoMPE>> ReadManyByTag(
            string partialTag)
        {
            var keys =
                FindOwnerPrimaryKeys<VideoTagRel>(e =>
                    e.Related != null && (
                        (e.Related.DE != null &&
                        e.Related.DE.Contains(partialTag)) ||
                        (e.Related.EN != null &&
                        e.Related.EN.Contains(partialTag)) ||
                        (e.Related.FR != null &&
                        e.Related.FR.Contains(partialTag))
                    )
                );

            return await ReadMany(e =>
                keys.Contains(e.Pk1),
                ICruder.INCLUDE_ALL);
        }

        public async Task<List<VideoMPE>> ReadManyByRemark(
            string partialRemark)
        {
            return await ReadMany(e =>
                e.Remark != null &&
                e.Remark.Contains(partialRemark),
                ICruder.INCLUDE_ALL);
        }
        #endregion

        #region Methods loading
        /***********************************************************/
        protected override VideoMEE Loading(
            int includeType,
            EntityEntry<VideoMEE> entry)
        {
            switch (includeType)
            {
                case ICruder.INCLUDE_ALL:
                    entry.Reference(e => e.Channel)
                        .Load();
                    entry.Collection(e => e.Comments)
                        .Load();
                    entry.Reference(e => e.Playlist).Query()
                        .Include(p => p.Channel)
                        .Load();
                    entry.Collection(e => e.ProductRels).Query()
                        .Include(r => r.Related!.Producer)
                        .Load();
                    entry.Collection(e => e.TagRels).Query()
                        .Include(r => r.Related)
                        .Load();
                    break;
                default:
                    break;
            }

            return entry.Entity;
        }
        #endregion
    }
}