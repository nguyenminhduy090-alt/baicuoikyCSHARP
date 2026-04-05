using LibraryManagement.WinForms;
using LibraryManagement.WinForms.DAL;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Text.RegularExpressions;

namespace LibraryManagement.WinForms.Services;

public class LibraryService
{
    public DataTable GetDashboardSummary()
    {
        const string sql = @"
select
    (select count(*) from sach) as tong_sach,
    (select count(*) from ban_sao_sach) as tong_ban_sao,
    (select count(*) from ban_doc where dang_hoat_dong = true) as tong_ban_doc,
    (select count(*) from phieu_muon where trang_thai in ('DANG_MUON','QUA_HAN')) as phieu_dang_mo,
    (select count(*) from phieu_muon where trang_thai = 'QUA_HAN') as phieu_qua_han,
    (select coalesce(sum(so_tien),0) from tien_phat where da_thanh_toan = false) as tong_tien_phat_chua_thu;";
        return Db.Query(sql);
    }

    public DataTable GetCategories(string keyword = "")
    {
        const string sql = @"
select danh_muc_id as ""ID"", ten as ""Tên danh mục"", coalesce(mo_ta,'') as ""Mô tả"", dang_hoat_dong as ""Hoạt động"", tao_luc as ""Tạo lúc""
from danh_muc
where @kw = '' or lower(ten) like lower(@pattern)
order by danh_muc_id desc;";
        return Db.Query(sql,
            new NpgsqlParameter("@kw", keyword ?? ""),
            new NpgsqlParameter("@pattern", $"%{keyword}%"));
    }

    public void AddCategory(string name, string description)
    {
        Db.Execute("insert into danh_muc(ten, mo_ta) values(@ten, @mota);",
            new NpgsqlParameter("@ten", name),
            new NpgsqlParameter("@mota", string.IsNullOrWhiteSpace(description) ? DBNull.Value : description));
    }

    public void UpdateCategory(int id, string name, string description, bool active)
    {
        Db.Execute("update danh_muc set ten=@ten, mo_ta=@mota, dang_hoat_dong=@active where danh_muc_id=@id;",
            new NpgsqlParameter("@ten", name),
            new NpgsqlParameter("@mota", string.IsNullOrWhiteSpace(description) ? DBNull.Value : description),
            new NpgsqlParameter("@active", active),
            new NpgsqlParameter("@id", id));
    }

    public void DeleteCategory(int id)
    {
        Db.Execute("delete from danh_muc where danh_muc_id=@id;", new NpgsqlParameter("@id", id));
    }

    public DataTable GetReaders(string keyword = "")
    {
        const string sql = @"
select bd.ban_doc_id as ""ID"",
       bd.ho_ten as ""Họ tên"",
       bd.ma_so as ""Mã số"",
       bd.loai_ban_doc as ""Loại"",
       bd.gioi_tinh as ""Giới tính"",
       bd.ngay_sinh as ""Ngày sinh"",
       bd.email as ""Email"",
       bd.sdt as ""SĐT"",
       bd.dia_chi as ""Địa chỉ"",
       bd.dang_hoat_dong as ""Hoạt động"",
       coalesce(v.so_sach_dang_muon,0) as ""Đang mượn""
from ban_doc bd
left join vw_ban_doc_muon_hien_tai v on v.ban_doc_id = bd.ban_doc_id
where @kw = ''
   or lower(bd.ho_ten) like lower(@pattern)
   or coalesce(bd.sdt,'') like @pattern
   or lower(coalesce(bd.email,'')) like lower(@pattern)
order by bd.ban_doc_id desc;";
        return Db.Query(sql,
            new NpgsqlParameter("@kw", keyword ?? ""),
            new NpgsqlParameter("@pattern", $"%{keyword}%"));
    }

    private static bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        return Regex.IsMatch(email.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    private static bool IsValidPhone(string? phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return false;

        return Regex.IsMatch(phone.Trim(), @"^\d{10,11}$");
    }

    private static void ValidateReaderData(
    string hoTen,
    string maSo,
    string loai,
    string gioiTinh,
    string email,
    string sdt,
    string diaChi)
    {
        if (string.IsNullOrWhiteSpace(hoTen))
            throw new Exception("Họ tên không được để trống.");

        if (string.IsNullOrWhiteSpace(maSo))
            throw new Exception("Mã số không được để trống.");

        if (string.IsNullOrWhiteSpace(loai))
            throw new Exception("Loại bạn đọc không được để trống.");

        if (string.IsNullOrWhiteSpace(gioiTinh))
            throw new Exception("Giới tính không được để trống.");

        if (string.IsNullOrWhiteSpace(email))
            throw new Exception("Email không được để trống.");

        if (!IsValidEmail(email))
            throw new Exception("Email không đúng định dạng.");

        if (string.IsNullOrWhiteSpace(sdt))
            throw new Exception("Số điện thoại không được để trống.");

        if (!IsValidPhone(sdt))
            throw new Exception("Số điện thoại phải gồm 10 đến 11 chữ số.");

        if (string.IsNullOrWhiteSpace(diaChi))
            throw new Exception("Địa chỉ không được để trống.");
    }

    public void AddReader(string hoTen, string maSo, string loai, string gioiTinh,
    DateTime? ngaySinh, string email, string sdt, string diaChi, bool active)
    {
        hoTen = hoTen?.Trim() ?? "";
        maSo = maSo?.Trim() ?? "";
        loai = loai?.Trim() ?? "";
        gioiTinh = gioiTinh?.Trim() ?? "";
        email = email?.Trim() ?? "";
        sdt = sdt?.Trim() ?? "";
        diaChi = diaChi?.Trim() ?? "";

        ValidateReaderData(hoTen, maSo, loai, gioiTinh, email, sdt, diaChi);

        using var conn = new NpgsqlConnection(AppSettings.ConnectionString);
        conn.Open();

        using (var checkCmd = new NpgsqlCommand(@"
select 
    sum(case when ma_so = @maSo then 1 else 0 end) as trung_ma_so,
    sum(case when coalesce(email,'') = @email then 1 else 0 end) as trung_email,
    sum(case when coalesce(sdt,'') = @sdt then 1 else 0 end) as trung_sdt
from ban_doc;", conn))
        {
            checkCmd.Parameters.AddWithValue("@maSo", maSo);
            checkCmd.Parameters.AddWithValue("@email", email);
            checkCmd.Parameters.AddWithValue("@sdt", sdt);

            using var rd = checkCmd.ExecuteReader();
            if (rd.Read())
            {
                var trungMaSo = rd.IsDBNull(0) ? 0 : Convert.ToInt32(rd.GetValue(0));
                var trungEmail = rd.IsDBNull(1) ? 0 : Convert.ToInt32(rd.GetValue(1));
                var trungSdt = rd.IsDBNull(2) ? 0 : Convert.ToInt32(rd.GetValue(2));

                if (trungMaSo > 0) throw new Exception("Mã số bạn đọc đã tồn tại.");
                if (trungEmail > 0) throw new Exception("Email đã tồn tại.");
                if (trungSdt > 0) throw new Exception("Số điện thoại đã tồn tại.");
            }
        }

        const string sql = @"
insert into ban_doc(ho_ten, ma_so, loai_ban_doc, gioi_tinh, ngay_sinh, email, sdt, dia_chi, dang_hoat_dong)
values(@ho_ten, @ma_so, @loai, @gioi_tinh, @ngay_sinh, @email, @sdt, @dia_chi, @active);";

        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ho_ten", hoTen);
        cmd.Parameters.AddWithValue("@ma_so", maSo);
        cmd.Parameters.AddWithValue("@loai", loai);
        cmd.Parameters.AddWithValue("@gioi_tinh", gioiTinh);
        cmd.Parameters.AddWithValue("@ngay_sinh", (object?)ngaySinh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@sdt", sdt);
        cmd.Parameters.AddWithValue("@dia_chi", diaChi);
        cmd.Parameters.AddWithValue("@active", active);
        cmd.ExecuteNonQuery();
    }

    public void UpdateReader(int id, string hoTen, string maSo, string loai, string gioiTinh,
    DateTime? ngaySinh, string email, string sdt, string diaChi, bool active)
    {
        hoTen = hoTen?.Trim() ?? "";
        maSo = maSo?.Trim() ?? "";
        loai = loai?.Trim() ?? "";
        gioiTinh = gioiTinh?.Trim() ?? "";
        email = email?.Trim() ?? "";
        sdt = sdt?.Trim() ?? "";
        diaChi = diaChi?.Trim() ?? "";

        ValidateReaderData(hoTen, maSo, loai, gioiTinh, email, sdt, diaChi);

        using var conn = new NpgsqlConnection(AppSettings.ConnectionString);
        conn.Open();

        using (var checkCmd = new NpgsqlCommand(@"
select 
    sum(case when ma_so = @maSo and ban_doc_id <> @id then 1 else 0 end) as trung_ma_so,
    sum(case when coalesce(email,'') = @email and ban_doc_id <> @id then 1 else 0 end) as trung_email,
    sum(case when coalesce(sdt,'') = @sdt and ban_doc_id <> @id then 1 else 0 end) as trung_sdt
from ban_doc;", conn))
        {
            checkCmd.Parameters.AddWithValue("@id", id);
            checkCmd.Parameters.AddWithValue("@maSo", maSo);
            checkCmd.Parameters.AddWithValue("@email", email);
            checkCmd.Parameters.AddWithValue("@sdt", sdt);

            using var rd = checkCmd.ExecuteReader();
            if (rd.Read())
            {
                var trungMaSo = rd.IsDBNull(0) ? 0 : Convert.ToInt32(rd.GetValue(0));
                var trungEmail = rd.IsDBNull(1) ? 0 : Convert.ToInt32(rd.GetValue(1));
                var trungSdt = rd.IsDBNull(2) ? 0 : Convert.ToInt32(rd.GetValue(2));

                if (trungMaSo > 0) throw new Exception("Mã số bạn đọc đã tồn tại.");
                if (trungEmail > 0) throw new Exception("Email đã tồn tại.");
                if (trungSdt > 0) throw new Exception("Số điện thoại đã tồn tại.");
            }
        }

        const string sql = @"
update ban_doc
set ho_ten=@ho_ten, ma_so=@ma_so, loai_ban_doc=@loai, gioi_tinh=@gioi_tinh, ngay_sinh=@ngay_sinh,
    email=@email, sdt=@sdt, dia_chi=@dia_chi, dang_hoat_dong=@active
where ban_doc_id=@id;";

        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ho_ten", hoTen);
        cmd.Parameters.AddWithValue("@ma_so", maSo);
        cmd.Parameters.AddWithValue("@loai", loai);
        cmd.Parameters.AddWithValue("@gioi_tinh", gioiTinh);
        cmd.Parameters.AddWithValue("@ngay_sinh", (object?)ngaySinh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@sdt", sdt);
        cmd.Parameters.AddWithValue("@dia_chi", diaChi);
        cmd.Parameters.AddWithValue("@active", active);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public void DeleteReader(int id)
    {
        Db.Execute("delete from ban_doc where ban_doc_id=@id;", new NpgsqlParameter("@id", id));
    }

    public DataTable GetBooks(string keyword = "", int? categoryId = null)
    {
        const string sql = @"
select s.sach_id as ""ID"",
       s.tieu_de as ""Tiêu đề"",
       s.tac_gia as ""Tác giả"",
       s.nha_xuat_ban as ""NXB"",
       s.nam_xuat_ban as ""Năm XB"",
       s.isbn as ""ISBN"",
       s.ngon_ngu as ""Ngôn ngữ"",
       s.gia_bia as ""Giá bìa"",
       dm.ten as ""Danh mục"",
       coalesce(total_copy.so_ban_sao,0) as ""Tổng bản sao"",
       coalesce(ready_copy.so_san_sang,0) as ""Sẵn sàng""
from sach s
join danh_muc dm on dm.danh_muc_id = s.danh_muc_id
left join (
    select sach_id, count(*) as so_ban_sao
    from ban_sao_sach
    group by sach_id
) total_copy on total_copy.sach_id = s.sach_id
left join (
    select sach_id, count(*) as so_san_sang
    from ban_sao_sach
    where trang_thai = 'SAN_SANG'
    group by sach_id
) ready_copy on ready_copy.sach_id = s.sach_id
where (@kw = '' or lower(s.tieu_de) like lower(@pattern) or lower(s.tac_gia) like lower(@pattern) or coalesce(s.isbn,'') like @pattern)
  and (@cid is null or s.danh_muc_id = @cid)
order by s.sach_id desc;";
        return Db.Query(sql,
            new NpgsqlParameter("@kw", NpgsqlDbType.Text) { Value = keyword ?? "" },
            new NpgsqlParameter("@pattern", NpgsqlDbType.Text) { Value = $"%{keyword}%" },
            new NpgsqlParameter("@cid", NpgsqlDbType.Integer) { Value = (object?)categoryId ?? DBNull.Value });
    }

    public void AddBook(string tieuDe, string tacGia, string nxb, int? namXb, string isbn, string ngonNgu, decimal giaBia, string moTa, int danhMucId, int soBanSao)
    {
        tieuDe = tieuDe?.Trim() ?? "";
        tacGia = tacGia?.Trim() ?? "";
        nxb = nxb?.Trim() ?? "";
        isbn = isbn?.Trim() ?? "";
        ngonNgu = ngonNgu?.Trim() ?? "";
        moTa = moTa?.Trim() ?? "";

        ValidateBookData(tieuDe, tacGia, nxb, isbn, danhMucId, soBanSao);

        Db.ExecuteTransaction((conn, tran) =>
        {
            var cmd = new NpgsqlCommand(@"
insert into sach(tieu_de, tac_gia, nha_xuat_ban, nam_xuat_ban, isbn, ngon_ngu, gia_bia, mo_ta, danh_muc_id)
values(@tieu_de, @tac_gia, @nxb, @nam_xb, @isbn, @ngon_ngu, @gia_bia, @mo_ta, @danh_muc_id)
returning sach_id;", conn, tran);

            cmd.Parameters.AddWithValue("@tieu_de", tieuDe);
            cmd.Parameters.AddWithValue("@tac_gia", tacGia);
            cmd.Parameters.AddWithValue("@nxb", nxb);
            cmd.Parameters.AddWithValue("@nam_xb", (object?)namXb ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@isbn", isbn);
            cmd.Parameters.AddWithValue("@ngon_ngu", string.IsNullOrWhiteSpace(ngonNgu) ? "Tiếng Việt" : ngonNgu);
            cmd.Parameters.AddWithValue("@gia_bia", giaBia);
            cmd.Parameters.AddWithValue("@mo_ta", string.IsNullOrWhiteSpace(moTa) ? DBNull.Value : moTa);
            cmd.Parameters.AddWithValue("@danh_muc_id", danhMucId);

            int sachId = Convert.ToInt32(cmd.ExecuteScalar());

            for (int i = 1; i <= soBanSao; i++)
            {
                string copyCode = $"S{sachId:000}-C{i:000}";
                var copyCmd = new NpgsqlCommand(@"
insert into ban_sao_sach(sach_id, ma_ban_sao, trang_thai, vi_tri_ke)
values(@sach_id, @ma, 'SAN_SANG', @vi_tri);", conn, tran);
                copyCmd.Parameters.AddWithValue("@sach_id", sachId);
                copyCmd.Parameters.AddWithValue("@ma", copyCode);
                copyCmd.Parameters.AddWithValue("@vi_tri", $"K-{sachId:000}");
                copyCmd.ExecuteNonQuery();
            }

            return 1;
        });
    }
    private static void ValidateBookData(string tieuDe, string tacGia, string nxb, string isbn, int danhMucId, int soBanSao)
    {
        if (string.IsNullOrWhiteSpace(tieuDe))
            throw new Exception("Tiêu đề không được để trống.");

        if (string.IsNullOrWhiteSpace(tacGia))
            throw new Exception("Tác giả không được để trống.");

        if (string.IsNullOrWhiteSpace(nxb))
            throw new Exception("Nhà xuất bản không được để trống.");

        if (string.IsNullOrWhiteSpace(isbn))
            throw new Exception("ISBN không được để trống.");

        if (danhMucId <= 0)
            throw new Exception("Vui lòng chọn danh mục.");

        if (soBanSao <= 0)
            throw new Exception("Tổng bản sao phải lớn hơn 0.");
    }

    public void UpdateBook(int id, string tieuDe, string tacGia, string nxb, int? namXb, string isbn, string ngonNgu, decimal giaBia, string moTa, int danhMucId, int soBanSao)
    {
        tieuDe = tieuDe?.Trim() ?? "";
        tacGia = tacGia?.Trim() ?? "";
        nxb = nxb?.Trim() ?? "";
        isbn = isbn?.Trim() ?? "";
        ngonNgu = ngonNgu?.Trim() ?? "";
        moTa = moTa?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(tieuDe))
            throw new Exception("Tiêu đề không được để trống.");

        if (string.IsNullOrWhiteSpace(tacGia))
            throw new Exception("Tác giả không được để trống.");

        if (string.IsNullOrWhiteSpace(nxb))
            throw new Exception("Nhà xuất bản không được để trống.");

        if (string.IsNullOrWhiteSpace(isbn))
            throw new Exception("ISBN không được để trống.");

        if (danhMucId <= 0)
            throw new Exception("Vui lòng chọn danh mục.");

        if (soBanSao <= 0)
            throw new Exception("Tổng bản sao phải lớn hơn 0.");

        Db.ExecuteTransaction((conn, tran) =>
        {
            const string updateBookSql = @"
update sach
set tieu_de=@tieu_de, tac_gia=@tac_gia, nha_xuat_ban=@nxb, nam_xuat_ban=@nam_xb, isbn=@isbn,
    ngon_ngu=@ngon_ngu, gia_bia=@gia_bia, mo_ta=@mo_ta, danh_muc_id=@danh_muc_id
where sach_id=@id;";

            using (var updateCmd = new NpgsqlCommand(updateBookSql, conn, tran))
            {
                updateCmd.Parameters.AddWithValue("@tieu_de", tieuDe);
                updateCmd.Parameters.AddWithValue("@tac_gia", tacGia);
                updateCmd.Parameters.AddWithValue("@nxb", nxb);
                updateCmd.Parameters.AddWithValue("@nam_xb", (object?)namXb ?? DBNull.Value);
                updateCmd.Parameters.AddWithValue("@isbn", isbn);
                updateCmd.Parameters.AddWithValue("@ngon_ngu", string.IsNullOrWhiteSpace(ngonNgu) ? "Tiếng Việt" : ngonNgu);
                updateCmd.Parameters.AddWithValue("@gia_bia", giaBia);
                updateCmd.Parameters.AddWithValue("@mo_ta", string.IsNullOrWhiteSpace(moTa) ? DBNull.Value : moTa);
                updateCmd.Parameters.AddWithValue("@danh_muc_id", danhMucId);
                updateCmd.Parameters.AddWithValue("@id", id);
                updateCmd.ExecuteNonQuery();
            }

            int currentCopies;
            using (var countCmd = new NpgsqlCommand("select count(*) from ban_sao_sach where sach_id=@id;", conn, tran))
            {
                countCmd.Parameters.AddWithValue("@id", id);
                currentCopies = Convert.ToInt32(countCmd.ExecuteScalar());
            }

            if (soBanSao > currentCopies)
            {
                int maxSuffix;
                using (var maxCmd = new NpgsqlCommand(@"
select coalesce(max((regexp_match(ma_ban_sao, '-C([0-9]+)$'))[1]::int), 0)
from ban_sao_sach
where sach_id=@id;", conn, tran))
                {
                    maxCmd.Parameters.AddWithValue("@id", id);
                    maxSuffix = Convert.ToInt32(maxCmd.ExecuteScalar());
                }

                for (int i = 1; i <= soBanSao - currentCopies; i++)
                {
                    maxSuffix++;
                    string copyCode = $"S{id:000}-C{maxSuffix:000}";
                    using var insertCopyCmd = new NpgsqlCommand(@"
insert into ban_sao_sach(sach_id, ma_ban_sao, trang_thai, vi_tri_ke)
values(@sach_id, @ma, 'SAN_SANG', @vi_tri);", conn, tran);
                    insertCopyCmd.Parameters.AddWithValue("@sach_id", id);
                    insertCopyCmd.Parameters.AddWithValue("@ma", copyCode);
                    insertCopyCmd.Parameters.AddWithValue("@vi_tri", $"K-{id:000}");
                    insertCopyCmd.ExecuteNonQuery();
                }
            }
            else if (soBanSao < currentCopies)
            {
                int canXoa = currentCopies - soBanSao;
                int sanSang;
                using (var readyCmd = new NpgsqlCommand("select count(*) from ban_sao_sach where sach_id=@id and trang_thai='SAN_SANG';", conn, tran))
                {
                    readyCmd.Parameters.AddWithValue("@id", id);
                    sanSang = Convert.ToInt32(readyCmd.ExecuteScalar());
                }

                if (sanSang < canXoa)
                    throw new Exception($"Không thể giảm xuống {soBanSao} bản sao vì đang có bản sao không ở trạng thái sẵn sàng. Hãy trả sách hoặc xóa bớt các bản sao còn trống trước.");

                using var deleteCopyCmd = new NpgsqlCommand(@"
delete from ban_sao_sach
where ban_sao_id in (
    select ban_sao_id
    from ban_sao_sach
    where sach_id=@id and trang_thai='SAN_SANG'
    order by ban_sao_id desc
    limit @so_xoa
);", conn, tran);
                deleteCopyCmd.Parameters.AddWithValue("@id", id);
                deleteCopyCmd.Parameters.AddWithValue("@so_xoa", canXoa);
                deleteCopyCmd.ExecuteNonQuery();
            }

            return 1;
        });
    }

    public void DeleteBook(int id)
    {
        Db.ExecuteTransaction((conn, tran) =>
        {
            var loanIds = new List<int>();

            using (var loanCmd = new NpgsqlCommand(@"
select distinct ctm.phieu_muon_id
from chi_tiet_muon ctm
join ban_sao_sach bs on bs.ban_sao_id = ctm.ban_sao_id
where bs.sach_id = @id;", conn, tran))
            {
                loanCmd.Parameters.AddWithValue("@id", id);
                using var reader = loanCmd.ExecuteReader();
                while (reader.Read())
                {
                    loanIds.Add(reader.GetInt32(0));
                }
            }

            using (var deleteDetailsCmd = new NpgsqlCommand(@"
delete from chi_tiet_muon
where ban_sao_id in (
    select ban_sao_id
    from ban_sao_sach
    where sach_id = @id
);", conn, tran))
            {
                deleteDetailsCmd.Parameters.AddWithValue("@id", id);
                deleteDetailsCmd.ExecuteNonQuery();
            }

            using (var deleteBookCmd = new NpgsqlCommand("delete from sach where sach_id=@id;", conn, tran))
            {
                deleteBookCmd.Parameters.AddWithValue("@id", id);
                deleteBookCmd.ExecuteNonQuery();
            }

            using (var cleanupFineCmd = new NpgsqlCommand(@"
delete from tien_phat tp
where not exists (
    select 1
    from chi_tiet_muon ctm
    where ctm.phieu_muon_id = tp.phieu_muon_id
);", conn, tran))
            {
                cleanupFineCmd.ExecuteNonQuery();
            }

            foreach (int loanId in loanIds.Distinct())
            {
                using var updateLoanCmd = new NpgsqlCommand("select fn_cap_nhat_tinh_trang_phieu_muon(@loan_id);", conn, tran);
                updateLoanCmd.Parameters.AddWithValue("@loan_id", loanId);
                updateLoanCmd.ExecuteNonQuery();
            }

            return 1;
        });
    }

    public DataTable GetLoans(string keyword = "", string? status = null)
    {
        const string sql = @"
select pm.phieu_muon_id as ""ID"",
       bd.ho_ten as ""Bạn đọc"",
       bd.ma_so as ""Mã số"",
       pm.ngay_muon as ""Ngày mượn"",
       pm.ngay_hen_tra as ""Hẹn trả"",
       pm.ngay_dong_phieu as ""Ngày đóng phiếu"",
       pm.trang_thai as ""Trạng thái"",
       pm.so_lan_gia_han as ""Gia hạn"",
       coalesce(detail.so_sach,0) as ""Số sách"",
       tk.ho_ten as ""Tạo bởi""
from phieu_muon pm
join ban_doc bd on bd.ban_doc_id = pm.ban_doc_id
left join tai_khoan_nguoi_dung tk on tk.tai_khoan_id = pm.tao_boi
left join (
    select phieu_muon_id, count(*) as so_sach
    from chi_tiet_muon
    group by phieu_muon_id
) detail on detail.phieu_muon_id = pm.phieu_muon_id
where (@kw = '' or lower(bd.ho_ten) like lower(@pattern) or coalesce(bd.ma_so,'') like @pattern or pm.phieu_muon_id::text like @pattern)
  and (@status = '' or pm.trang_thai = @status)
order by pm.phieu_muon_id desc;";
        return Db.Query(sql,
            new NpgsqlParameter("@kw", keyword ?? ""),
            new NpgsqlParameter("@pattern", $"%{keyword}%"),
            new NpgsqlParameter("@status", status ?? ""));
    }
    public void UpdateLoan(int id, DateTime dueDate, int extendCount)
    {
        const string sql = @"
update phieu_muon
set ngay_hen_tra = @due_date,
    so_lan_gia_han = @extend_count
where phieu_muon_id = @id;";

        Db.Execute(sql,
            new NpgsqlParameter("@due_date", dueDate.Date),
            new NpgsqlParameter("@extend_count", extendCount),
            new NpgsqlParameter("@id", id));
    }

    public DataTable GetLoanDetails(int loanId)
    {
        const string sql = @"
select ctm.chi_tiet_muon_id as ""ID"",
       bs.ma_ban_sao as ""Mã bản sao"",
       s.tieu_de as ""Tên sách"",
       ctm.thoi_gian_muon as ""Thời gian mượn"",
       ctm.thoi_gian_tra as ""Thời gian trả"",
       coalesce(ctm.tinh_trang_luc_muon,'') as ""Lúc mượn"",
       coalesce(ctm.tinh_trang_luc_tra,'') as ""Lúc trả""
from chi_tiet_muon ctm
join ban_sao_sach bs on bs.ban_sao_id = ctm.ban_sao_id
join sach s on s.sach_id = bs.sach_id
where ctm.phieu_muon_id = @id
order by ctm.chi_tiet_muon_id;";
        return Db.Query(sql, new NpgsqlParameter("@id", loanId));
    }

    public void CreateLoan(int readerId, DateTime ngayMuon, DateTime ngayHenTra, IEnumerable<int> copyIds, int createdBy)
    {
        var ids = copyIds.ToList();
        if (ids.Count == 0) throw new Exception("Bạn phải chọn ít nhất 1 bản sao sách.");

        Db.ExecuteTransaction((conn, tran) =>
        {
            const int defaultMaxBooksPerReader = 5;

            using (var configCmd = new NpgsqlCommand(@"
insert into cau_hinh_he_thong(ma, gia_tri, mo_ta)
values('SO_SACH_MUON_TOI_DA', @gia_tri, 'Số lượng sách tối đa một bạn đọc được mượn cùng lúc')
on conflict (ma) do update
set gia_tri = case
    when coalesce(nullif(cau_hinh_he_thong.gia_tri, ''), '0')::int < @gia_tri::int then @gia_tri
    else cau_hinh_he_thong.gia_tri
end;", conn, tran))
            {
                configCmd.Parameters.AddWithValue("@gia_tri", defaultMaxBooksPerReader.ToString());
                configCmd.ExecuteNonQuery();
            }

            var cmd = new NpgsqlCommand(@"
insert into phieu_muon(ban_doc_id, ngay_muon, ngay_hen_tra, ghi_chu, tao_boi)
values(@ban_doc_id, @ngay_muon, @ngay_hen_tra, @ghi_chu, @tao_boi)
returning phieu_muon_id;", conn, tran);

            cmd.Parameters.AddWithValue("@ban_doc_id", readerId);
            cmd.Parameters.AddWithValue("@ngay_muon", ngayMuon.Date);
            cmd.Parameters.AddWithValue("@ngay_hen_tra", ngayHenTra.Date);
            cmd.Parameters.AddWithValue("@ghi_chu", "Lập từ ứng dụng WinForms");
            cmd.Parameters.AddWithValue("@tao_boi", createdBy);

            int loanId = Convert.ToInt32(cmd.ExecuteScalar());

            foreach (var copyId in ids)
            {
                var detailCmd = new NpgsqlCommand(@"
insert into chi_tiet_muon(phieu_muon_id, ban_sao_id, tinh_trang_luc_muon)
values(@phieu_muon_id, @ban_sao_id, 'Tốt');", conn, tran);
                detailCmd.Parameters.AddWithValue("@phieu_muon_id", loanId);
                detailCmd.Parameters.AddWithValue("@ban_sao_id", copyId);
                detailCmd.ExecuteNonQuery();
            }

            return 1;
        });
    }

    public void ReturnLoanDetail(int detailId, int staffId, string condition)
    {
        const string sql = @"
update chi_tiet_muon
set thoi_gian_tra = current_timestamp,
    tinh_trang_luc_tra = @condition,
    tra_boi = @staff
where chi_tiet_muon_id = @id
  and thoi_gian_tra is null;";
        Db.Execute(sql,
            new NpgsqlParameter("@condition", condition),
            new NpgsqlParameter("@staff", staffId),
            new NpgsqlParameter("@id", detailId));
    }

    public DataTable GetFines()
    {
        const string sql = @"
select tp.tien_phat_id as ""ID"",
       tp.phieu_muon_id as ""Phiếu mượn"",
       bd.ho_ten as ""Bạn đọc"",
       tp.ly_do as ""Lý do"",
       tp.so_ngay_tre as ""Số ngày trễ"",
       tp.so_tien as ""Số tiền"",
       tp.da_thanh_toan as ""Đã thanh toán"",
       tp.ngay_tao as ""Ngày tạo""
from tien_phat tp
join phieu_muon pm on pm.phieu_muon_id = tp.phieu_muon_id
join ban_doc bd on bd.ban_doc_id = pm.ban_doc_id
order by tp.tien_phat_id desc;";
        return Db.Query(sql);
    }

    public void MarkFineAsPaid(int fineId, int staffId)
    {
        const string sql = @"
update tien_phat
set da_thanh_toan = true,
    ngay_thanh_toan = current_timestamp,
    thu_boi = @staff
where tien_phat_id = @id;";
        Db.Execute(sql,
            new NpgsqlParameter("@staff", staffId),
            new NpgsqlParameter("@id", fineId));
    }

    public DataTable GetTopBorrowedBooks()
    {
        const string sql = @"
select s.tieu_de as ""Tên sách"", count(*) as ""Lượt mượn""
from chi_tiet_muon ctm
join ban_sao_sach bs on bs.ban_sao_id = ctm.ban_sao_id
join sach s on s.sach_id = bs.sach_id
group by s.tieu_de
order by count(*) desc, s.tieu_de
limit 5;";
        return Db.Query(sql);
    }
}