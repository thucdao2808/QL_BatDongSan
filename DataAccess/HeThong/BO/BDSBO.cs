using DataAccess.HeThong.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.HeThong.BO
{
    public class BDSBO
    {
        #region Get Dữ liệu 
        public List<BDS> GetDataBDS()
        {
            var cacheBDs = new DefaultCacheProvider();
            string CachekeyBds = cacheBDs.BuildCachedKey("BDS", "GetDataBDS");
            var dataBDS = new List<BDS>();
            using (QL_BatDongSanEntities1 bds = new QL_BatDongSanEntities1())
            {
                dataBDS = bds.BDS.ToList();
            } 
            cacheBDs.Invalidate(CachekeyBds);

            return dataBDS;
        }

        public BDS GetBDSByIdDirect(int id)
        {
            using (var db = new QL_BatDongSanEntities1())
            {
                var item = db.BDS
                    .Where(gt => gt.Id == id)
                    .Select(gt => new BDS
                    {
                        Id = gt.Id,
                        MaBDS = gt.MaBDS,
                        TinhTrang = gt.TinhTrang,
                        Gia = gt.Gia,
                        DienTich = gt.DienTich,
                        HinhAnhBDS = gt.HinhAnhBDS,
                        Loai = gt.Loai,
                        DiaChi = gt.DiaChi,
                        ChuSoHuuId = gt.ChuSoHuuId
                    })
                    .FirstOrDefault();
                return item;
            }
        }

        #endregion

        #region Logic

        public static bool Insert(BDS nhadat)
        {
            
            using (var context = new QL_BatDongSanEntities1())
            {
                context.BDS.Add(nhadat);
                return context.SaveChanges() > 0;
            }
        }
        public static bool UpdateBDS(BDS BDS)
        {
            var Cache_nguoiDung = new DefaultCacheProvider();
            string Cache_key_BDS = Cache_nguoiDung.BuildCachedKey("BDS", "Update");
            string sql = "UPDATE BDS SET MaBDS = @MaBDS, Gia = @Gia, Loai = @Loai, HinhAnhBDS = @Hinhanh, DienTich = @DienTich, DiaChi = @DiaChi, ChuSoHuuId = @ChuSoHuuId, TinhTrang = @TinhTrang WHERE Id = @ID";

            using (var db = new QL_BatDongSanEntities1())
            {
                db.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("@ID", BDS.Id),
                    new SqlParameter("@MaBDS", BDS.MaBDS),
                    new SqlParameter("@Gia", BDS.Gia),
                    new SqlParameter("@DienTich", BDS.DienTich),
                    new SqlParameter("@TinhTrang", BDS.TinhTrang),
                    new SqlParameter("@Loai", BDS.Loai),
                    new SqlParameter("@Hinhanh", BDS.HinhAnhBDS),
                    new SqlParameter("@DiaChi", BDS.DiaChi),
                    new SqlParameter("@ChuSoHuuId", BDS.ChuSoHuuId)
                );
            }
            Cache_nguoiDung.RemoveByFirstName(Cache_key_BDS);
            Cache_nguoiDung.RemoveByFirstName(Cache_nguoiDung.BuildCachedKey("BDS", "GetBDS"));
            return true;
        }

        public static bool Deleted(int ID)
        {
            bool isDeleted = false;
            var Cache_BDSDel = new DefaultCacheProvider();
            string Cache_keyBDSDel = Cache_BDSDel.BuildCachedKey("BDS", "deleted");
            using (var db = new QL_BatDongSanEntities1())
            {
                string sql = "DELETE FROM  BDS where Id = @ID";
                int rowDel = db.Database.ExecuteSqlCommand(sql, new SqlParameter("@id", ID));

                if (rowDel > 0)
                {
                    isDeleted = true;
                    Cache_BDSDel.RemoveByFirstName(Cache_keyBDSDel);
                    Cache_BDSDel.RemoveByFirstName(Cache_BDSDel.BuildCachedKey("BDS", "GetBDS"));
                }
            }
            return isDeleted;

        }

        #endregion





    }
}
