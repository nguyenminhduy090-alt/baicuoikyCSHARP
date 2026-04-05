# LibraryManagement.WinForms

Ứng dụng WinForms quản lý thư viện theo mô hình 3-layer đơn giản:
- `Forms`: giao diện
- `Services`: nghiệp vụ
- `DAL`: truy cập dữ liệu

## 1. Chuẩn bị database
1. Mở pgAdmin / PostgreSQL.
2. Tạo database tên `quanlythuvien`.
3. Chạy file `DBquanlythuvien.sql`.

Tài khoản mẫu trong database:
- `admin / 123456`
- `staff01 / 123456`

## 2. Cập nhật connection string
Mở file `AppSettings.cs` và sửa:
```csharp
"Host=localhost;Port=5432;Database=quanlythuvien;Username=postgres;Password=123456;Include Error Detail=true"
```

## 3. Chạy project
- Mở `LibraryManagement.sln` bằng Visual Studio 2022.
- Restore NuGet packages.
- Build solution.
- Run.

## 4. Chức năng đã có
- Đăng nhập + phân quyền Admin/Staff
- CRUD Danh mục
- CRUD Bạn đọc
- CRUD Sách
- Lập phiếu mượn
- Trả sách
- Tiền phạt
- Thống kê nhanh dashboard

## 5. Gợi ý nâng cấp
- Thêm module tài khoản
- Xuất Excel/PDF
- Biểu đồ WinForms Chart
- Nhật ký hệ thống giao diện riêng
