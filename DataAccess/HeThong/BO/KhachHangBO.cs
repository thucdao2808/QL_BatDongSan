using DataAccess.HeThong.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.HeThong.BO
{
    public class KhachHangBO
    {
        public List<KhachHang> GetDataKhachHang()
        {
            var cacheKhachHang = new DefaultCacheProvider();
            string CachekeyKhachHang = cacheKhachHang.BuildCachedKey("KhachHang", "GetDataKhachHang");
            var dataBDS = new List<KhachHang>();
            using (QL_BatDongSanEntities1 bds = new QL_BatDongSanEntities1())
            {
                dataBDS = bds.KhachHangs.ToList();
            }
            cacheKhachHang.Invalidate(CachekeyKhachHang);

            return dataBDS;
        }


        public static bool Insert(KhachHang csh)
        {

            using (var context = new QL_BatDongSanEntities1())
            {
                context.KhachHangs.Add(csh);
                return context.SaveChanges() > 0;
            }
        }


        public static bool UpdateKhachHang(KhachHang KhachHang)
        {
            var Cache_nguoiDung = new DefaultCacheProvider();
            string Cache_key_KhachHang = Cache_nguoiDung.BuildCachedKey("KhachHang", "Update");
            string sql = "UPDATE KhachHang SET MaKH = @MaKhachHang,HoTen=@hoten , SDT =@sdt,NhuCau =@nhucau ,TamGia =@Tamgia,KhuVuc=@khuvuc WHERE Id = @ID";

            using (var db = new QL_BatDongSanEntities1())
            {
                db.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("@ID", KhachHang.Id),
                    new SqlParameter("@MaKhachHang", KhachHang.MaKH),
                    new SqlParameter("@hoten", KhachHang.HoTen),
                    new SqlParameter("@sdt", KhachHang.SDT),
                    new SqlParameter("@nhucau", KhachHang.NhuCau),
                    new SqlParameter("@Tamgia", KhachHang.TamGia),
                    new SqlParameter("@khuvuc", KhachHang.KhuVuc)

                );
            }
            Cache_nguoiDung.RemoveByFirstName(Cache_key_KhachHang);
            Cache_nguoiDung.RemoveByFirstName(Cache_nguoiDung.BuildCachedKey("KhachHang", "GetKhachHang"));
            return true;
        }
        public static bool Deleted(int ID)
        {
            bool isDeleted = false;
            var Cache_KhachHangDel = new DefaultCacheProvider();
            string Cache_keyKhachHangDel = Cache_KhachHangDel.BuildCachedKey("KhachHang", "deleted");
            using (var db = new QL_BatDongSanEntities1())
            {
                string sql = "DELETE FROM  KhachHang where Id = @ID";
                int rowDel = db.Database.ExecuteSqlCommand(sql, new SqlParameter("@id", ID));

                if (rowDel > 0)
                {
                    isDeleted = true;
                    Cache_KhachHangDel.RemoveByFirstName(Cache_keyKhachHangDel);
                    Cache_KhachHangDel.RemoveByFirstName(Cache_KhachHangDel.BuildCachedKey("KhachHang", "GetDataKhachHang"));
                }
            }
            return isDeleted;
        }
    }
}
