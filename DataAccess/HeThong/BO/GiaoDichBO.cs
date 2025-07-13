using DataAccess.HeThong.Model;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.HeThong.BO
{
    public class GiaoDichBO
    {
        public GiaoDichBO() { }
        public List<GiaoDich> GetDataGiaoDich()
        {
            var cacheGiaoDich = new DefaultCacheProvider();
            string CachekeyGiaoDich = cacheGiaoDich.BuildCachedKey("GiaoDich", "GetDataGiaoDich");
            var dataBDS = new List<GiaoDich>();
            using (QL_BatDongSanEntities1 bds = new QL_BatDongSanEntities1())
            {
                dataBDS = bds.GiaoDiches.ToList();
            }
            cacheGiaoDich.Invalidate(CachekeyGiaoDich);

            return dataBDS;
        }

        public static bool Insert(GiaoDich giaoDich)
        {
            using (QL_BatDongSanEntities1 context = new QL_BatDongSanEntities1())
            {
                context.GiaoDiches.Add(giaoDich);
                return context.SaveChanges() > 0;
            }
        }
        public static bool Update(GiaoDich giaoDich)
        {
            using (QL_BatDongSanEntities1 context = new QL_BatDongSanEntities1())
            {
                var Cache_GiaoDich = new DefaultCacheProvider();
                string Cache_key_GiaoDich = Cache_GiaoDich.BuildCachedKey("GiaoDich", "Update");
                string sql = "UPDATE GiaoDich SET MaGD = @MaGiaoDich,BDSId=@bdsId ,KhachHangId =@khId,NgayGD =@Ngay,NhanVienId=@nhanvienId, LoaiGD = @LoaiGd ,GiaTri =@Giatri,GhiChu = @ghichu WHERE Id = @ID";

                using (var db = new QL_BatDongSanEntities1())
                {
                    db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@ID", giaoDich.Id),
                        new SqlParameter("@MaGiaoDich", giaoDich.MaGD),
                        new SqlParameter("@bdsId", giaoDich.BDSId),
                        new SqlParameter("@khId", giaoDich.KhachHangId),
                        new SqlParameter("@nhanvienId", giaoDich.NhanVienId),
                        new SqlParameter("@Ngay", giaoDich.NgayGD),
                        new SqlParameter("@LoaiGd", giaoDich.LoaiGD),
                        new SqlParameter("@Giatri", giaoDich.GiaTri),
                        new SqlParameter("@ghichu", giaoDich.GhiChu)
                    );
                }
                Cache_GiaoDich.RemoveByFirstName(Cache_key_GiaoDich);
                Cache_GiaoDich.RemoveByFirstName(Cache_GiaoDich.BuildCachedKey("GiaoDich", "GetGiaoDich"));
                return true;
            }
        }
        public static bool Delete(int id)
        {
            using (QL_BatDongSanEntities1 context = new QL_BatDongSanEntities1())
            {
                bool isDeleted = false;
                var Cache_GiaoDichDel = new DefaultCacheProvider();
                string Cache_keyGiaoDichDel = Cache_GiaoDichDel.BuildCachedKey("GiaoDich", "deleted");
                using (var db = new QL_BatDongSanEntities1())
                {
                    string sql = "DELETE FROM  GiaoDich where Id = @ID";
                    int rowDel = db.Database.ExecuteSqlCommand(sql, new SqlParameter("@id",id));

                    if (rowDel > 0)
                    {
                        isDeleted = true;
                        Cache_GiaoDichDel.RemoveByFirstName(Cache_keyGiaoDichDel);
                        Cache_GiaoDichDel.RemoveByFirstName(Cache_GiaoDichDel.BuildCachedKey("GiaoDich", "GetDataGiaoDich"));                      
                    }
                }
                return isDeleted;
            }
        }
    }
}
