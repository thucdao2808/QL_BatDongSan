//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.HeThong.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class TaiKhoanDangNhap
    {
        public int Id { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string VaiTro { get; set; }
        public Nullable<int> NhanVienId { get; set; }
    
        public virtual NhanVien NhanVien { get; set; }
    }
}
