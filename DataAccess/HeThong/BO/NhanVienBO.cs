using DataAccess.HeThong.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.HeThong.BO
{
    public class NhanVienBO
    {
        public NhanVienBO() { }
        public List<Model.NhanVien> GetDataNhanVien()
        {
            var cacheNhanVien = new DefaultCacheProvider();
            string CachekeyNhanVien = cacheNhanVien.BuildCachedKey("NhanVien", "GetDataNhanVien");
            var dataBDS = new List<Model.NhanVien>();
            using (Model.QL_BatDongSanEntities1 bds = new Model.QL_BatDongSanEntities1())
            {
                dataBDS = bds.NhanViens.ToList();
            }
            cacheNhanVien.Invalidate(CachekeyNhanVien);

            return dataBDS;
        }

        public static bool Insert(NhanVien csh)
        {

            using (var context = new QL_BatDongSanEntities1())
            {
                context.NhanViens.Add(csh);
                return context.SaveChanges() > 0;
            }
        }

        public static bool UpdateNhanVien(NhanVien NhanVien)
        {
            var Cache_nguoiDung = new DefaultCacheProvider();
            string Cache_key_NhanVien = Cache_nguoiDung.BuildCachedKey("NhanVien", "Update");
            string sql = "UPDATE NhanVien SET MaNV = @MaNhanVien,HoTen=@hoten , SDT =@sdt,PhongBan =@pb WHERE Id = @ID";

            using (var db = new QL_BatDongSanEntities1())
            {
                db.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("@ID", NhanVien.Id),
                    new SqlParameter("@MaNhanVien", NhanVien.MaNV),
                    new SqlParameter("@hoten", NhanVien.HoTen),
                    new SqlParameter("@sdt", NhanVien.SDT),
                    new SqlParameter("@pb", NhanVien.PhongBan)

                );
            }
            Cache_nguoiDung.RemoveByFirstName(Cache_key_NhanVien);
            Cache_nguoiDung.RemoveByFirstName(Cache_nguoiDung.BuildCachedKey("NhanVien", "GetNhanVien"));
            return true;
        }

        public static bool Deleted(int ID)
        {
            bool isDeleted = false;
            var Cache_NhanVienDel = new DefaultCacheProvider();
            string Cache_keyNhanVienDel = Cache_NhanVienDel.BuildCachedKey("NhanVien", "deleted");
            using (var db = new QL_BatDongSanEntities1())
            {
                string sql = "DELETE FROM  NhanVien where Id = @ID";
                int rowDel = db.Database.ExecuteSqlCommand(sql, new SqlParameter("@id", ID));

                if (rowDel > 0)
                {
                    isDeleted = true;
                    Cache_NhanVienDel.RemoveByFirstName(Cache_keyNhanVienDel);
                    Cache_NhanVienDel.RemoveByFirstName(Cache_NhanVienDel.BuildCachedKey("NhanVien", "GetDataNhanVien"));
                }
            }
            return isDeleted;

        }
    }
}
