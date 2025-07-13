using DataAccess.HeThong.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.HeThong.BO
{
    public class ChuSoHuuBO
    {
        public ChuSoHuuBO()
        {
            // Constructor logic can be added here if needed
        }
        public List<ChuSoHuu> GetDataChuSoHuu()
        {
            var cacheChuSoHuu = new DefaultCacheProvider();
            string CachekeyChuSoHuu = cacheChuSoHuu.BuildCachedKey("ChuSoHuu", "GetDataChuSoHuu");
            var dataChuSoHuu = new List<ChuSoHuu>();
            using (QL_BatDongSanEntities1 ChuSoHuu = new QL_BatDongSanEntities1())
            {
                dataChuSoHuu = ChuSoHuu.ChuSoHuus.ToList();
            }
            cacheChuSoHuu.Invalidate(CachekeyChuSoHuu);

            return dataChuSoHuu;
        }

        public static bool Insert(ChuSoHuu csh)
        {

            using (var context = new QL_BatDongSanEntities1())
            {
                context.ChuSoHuus.Add(csh);
                return context.SaveChanges() > 0;
            }
        }

        public static bool UpdateChuSoHuu(ChuSoHuu ChuSoHuu)
        {
            var Cache_nguoiDung = new DefaultCacheProvider();
            string Cache_key_ChuSoHuu = Cache_nguoiDung.BuildCachedKey("ChuSoHuu", "Update");
            string sql = "UPDATE ChuSoHuu SET MaChu = @MaChuSoHuu,HoTen=@hoten , SDT =@sdt,DiaChi =@dc WHERE Id = @ID";

            using (var db = new QL_BatDongSanEntities1())
            {
                db.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("@ID", ChuSoHuu.Id),
                    new SqlParameter("@MaChuSoHuu", ChuSoHuu.MaChu),
                    new SqlParameter("@hoten", ChuSoHuu.HoTen),
                    new SqlParameter("@sdt", ChuSoHuu.SDT),
                    new SqlParameter("@dc", ChuSoHuu.DiaChi)

                );
            }
            Cache_nguoiDung.RemoveByFirstName(Cache_key_ChuSoHuu);
            Cache_nguoiDung.RemoveByFirstName(Cache_nguoiDung.BuildCachedKey("ChuSoHuu", "GetChuSoHuu"));
            return true;
        }

        public static bool Deleted(int ID)
        {
            bool isDeleted = false;
            var Cache_ChuSoHuuDel = new DefaultCacheProvider();
            string Cache_keyChuSoHuuDel = Cache_ChuSoHuuDel.BuildCachedKey("ChuSoHuu", "deleted");
            using (var db = new QL_BatDongSanEntities1())
            {
                string sql = "DELETE FROM  ChuSoHuu where Id = @ID";
                int rowDel = db.Database.ExecuteSqlCommand(sql, new SqlParameter("@id", ID));

                if (rowDel > 0)
                {
                    isDeleted = true;
                    Cache_ChuSoHuuDel.RemoveByFirstName(Cache_keyChuSoHuuDel);
                    Cache_ChuSoHuuDel.RemoveByFirstName(Cache_ChuSoHuuDel.BuildCachedKey("ChuSoHuu", "GetDataChuSoHuu"));
                }
            }
            return isDeleted;

        }

    }
}
