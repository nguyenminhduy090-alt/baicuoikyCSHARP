using LibraryManagement.WinForms.DAL;
using LibraryManagement.WinForms.Helpers;
using LibraryManagement.WinForms.Models;
using Npgsql;

namespace LibraryManagement.WinForms.Services;

public class AuthService
{
    public UserSession? Login(string username, string password)
    {
        string passwordHash = Sha256Helper.ComputeHash(password);

        const string sql = @"
select tk.tai_khoan_id,
       tk.ten_dang_nhap,
       tk.ho_ten,
       vt.ten_vai_tro
from tai_khoan_nguoi_dung tk
join vai_tro vt on vt.vai_tro_id = tk.vai_tro_id
where tk.ten_dang_nhap = @u
  and tk.mat_khau_hash = @p
  and tk.dang_hoat_dong = true;";

        var dt = Db.Query(sql,
            new NpgsqlParameter("@u", username),
            new NpgsqlParameter("@p", passwordHash));

        if (dt.Rows.Count == 0) return null;

        var row = dt.Rows[0];
        var session = new UserSession
        {
            UserId = Convert.ToInt32(row["tai_khoan_id"]),
            Username = Convert.ToString(row["ten_dang_nhap"]) ?? "",
            FullName = Convert.ToString(row["ho_ten"]) ?? "",
            RoleName = Convert.ToString(row["ten_vai_tro"]) ?? ""
        };

        session.Permissions = LoadPermissions(session.Username);

        // Cho phép STAFF xóa bạn đọc ngay cả khi CSDL hiện tại chưa được seed quyền này.
        // Nhờ vậy bản project cũ vẫn chạy được mà không cần reset lại database.
        if (string.Equals(session.RoleName, "STAFF", StringComparison.OrdinalIgnoreCase))
        {
            session.Permissions.Add("BAN_DOC_XOA");
        }

        return session;
    }

    public HashSet<string> LoadPermissions(string username)
    {
        const string sql = @"
select ma_quyen
from vw_tai_khoan_quyen
where ten_dang_nhap = @u and duoc_phep = true;";

        var dt = Db.Query(sql, new NpgsqlParameter("@u", username));
        var permissions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (System.Data.DataRow row in dt.Rows)
        {
            var permission = row["ma_quyen"]?.ToString() ?? "";
            if (!string.IsNullOrWhiteSpace(permission))
            {
                permissions.Add(permission);
            }
        }

        return permissions;
    }

    public System.Data.DataTable GetRoles()
    {
        const string sql = @"
select vai_tro_id, ten_vai_tro
from vai_tro
order by vai_tro_id;";
        return Db.Query(sql);
    }

    public System.Data.DataTable GetAccounts(string keyword = "")
    {
        const string sql = @"
select tk.tai_khoan_id as ""ID"",
       tk.ten_dang_nhap as ""Tên đăng nhập"",
       tk.ho_ten as ""Họ tên"",
       coalesce(tk.email::text,'') as ""Email"",
       coalesce(tk.sdt,'') as ""SĐT"",
       vt.ten_vai_tro as ""Vai trò"",
       tk.dang_hoat_dong as ""Hoạt động"",
       tk.tao_luc as ""Tạo lúc""
from tai_khoan_nguoi_dung tk
join vai_tro vt on vt.vai_tro_id = tk.vai_tro_id
where @kw = ''
   or lower(tk.ten_dang_nhap) like lower(@pattern)
   or lower(tk.ho_ten) like lower(@pattern)
   or lower(coalesce(tk.email::text,'')) like lower(@pattern)
order by tk.tai_khoan_id desc;";

        return Db.Query(sql,
            new NpgsqlParameter("@kw", keyword ?? ""),
            new NpgsqlParameter("@pattern", $"%{keyword}%"));
    }

    public void AddAccount(string username, string password, string fullName, string email, string phone, int roleId, bool active)
    {
        ValidateAccountData(username, password, fullName, email, phone, roleId, true);
        const string sql = @"
insert into tai_khoan_nguoi_dung(ten_dang_nhap, mat_khau_hash, ho_ten, email, sdt, vai_tro_id, dang_hoat_dong)
values(@u, @p, @name, @email, @phone, @role, @active);";

        Db.Execute(sql,
            new NpgsqlParameter("@u", username.Trim()),
            new NpgsqlParameter("@p", Sha256Helper.ComputeHash(password)),
            new NpgsqlParameter("@name", fullName.Trim()),
            new NpgsqlParameter("@email", string.IsNullOrWhiteSpace(email) ? DBNull.Value : email.Trim()),
            new NpgsqlParameter("@phone", string.IsNullOrWhiteSpace(phone) ? DBNull.Value : phone.Trim()),
            new NpgsqlParameter("@role", roleId),
            new NpgsqlParameter("@active", active));
    }

    public void UpdateAccount(int id, string username, string? password, string fullName, string email, string phone, int roleId, bool active)
    {
        ValidateAccountData(username, password ?? "", fullName, email, phone, roleId, false);
        var sql = @"
update tai_khoan_nguoi_dung
set ten_dang_nhap = @u,
    ho_ten = @name,
    email = @email,
    sdt = @phone,
    vai_tro_id = @role,
    dang_hoat_dong = @active";

        var parameters = new List<NpgsqlParameter>
        {
            new("@u", username.Trim()),
            new("@name", fullName.Trim()),
            new("@email", string.IsNullOrWhiteSpace(email) ? DBNull.Value : email.Trim()),
            new("@phone", string.IsNullOrWhiteSpace(phone) ? DBNull.Value : phone.Trim()),
            new("@role", roleId),
            new("@active", active),
            new("@id", id)
        };

        if (!string.IsNullOrWhiteSpace(password))
        {
            sql += ",\n    mat_khau_hash = @p";
            parameters.Add(new NpgsqlParameter("@p", Sha256Helper.ComputeHash(password)));
        }

        sql += "\nwhere tai_khoan_id = @id;";
        Db.Execute(sql, parameters.ToArray());
    }

    public void DeleteAccount(int id)
    {
        const string sql = @"update tai_khoan_nguoi_dung set dang_hoat_dong = false where tai_khoan_id = @id;";
        Db.Execute(sql, new NpgsqlParameter("@id", id));
    }

    private void ValidateAccountData(string username, string password, string fullName, string email, string phone, int roleId, bool isCreate)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new Exception("Tên đăng nhập không được để trống.");

        if (username.Trim().Length < 3)
            throw new Exception("Tên đăng nhập phải có ít nhất 3 ký tự.");

        if (isCreate && string.IsNullOrWhiteSpace(password))
            throw new Exception("Mật khẩu không được để trống khi tạo tài khoản.");

        if (!string.IsNullOrWhiteSpace(password) && password.Trim().Length < 6)
            throw new Exception("Mật khẩu phải có ít nhất 6 ký tự.");

        if (string.IsNullOrWhiteSpace(fullName))
            throw new Exception("Họ tên không được để trống.");

        if (!string.IsNullOrWhiteSpace(email) && !System.Text.RegularExpressions.Regex.IsMatch(email.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new Exception("Email không đúng định dạng.");

        if (!string.IsNullOrWhiteSpace(phone) && !System.Text.RegularExpressions.Regex.IsMatch(phone.Trim(), @"^[0-9]{9,11}$"))
            throw new Exception("Số điện thoại phải gồm 9-11 chữ số.");

        if (roleId <= 0)
            throw new Exception("Vui lòng chọn vai trò hợp lệ.");
    }

    public void ChangePassword(int userId, string newPassword)
    {
        const string sql = @"update tai_khoan_nguoi_dung set mat_khau_hash = @p where tai_khoan_id = @id;";
        Db.Execute(sql,
            new NpgsqlParameter("@p", Sha256Helper.ComputeHash(newPassword)),
            new NpgsqlParameter("@id", userId));
    }
}
