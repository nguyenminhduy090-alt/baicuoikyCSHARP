using LibraryManagement.WinForms.DAL;
using Npgsql;
using System.Data;

namespace LibraryManagement.WinForms.Services;

public class LookupService
{
    public DataTable GetCategories()
        => Db.Query("select danh_muc_id, ten from danh_muc where dang_hoat_dong = true order by ten;");

    public DataTable GetReaders()
        => Db.Query("select ban_doc_id, ho_ten || ' - ' || coalesce(ma_so,'') as display_name from ban_doc where dang_hoat_dong = true order by ho_ten;");

    public DataTable GetAvailableCopies()
        => Db.Query(@"
select bs.ban_sao_id,
       bs.ma_ban_sao || ' | ' || s.tieu_de as display_name
from ban_sao_sach bs
join sach s on s.sach_id = bs.sach_id
where bs.trang_thai = 'SAN_SANG'
order by s.tieu_de, bs.ma_ban_sao;");
}
