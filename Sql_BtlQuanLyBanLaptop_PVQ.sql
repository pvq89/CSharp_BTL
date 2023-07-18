CREATE DATABASE BTL_QuanLyBanLaptop
GO
USE BTL_QuanLyBanLaptop
GO

CREATE TABLE tblLoaiHang(
	iMaLH INT IDENTITY(1,1) PRIMARY KEY,
	sTenHang NVARCHAR(50) NOT NULL
)
GO
CREATE TABLE tblMatHang(
	iMaMH INT IDENTITY(1,1) PRIMARY KEY,
	iMaLH INT NOT NULL,
	sTenHH  NVARCHAR(50) NOT NULL UNIQUE,
	fGiaBan FLOAT NOT NULL,
	iSoLuong int DEFAULT (0)
	FOREIGN KEY (iMaLH) REFERENCES tblLoaiHang(iMaLH)
)
GO
CREATE TABLE tblNhanVien(
	iMaNV	INT IDENTITY(1,1) PRIMARY KEY,
	sTen	NVARCHAR(100) NOT NULL,
	sSDT	VARCHAR(15) UNIQUE,
	sDiachi nvarchar(200),
	fLuong float,
	dNgaysinh date
)
GO
CREATE TABLE tblHoaDonNhap(
	iMaHD		INT IDENTITY(1,1) PRIMARY KEY,
	iMaNV		INT NOT NULL,
	dNgayTao	DATE DEFAULT GETDATE(),
	FOREIGN KEY (iMaNV) REFERENCES tblNhanVien(iMaNV) ON DELETE CASCADE
)

Alter table tblHoaDonNhap add fTongTien float default(0)
Alter table tblHoaDonNhap add sNhaCungCap nvarchar(50)
GO
CREATE TABLE tblChiTietHoaDonNhap(
	iMaHD INT NOT NULL,
	iMaMH INT NOT NULL,
	iSoLuong INT NOT NULL,
	fDonGia FLOAT NOT NULL,
	UNIQUE(iMaHD, iMaMH),
	FOREIGN KEY(iMaHD) REFERENCES tblHoaDonNhap(iMaHD) ON DELETE CASCADE,
	FOREIGN KEY(iMaMH) REFERENCES tblMatHang(iMaMH)
)
Alter table tblChiTietHoaDonNhap add fThanhTien float default (0)
GO
CREATE TABLE tblHoaDonBan(
	iMaHD INT IDENTITY(1,1) PRIMARY KEY,
	iMaNV INT NOT NULL,
	dNgayTao DATE DEFAULT GETDATE() NOT NULL,
	FOREIGN KEY(iMaNV) REFERENCES tblNhanVien(iMaNV)
)
Alter table tblHoaDonBan add fTongTien float default(0)
GO
--
CREATE TABLE tblChiTietHoaDonBan(
	iMaHD		INT NOT NULL,
	iMaMH		INT NOT NULL,
	iSoLuong	INT NOT NULL,
	iBaoHanh	INT NOT NULL,
	UNIQUE(iMaHD, iMaMH),
	FOREIGN KEY(iMaHD) REFERENCES tblHoaDonban(iMaHD) ON DELETE CASCADE,
	FOREIGN KEY(iMaMH) REFERENCES tblMatHang(iMaMH)
)
Alter table tblChiTietHoaDonBan add fThanhTien float default(0)
GO

--view tblNhanVien
Create view viewNhanVien([Mã NV],[Họ tên],[SĐT],[Địa chỉ],[Lương],[Ngày sinh],[Tuổi])
as		
	Select*from tblNhanVien
go

drop view viewNhanVien

--proc tblNhanVien
CREATE PROCEDURE procThemNV
    @tenNV NVARCHAR(100),
    @sdt NVARCHAR(15),
    @diaChi NVARCHAR(200),
    @luong float,
    @ngaySinh DATE
AS
BEGIN
    INSERT INTO tblNhanVien(sTen, sSDT, sDiachi, fLuong, dNgaysinh)
    VALUES(@tenNV, @sdt, @diaChi, @luong, @ngaySinh)
END
GO
--
CREATE PROCEDURE procCapNhatNV
    @maNV INT,
    @tenNV NVARCHAR(50),
    @sdt NVARCHAR(20),
    @diaChi NVARCHAR(100),
    @luong FLOAT,
    @ngaySinh DATE
AS
BEGIN
    UPDATE tblNhanVien SET 
        sTen = @tenNV,
        sSDT = @sdt,
        sDiachi = @diaChi,
        fLuong = @luong,
        dNgaysinh = @ngaySinh
		WHERE iMaNV = @maNV;
END;
GO
--
CREATE PROCEDURE procXoaNV
    @maNV INT
AS
BEGIN
    DELETE FROM tblNhanVien WHERE iMaNV = @maNV;
END
GO

--proc tblMatHang
/*CREATE PROC prMatHang_LoaiHang
AS
	BEGIN
	SELECT iMaMH,sTenHH,sTenHang,fGiaBan,iSoLuong
	FROM tblLoaiHang,tblMatHang
	WHERE tblLoaiHang.iMaLH=tblMatHang.iMaLH
END*/
--nên tạo thành view 
CREATE VIEW v_MatHang_LoaiHang([Mã MH],[Mặt hàng],[Loại hàng],[Giá bán],[Số lượng])
AS
	SELECT iMaMH,sTenHH,sTenHang,fGiaBan,iSoLuong
	FROM tblLoaiHang,tblMatHang
	WHERE tblLoaiHang.iMaLH=tblMatHang.iMaLH
GO
select *from v_MatHang_LoaiHang
--
CREATE PROC procThemMatHang(@tenLoaiHang NVARCHAR(50), @tenHH NVARCHAR(50),@soLuong INT, @giaBan FLOAT)
AS
BEGIN
	DECLARE @maLH INT;
	--lấy maLH bên tblLoaiHang 
	SELECT @maLH=iMaLH FROM tblLoaiHang WHERE sTenHang=@tenLoaiHang
	--insert
	INSERT INTO tblMatHang(iMaLH,sTenHH,iSoLuong,fGiaBan)
	VALUES (@maLH,@tenHH,@soLuong,@giaBan)
END
GO
--
CREATE PROC procSuaMatHang
(@mahang INT,@tenloaihang NVARCHAR(50), @tenHH NVARCHAR(50), @giaban FLOAT,@soLuong INT)
AS
BEGIN
	DECLARE @maLH INT;
	SELECT @maLH=iMaLH FROM tblLoaiHang WHERE sTenHang=@tenloaihang
	UPDATE tblMatHang
	SET iMaLH=@maLH,sTenHH=@tenHH,fGiaBan=@giaban,iSoLuong=@soLuong
	WHERE iMaMH=@mahang
END
GO
--
Create proc procXoaMatHang (@maMH int )
As
Begin
	Delete from tblMatHang where iMaMH=@maMH 
End

--Hoa don nhap
--trigger : sau khi thêm, sửa sẽ tự cập nhật
Create trigger triggerChiTietHoaDonNhap on tblChiTietHoaDonNhap
after insert, update
as
	Begin
		--Khai báo
		Declare @maHD int, @maMH int, @soLuong int, @donGia float, @thanhTien float
		-- Gán giá trị
		Select @maHD=iMaHD, @maMH=iMaMH, @soLuong=iSoLuong, @donGia=fDonGia, @thanhTien=fThanhTien
		from inserted
		--tính giá trị @thanhTien
		Select @thanhTien=@soLuong*@donGia;
		--Cập nhật fThanhTien trong tblChiTietHoaDonNhap
		Update tblChiTietHoaDonNhap
		set fThanhTien = @thanhTien where iMaHD=@maHD and iMaMH=@maMH
		--Cập nhật fTongTien trong tblHoaDonNhap 
		Update tblHoaDonNhap
		set fTongTien = (Select sum(fThanhTien) from tblChiTietHoaDonNhap where iMaHD=@maHD)
		where iMaHD=@maHD
		--Cập nhật số lượng hàng nhập vào tblMatHang
		Update tblMatHang set iSoLuong=iSoLuong+@soLuong where iMaMH = @maMH
			
	end
Select *from tblChiTietHoaDonNhap
--trigger khi xoá hoá đơn thì số lượng mặt hàng thay đổi 
Create trigger triggerXoaCTHoaDonNhap on tblChiTietHoaDonNhap
after delete
as
	Begin 
		Declare @maHD int, @maMH int, @soLuong int, @tongTien float
		--
		Select @maHD=iMaHD, @maMH=iMaMH, @soLuong=iSoLuong
		from deleted
		-- gán @tongTien 
		Select  @tongTien = ISNULL(sum(fThanhTien),0) 
		from tblChiTietHoaDonNhap where iMaHD=@maHD
		--
		Update tblHoaDonNhap set fTongTien=@tongTien where iMaHD=@maHD
		-- Nếu xoá thì trừ đi số lượng mặt hàng trên hoá đơn
		Update tblMatHang set iSoLuong -= @soLuong where iMaMH=@maMH
	end

--view hoadonnhap
Create view viewHoaDonNhap
as
	Select hdn.iMaHD, hdn.iMaNV,nv.sTen,hdn.sNhaCungCap, hdn.dNgayTao, hdn.fTongTien
	from tblHoaDonNhap hdn, tblNhanVien nv
	where hdn.iMaNV=nv.iMaNV

Select *from viewHoaDonNhap
go
--procThemHoaDonNhap
Create proc procThemHoaDonNhap (@maNV int, @nhaCungCap nvarchar(50))
as
	begin 
		Insert into tblHoaDonNhap (iMaNV, sNhaCungCap) values (@maNV, @nhaCungCap)
	end
--proc suaHoaDonNhap
Create proc procSuaHoaDonNhap(@maHD int, @nhaCungCap nvarchar(50))
as
	Begin 
		Update tblHoaDonNhap set sNhaCungCap = @nhaCungCap where iMaHD=@maHD
	end

-- Chi tiết hd nhập
Create view viewCTHoaDonNhap
as
	SELECT a.iMaHD, b.iMaLH,a.iMaMH,d.sTenHang,b.sTenHH, c.sTen as 'TenNVLap', c.sNhaCungCap ,
		a.iSoLuong, a.fDonGia, a.fThanhTien
	FROM tblChiTietHoaDonNhap a
	INNER JOIN tblMatHang b ON a.iMaMH = b.iMaMH
	INNER JOIN viewHoaDonNhap c ON c.iMaHD = a.iMaHD
	INNER JOIN tblLoaiHang d on b.iMaLH=d.iMaLH
Select *from viewCTHoaDonNhap

-- proc hiện ra hoá đơn có mã tương ứng
Create proc procCTHoaDonNhap(@maHD int)
as
	begin 
		If @maHD!=0
			Select *from viewCTHoaDonNhap where iMaHD = @maHD
		else
			Select *from viewCTHoaDonNhap
	end
exec procCTHoaDonNhap @maHD=1

--proc khi chọn loại hàng thì sẽ hiện ra các mặt hàng của loại hàng đó
Create proc procHienMatHangCuaLoaiHang(@maLH int)
as
	begin
		Select *from tblMatHang where iMaLH=@maLH
	end

exec procHienMatHangCuaLoaiHang @maLH=1
-- proc khi chọn mặt hàng hiện ra số lượng tương ứng
Create proc procSoLuongMatHang(@maMH int)
as
	Begin 
		Select iSoLuong from tblMatHang where iMaMH=@maMH
	end
exec procSoLuongMatHang @maMH=3

--proc them chi tiet hoa don nhap
Create proc procThemCTHoaDonNhap (@maHD int, @maMH int, @soLuong int, @donGia float)
as
	Begin 
		Insert into tblChiTietHoaDonNhap(iMaHD,iMaMH,iSoLuong,fDonGia) values (@maHD,@maMH,@soLuong,@donGia)
	end


--proc sửa ct hoá đơn nhập
Create proc procSuaCTHoaDonNhap(@maHD int, @maMH int, @soLuong int, @donGia float)
as
	Begin 
		Update tblChiTietHoaDonNhap set iSoLuong=@soLuong,fDonGia=@donGia
		where iMaHD = @maHD and iMaMH = @maMH
	end

select *from tblChiTietHoaDonNhap
--proc xoá ct hoá đơn nhập
Create proc procXoaCTHoaDonNhap (@maHD int, @maMH int)
as
	Begin 
		Delete from tblChiTietHoaDonNhap where iMaHD=@maHD and iMaMH=@maMH
	end

-- hoá đơn bán
--
Create trigger triggerCTHoaDonBan on tblChiTietHoaDonBan 
after insert, update
as
	Begin 
		Declare @maHD int,@maMH int,@soLuong int,@donGia float,@thanhTien float
		Select @maHD = iMaHD,@maMH = iMaMH,@soLuong = iSoLuong from inserted
		--Lấy đơn giá ở bảng mặt hàng 
		Select @donGia = fGiaBan from tblMatHang where iMaMH=@maMH 
		--tính fThanhTien = soLuong*donGia
		Select @ThanhTien = @soLuong * @donGia
		--Update tblChiTietHoaDonBan
		Update tblChiTietHoaDonBan set fThanhTien = @thanhTien where iMaHD=@maHD and iMaMH=@maMH
		--Update tblHoaDonBan
		Update tblHoaDonBan 
		set fTongTien=(Select sum(fThanhTien) from tblChiTietHoaDonBan where iMaHD=@maHD)
		where iMaHD=@maHD
		--Update lại số lượng trên tblMatHang
		Update tblMatHang set iSoLuong =a.iSoLuong - @soLuong from tblMatHang a where a.iMaMH=@maMH
	end
--
Create trigger triggerXoaCTHoaDonBan on tblChiTietHoaDonBan
after delete
as
	Begin 
		Declare @maHD int, @maMH int, @tongTien float, @soLuong int
		--
		Select @maHD = iMaHD, @MaMH = iMaMH, @soLuong=iSoLuong
		from deleted
		--Đặt lại giá trị tổng tiền
		Select @tongTien=  ISNULL(fThanhTien,0) from tblChiTietHoaDonBan where iMaHD=@maHD
		--Update cho tblHDban
		Update tblHoaDonBan set fTongTien = @tongTien where iMaHD=@maHD
		-- Nếu xoá hd => ko bán dc => hàng tồn tăng lại như ban đầu
		Update tblMatHang set iSoLuong +=@soLuong where iMaMH=iMaMH
	end

--viewHDBan
Create view viewHDBan 
as
	Select iMaHD, tblHoaDonBan.iMaNV, sTen as[sNhanVienLap], dNgayTao, fTongTien
	from tblHoaDonBan inner join tblNhanVien on tblHoaDonBan.iMaNV=tblNhanVien.iMaNV
--procThemHDBan
Create proc procThemHoaDonBan (@maNV int)
as
	Begin 
		Insert into tblHoaDonBan (iMaNV) values (@maNV)
	end
--procXoaHDBan
GO
Create proc prXoaHDban(@iMaHD int)
as
	Delete tblHoaDonBan
	where iMaHD = @iMaHD
--ProcSuaHDBan
Go
Create proc procSuaHDban(@iMaNV int, @iMaHD int)
AS
	Update tblHoaDonBan
	set iMaNV = @iMaNV
	where iMaHD = @iMaHD

--proc tim kiem
Create proc procTimKiemTheoGiaHoaDonBan (@min float, @max float)
as
	begin 
		Select *from viewHDBan where fTongTien between @min and @max
	end

--ChiTietHoaDonBan
CREATE VIEW viewCTHDBan
AS 
	SELECT a.iMaHD[Mã hoá đơn], b.iMaLH[Mã Loại hàng], b.iMaMH[Mã mặt hàng],d.sTenHang[Loại hàng],b.sTenHH[Sản phẩm],c.sNhanVienLap[Nhân viên], a.iSoLuong[Số lượng], b.fGiaBan[Giá bán], a.iBaoHanh[Bảo hành], a.fThanhTien[Thành tiền]
	FROM tblChiTietHoaDonBan a
	INNER JOIN tblMatHang b
	ON a.iMaMH = b.iMaMH
	INNER JOIN viewHDBan c 
	ON c.iMaHD = a.iMaHD
	INNER JOIN tblLoaiHang d
	ON d.iMaLH=b.iMaLH

	drop view viewCTHDBan
--proc hiện ct hoá đơn bán theo mã truyền vào
CREATE PROC procHienCTHDBan(@iMaHD INT)
AS
	if @iMaHD = 0
		SELECT * FROM viewCTHDBan
	else
		SELECT * FROM viewCTHDBan
		WHERE [Mã hoá đơn] = @iMaHD
		drop proc procHienCTHDBan
exec procHienCTHDBan @iMaHD=1
--proc themCTHDBan
CREATE PROC procThemCTHDBan(@iMaHD INT, @iMaMH INT, @iSoLuong INT, @iBaoHanh INT)
AS
	INSERT INTO tblChiTietHoaDonBan(iMaHD, iMaMH, iSoLuong, iBaoHanh)
	VALUES (@iMaHD, @imaMH, @iSoLuong, @iBaoHanh)

	select *from tblChiTietHoaDonBan
--proc xoáCTHDBan
Create proc procXoaCTHDBan (@maHD int, @maMH int)
as
	Begin
		Delete tblChiTietHoaDonBan where iMaHD=@maHD and iMaMH=@maMH
	end

--proc suaCTHDBan
CREATE PROC procSuaCTHDBan(
					@iMaHD INT,
					@iMaMH INT,
					@iSoluong INT,
					@iBaohanh INT,
					@sTenMHCu NVARCHAR(100))
AS
	DECLARE @iSoLuongCu INT
	DECLARE @iMaMHCu INT
	--set iMaHDCu
	Select @iMaMHCu = (SELECT iMaMH FROM tblMatHang WHERE sTenHH = @sTenMHCu)
	--set soluong = số lượng của mặt hàng trong hoá đơn cũ
	SELECT @iSoLuongCu = isNull(iSoLuong,0) 
	FROM tblChiTietHoaDonBan
	WHERE iMaHD = @iMaHD AND iMaMH = @iMaMHCu
	
	UPDATE tblChiTietHoaDonBan
	SET iSoLuong = @iSoluong,
		iBaoHanh = @iBaohanh,
		iMaMH = @iMaMH --mã hoá đơn truyền vào
	WHERE iMaHD = @iMaHD AND iMaMH=@iMaMHCu
	
	-- Update lại số lượng
	UPDATE tblMatHang
	SET  iSoLuong +=  @iSoLuongCu
	FROM tblMatHang a
	WHERE a.iMaMH = @iMaMHCu
GO
-- Report 
Create view viewRptHoaDonBan
as
	Select a.iMaHD, a.iMaNV, a.dNgayTao, a.fTongTien, b.sTen, b.sDiaChi, b.sSDT,
			c.iSoLuong, c.iBaoHanh, c.fThanhTien, c.iMaMH, c.sTenHH, c.fGiaBan
	from tblHoaDonBan a 
	inner join tblNhanVien b on a.iMaNV = b.iMaNV
	inner join viewCTHDBan c on a.iMaHD = c.iMaHD	
	
Create proc rptHoaDonBan (@iMaHD int)
as
	Select *from viewRptHoaDonBan where iMaHD = @iMaHD

exec rptHoaDonBan @iMaHD = 2
--
Create view viewRptHoaDonNhap 
as 
	Select a.iMaHD, a.TenNVLap, a.sTenHH, a.fDonGia,a.fThanhTien, a.iSoLuong,a.sNhaCungCap,
			b.dNgayTao, b.iMaNV, b.fTongTien, c.sDiachi, c.sSDT
	from viewCTHoaDonNhap a 
	inner join viewHoaDonNhap b on a.iMaHD=b.iMaHD
	inner join tblNhanVien c on b.iMaNV=c.iMaNV


Create proc rptHoaDonNhap (@iMaHD int)
as
	Begin 
		Select *from viewRptHoaDonNhap where iMaHD=@iMaHD
	end

exec rptHoaDonNhap @iMaHD = 1


	




----Test
--SELECT * FROM tblNhanVien ORDER BY fLuong ASC;
--select count(*) from tblNhanVien where sDiachi like '%Nam dinh%'
--Select count(*) from tblNhanVien where sSDT='012345678'

--Select sTenHang from tblLoaiHang
--Select *from v_MatHang_LoaiHang where [Loại hàng]='Ban phim co'
----proc report
--create proc testProcRpt (@loaiHang nvarchar(50))
--as
--	begin 
--		Select *from v_MatHang_LoaiHang where [Loại hàng] like N'%@loaiHang%'
--	end
--	exec testProcRpt @loaiHang=N'Ban phim co'

--	select *from tblChiTietHoaDonBan

/**select top(5) from tblNhanVien where  


Create view top5 as
SELECT TOP(5)*
FROM tblMatHang
ORDER BY fGiaBan DESC;

select *from tblMatHang


select 

Alter table tblNhanVien add iTuoi int

Create trigger triggerTinhTuoi
on tblNhanVien
alter insert, update
as
begin
    if update (dNgaySinh)
    begin
        update tblNhanVien
        set iTuoi = datediff(YEAR, dNgaySinh, GETDATE())
        where iMaNV IN (select iMaNV from inserted);
    end
end

Select *from tblNhanVien**/