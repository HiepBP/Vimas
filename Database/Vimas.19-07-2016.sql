USE [master]
GO
/****** Object:  Database [Vimas]    Script Date: 19/07/2016 8:02:05 PM ******/
CREATE DATABASE [Vimas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Vimas', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Vimas.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Vimas_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Vimas_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Vimas] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Vimas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Vimas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Vimas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Vimas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Vimas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Vimas] SET ARITHABORT OFF 
GO
ALTER DATABASE [Vimas] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Vimas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Vimas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Vimas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Vimas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Vimas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Vimas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Vimas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Vimas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Vimas] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Vimas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Vimas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Vimas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Vimas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Vimas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Vimas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Vimas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Vimas] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Vimas] SET  MULTI_USER 
GO
ALTER DATABASE [Vimas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Vimas] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Vimas] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Vimas] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Vimas] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Vimas]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BangCap]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BangCap](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdThongTinDuTuyen] [int] NOT NULL,
	[Thang] [int] NULL,
	[Nam] [int] NULL,
	[BangCap] [nvarchar](50) NULL,
	[DaNop] [bit] NULL,
	[TrinhDo] [nvarchar](50) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_BangCap] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BienBan]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BienBan](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idThongTinCaNhan] [int] NOT NULL,
	[GhiChu] [nvarchar](100) NOT NULL,
	[HinhAnh] [nvarchar](2803) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_BienBan] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CongTyChungNghe]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CongTyChungNghe](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenVietTat] [nvarchar](50) NULL,
	[TenTiengAnh] [nvarchar](100) NULL,
	[TenTiengViet] [nvarchar](100) NULL,
	[DiaChiTiengAnh] [nvarchar](500) NULL,
	[DiaChiTiengViet] [nvarchar](500) NULL,
	[DienThoai] [nvarchar](20) NULL,
	[NguoiDaiDien] [nvarchar](50) NULL,
	[ChucDanh] [nvarchar](50) NULL,
	[VonDieuLe] [decimal](18, 0) NULL,
	[SoNhanVien] [int] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CongTyChungNghe] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CongTyTiepNhan]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CongTyTiepNhan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenTiengAnh] [nvarchar](100) NULL,
	[TenTiengNhat] [nvarchar](100) NULL,
	[NganhNghe] [nvarchar](50) NULL,
	[NguoiDaiDien] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](500) NULL,
	[DienThoai] [nvarchar](20) NULL,
	[Fax] [nvarchar](20) NULL,
	[idNghiepDoan] [int] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CongTyTiepNhan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HopDongDOLAB]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HopDongDOLAB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NgayDangKy] [datetime] NULL,
	[NgayNhan] [datetime] NULL,
	[SoDKHopDong] [nvarchar](50) NULL,
	[SoCongVan] [nvarchar](50) NULL,
	[SoPhieuTiepNhan] [nvarchar](50) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_HopDongDOLAB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HopDongDOLABHocVienMapping]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HopDongDOLABHocVienMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdThongTinCaNhan] [int] NULL,
	[IdHopDongDOLAB] [int] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_HopDongDOLABHocVienMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KyTucXa]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KyTucXa](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdThongTinCaNhan] [int] NULL,
	[NgayVao] [datetime] NULL,
	[NgayRa] [datetime] NULL,
	[SoPhong] [nvarchar](20) NULL,
	[SoHocTuDo] [nvarchar](20) NULL,
	[GhiChu] [nvarchar](500) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_KyTucXa] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NghiepDoan]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NghiepDoan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaNghiepDoan] [nvarchar](50) NULL,
	[TenNghiepDoan] [nvarchar](100) NULL,
	[TenVietTat] [nvarchar](50) NULL,
	[NguoiDaiDien] [nvarchar](50) NULL,
	[ChucDanh] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](500) NULL,
	[DienThoai] [nvarchar](20) NULL,
	[Fax] [nvarchar](20) NULL,
	[NgayKyHopDong] [datetime] NULL,
	[LuongCoBan] [decimal](18, 0) NULL,
	[PhiDichVu] [decimal](18, 0) NULL,
	[PhiUTDT] [decimal](18, 0) NULL,
	[WebsiteUrl] [nvarchar](2083) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_NghiepDoan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuaTrinhHocTap]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuaTrinhHocTap](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdThongTinCaNhan] [int] NOT NULL,
	[TuNam] [int] NULL,
	[DenNam] [int] NULL,
	[TenTruong] [nvarchar](80) NULL,
	[LoaiTruong] [int] NULL,
	[NganhHoc] [nvarchar](50) NULL,
	[DaTotNghiep] [bit] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_QuaTrinhHocTap] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuaTrinhLamViec]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuaTrinhLamViec](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdThongTinCaNhan] [int] NOT NULL,
	[TuNam] [int] NULL,
	[DenNam] [int] NULL,
	[TenCongTy] [nvarchar](200) NULL,
	[HinhThucCongTy] [nvarchar](80) NULL,
	[DiaChiCongTy] [nvarchar](300) NULL,
	[ChiTietCongViec] [nvarchar](500) NULL,
	[DangLam] [bit] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_QuaTrinhLamViec] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SucKhoe]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SucKhoe](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdThongTinCaNhan] [int] NOT NULL,
	[ChieuCao] [nvarchar](10) NULL,
	[CanNang] [nvarchar](10) NULL,
	[NhomMau] [varchar](2) NULL,
	[TayThuan] [int] NULL,
	[ThiLucMatTrai] [int] NULL,
	[ThiLucMatPhai] [int] NULL,
	[HinhXam] [bit] NULL,
	[NgayKhamDot1] [datetime] NULL,
	[GhiChuSucKhoeDot1] [nvarchar](800) NULL,
	[NgayKhamDot2] [datetime] NULL,
	[GhiChuSucKhoeDot2] [nvarchar](800) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_SucKhoe] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SystemLog]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemLog](
	[no] [int] IDENTITY(1,1) NOT NULL,
	[id] [int] NULL,
	[TenBang] [nvarchar](50) NULL,
	[HanhDong] [nvarchar](50) NULL,
	[ThucHienBoi] [nvarchar](255) NULL,
	[NgayThucHien] [datetime] NULL,
 CONSTRAINT [PK_SystemLog] PRIMARY KEY CLUSTERED 
(
	[no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ThongTinCaNhan]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ThongTinCaNhan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaLuuHoSo] [nvarchar](50) NOT NULL,
	[IdTrungTamGTVL] [int] NOT NULL,
	[HoTen] [nvarchar](80) NOT NULL,
	[TenPhienAmNhat] [nvarchar](100) NOT NULL,
	[GioiTinh] [int] NOT NULL,
	[NgaySinh] [datetime] NOT NULL,
	[NoiSinh] [nvarchar](50) NOT NULL,
	[TrinhDoVanHoa] [int] NULL,
	[TinhTrangGiaDinh] [int] NOT NULL,
	[CMND] [nvarchar](12) NOT NULL,
	[NgayCap] [datetime] NOT NULL,
	[NoiCap] [nvarchar](50) NOT NULL,
	[SoHoChieu] [nvarchar](12) NULL,
	[NgayCapHC] [date] NULL,
	[NoiCapHC] [nvarchar](50) NULL,
	[NgayHetHan] [date] NULL,
	[HoKhau] [nvarchar](300) NULL,
	[DiaChiLienLac] [nvarchar](300) NULL,
	[DiaChiTiengAnh] [nvarchar](300) NULL,
	[DienThoaiDiDong] [varchar](12) NULL,
	[DienThoaiNha] [varchar](20) NULL,
	[CoGiay] [int] NULL,
	[SizeQuanAo] [varchar](5) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_ThongTinCaNhan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ThongTinDuTuyen]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongTinDuTuyen](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdThongTinCaNhan] [int] NOT NULL,
	[NgayDangKy] [datetime] NOT NULL,
	[UuDiem] [nvarchar](500) NULL,
	[NhuocDiem] [nvarchar](500) NULL,
	[KyNangKhac] [nvarchar](500) NULL,
	[SoThich] [nvarchar](500) NULL,
	[LyDoDiNhat] [nvarchar](500) NULL,
	[VeNuocLamGi] [nvarchar](500) NULL,
	[NguoiGioiThieu] [nvarchar](50) NULL,
	[BanBeBenNhat] [nvarchar](50) NULL,
	[AnhChiBenNhat] [nvarchar](50) NULL,
	[AnhChiDangKyOVimas] [nvarchar](50) NULL,
	[DaDKDiNhatOCongtyKhac] [bit] NOT NULL,
	[DaDiNuocNgoai] [bit] NOT NULL,
	[DaDiNhat] [bit] NOT NULL,
	[GhiChuSoVan] [nvarchar](500) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_ThongTinDuTuyen] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ThongTinGiaDinh]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongTinGiaDinh](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdThongTinCaNhan] [int] NOT NULL,
	[HoTen] [nvarchar](50) NOT NULL,
	[QuanHe] [int] NULL,
	[NgaySinh] [datetime] NOT NULL,
	[NgheNghiep] [nvarchar](50) NULL,
	[NoiLamViec] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](300) NULL,
	[SoDienThoai] [nvarchar](20) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_ThongTinGiaDinh] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ThongTinNopTien]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongTinNopTien](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdThongTinCaNhan] [int] NOT NULL,
	[LoaiTien] [int] NOT NULL,
	[SoPhieu] [nvarchar](50) NOT NULL,
	[NgayLapPhieu] [datetime] NOT NULL,
	[ThuHayChi] [int] NOT NULL,
	[SoTien] [decimal](18, 0) NOT NULL,
	[LyDo] [nvarchar](50) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_ThongTinNopTien] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ThongTinPhongVan]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongTinPhongVan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdThongTinCaNhan] [int] NOT NULL,
	[NgayPhongVan] [datetime] NULL,
	[NghePhongVan] [nvarchar](50) NULL,
	[GhiChuPV] [nvarchar](500) NULL,
	[NgayTrungTuyen] [datetime] NULL,
	[DotTrungTuyen] [nvarchar](50) NULL,
	[NgheTrungTuyenTiengAnh] [nvarchar](50) NULL,
	[NgheTrungTuyenTiengViet] [nvarchar](50) NULL,
	[IdCongTyTiepNhan] [int] NULL,
	[ThoiHanHopDong] [int] NULL,
	[GhiChuSauTrungTuyen] [nvarchar](500) NULL,
	[LopHoc] [nvarchar](50) NULL,
	[NgayNhapHoc] [datetime] NULL,
	[NgayKetThucKhoaHoc] [datetime] NULL,
	[GhiChuKhenThuongKyLuat] [nvarchar](500) NULL,
	[IdCongTyChungNghe] [int] NULL,
	[SoPhieuTiepNhan] [nvarchar](50) NULL,
	[GhiChuPhaiCu] [nvarchar](500) NULL,
	[HopDongTTS] [nvarchar](50) NULL,
	[NgayKyHopDong] [datetime] NULL,
	[HinhAnh] [nvarchar](2083) NULL,
	[NgayHuySauKhiTrungTuyen] [datetime] NULL,
	[LyDoHuy] [nvarchar](500) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_ThongTinPhongVan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ThongTinVeNuoc]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongTinVeNuoc](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdThongTinCaNhan] [int] NOT NULL,
	[NgayDi] [datetime] NULL,
	[NgayVe] [datetime] NULL,
	[LyDoVe] [nvarchar](500) NULL,
	[ThanhLyHopDong] [bit] NULL,
	[NgayThanhLy] [datetime] NULL,
	[SoHopDongThanhLy] [nvarchar](50) NULL,
	[NgayTron] [datetime] NULL,
	[NgayBiTrucXuat] [datetime] NULL,
	[GhiChuTheoDoi] [nvarchar](500) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_ThongTinVeNuoc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TrungTamGTVL]    Script Date: 19/07/2016 8:02:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrungTamGTVL](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaNguon] [nvarchar](50) NOT NULL,
	[TenCoSo] [nvarchar](100) NOT NULL,
	[DiaChi] [nvarchar](500) NULL,
	[DienThoai] [nvarchar](20) NULL,
	[Fax] [nvarchar](20) NULL,
	[SoHDLK] [nvarchar](50) NULL,
	[NgayKyHopDong] [datetime] NULL,
	[NgayHetHan] [datetime] NULL,
	[NguoiDaiDien] [nvarchar](50) NULL,
	[ChucDanh] [nvarchar](50) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_TrungTamGTVL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 19/07/2016 8:02:05 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 19/07/2016 8:02:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 19/07/2016 8:02:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 19/07/2016 8:02:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 19/07/2016 8:02:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 19/07/2016 8:02:05 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[BangCap]  WITH CHECK ADD  CONSTRAINT [FK_BangCap_ThongTinDuTuyen] FOREIGN KEY([IdThongTinDuTuyen])
REFERENCES [dbo].[ThongTinDuTuyen] ([Id])
GO
ALTER TABLE [dbo].[BangCap] CHECK CONSTRAINT [FK_BangCap_ThongTinDuTuyen]
GO
ALTER TABLE [dbo].[BienBan]  WITH CHECK ADD  CONSTRAINT [FK_BienBan_ThongTinCaNhan] FOREIGN KEY([idThongTinCaNhan])
REFERENCES [dbo].[ThongTinCaNhan] ([Id])
GO
ALTER TABLE [dbo].[BienBan] CHECK CONSTRAINT [FK_BienBan_ThongTinCaNhan]
GO
ALTER TABLE [dbo].[CongTyTiepNhan]  WITH CHECK ADD  CONSTRAINT [FK_CongTyTiepNhan_NghiepDoan] FOREIGN KEY([idNghiepDoan])
REFERENCES [dbo].[NghiepDoan] ([Id])
GO
ALTER TABLE [dbo].[CongTyTiepNhan] CHECK CONSTRAINT [FK_CongTyTiepNhan_NghiepDoan]
GO
ALTER TABLE [dbo].[HopDongDOLABHocVienMapping]  WITH CHECK ADD  CONSTRAINT [FK_HopDongDOLAB_HocVien_HopDongDOLAB] FOREIGN KEY([IdHopDongDOLAB])
REFERENCES [dbo].[HopDongDOLAB] ([Id])
GO
ALTER TABLE [dbo].[HopDongDOLABHocVienMapping] CHECK CONSTRAINT [FK_HopDongDOLAB_HocVien_HopDongDOLAB]
GO
ALTER TABLE [dbo].[HopDongDOLABHocVienMapping]  WITH CHECK ADD  CONSTRAINT [FK_HopDongDOLAB_HocVien_ThongTinCaNhan] FOREIGN KEY([IdThongTinCaNhan])
REFERENCES [dbo].[ThongTinCaNhan] ([Id])
GO
ALTER TABLE [dbo].[HopDongDOLABHocVienMapping] CHECK CONSTRAINT [FK_HopDongDOLAB_HocVien_ThongTinCaNhan]
GO
ALTER TABLE [dbo].[KyTucXa]  WITH CHECK ADD  CONSTRAINT [FK_KyTucXa_ThongTinCaNhan] FOREIGN KEY([IdThongTinCaNhan])
REFERENCES [dbo].[ThongTinCaNhan] ([Id])
GO
ALTER TABLE [dbo].[KyTucXa] CHECK CONSTRAINT [FK_KyTucXa_ThongTinCaNhan]
GO
ALTER TABLE [dbo].[QuaTrinhHocTap]  WITH CHECK ADD  CONSTRAINT [FK_QuaTrinhHocTap_ThongTinCaNhan] FOREIGN KEY([IdThongTinCaNhan])
REFERENCES [dbo].[ThongTinCaNhan] ([Id])
GO
ALTER TABLE [dbo].[QuaTrinhHocTap] CHECK CONSTRAINT [FK_QuaTrinhHocTap_ThongTinCaNhan]
GO
ALTER TABLE [dbo].[QuaTrinhLamViec]  WITH CHECK ADD  CONSTRAINT [FK_QuaTrinhLamViec_ThongTinCaNhan] FOREIGN KEY([IdThongTinCaNhan])
REFERENCES [dbo].[ThongTinCaNhan] ([Id])
GO
ALTER TABLE [dbo].[QuaTrinhLamViec] CHECK CONSTRAINT [FK_QuaTrinhLamViec_ThongTinCaNhan]
GO
ALTER TABLE [dbo].[SucKhoe]  WITH CHECK ADD  CONSTRAINT [FK_SucKhoe_ThongTinCaNhan] FOREIGN KEY([IdThongTinCaNhan])
REFERENCES [dbo].[ThongTinCaNhan] ([Id])
GO
ALTER TABLE [dbo].[SucKhoe] CHECK CONSTRAINT [FK_SucKhoe_ThongTinCaNhan]
GO
ALTER TABLE [dbo].[ThongTinCaNhan]  WITH CHECK ADD  CONSTRAINT [FK_ThongTinCaNhan_TrungTamGTVL] FOREIGN KEY([IdTrungTamGTVL])
REFERENCES [dbo].[TrungTamGTVL] ([Id])
GO
ALTER TABLE [dbo].[ThongTinCaNhan] CHECK CONSTRAINT [FK_ThongTinCaNhan_TrungTamGTVL]
GO
ALTER TABLE [dbo].[ThongTinDuTuyen]  WITH CHECK ADD  CONSTRAINT [FK_ThongTinDuTuyen_ThongTinCaNhan] FOREIGN KEY([IdThongTinCaNhan])
REFERENCES [dbo].[ThongTinCaNhan] ([Id])
GO
ALTER TABLE [dbo].[ThongTinDuTuyen] CHECK CONSTRAINT [FK_ThongTinDuTuyen_ThongTinCaNhan]
GO
ALTER TABLE [dbo].[ThongTinGiaDinh]  WITH CHECK ADD  CONSTRAINT [FK_ThongTinGiaDinh_ThongTinCaNhan] FOREIGN KEY([IdThongTinCaNhan])
REFERENCES [dbo].[ThongTinCaNhan] ([Id])
GO
ALTER TABLE [dbo].[ThongTinGiaDinh] CHECK CONSTRAINT [FK_ThongTinGiaDinh_ThongTinCaNhan]
GO
ALTER TABLE [dbo].[ThongTinNopTien]  WITH CHECK ADD  CONSTRAINT [FK_ThongTinNopTien_ThongTinCaNhan] FOREIGN KEY([IdThongTinCaNhan])
REFERENCES [dbo].[ThongTinCaNhan] ([Id])
GO
ALTER TABLE [dbo].[ThongTinNopTien] CHECK CONSTRAINT [FK_ThongTinNopTien_ThongTinCaNhan]
GO
ALTER TABLE [dbo].[ThongTinPhongVan]  WITH CHECK ADD  CONSTRAINT [FK_ThongTinPhongVan_CongTyChungNghe1] FOREIGN KEY([IdCongTyChungNghe])
REFERENCES [dbo].[CongTyChungNghe] ([Id])
GO
ALTER TABLE [dbo].[ThongTinPhongVan] CHECK CONSTRAINT [FK_ThongTinPhongVan_CongTyChungNghe1]
GO
ALTER TABLE [dbo].[ThongTinPhongVan]  WITH CHECK ADD  CONSTRAINT [FK_ThongTinPhongVan_CongTyTiepNhan] FOREIGN KEY([IdCongTyTiepNhan])
REFERENCES [dbo].[CongTyTiepNhan] ([Id])
GO
ALTER TABLE [dbo].[ThongTinPhongVan] CHECK CONSTRAINT [FK_ThongTinPhongVan_CongTyTiepNhan]
GO
ALTER TABLE [dbo].[ThongTinPhongVan]  WITH CHECK ADD  CONSTRAINT [FK_ThongTinPhongVan_ThongTinCaNhan] FOREIGN KEY([IdThongTinCaNhan])
REFERENCES [dbo].[ThongTinCaNhan] ([Id])
GO
ALTER TABLE [dbo].[ThongTinPhongVan] CHECK CONSTRAINT [FK_ThongTinPhongVan_ThongTinCaNhan]
GO
ALTER TABLE [dbo].[ThongTinVeNuoc]  WITH CHECK ADD  CONSTRAINT [FK_ThongTinVeNuoc_ThongTinCaNhan] FOREIGN KEY([IdThongTinCaNhan])
REFERENCES [dbo].[ThongTinCaNhan] ([Id])
GO
ALTER TABLE [dbo].[ThongTinVeNuoc] CHECK CONSTRAINT [FK_ThongTinVeNuoc_ThongTinCaNhan]
GO
USE [master]
GO
ALTER DATABASE [Vimas] SET  READ_WRITE 
GO
