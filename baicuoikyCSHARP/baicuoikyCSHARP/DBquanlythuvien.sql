-- =========================================================
-- DATABASE: QUAN LY THU VIEN - BAN HOAN THIEN + PHAN QUYEN
-- PostgreSQL / pgAdmin
-- Dung cho WinForms C# ket noi bang Npgsql
-- Co the chay lai nhieu lan
-- 1) Vai_tro_quyen de phan quyen ro rang
-- 2) ADMIN duoc gan toan bo quyen
-- 3) STAFF bi han che o cac module quan tri, dac biet KHONG duoc xoa danh muc
-- =========================================================

CREATE EXTENSION IF NOT EXISTS citext;

DROP VIEW IF EXISTS vw_tai_khoan_quyen CASCADE;
DROP FUNCTION IF EXISTS fn_kiem_tra_quyen(VARCHAR, VARCHAR) CASCADE;
DROP TRIGGER IF EXISTS trg_dong_phieu_khi_tra_het ON chi_tiet_muon;
DROP TRIGGER IF EXISTS trg_dong_bo_trang_thai_ban_sao_update ON chi_tiet_muon;
DROP TRIGGER IF EXISTS trg_dong_bo_trang_thai_ban_sao_insert ON chi_tiet_muon;
DROP TRIGGER IF EXISTS trg_kiem_tra_ban_sao_san_sang ON chi_tiet_muon;
DROP FUNCTION IF EXISTS fn_dong_phieu_khi_tra_het() CASCADE;
DROP FUNCTION IF EXISTS fn_dong_bo_trang_thai_ban_sao() CASCADE;
DROP FUNCTION IF EXISTS fn_kiem_tra_ban_sao_san_sang() CASCADE;
DROP VIEW IF EXISTS vw_ban_doc_muon_hien_tai CASCADE;
DROP FUNCTION IF EXISTS fn_kiem_tra_dieu_kien_muon() CASCADE;
DROP FUNCTION IF EXISTS fn_cap_nhat_tinh_trang_phieu_muon(INT) CASCADE;
DROP FUNCTION IF EXISTS fn_tu_dong_tien_phat_khi_tra() CASCADE;
DROP TRIGGER IF EXISTS trg_kiem_tra_dieu_kien_muon ON chi_tiet_muon;
DROP TRIGGER IF EXISTS trg_tu_dong_tien_phat_khi_tra ON chi_tiet_muon;

DROP TABLE IF EXISTS vai_tro_quyen CASCADE;
DROP TABLE IF EXISTS chuc_nang CASCADE;
DROP TABLE IF EXISTS tien_phat CASCADE;
DROP TABLE IF EXISTS chi_tiet_muon CASCADE;
DROP TABLE IF EXISTS phieu_muon CASCADE;
DROP TABLE IF EXISTS ban_sao_sach CASCADE;
DROP TABLE IF EXISTS sach CASCADE;
DROP TABLE IF EXISTS ban_doc CASCADE;
DROP TABLE IF EXISTS danh_muc CASCADE;
DROP TABLE IF EXISTS nhat_ky_he_thong CASCADE;
DROP TABLE IF EXISTS cau_hinh_he_thong CASCADE;
DROP TABLE IF EXISTS tai_khoan_nguoi_dung CASCADE;
DROP TABLE IF EXISTS vai_tro CASCADE;

-- =========================================================
-- 1. BANG VAI TRO
-- =========================================================
CREATE TABLE vai_tro (
    vai_tro_id SERIAL PRIMARY KEY,
    ten_vai_tro VARCHAR(20) UNIQUE NOT NULL,
    mo_ta VARCHAR(255)
);

-- =========================================================
-- 2. BANG CHUC NANG / QUYEN
-- Muc dich: de C# query ra va an/disable nut theo role
-- =========================================================
CREATE TABLE chuc_nang (
    chuc_nang_id SERIAL PRIMARY KEY,
    ma_quyen VARCHAR(50) UNIQUE NOT NULL,
    ten_hien_thi VARCHAR(150) NOT NULL,
    nhom VARCHAR(50) NOT NULL,
    mo_ta VARCHAR(255)
);

CREATE TABLE vai_tro_quyen (
    vai_tro_id INT NOT NULL REFERENCES vai_tro(vai_tro_id) ON DELETE CASCADE,
    chuc_nang_id INT NOT NULL REFERENCES chuc_nang(chuc_nang_id) ON DELETE CASCADE,
    duoc_phep BOOLEAN NOT NULL DEFAULT TRUE,
    PRIMARY KEY (vai_tro_id, chuc_nang_id)
);

-- =========================================================
-- 3. TAI KHOAN NGUOI DUNG
-- Mat khau luu dang SHA-256 hash
-- Tai khoan mau:
--   username: admin / staff01
--   password goc: 123456
-- =========================================================
CREATE TABLE tai_khoan_nguoi_dung (
    tai_khoan_id SERIAL PRIMARY KEY,
    ten_dang_nhap VARCHAR(50) UNIQUE NOT NULL,
    mat_khau_hash VARCHAR(255) NOT NULL,
    ho_ten VARCHAR(150) NOT NULL,
    email CITEXT UNIQUE,
    sdt VARCHAR(20),
    vai_tro_id INT NOT NULL REFERENCES vai_tro(vai_tro_id),
    dang_hoat_dong BOOLEAN NOT NULL DEFAULT TRUE,
    tao_luc TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT chk_tai_khoan_sdt CHECK (sdt IS NULL OR sdt ~ '^[0-9]{9,11}$')
);

-- =========================================================
-- 4. DANH MUC
-- =========================================================
CREATE TABLE danh_muc (
    danh_muc_id SERIAL PRIMARY KEY,
    ten VARCHAR(100) UNIQUE NOT NULL,
    mo_ta VARCHAR(255),
    dang_hoat_dong BOOLEAN NOT NULL DEFAULT TRUE,
    tao_luc TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- =========================================================
-- 5. SACH (DAU SACH)
-- =========================================================
CREATE TABLE sach (
    sach_id SERIAL PRIMARY KEY,
    tieu_de VARCHAR(200) NOT NULL,
    tac_gia VARCHAR(150) NOT NULL,
    nha_xuat_ban VARCHAR(150),
    nam_xuat_ban INT,
    isbn VARCHAR(20) UNIQUE,
    ngon_ngu VARCHAR(50) DEFAULT 'Tiếng Việt',
    gia_bia NUMERIC(12,2) NOT NULL DEFAULT 0,
    mo_ta TEXT,
    anh_bia VARCHAR(255),
    danh_muc_id INT NOT NULL REFERENCES danh_muc(danh_muc_id),
    tao_luc TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT chk_sach_nam_xuat_ban CHECK (
        nam_xuat_ban IS NULL
        OR nam_xuat_ban BETWEEN 1900 AND EXTRACT(YEAR FROM CURRENT_DATE)::INT + 1
    ),
    CONSTRAINT chk_sach_gia_bia CHECK (gia_bia >= 0)
);

-- =========================================================
-- 6. BAN SAO SACH
-- =========================================================
CREATE TABLE ban_sao_sach (
    ban_sao_id SERIAL PRIMARY KEY,
    sach_id INT NOT NULL REFERENCES sach(sach_id) ON DELETE CASCADE,
    ma_ban_sao VARCHAR(50) UNIQUE NOT NULL,
    trang_thai VARCHAR(20) NOT NULL DEFAULT 'SAN_SANG',
    vi_tri_ke VARCHAR(50),
    ngay_nhap DATE DEFAULT CURRENT_DATE,
    ghi_chu VARCHAR(200),
    CONSTRAINT chk_ban_sao_trang_thai CHECK (trang_thai IN ('SAN_SANG', 'DANG_MUON', 'HU_HONG', 'MAT'))
);

-- =========================================================
-- 7. BAN DOC
-- =========================================================
CREATE TABLE ban_doc (
    ban_doc_id SERIAL PRIMARY KEY,
    ho_ten VARCHAR(150) NOT NULL,
    ma_so VARCHAR(50) UNIQUE,
    loai_ban_doc VARCHAR(20) NOT NULL,
    gioi_tinh VARCHAR(10) NOT NULL,
    ngay_sinh DATE,
    email CITEXT UNIQUE,
    sdt VARCHAR(20),
    dia_chi VARCHAR(255),
    ngay_tao_the DATE NOT NULL DEFAULT CURRENT_DATE,
    dang_hoat_dong BOOLEAN NOT NULL DEFAULT TRUE,
    ghi_chu VARCHAR(255),
    CONSTRAINT chk_ban_doc_loai CHECK (loai_ban_doc IN ('HOC_SINH', 'SINH_VIEN', 'GIAO_VIEN', 'KHACH')),
    CONSTRAINT chk_ban_doc_gioi_tinh CHECK (gioi_tinh IN ('NAM', 'NU', 'KHAC')),
    CONSTRAINT chk_ban_doc_sdt CHECK (sdt IS NULL OR sdt ~ '^[0-9]{9,11}$'),
    CONSTRAINT chk_ban_doc_ngay_sinh CHECK (ngay_sinh IS NULL OR ngay_sinh <= CURRENT_DATE)
);
-- =========================================================
-- 8. PHIEU MUON
-- =========================================================
CREATE TABLE phieu_muon (
    phieu_muon_id SERIAL PRIMARY KEY,
    ban_doc_id INT NOT NULL REFERENCES ban_doc(ban_doc_id),
    ngay_muon DATE NOT NULL,
    ngay_hen_tra DATE NOT NULL,
    ngay_dong_phieu DATE,
    trang_thai VARCHAR(20) NOT NULL DEFAULT 'DANG_MUON',
    so_lan_gia_han INT NOT NULL DEFAULT 0,
    ghi_chu VARCHAR(250),
    tao_boi INT REFERENCES tai_khoan_nguoi_dung(tai_khoan_id),
    tao_luc TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT chk_phieu_muon_ngay CHECK (ngay_hen_tra >= ngay_muon),
    CONSTRAINT chk_phieu_muon_trang_thai CHECK (trang_thai IN ('DANG_MUON', 'DA_TRA', 'QUA_HAN', 'HUY')),
    CONSTRAINT chk_phieu_muon_gia_han CHECK (so_lan_gia_han >= 0),
    CONSTRAINT chk_phieu_muon_dong CHECK (ngay_dong_phieu IS NULL OR ngay_dong_phieu >= ngay_muon)
);

-- =========================================================
-- 9. CHI TIET MUON
-- =========================================================
CREATE TABLE chi_tiet_muon (
    chi_tiet_muon_id SERIAL PRIMARY KEY,
    phieu_muon_id INT NOT NULL REFERENCES phieu_muon(phieu_muon_id) ON DELETE CASCADE,
    ban_sao_id INT NOT NULL REFERENCES ban_sao_sach(ban_sao_id),
    thoi_gian_muon TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    thoi_gian_tra TIMESTAMPTZ,
    tinh_trang_luc_muon VARCHAR(100),
    tinh_trang_luc_tra VARCHAR(100),
    ghi_chu VARCHAR(255),
    tra_boi INT REFERENCES tai_khoan_nguoi_dung(tai_khoan_id),
    CONSTRAINT uq_chi_tiet_muon_phieu_ban_sao UNIQUE (phieu_muon_id, ban_sao_id),
    CONSTRAINT chk_chi_tiet_muon_tra CHECK (thoi_gian_tra IS NULL OR thoi_gian_tra >= thoi_gian_muon)
);

-- =========================================================
-- 10. TIEN PHAT
-- Moi phieu muon toi da 1 dong phat tong hop
-- =========================================================
CREATE TABLE tien_phat (
    tien_phat_id SERIAL PRIMARY KEY,
    phieu_muon_id INT NOT NULL UNIQUE REFERENCES phieu_muon(phieu_muon_id) ON DELETE CASCADE,
    ly_do VARCHAR(20) NOT NULL DEFAULT 'TRE_HAN',
    so_ngay_tre INT NOT NULL DEFAULT 0,
    so_tien NUMERIC(12,2) NOT NULL DEFAULT 0,
    da_thanh_toan BOOLEAN NOT NULL DEFAULT FALSE,
    ngay_tao TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ngay_thanh_toan TIMESTAMPTZ,
    thu_boi INT REFERENCES tai_khoan_nguoi_dung(tai_khoan_id),
    ghi_chu VARCHAR(255),
    CONSTRAINT chk_tien_phat_ly_do CHECK (ly_do IN ('TRE_HAN', 'MAT_SACH', 'HU_HONG')),
    CONSTRAINT chk_tien_phat_ngay_tre CHECK (so_ngay_tre >= 0),
    CONSTRAINT chk_tien_phat_so_tien CHECK (so_tien >= 0),
    CONSTRAINT chk_tien_phat_ngay_tt CHECK (ngay_thanh_toan IS NULL OR ngay_thanh_toan >= ngay_tao)
);

-- =========================================================
-- 11. CAU HINH HE THONG
-- =========================================================
CREATE TABLE cau_hinh_he_thong (
    cau_hinh_id SERIAL PRIMARY KEY,
    ma VARCHAR(50) UNIQUE NOT NULL,
    gia_tri VARCHAR(100) NOT NULL,
    mo_ta VARCHAR(255)
);

-- =========================================================
-- 12. NHAT KY HE THONG
-- =========================================================
CREATE TABLE nhat_ky_he_thong (
    nhat_ky_id SERIAL PRIMARY KEY,
    tai_khoan_id INT REFERENCES tai_khoan_nguoi_dung(tai_khoan_id),
    hanh_dong VARCHAR(100) NOT NULL,
    doi_tuong VARCHAR(50),
    doi_tuong_id INT,
    mo_ta VARCHAR(255),
    tao_luc TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- =========================================================
-- INDEX TOI UU TIM KIEM / JOIN
-- =========================================================
CREATE INDEX idx_tai_khoan_ten_dang_nhap ON tai_khoan_nguoi_dung(ten_dang_nhap);
CREATE INDEX idx_tai_khoan_vai_tro_id ON tai_khoan_nguoi_dung(vai_tro_id);
CREATE INDEX idx_danh_muc_ten ON danh_muc(ten);
CREATE INDEX idx_sach_tieu_de ON sach(tieu_de);
CREATE INDEX idx_sach_tac_gia ON sach(tac_gia);
CREATE INDEX idx_sach_isbn ON sach(isbn);
CREATE INDEX idx_sach_danh_muc_id ON sach(danh_muc_id);
CREATE INDEX idx_ban_sao_sach_id ON ban_sao_sach(sach_id);
CREATE INDEX idx_ban_sao_ma_ban_sao ON ban_sao_sach(ma_ban_sao);
CREATE INDEX idx_ban_sao_trang_thai ON ban_sao_sach(trang_thai);
CREATE INDEX idx_ban_doc_ho_ten ON ban_doc(ho_ten);
CREATE INDEX idx_ban_doc_sdt ON ban_doc(sdt);
CREATE INDEX idx_ban_doc_email ON ban_doc(email);
CREATE INDEX idx_phieu_muon_ban_doc_id ON phieu_muon(ban_doc_id);
CREATE INDEX idx_phieu_muon_trang_thai ON phieu_muon(trang_thai);
CREATE INDEX idx_phieu_muon_ngay_muon ON phieu_muon(ngay_muon);
CREATE INDEX idx_chi_tiet_muon_phieu_id ON chi_tiet_muon(phieu_muon_id);
CREATE INDEX idx_chi_tiet_muon_ban_sao_id ON chi_tiet_muon(ban_sao_id);
CREATE INDEX idx_tien_phat_phieu_muon_id ON tien_phat(phieu_muon_id);
CREATE INDEX idx_tien_phat_da_thanh_toan ON tien_phat(da_thanh_toan);
CREATE INDEX idx_nhat_ky_tai_khoan_id ON nhat_ky_he_thong(tai_khoan_id);
CREATE INDEX idx_nhat_ky_tao_luc ON nhat_ky_he_thong(tao_luc);
CREATE INDEX idx_chuc_nang_ma_quyen ON chuc_nang(ma_quyen);

-- =========================================================
-- TRIGGER / FUNCTION NGHIEP VU
-- 1) Khong cho muon ban sao khong san sang
-- 2) Kiem tra dieu kien muon: ban doc con han, dang hoat dong, chua vuot so sach toi da
-- 3) Tu dong doi trang thai ban sao khi muon/tra
-- 4) Tu dong dong/cap nhat trang thai phieu muon
-- 5) Tu dong tao/cap nhat tien phat khi tra tre
-- =========================================================
CREATE OR REPLACE FUNCTION fn_kiem_tra_ban_sao_san_sang()
RETURNS TRIGGER AS $$
DECLARE
    v_trang_thai VARCHAR(20);
BEGIN
    SELECT trang_thai INTO v_trang_thai
    FROM ban_sao_sach
    WHERE ban_sao_id = NEW.ban_sao_id;

    IF v_trang_thai IS NULL THEN
        RAISE EXCEPTION 'Ban sao sach % khong ton tai.', NEW.ban_sao_id;
    END IF;

    IF v_trang_thai <> 'SAN_SANG' THEN
        RAISE EXCEPTION 'Ban sao sach % khong san sang de muon. Trang thai hien tai: %', NEW.ban_sao_id, v_trang_thai;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION fn_dong_bo_trang_thai_ban_sao()
RETURNS TRIGGER AS $$
BEGIN
    IF TG_OP = 'INSERT' THEN
        UPDATE ban_sao_sach
        SET trang_thai = 'DANG_MUON'
        WHERE ban_sao_id = NEW.ban_sao_id;
        RETURN NEW;
    ELSIF TG_OP = 'UPDATE' THEN
        IF OLD.thoi_gian_tra IS NULL AND NEW.thoi_gian_tra IS NOT NULL THEN
            UPDATE ban_sao_sach
            SET trang_thai = 'SAN_SANG'
            WHERE ban_sao_id = NEW.ban_sao_id;
        END IF;
        RETURN NEW;
    END IF;
    RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION fn_dong_phieu_khi_tra_het()
RETURNS TRIGGER AS $$
BEGIN
    PERFORM fn_cap_nhat_tinh_trang_phieu_muon(NEW.phieu_muon_id);
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION fn_kiem_tra_dieu_kien_muon()
RETURNS TRIGGER AS $$
DECLARE
    v_ban_doc_id INT;
    v_dang_hoat_dong BOOLEAN;
    v_so_sach_dang_muon INT;
    v_so_sach_toi_da INT;
BEGIN
    SELECT pm.ban_doc_id INTO v_ban_doc_id
    FROM phieu_muon pm
    WHERE pm.phieu_muon_id = NEW.phieu_muon_id;

    IF v_ban_doc_id IS NULL THEN
        RAISE EXCEPTION 'Phieu muon % khong ton tai.', NEW.phieu_muon_id;
    END IF;

    SELECT bd.dang_hoat_dong,
    INTO v_dang_hoat_dong, v_han_the
    FROM ban_doc bd
    WHERE bd.ban_doc_id = v_ban_doc_id;

    IF COALESCE(v_dang_hoat_dong, FALSE) = FALSE THEN
        RAISE EXCEPTION 'Ban doc % dang bi khoa hoac khong hoat dong.', v_ban_doc_id;
    END IF;
    SELECT COALESCE(gia_tri::INT, 5) INTO v_so_sach_toi_da
    FROM cau_hinh_he_thong
    WHERE ma = 'SO_SACH_MUON_TOI_DA';

    SELECT COUNT(*) INTO v_so_sach_dang_muon
    FROM chi_tiet_muon ctm
    JOIN phieu_muon pm ON pm.phieu_muon_id = ctm.phieu_muon_id
    WHERE pm.ban_doc_id = v_ban_doc_id
      AND ctm.thoi_gian_tra IS NULL;

    IF v_so_sach_dang_muon >= COALESCE(v_so_sach_toi_da, 5) THEN
        RAISE EXCEPTION 'Ban doc % da muon toi da % sach.', v_ban_doc_id, COALESCE(v_so_sach_toi_da, 5);
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION fn_cap_nhat_tinh_trang_phieu_muon(p_phieu_muon_id INT)
RETURNS VOID AS $$
DECLARE
    v_con_chua_tra INT;
    v_ngay_hen_tra DATE;
    v_trang_thai_moi VARCHAR(20);
BEGIN
    SELECT pm.ngay_hen_tra INTO v_ngay_hen_tra
    FROM phieu_muon pm
    WHERE pm.phieu_muon_id = p_phieu_muon_id;

    IF v_ngay_hen_tra IS NULL THEN
        RETURN;
    END IF;

    SELECT COUNT(*) INTO v_con_chua_tra
    FROM chi_tiet_muon
    WHERE phieu_muon_id = p_phieu_muon_id
      AND thoi_gian_tra IS NULL;

    IF v_con_chua_tra = 0 THEN
        v_trang_thai_moi := 'DA_TRA';
        UPDATE phieu_muon
        SET trang_thai = v_trang_thai_moi,
            ngay_dong_phieu = COALESCE(ngay_dong_phieu, CURRENT_DATE)
        WHERE phieu_muon_id = p_phieu_muon_id
          AND trang_thai <> 'HUY';
    ELSE
        IF v_ngay_hen_tra < CURRENT_DATE THEN
            v_trang_thai_moi := 'QUA_HAN';
        ELSE
            v_trang_thai_moi := 'DANG_MUON';
        END IF;

        UPDATE phieu_muon
        SET trang_thai = v_trang_thai_moi,
            ngay_dong_phieu = NULL
        WHERE phieu_muon_id = p_phieu_muon_id
          AND trang_thai <> 'HUY';
    END IF;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION fn_tu_dong_tien_phat_khi_tra()
RETURNS TRIGGER AS $$
DECLARE
    v_ngay_hen_tra DATE;
    v_so_ngay_tre INT;
    v_tien_phat_moi_ngay NUMERIC(12,2);
    v_so_tien NUMERIC(12,2);
BEGIN
    IF OLD.thoi_gian_tra IS NOT NULL OR NEW.thoi_gian_tra IS NULL THEN
        RETURN NEW;
    END IF;

    SELECT ngay_hen_tra INTO v_ngay_hen_tra
    FROM phieu_muon
    WHERE phieu_muon_id = NEW.phieu_muon_id;

    IF v_ngay_hen_tra IS NULL THEN
        RETURN NEW;
    END IF;

    v_so_ngay_tre := GREATEST((NEW.thoi_gian_tra::DATE - v_ngay_hen_tra), 0);

    IF v_so_ngay_tre <= 0 THEN
        DELETE FROM tien_phat
        WHERE phieu_muon_id = NEW.phieu_muon_id
          AND ly_do = 'TRE_HAN'
          AND da_thanh_toan = FALSE;
        RETURN NEW;
    END IF;

    SELECT COALESCE(gia_tri::NUMERIC(12,2), 5000) INTO v_tien_phat_moi_ngay
    FROM cau_hinh_he_thong
    WHERE ma = 'TIEN_PHAT_MOI_NGAY';

    v_so_tien := v_so_ngay_tre * COALESCE(v_tien_phat_moi_ngay, 5000);

    INSERT INTO tien_phat (phieu_muon_id, ly_do, so_ngay_tre, so_tien, da_thanh_toan, ngay_tao, ghi_chu)
    VALUES (NEW.phieu_muon_id, 'TRE_HAN', v_so_ngay_tre, v_so_tien, FALSE, CURRENT_TIMESTAMP,
            'Tự động tạo khi trả sách trễ hạn')
    ON CONFLICT (phieu_muon_id)
    DO UPDATE SET
        ly_do = EXCLUDED.ly_do,
        so_ngay_tre = EXCLUDED.so_ngay_tre,
        so_tien = EXCLUDED.so_tien,
        ghi_chu = EXCLUDED.ghi_chu;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- =========================================================
-- FUNCTION KIEM TRA QUYEN
-- Su dung trong C# hoac query test nhanh
-- SELECT fn_kiem_tra_quyen('staff01','DANH_MUC_XOA');
-- =========================================================
CREATE OR REPLACE FUNCTION fn_kiem_tra_quyen(p_ten_dang_nhap VARCHAR, p_ma_quyen VARCHAR)
RETURNS BOOLEAN AS $$
DECLARE
    v_duoc_phep BOOLEAN;
BEGIN
    SELECT vq.duoc_phep INTO v_duoc_phep
    FROM tai_khoan_nguoi_dung tk
    JOIN vai_tro_quyen vq ON vq.vai_tro_id = tk.vai_tro_id
    JOIN chuc_nang cn ON cn.chuc_nang_id = vq.chuc_nang_id
    WHERE tk.ten_dang_nhap = p_ten_dang_nhap
      AND tk.dang_hoat_dong = TRUE
      AND cn.ma_quyen = p_ma_quyen
    LIMIT 1;

    RETURN COALESCE(v_duoc_phep, FALSE);
END;
$$ LANGUAGE plpgsql STABLE;

CREATE TRIGGER trg_kiem_tra_ban_sao_san_sang
BEFORE INSERT ON chi_tiet_muon
FOR EACH ROW
EXECUTE FUNCTION fn_kiem_tra_ban_sao_san_sang();

CREATE TRIGGER trg_kiem_tra_dieu_kien_muon
BEFORE INSERT ON chi_tiet_muon
FOR EACH ROW
EXECUTE FUNCTION fn_kiem_tra_dieu_kien_muon();

CREATE TRIGGER trg_dong_bo_trang_thai_ban_sao_insert
AFTER INSERT ON chi_tiet_muon
FOR EACH ROW
EXECUTE FUNCTION fn_dong_bo_trang_thai_ban_sao();

CREATE TRIGGER trg_dong_bo_trang_thai_ban_sao_update
AFTER UPDATE OF thoi_gian_tra ON chi_tiet_muon
FOR EACH ROW
EXECUTE FUNCTION fn_dong_bo_trang_thai_ban_sao();

CREATE TRIGGER trg_dong_phieu_khi_tra_het
AFTER UPDATE OF thoi_gian_tra ON chi_tiet_muon
FOR EACH ROW
WHEN (OLD.thoi_gian_tra IS NULL AND NEW.thoi_gian_tra IS NOT NULL)
EXECUTE FUNCTION fn_dong_phieu_khi_tra_het();

CREATE TRIGGER trg_tu_dong_tien_phat_khi_tra
AFTER UPDATE OF thoi_gian_tra ON chi_tiet_muon
FOR EACH ROW
WHEN (OLD.thoi_gian_tra IS NULL AND NEW.thoi_gian_tra IS NOT NULL)
EXECUTE FUNCTION fn_tu_dong_tien_phat_khi_tra();

-- =========================================================
-- DU LIEU MAU
-- =========================================================
INSERT INTO vai_tro (ten_vai_tro, mo_ta) VALUES
('ADMIN', 'Toàn quyền hệ thống'),
('STAFF', 'Nhân viên thư viện, bị hạn chế một số chức năng quản trị');

INSERT INTO chuc_nang (ma_quyen, ten_hien_thi, nhom, mo_ta) VALUES
('DASHBOARD_XEM', 'Xem trang tổng quan', 'HE_THONG', 'Truy cập màn hình chính'),
('TAI_KHOAN_XEM', 'Xem tài khoản', 'TAI_KHOAN', 'Xem danh sách tài khoản'),
('TAI_KHOAN_THEM', 'Thêm tài khoản', 'TAI_KHOAN', 'Tạo tài khoản mới'),
('TAI_KHOAN_SUA', 'Sửa tài khoản', 'TAI_KHOAN', 'Cập nhật thông tin tài khoản'),
('TAI_KHOAN_XOA', 'Xóa tài khoản', 'TAI_KHOAN', 'Xóa tài khoản'),
('DANH_MUC_XEM', 'Xem danh mục', 'DANH_MUC', 'Xem danh mục sách'),
('DANH_MUC_THEM', 'Thêm danh mục', 'DANH_MUC', 'Thêm danh mục sách'),
('DANH_MUC_SUA', 'Sửa danh mục', 'DANH_MUC', 'Sửa danh mục sách'),
('DANH_MUC_XOA', 'Xóa danh mục', 'DANH_MUC', 'Xóa danh mục sách'),
('SACH_XEM', 'Xem sách', 'SACH', 'Xem đầu sách'),
('SACH_THEM', 'Thêm sách', 'SACH', 'Thêm đầu sách'),
('SACH_SUA', 'Sửa sách', 'SACH', 'Cập nhật đầu sách'),
('SACH_XOA', 'Xóa sách', 'SACH', 'Xóa đầu sách'),
('BAN_SAO_XEM', 'Xem bản sao', 'BAN_SAO', 'Xem bản sao sách'),
('BAN_SAO_THEM', 'Thêm bản sao', 'BAN_SAO', 'Thêm bản sao sách'),
('BAN_SAO_SUA', 'Sửa bản sao', 'BAN_SAO', 'Cập nhật bản sao sách'),
('BAN_SAO_XOA', 'Xóa bản sao', 'BAN_SAO', 'Xóa bản sao sách'),
('BAN_DOC_XEM', 'Xem bạn đọc', 'BAN_DOC', 'Xem danh sách bạn đọc'),
('BAN_DOC_THEM', 'Thêm bạn đọc', 'BAN_DOC', 'Thêm bạn đọc'),
('BAN_DOC_SUA', 'Sửa bạn đọc', 'BAN_DOC', 'Cập nhật bạn đọc'),
('BAN_DOC_XOA', 'Xóa bạn đọc', 'BAN_DOC', 'Xóa bạn đọc'),
('PHIEU_MUON_XEM', 'Xem phiếu mượn', 'PHIEU_MUON', 'Xem danh sách phiếu mượn'),
('PHIEU_MUON_THEM', 'Lập phiếu mượn', 'PHIEU_MUON', 'Tạo phiếu mượn mới'),
('PHIEU_MUON_SUA', 'Sửa phiếu mượn', 'PHIEU_MUON', 'Cập nhật phiếu mượn'),
('PHIEU_MUON_XOA', 'Xóa phiếu mượn', 'PHIEU_MUON', 'Xóa phiếu mượn / hủy phiếu'),
('TRA_SACH', 'Trả sách', 'PHIEU_MUON', 'Thực hiện trả sách'),
('TIEN_PHAT_XEM', 'Xem tiền phạt', 'TIEN_PHAT', 'Xem các khoản phạt'),
('TIEN_PHAT_THEM', 'Thêm tiền phạt', 'TIEN_PHAT', 'Lập tiền phạt'),
('TIEN_PHAT_SUA', 'Sửa tiền phạt', 'TIEN_PHAT', 'Cập nhật tiền phạt'),
('TIEN_PHAT_XOA', 'Xóa tiền phạt', 'TIEN_PHAT', 'Xóa tiền phạt'),
('BAO_CAO_XEM', 'Xem báo cáo', 'BAO_CAO', 'Xem báo cáo thống kê'),
('NHAT_KY_XEM', 'Xem nhật ký hệ thống', 'NHAT_KY', 'Xem lịch sử thao tác');

-- ADMIN: duoc toan bo quyen
INSERT INTO vai_tro_quyen (vai_tro_id, chuc_nang_id, duoc_phep)
SELECT vt.vai_tro_id, cn.chuc_nang_id, TRUE
FROM vai_tro vt
CROSS JOIN chuc_nang cn
WHERE vt.ten_vai_tro = 'ADMIN';

-- STAFF: chi duoc cac chuc nang nghiep vu, khong duoc quan tri tai khoan; duoc phep xoa ban doc
INSERT INTO vai_tro_quyen (vai_tro_id, chuc_nang_id, duoc_phep)
SELECT vt.vai_tro_id, cn.chuc_nang_id, TRUE
FROM vai_tro vt
JOIN chuc_nang cn ON cn.ma_quyen IN (
    'DASHBOARD_XEM',
    'DANH_MUC_XEM',
    'SACH_XEM', 'BAN_SAO_XEM',
    'BAN_DOC_XEM', 'BAN_DOC_THEM', 'BAN_DOC_SUA', 'BAN_DOC_XOA',
    'PHIEU_MUON_XEM', 'PHIEU_MUON_THEM', 'PHIEU_MUON_SUA', 'PHIEU_MUON_XOA', 'TRA_SACH',
    'TIEN_PHAT_XEM', 'TIEN_PHAT_THEM', 'TIEN_PHAT_SUA',
    'BAO_CAO_XEM'
)
WHERE vt.ten_vai_tro = 'STAFF';

INSERT INTO tai_khoan_nguoi_dung (ten_dang_nhap, mat_khau_hash, ho_ten, email, sdt, vai_tro_id)
VALUES
('admin', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'Quản trị viên', 'admin@library.local', '0900000000', 1),
('staff01', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'Nhân viên 01', 'staff01@library.local', '0900000009', 2);

INSERT INTO danh_muc (ten, mo_ta) VALUES
('CNTT', 'Sách công nghệ thông tin'),
('Văn học', 'Tiểu thuyết, truyện ngắn, văn học Việt Nam và thế giới'),
('Kinh tế', 'Quản trị, marketing, tài chính, kinh doanh');

INSERT INTO sach (tieu_de, tac_gia, nha_xuat_ban, nam_xuat_ban, isbn, ngon_ngu, gia_bia, mo_ta, danh_muc_id)
VALUES
('Lập trình C# cơ bản', 'Nguyễn Văn A', 'NXB Giáo Dục', 2023, '1111111111', 'Tiếng Việt', 85000, 'Giới thiệu nền tảng lập trình C# và WinForms.', 1),
('Cơ sở dữ liệu', 'Trần Văn B', 'NXB Trẻ', 2022, '2222222222', 'Tiếng Việt', 92000, 'Kiến thức về mô hình quan hệ, SQL và thiết kế CSDL.', 1),
('Dế Mèn Phiêu Lưu Ký', 'Tô Hoài', 'NXB Kim Đồng', 2021, '3333333333', 'Tiếng Việt', 60000, 'Tác phẩm văn học thiếu nhi kinh điển.', 2),
('Nguyên lý kinh tế học', 'Lê Văn C', 'NXB Kinh Tế', 2020, '4444444444', 'Tiếng Việt', 115000, 'Tài liệu nhập môn kinh tế học.', 3);

INSERT INTO ban_sao_sach (sach_id, ma_ban_sao, trang_thai, vi_tri_ke, ngay_nhap, ghi_chu)
VALUES
(1, 'CS001', 'SAN_SANG', 'A1-01', CURRENT_DATE, NULL),
(1, 'CS002', 'SAN_SANG', 'A1-01', CURRENT_DATE, NULL),
(2, 'DB001', 'SAN_SANG', 'A1-02', CURRENT_DATE, NULL),
(3, 'VH001', 'SAN_SANG', 'B2-01', CURRENT_DATE, NULL),
(4, 'KT001', 'SAN_SANG', 'C1-03', CURRENT_DATE, NULL);

INSERT INTO ban_doc (ho_ten, ma_so, loai_ban_doc, gioi_tinh, ngay_sinh, email, sdt, dia_chi, dang_hoat_dong, ghi_chu)
VALUES
('Nguyễn Minh Duy', 'HS001', 'HOC_SINH', 'NAM', '2008-05-10', 'duy@gmail.com', '0900000001', 'Hà Nội', TRUE, NULL),
('Trần Văn Chính', 'GV001', 'GIAO_VIEN', 'NAM', '1989-03-12', 'chinh@gmail.com', '0900000002', 'Hà Nội', TRUE, NULL),
('Lê Thu Hà', 'SV001', 'SINH_VIEN', 'NU', '2004-09-21', 'hale@gmail.com', '0900000003', 'Hà Nội', TRUE, NULL);

INSERT INTO phieu_muon (ban_doc_id, ngay_muon, ngay_hen_tra, trang_thai, so_lan_gia_han, ghi_chu, tao_boi)
VALUES
(1, CURRENT_DATE - 10, CURRENT_DATE - 3, 'DA_TRA', 0, 'Phiếu mượn đã hoàn tất', 2),
(2, CURRENT_DATE - 2, CURRENT_DATE + 5, 'DANG_MUON', 0, 'Đang mượn bình thường', 2),
(3, CURRENT_DATE - 15, CURRENT_DATE - 5, 'QUA_HAN', 1, 'Quá hạn chưa trả đủ', 2);

INSERT INTO chi_tiet_muon (phieu_muon_id, ban_sao_id, thoi_gian_muon, thoi_gian_tra, tinh_trang_luc_muon, tinh_trang_luc_tra, tra_boi)
VALUES
(1, 1, CURRENT_TIMESTAMP - INTERVAL '10 day', CURRENT_TIMESTAMP - INTERVAL '2 day', 'Tốt', 'Tốt', 2),
(2, 2, CURRENT_TIMESTAMP - INTERVAL '2 day', NULL, 'Tốt', NULL, NULL),
(3, 3, CURRENT_TIMESTAMP - INTERVAL '15 day', NULL, 'Tốt', NULL, NULL);

UPDATE phieu_muon
SET ngay_dong_phieu = CURRENT_DATE - 2
WHERE phieu_muon_id = 1;

INSERT INTO tien_phat (phieu_muon_id, ly_do, so_ngay_tre, so_tien, da_thanh_toan, ngay_tao, ghi_chu)
VALUES
(3, 'TRE_HAN', 5, 25000, FALSE, CURRENT_TIMESTAMP, 'Phạt trễ hạn 5 ngày');

INSERT INTO cau_hinh_he_thong (ma, gia_tri, mo_ta) VALUES
('TIEN_PHAT_MOI_NGAY', '5000', 'Số tiền phạt cho mỗi ngày trễ hạn'),
('SO_SACH_MUON_TOI_DA', '5', 'Số lượng sách tối đa một bạn đọc được mượn cùng lúc'),
('SO_NGAY_MUON_MAC_DINH', '7', 'Số ngày mượn mặc định khi lập phiếu');

INSERT INTO nhat_ky_he_thong (tai_khoan_id, hanh_dong, doi_tuong, doi_tuong_id, mo_ta)
VALUES
(1, 'DANG_NHAP', 'TAI_KHOAN', 1, 'Tài khoản admin đăng nhập hệ thống'),
(2, 'TAO_PHIEU_MUON', 'PHIEU_MUON', 2, 'Nhân viên tạo phiếu mượn mẫu');

-- =========================================================
-- VIEW DE C# DOC QUYEN NHANH
-- =========================================================
CREATE OR REPLACE VIEW vw_tai_khoan_quyen AS
SELECT
    tk.tai_khoan_id,
    tk.ten_dang_nhap,
    vt.ten_vai_tro,
    cn.ma_quyen,
    cn.ten_hien_thi,
    cn.nhom,
    vq.duoc_phep
FROM tai_khoan_nguoi_dung tk
JOIN vai_tro vt ON vt.vai_tro_id = tk.vai_tro_id
JOIN vai_tro_quyen vq ON vq.vai_tro_id = vt.vai_tro_id
JOIN chuc_nang cn ON cn.chuc_nang_id = vq.chuc_nang_id
WHERE tk.dang_hoat_dong = TRUE;

CREATE OR REPLACE VIEW vw_ban_doc_muon_hien_tai AS
SELECT
    bd.ban_doc_id,
    bd.ho_ten,
    bd.ma_so,
    bd.loai_ban_doc,
    bd.email,
    bd.sdt,
    COUNT(ctm.chi_tiet_muon_id) FILTER (WHERE ctm.thoi_gian_tra IS NULL) AS so_sach_dang_muon,
    MAX(pm.ngay_hen_tra) FILTER (WHERE ctm.thoi_gian_tra IS NULL) AS han_tra_gan_nhat
FROM ban_doc bd
LEFT JOIN phieu_muon pm ON pm.ban_doc_id = bd.ban_doc_id AND pm.trang_thai <> 'HUY'
LEFT JOIN chi_tiet_muon ctm ON ctm.phieu_muon_id = pm.phieu_muon_id
GROUP BY bd.ban_doc_id, bd.ho_ten, bd.ma_so, bd.loai_ban_doc, bd.email, bd.sdt;

-- =========================================================
-- GOI Y CHO C#
-- 1. So sanh mat khau bang cach hash SHA-256 chuoi nguoi dung nhap
-- 2. Sau khi login, query view vw_tai_khoan_quyen hoac goi fn_kiem_tra_quyen()
-- 3. ADMIN: mo toan bo menu / nut
-- 4. STAFF: bi han che, dac biet khong co quyen DANH_MUC_XOA, TAI_KHOAN_*
--
-- Vi du test nhanh:
-- SELECT fn_kiem_tra_quyen('admin', 'DANH_MUC_XOA');   -- true
-- SELECT fn_kiem_tra_quyen('staff01', 'DANH_MUC_XOA'); -- false
-- SELECT * FROM vw_tai_khoan_quyen WHERE ten_dang_nhap = 'staff01' ORDER BY nhom, ma_quyen;
-- SELECT * FROM vw_ban_doc_muon_hien_tai ORDER BY ban_doc_id;
-- =========================================================
