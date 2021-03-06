USE [master]
GO
/****** Object:  Database [Vimas]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[BangCap]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[BienBan]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[CongTyChungNghe]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[CongTyTiepNhan]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[HopDongDOLAB]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[HopDongDOLABHocVienMapping]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[KyTucXa]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[NghiepDoan]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[QuaTrinhHocTap]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[QuaTrinhLamViec]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[SucKhoe]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[SystemLog]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[ThongTinCaNhan]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[ThongTinDuTuyen]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[ThongTinGiaDinh]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[ThongTinNopTien]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[ThongTinPhongVan]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[ThongTinVeNuoc]    Script Date: 19/07/2016 8:33:16 PM ******/
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
/****** Object:  Table [dbo].[TrungTamGTVL]    Script Date: 19/07/2016 8:33:16 PM ******/
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
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201606281537263_InitialCreate', N'Vimas.Models.ApplicationDbContext', 0x1F8B0800000000000400DD5C5B6FE336167E5F60FF83A0A7EE22B572D919CC06F60C5227E9069D5C30CE0CFA36A025DA2146A254894A1314FD657DE84FEA5FD843DD2CDE74B115DB290A1463F1F03B878787E4E1D1A7FCF5C79FE30F4F816F3DE23821219DD847A343DBC2D40D3D4297133B658BEFDFD91FDEFFF31FE30B2F78B2BE9472275C0E7AD264623F30169D3A4EE23EE00025A380B87198840B3672C3C0415EE81C1F1EFED7393A723040D8806559E34F296524C0D90FF8390DA98B239622FF3AF4B09F14CFA16596A15A3728C049845C3CB1BF10AE2597B3AD339F20B06186FD856D214A4386185878FA39C133168774398BE001F2EF9F230C720BE427B8B0FC7425DE751087C77C10CEAA6309E5A6090B839E80472785571CB9FB5ABEB52BAF81DF2EC0BFEC998F3AF3DDC4BEF270F6E853E883036485A7533FE6C213FBBA5271964437988DCA8EA31CF23206B85FC3F8DBA88E786075EE775045D1F1E890FF77604D539FA5319E509CB218F907D65D3AF789FB137EBE0FBF613A39399A2F4EDEBD798BBC93B7FFC1276FEA2385B1829CF0001EDDC5618463B00D2FAAF1DB9623F673E48E55B75A9FDC2B104BB0206CEB1A3D7DC474C91E60A91CBFB3AD4BF284BDF249115C9F2981F5039D589CC2CF9BD4F7D1DCC755BBD3A893FFBF41EBF19BB78368BD418F64994DBDA41F164E0CEBEA13F6B3D6E48144F9F212E6FB6B2176198701FF2DC657DEFA7516A6B1CB07131A45EE51BCC44CB46EECAC82B7534873A8E1C3BA44DDFFD0E696AAE1AD15E5035A6725942AB6BD1A4A7B5F566FE7883B8B2298BC2CB4B8479A02AE7E4C8DA47E1027BC75152E475DC385C230FECEBBDF4580883FC0F6D7410BE41C0B1207B81AE50F21041BA2BD6DBE434902ABDFFB1F4A1E1A4C877F0E60FA0CBB690C41396328885E5CDBDD4348F14D1ACC79AC6F4FD7605373FF6B78895C16C61794F7DA18EF63E87E0B537641BD73C4F067E69680FCE73D09BA030C62CE99EBE224B98460C6DE348494BA04BCA2ECE4B8371CDF9A769D7E4C7D44027DFE216DA25F4BD1550EA29750F21083982E176932F563B824B49BA9A5A8D9D45CA2D5D442ACAFA91CAC9BA585A4D9D04CA0D5CE5C6AB0EC2E9BA1E1D3BB0C76FFF3BBCD0E6FD35E5073E30C7648FC23A638866DCCBB438CE198AE66A0CBBEB18B64219B3EAEF4C5CFA64CD317E4A743AB5A6B35649BC0F0AB2183DDFFD59099098F1F89C7B3920E979E5218E03BC9EBEF53ED6B4EB26CDBCB4118E6B6956F670F302D97B324095D92AD024DB9AB285688F6430E67B5572EF2D1C8D50F1818043AE1471E3C81B1D97250DDD273EC6386AD33372F074E51E2224F75230CC8EB615879A26A0C5B554144E3FEADE88448C731EF84F8252881954A28539705A12E8990DFEA25A967C7238C8FBDD221B79CE30853AEB0D5135D94EB8B1EDC804A8F34296D1E1A3BB5886B0E4443D66A9AF3B6147635EF4A2D622B31D9923B1BE2B2C8DF5E24309B3DB685E06C764917038C05BC5D04687157E91A00F2C565DF0254BA311902B448A9B612A0A2C77610A0A24B5E5D80E657D4AEF32FDD57F72D3CC58BF2F68FF54677ED2036057FEC5968E6B927F461D003C76A789ECF79237E629ACB19D859DCCF9222D595438483CF30134B36AB7C579B873ACD2072103501AE02AD05B478F5A700290BAA8771652DAFD1BA228BE8015BD6DD1A618BBD5F82ADC5808A5D7F055A1334BF289583B3D3EDA31A59150D4A9077BA2CD4703401216F5EE2C03B38C55497551DD32517EE930DD706564C4683835A32578393CAC10CEEA53234DBBDA44BC8FAA4641B79494A9F0C5E2A0733B8978A186D77922629E891166CE422F1081F68B195958EEAB4A9DAC64ECE892A1E8C1D03796A7C8DA288D0658D4C553CB16639936AFAFDAC3FD128C8311C37D1F08D2A6B2B4D2C8CD1124BADA01A2CBD2471C2CE114373C4EB3C532F50C4B467AB61FB2F55D68F4F7512CB73A094E6FFCE7B082FEC8553564D438ADE9730B680E73259015D33F3FAEE16A7B5211FC59A9AFD34F4D3809A532B73EFFCCD5DBD7FFE4445183B92FD4AEAA4F849497045A7779A1275396C3C3D55CEB2FE1499214C8E2E33CEBAAB4D59A819A52C4AD5514C85AA9D4D992979E9384D7246D87F965A115E662D1534943A40F1A827468DC9A080D5DABAA38A64933AA6D8D21D516294D421A5A61E56D679238291F586B5F00C1ED54B74D7A03245EAE86A6B77640D67A40EAD695E035B63B3DCD61D55432BA9036B9ABB63AF3826F2F6B9C7A795F19EB2C671955F62373BAF0C182FB3170E73DCD5DED5D7816A8F7B62156FE315B0E2F95EC691F126B7461CE5558BCDE2C88061DE6D84F7DBE266D3F852DE8C29BCB41636F4A697F666BC7ED1FAA231A15CE164914A7B759593AE6CE3E2FAD4FE518C729FCA456CAB74231CE6CF09C3C1880B8C66BFF8539F60BE759702D78892054E584ED4B08F0F8F8EA5AF6BF6E74B1727493C5F73FD347DEE22CED9163857F411C5EE038A5506C4065F83AC4095E2F215F5F0D3C4FE2DEB759AD529F8BFB2C707D655F299925F5268B88F536CFDAE323A8761C7375FA9F6F45B86EE5EBDFAF96BDEF5C0BA8D61C59C5A87922FD79961F10B875ED6E45D37B066EDEF1E5EEF82123E30D0A24A0B62FDEF09E6840DF22D4169E577017AFA575FD3B4DF0B6C84A8F9266028BC415C68E2FCAF8365E4FB7BF093657CFF7E83D5F3FFD731CDC8FD27B43F98CCFCEFBE0D953D7778D4686E43DBD892323FB732A737A251EEFA6C5208D61B2D749544DD036E03A2F41A91F1CA38C6839D8E1A0AF160D8BB0CED17E70DEF0B557845E2D82D43789BA4E08697407F2B2EF01EB0D7346C9CDD337EB71D6BA60AEE9ED326FBF17AF72CD80A8ED6EED9BBDB0E36539977CF83AD174777CF626D57E7E78E23ADF311BA73C6AD4A1E32BC89D1D582DB18B579E11C6EF8F3108220CF28F30F21F514AE26FA698BC2958859A9993B262B56168EA257916856DB6FACC581DF38D842A659AD8171D9A4BBD8FF1B751732CDBA0D3CC65D7081B54C421D3FBB651F6BA23CBD26EEAF309216AA795BCEDAF85AFD35517D07718AB07A0CEF885F0FB37710970CB9747A3079D5D7BD7076D6FE52229CDF0959AE20F8DF4DA4D8154ECD4AE68A2EC2F2F0962C2A45A40ACD3566C88323F52C6664815C06CDBCC69C7DC99DD5EDF89B8E39F6AEE86DCAA294C1907130F78582174F029AF4677465D1E6F16D94FD519221860066125E9BBFA53FA4C4F72ABB2F3535210304CF2E8A8A2E9F4BC62BBBCBE70AE926A41D810AF75549D13D0E221FC0925B3A438F781DDB20FC3EE225729F5715401348FB44886E1F9F13B48C51901418ABFEF01362D80B9EDEFF1F0D7D45C430540000, N'6.1.3-40302')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'Admin')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4', N'PhongKeToan')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'PhongNguon')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'5', N'PhongQuanLyKTX')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3', N'PhongXKLD')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'6', N'SysAdmin')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'30f595ef-31e8-4fc3-906d-37584a7656c2', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'33cd4f7d-da78-41b3-8c6d-ba1ac6e4a82b', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'dbb03fba-e3eb-477d-bb62-4b5aa0d8a2e0', N'2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e894bbb7-54f8-4ec5-b322-1a2c7f06d3ae', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1424498f-ae1f-4138-8dba-cfd6c51ed037', N'4')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'51a01064-1806-4315-a10e-4c6f08f78c63', N'5')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'1424498f-ae1f-4138-8dba-cfd6c51ed037', N'phongketoan@gmail.com', 0, N'E10ADC3949BA59ABBE56E057F20F883E', N'80338ffe-9730-4363-acbe-bda1e07fb786', NULL, 0, 0, NULL, 1, 0, N'phongketoan')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'30f595ef-31e8-4fc3-906d-37584a7656c2', N'admin@gmail.com', 0, N'E10ADC3949BA59ABBE56E057F20F883E', N'12f24ca5-54d4-40b5-9ea1-a545ee930a68', NULL, 0, 0, NULL, 1, 0, N'admin')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'33cd4f7d-da78-41b3-8c6d-ba1ac6e4a82b', N'phuhiep11395@gmail.com', 0, N'E10ADC3949BA59ABBE56E057F20F883E', N'7aeaa3f5-e5e6-41d3-a53c-d2fd2a97e3dc', NULL, 0, 0, NULL, 1, 0, N'phuhiep11395')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'51a01064-1806-4315-a10e-4c6f08f78c63', N'phongquanlyktx@gmail.com', 0, N'E10ADC3949BA59ABBE56E057F20F883E', N'c8601d17-4b61-4b10-8df2-cda3e941c185', NULL, 0, 0, NULL, 1, 0, N'phongquanlyktx')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'dbb03fba-e3eb-477d-bb62-4b5aa0d8a2e0', N'phongnguon@gmail.com', 0, N'E10ADC3949BA59ABBE56E057F20F883E', N'57a245e1-b5cf-435c-bbea-f703a906e4eb', NULL, 0, 0, NULL, 1, 0, N'phongnguon')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e894bbb7-54f8-4ec5-b322-1a2c7f06d3ae', N'phongxkld@gmail.com', 0, N'E10ADC3949BA59ABBE56E057F20F883E', N'bf593220-38c4-4151-afdd-a35cdb742717', NULL, 0, 0, NULL, 1, 0, N'phongxkld')
SET IDENTITY_INSERT [dbo].[BangCap] ON 

INSERT [dbo].[BangCap] ([Id], [IdThongTinDuTuyen], [Thang], [Nam], [BangCap], [DaNop], [TrinhDo], [Active]) VALUES (2, 5, 7, 2015, N'Test', NULL, NULL, 0)
INSERT [dbo].[BangCap] ([Id], [IdThongTinDuTuyen], [Thang], [Nam], [BangCap], [DaNop], [TrinhDo], [Active]) VALUES (3, 5, NULL, NULL, N'Test 2 edit', NULL, NULL, 0)
INSERT [dbo].[BangCap] ([Id], [IdThongTinDuTuyen], [Thang], [Nam], [BangCap], [DaNop], [TrinhDo], [Active]) VALUES (4, 5, NULL, NULL, N'Test 2 edit 2', NULL, NULL, 0)
INSERT [dbo].[BangCap] ([Id], [IdThongTinDuTuyen], [Thang], [Nam], [BangCap], [DaNop], [TrinhDo], [Active]) VALUES (5, 5, 6, 2017, N'Test editjj', NULL, N'jj', 0)
INSERT [dbo].[BangCap] ([Id], [IdThongTinDuTuyen], [Thang], [Nam], [BangCap], [DaNop], [TrinhDo], [Active]) VALUES (6, 5, 1, 2011, N'Giấy CMND1', NULL, NULL, 1)
INSERT [dbo].[BangCap] ([Id], [IdThongTinDuTuyen], [Thang], [Nam], [BangCap], [DaNop], [TrinhDo], [Active]) VALUES (7, 5, 1, 2010, N'test1', 1, N'A', 1)
INSERT [dbo].[BangCap] ([Id], [IdThongTinDuTuyen], [Thang], [Nam], [BangCap], [DaNop], [TrinhDo], [Active]) VALUES (8, 8, 12, 2012, N'Test log', 1, N'AAA', 0)
SET IDENTITY_INSERT [dbo].[BangCap] OFF
SET IDENTITY_INSERT [dbo].[BienBan] ON 

INSERT [dbo].[BienBan] ([id], [idThongTinCaNhan], [GhiChu], [HinhAnh], [Active]) VALUES (1, 5, N'Temp 2', N'/BienBan/BienBan_74fe3eef39d44c8ca9ec112270977c43.jpg', 0)
INSERT [dbo].[BienBan] ([id], [idThongTinCaNhan], [GhiChu], [HinhAnh], [Active]) VALUES (2, 5, N'Kỷ luật', N'/BienBan/BienBan_448735f71c874483a0a28da8452e13c5.jpg', 0)
INSERT [dbo].[BienBan] ([id], [idThongTinCaNhan], [GhiChu], [HinhAnh], [Active]) VALUES (3, 7, N'AAA', N'/BienBan/BienBan_c50def7225b64a17bbed8f70c8685df7.jpg', 0)
INSERT [dbo].[BienBan] ([id], [idThongTinCaNhan], [GhiChu], [HinhAnh], [Active]) VALUES (4, 5, N'Thông test', N'/BienBan/BienBan_8a75cdea8fdc4120837ae389262f10b4.png', 0)
INSERT [dbo].[BienBan] ([id], [idThongTinCaNhan], [GhiChu], [HinhAnh], [Active]) VALUES (5, 5, N'Test 2', N'/BienBan/BienBan_0bb7f0693f8a444f9b5bd923a6142fdb.jpg', 0)
INSERT [dbo].[BienBan] ([id], [idThongTinCaNhan], [GhiChu], [HinhAnh], [Active]) VALUES (6, 5, N'Test log1', N'/BienBan/BienBan_6078e873d77f45bba32d18cc319983d3.jpg', 0)
INSERT [dbo].[BienBan] ([id], [idThongTinCaNhan], [GhiChu], [HinhAnh], [Active]) VALUES (7, 5, N'Testlog 2', N'/BienBan/BienBan_046f1f31a88d4a2689341eafdb84bd07.jpg', 0)
INSERT [dbo].[BienBan] ([id], [idThongTinCaNhan], [GhiChu], [HinhAnh], [Active]) VALUES (8, 5, N'Testlog nè', N'/BienBan/BienBan_a5074059f263419c915f503bba137f22.jpg', 0)
INSERT [dbo].[BienBan] ([id], [idThongTinCaNhan], [GhiChu], [HinhAnh], [Active]) VALUES (9, 5, N'Test log sau khi fix', N'/BienBan/BienBan_c3caac77bf4944ff93fa27fdee3769ef.jpg', 0)
INSERT [dbo].[BienBan] ([id], [idThongTinCaNhan], [GhiChu], [HinhAnh], [Active]) VALUES (10, 6, N'Test nè1', N'/BienBan/BienBan_c39510b2b1434787aaa5cc203cbaa9f7.jpg', 1)
INSERT [dbo].[BienBan] ([id], [idThongTinCaNhan], [GhiChu], [HinhAnh], [Active]) VALUES (11, 7, N'TestXKLD', N'/BienBan/BienBan_887c61e1a8e0478fb0c88b59d81cff5f.jpg', 1)
SET IDENTITY_INSERT [dbo].[BienBan] OFF
SET IDENTITY_INSERT [dbo].[CongTyChungNghe] ON 

INSERT [dbo].[CongTyChungNghe] ([Id], [TenVietTat], [TenTiengAnh], [TenTiengViet], [DiaChiTiengAnh], [DiaChiTiengViet], [DienThoai], [NguoiDaiDien], [ChucDanh], [VonDieuLe], [SoNhanVien], [Active]) VALUES (1, N'CN-Thông', N'ABC Company', N'Công ty ABC CHứng nghề', N'123/3453/65/54324/234/5 Hai Bà Trưng Quận 12, Tp.HCM', N'123/3453/65/54324/234/5 Hai Bà Trưng Quận 12, Tp.HCM', N'21321', N'Thông', N'6546', CAST(32131 AS Decimal(18, 0)), 351321, 1)
INSERT [dbo].[CongTyChungNghe] ([Id], [TenVietTat], [TenTiengAnh], [TenTiengViet], [DiaChiTiengAnh], [DiaChiTiengViet], [DienThoai], [NguoiDaiDien], [ChucDanh], [VonDieuLe], [SoNhanVien], [Active]) VALUES (2, N'sfdflj', N'sdf', N's', N'sdf', N'sdf', N'4234234234234', N'sdfsdf', N'sdfsdf', CAST(234234 AS Decimal(18, 0)), 234234234, 0)
INSERT [dbo].[CongTyChungNghe] ([Id], [TenVietTat], [TenTiengAnh], [TenTiengViet], [DiaChiTiengAnh], [DiaChiTiengViet], [DienThoai], [NguoiDaiDien], [ChucDanh], [VonDieuLe], [SoNhanVien], [Active]) VALUES (3, N'dfgdf', N'gdfg', N'gfdgdf', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[CongTyChungNghe] ([Id], [TenVietTat], [TenTiengAnh], [TenTiengViet], [DiaChiTiengAnh], [DiaChiTiengViet], [DienThoai], [NguoiDaiDien], [ChucDanh], [VonDieuLe], [SoNhanVien], [Active]) VALUES (4, N'TL', N'TestSystemLog', N'TestSystemLog', N'aaa', NULL, NULL, NULL, NULL, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[CongTyChungNghe] OFF
SET IDENTITY_INSERT [dbo].[CongTyTiepNhan] ON 

INSERT [dbo].[CongTyTiepNhan] ([Id], [TenTiengAnh], [TenTiengNhat], [NganhNghe], [NguoiDaiDien], [DiaChi], [DienThoai], [Fax], [idNghiepDoan], [Active]) VALUES (3, NULL, N'Cty1', N'IT', N' HiepBP', NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[CongTyTiepNhan] ([Id], [TenTiengAnh], [TenTiengNhat], [NganhNghe], [NguoiDaiDien], [DiaChi], [DienThoai], [Fax], [idNghiepDoan], [Active]) VALUES (4, N'abc', N'abc edit', N'abc', N'abc', NULL, N'1234', NULL, NULL, 0)
INSERT [dbo].[CongTyTiepNhan] ([Id], [TenTiengAnh], [TenTiengNhat], [NganhNghe], [NguoiDaiDien], [DiaChi], [DienThoai], [Fax], [idNghiepDoan], [Active]) VALUES (5, N'Company ABC1', N'テスト会社ートン1', N'A1', N'A1', N'112/4/4 phạm abc, Bến Nghé, Quận 1, HCM', N'0123668791', N'01246541', 1, 1)
INSERT [dbo].[CongTyTiepNhan] ([Id], [TenTiengAnh], [TenTiengNhat], [NganhNghe], [NguoiDaiDien], [DiaChi], [DienThoai], [Fax], [idNghiepDoan], [Active]) VALUES (6, N'TestDelete', N'TestDelete', N'TestDelete', N'TestDelete', N'TestDelete', N'123123123', N'123123', 1, 0)
INSERT [dbo].[CongTyTiepNhan] ([Id], [TenTiengAnh], [TenTiengNhat], [NganhNghe], [NguoiDaiDien], [DiaChi], [DienThoai], [Fax], [idNghiepDoan], [Active]) VALUES (7, N'テスト会社ートン1', N'テスト会社ートン2', N'テスト会社ートン1', N'テスト会社ートン1', N'テスト会社ートン1', N'1111', N'1111', 2, 1)
INSERT [dbo].[CongTyTiepNhan] ([Id], [TenTiengAnh], [TenTiengNhat], [NganhNghe], [NguoiDaiDien], [DiaChi], [DienThoai], [Fax], [idNghiepDoan], [Active]) VALUES (8, N'sdf', N'sdf', N'sdfsdf', N'sdfsdf', N'sdfsdf', N'4234234234234', N'234234324', 2, 0)
INSERT [dbo].[CongTyTiepNhan] ([Id], [TenTiengAnh], [TenTiengNhat], [NganhNghe], [NguoiDaiDien], [DiaChi], [DienThoai], [Fax], [idNghiepDoan], [Active]) VALUES (9, N'sdf', N'sdf', NULL, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[CongTyTiepNhan] ([Id], [TenTiengAnh], [TenTiengNhat], [NganhNghe], [NguoiDaiDien], [DiaChi], [DienThoai], [Fax], [idNghiepDoan], [Active]) VALUES (10, N'asd', N'asd', NULL, NULL, NULL, NULL, NULL, 1, 0)
INSERT [dbo].[CongTyTiepNhan] ([Id], [TenTiengAnh], [TenTiengNhat], [NganhNghe], [NguoiDaiDien], [DiaChi], [DienThoai], [Fax], [idNghiepDoan], [Active]) VALUES (11, N'A', N'A', NULL, NULL, NULL, NULL, NULL, 1, 1)
INSERT [dbo].[CongTyTiepNhan] ([Id], [TenTiengAnh], [TenTiengNhat], [NganhNghe], [NguoiDaiDien], [DiaChi], [DienThoai], [Fax], [idNghiepDoan], [Active]) VALUES (12, N'Test1', N'TestLog1', NULL, NULL, NULL, NULL, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[CongTyTiepNhan] OFF
SET IDENTITY_INSERT [dbo].[HopDongDOLAB] ON 

INSERT [dbo].[HopDongDOLAB] ([Id], [NgayDangKy], [NgayNhan], [SoDKHopDong], [SoCongVan], [SoPhieuTiepNhan], [Active]) VALUES (1, CAST(N'2016-07-11 00:00:00.000' AS DateTime), CAST(N'2016-07-06 00:00:00.000' AS DateTime), N'ASD', N'SDF', N'AWE', 1)
INSERT [dbo].[HopDongDOLAB] ([Id], [NgayDangKy], [NgayNhan], [SoDKHopDong], [SoCongVan], [SoPhieuTiepNhan], [Active]) VALUES (2, CAST(N'2016-07-11 00:00:00.000' AS DateTime), CAST(N'2016-07-11 00:00:00.000' AS DateTime), N'TEST delete', N'A', N'A', 0)
INSERT [dbo].[HopDongDOLAB] ([Id], [NgayDangKy], [NgayNhan], [SoDKHopDong], [SoCongVan], [SoPhieuTiepNhan], [Active]) VALUES (3, CAST(N'2016-07-17 00:00:00.000' AS DateTime), CAST(N'2016-07-17 00:00:00.000' AS DateTime), N'TestLog', N'TestLog', N'Testlog', 1)
INSERT [dbo].[HopDongDOLAB] ([Id], [NgayDangKy], [NgayNhan], [SoDKHopDong], [SoCongVan], [SoPhieuTiepNhan], [Active]) VALUES (5, CAST(N'2016-07-17 00:00:00.000' AS DateTime), CAST(N'2016-07-17 00:00:00.000' AS DateTime), N'log2', N'log2', N'log2', 0)
INSERT [dbo].[HopDongDOLAB] ([Id], [NgayDangKy], [NgayNhan], [SoDKHopDong], [SoCongVan], [SoPhieuTiepNhan], [Active]) VALUES (6, CAST(N'2016-07-17 00:00:00.000' AS DateTime), CAST(N'2016-07-17 00:00:00.000' AS DateTime), N'logne', N'logne', N'logne', 1)
SET IDENTITY_INSERT [dbo].[HopDongDOLAB] OFF
SET IDENTITY_INSERT [dbo].[HopDongDOLABHocVienMapping] ON 

INSERT [dbo].[HopDongDOLABHocVienMapping] ([Id], [IdThongTinCaNhan], [IdHopDongDOLAB], [Active]) VALUES (1, 6, 1, 0)
INSERT [dbo].[HopDongDOLABHocVienMapping] ([Id], [IdThongTinCaNhan], [IdHopDongDOLAB], [Active]) VALUES (2, 7, 1, 1)
INSERT [dbo].[HopDongDOLABHocVienMapping] ([Id], [IdThongTinCaNhan], [IdHopDongDOLAB], [Active]) VALUES (3, 5, 1, 1)
INSERT [dbo].[HopDongDOLABHocVienMapping] ([Id], [IdThongTinCaNhan], [IdHopDongDOLAB], [Active]) VALUES (4, 5, 2, 0)
INSERT [dbo].[HopDongDOLABHocVienMapping] ([Id], [IdThongTinCaNhan], [IdHopDongDOLAB], [Active]) VALUES (5, 5, 3, 1)
INSERT [dbo].[HopDongDOLABHocVienMapping] ([Id], [IdThongTinCaNhan], [IdHopDongDOLAB], [Active]) VALUES (6, 6, 3, 1)
INSERT [dbo].[HopDongDOLABHocVienMapping] ([Id], [IdThongTinCaNhan], [IdHopDongDOLAB], [Active]) VALUES (7, 7, 3, 1)
INSERT [dbo].[HopDongDOLABHocVienMapping] ([Id], [IdThongTinCaNhan], [IdHopDongDOLAB], [Active]) VALUES (8, 5, 5, 0)
INSERT [dbo].[HopDongDOLABHocVienMapping] ([Id], [IdThongTinCaNhan], [IdHopDongDOLAB], [Active]) VALUES (9, 6, 5, 0)
INSERT [dbo].[HopDongDOLABHocVienMapping] ([Id], [IdThongTinCaNhan], [IdHopDongDOLAB], [Active]) VALUES (10, 7, 5, 0)
INSERT [dbo].[HopDongDOLABHocVienMapping] ([Id], [IdThongTinCaNhan], [IdHopDongDOLAB], [Active]) VALUES (11, 5, 6, 1)
INSERT [dbo].[HopDongDOLABHocVienMapping] ([Id], [IdThongTinCaNhan], [IdHopDongDOLAB], [Active]) VALUES (12, 6, 6, 1)
SET IDENTITY_INSERT [dbo].[HopDongDOLABHocVienMapping] OFF
SET IDENTITY_INSERT [dbo].[KyTucXa] ON 

INSERT [dbo].[KyTucXa] ([Id], [IdThongTinCaNhan], [NgayVao], [NgayRa], [SoPhong], [SoHocTuDo], [GhiChu], [Active]) VALUES (1, 7, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[KyTucXa] ([Id], [IdThongTinCaNhan], [NgayVao], [NgayRa], [SoPhong], [SoHocTuDo], [GhiChu], [Active]) VALUES (2, 7, CAST(N'2016-07-13 00:00:00.000' AS DateTime), CAST(N'2016-07-06 00:00:00.000' AS DateTime), N'123', NULL, NULL, 0)
INSERT [dbo].[KyTucXa] ([Id], [IdThongTinCaNhan], [NgayVao], [NgayRa], [SoPhong], [SoHocTuDo], [GhiChu], [Active]) VALUES (3, 7, CAST(N'2016-07-06 00:00:00.000' AS DateTime), CAST(N'2016-06-28 00:00:00.000' AS DateTime), N'123', N'AA', N'AA', 1)
INSERT [dbo].[KyTucXa] ([Id], [IdThongTinCaNhan], [NgayVao], [NgayRa], [SoPhong], [SoHocTuDo], [GhiChu], [Active]) VALUES (4, 7, CAST(N'2016-06-28 00:00:00.000' AS DateTime), NULL, N'1', N'11', N'11', 1)
INSERT [dbo].[KyTucXa] ([Id], [IdThongTinCaNhan], [NgayVao], [NgayRa], [SoPhong], [SoHocTuDo], [GhiChu], [Active]) VALUES (5, 5, CAST(N'2016-07-13 00:00:00.000' AS DateTime), CAST(N'2016-07-13 00:00:00.000' AS DateTime), N'aa', N'aaaaa', N'aaaa', 1)
INSERT [dbo].[KyTucXa] ([Id], [IdThongTinCaNhan], [NgayVao], [NgayRa], [SoPhong], [SoHocTuDo], [GhiChu], [Active]) VALUES (6, 5, CAST(N'2016-07-05 00:00:00.000' AS DateTime), CAST(N'2014-07-10 00:00:00.000' AS DateTime), N'12', N'Testloga', N'testloga', 1)
SET IDENTITY_INSERT [dbo].[KyTucXa] OFF
SET IDENTITY_INSERT [dbo].[NghiepDoan] ON 

INSERT [dbo].[NghiepDoan] ([Id], [MaNghiepDoan], [TenNghiepDoan], [TenVietTat], [NguoiDaiDien], [ChucDanh], [DiaChi], [DienThoai], [Fax], [NgayKyHopDong], [LuongCoBan], [PhiDichVu], [PhiUTDT], [WebsiteUrl], [Active]) VALUES (1, N'ND001', N'ND của Thông', N'ND-Thông', N'Tông', N'Giám đốc', N'12/4/4 phạm abc, Bến Nghé, Quận 1, HCM', N'0123456', N'0123456', CAST(N'2016-06-27 00:00:00.000' AS DateTime), CAST(1000000 AS Decimal(18, 0)), CAST(122000 AS Decimal(18, 0)), CAST(1212 AS Decimal(18, 0)), N'www.google.com.vn', 1)
INSERT [dbo].[NghiepDoan] ([Id], [MaNghiepDoan], [TenNghiepDoan], [TenVietTat], [NguoiDaiDien], [ChucDanh], [DiaChi], [DienThoai], [Fax], [NgayKyHopDong], [LuongCoBan], [PhiDichVu], [PhiUTDT], [WebsiteUrl], [Active]) VALUES (2, N'ND002', N'ND của Thông 2', N'A', N'Thông', N'NV', N'123a Phan Văn Lưu, Quận 1. Tp.HCM', N'165464', N'654654', CAST(N'2016-06-27 00:00:00.000' AS DateTime), CAST(654654 AS Decimal(18, 0)), CAST(654 AS Decimal(18, 0)), CAST(654 AS Decimal(18, 0)), N'www.google.com.vn', 1)
INSERT [dbo].[NghiepDoan] ([Id], [MaNghiepDoan], [TenNghiepDoan], [TenVietTat], [NguoiDaiDien], [ChucDanh], [DiaChi], [DienThoai], [Fax], [NgayKyHopDong], [LuongCoBan], [PhiDichVu], [PhiUTDT], [WebsiteUrl], [Active]) VALUES (3, N'asd', N'asd', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2016-07-12 00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[NghiepDoan] ([Id], [MaNghiepDoan], [TenNghiepDoan], [TenVietTat], [NguoiDaiDien], [ChucDanh], [DiaChi], [DienThoai], [Fax], [NgayKyHopDong], [LuongCoBan], [PhiDichVu], [PhiUTDT], [WebsiteUrl], [Active]) VALUES (4, N'asd', N'asd', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2016-07-12 00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[NghiepDoan] ([Id], [MaNghiepDoan], [TenNghiepDoan], [TenVietTat], [NguoiDaiDien], [ChucDanh], [DiaChi], [DienThoai], [Fax], [NgayKyHopDong], [LuongCoBan], [PhiDichVu], [PhiUTDT], [WebsiteUrl], [Active]) VALUES (5, N'Test message', N'Test Message', N'T', NULL, NULL, N'12/4/4 phạm abc, Bến Nghé, Quận 1, HCM', N'0123456789', N'd', CAST(N'2016-06-28 00:00:00.000' AS DateTime), NULL, NULL, NULL, N'www.pixiv.net', 0)
INSERT [dbo].[NghiepDoan] ([Id], [MaNghiepDoan], [TenNghiepDoan], [TenVietTat], [NguoiDaiDien], [ChucDanh], [DiaChi], [DienThoai], [Fax], [NgayKyHopDong], [LuongCoBan], [PhiDichVu], [PhiUTDT], [WebsiteUrl], [Active]) VALUES (6, N'AAAA', N'AAAA', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2016-07-17 00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[NghiepDoan] ([Id], [MaNghiepDoan], [TenNghiepDoan], [TenVietTat], [NguoiDaiDien], [ChucDanh], [DiaChi], [DienThoai], [Fax], [NgayKyHopDong], [LuongCoBan], [PhiDichVu], [PhiUTDT], [WebsiteUrl], [Active]) VALUES (7, N'Test log', N'Test log', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2016-07-17 00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[NghiepDoan] OFF
SET IDENTITY_INSERT [dbo].[QuaTrinhHocTap] ON 

INSERT [dbo].[QuaTrinhHocTap] ([Id], [IdThongTinCaNhan], [TuNam], [DenNam], [TenTruong], [LoaiTruong], [NganhHoc], [DaTotNghiep], [Active]) VALUES (1, 2, 2013, 2016, N'FPT', 5, N'IS', 0, 1)
INSERT [dbo].[QuaTrinhHocTap] ([Id], [IdThongTinCaNhan], [TuNam], [DenNam], [TenTruong], [LoaiTruong], [NganhHoc], [DaTotNghiep], [Active]) VALUES (2, 7, 2012, 2016, N'FPT', 4, N'IS', 0, 0)
INSERT [dbo].[QuaTrinhHocTap] ([Id], [IdThongTinCaNhan], [TuNam], [DenNam], [TenTruong], [LoaiTruong], [NganhHoc], [DaTotNghiep], [Active]) VALUES (3, 5, 2010, 2016, N'DAI HOC FPT', 5, N'ASD', 0, 1)
INSERT [dbo].[QuaTrinhHocTap] ([Id], [IdThongTinCaNhan], [TuNam], [DenNam], [TenTruong], [LoaiTruong], [NganhHoc], [DaTotNghiep], [Active]) VALUES (4, 6, 2001, 2003, N'A', 1, N'SAD', 1, 0)
INSERT [dbo].[QuaTrinhHocTap] ([Id], [IdThongTinCaNhan], [TuNam], [DenNam], [TenTruong], [LoaiTruong], [NganhHoc], [DaTotNghiep], [Active]) VALUES (5, 5, 1999, 2012, N'ASDHB', 0, N'FDSFSF', 1, 1)
INSERT [dbo].[QuaTrinhHocTap] ([Id], [IdThongTinCaNhan], [TuNam], [DenNam], [TenTruong], [LoaiTruong], [NganhHoc], [DaTotNghiep], [Active]) VALUES (6, 5, 2000, 2016, N'ASDASD', 4, N'DSDGSDG', 1, 1)
INSERT [dbo].[QuaTrinhHocTap] ([Id], [IdThongTinCaNhan], [TuNam], [DenNam], [TenTruong], [LoaiTruong], [NganhHoc], [DaTotNghiep], [Active]) VALUES (9, 7, NULL, NULL, N'abc', 0, N'a', 0, 0)
INSERT [dbo].[QuaTrinhHocTap] ([Id], [IdThongTinCaNhan], [TuNam], [DenNam], [TenTruong], [LoaiTruong], [NganhHoc], [DaTotNghiep], [Active]) VALUES (10, 5, 2002, 2009, N'Test sau khi fix bug', 1, N'asdasd', 1, 1)
INSERT [dbo].[QuaTrinhHocTap] ([Id], [IdThongTinCaNhan], [TuNam], [DenNam], [TenTruong], [LoaiTruong], [NganhHoc], [DaTotNghiep], [Active]) VALUES (11, 5, 2012, 2016, N'Testlog', 0, N'AAA', 1, 1)
SET IDENTITY_INSERT [dbo].[QuaTrinhHocTap] OFF
SET IDENTITY_INSERT [dbo].[QuaTrinhLamViec] ON 

INSERT [dbo].[QuaTrinhLamViec] ([Id], [IdThongTinCaNhan], [TuNam], [DenNam], [TenCongTy], [HinhThucCongTy], [DiaChiCongTy], [ChiTietCongViec], [DangLam], [Active]) VALUES (1, 5, 12001, 12016, N'FPT Software1', N'TNHH1', N'1123 Láng Hạ, Hà Nội, Việt Nam', N'1Làm tester phá đám coder', 0, 1)
INSERT [dbo].[QuaTrinhLamViec] ([Id], [IdThongTinCaNhan], [TuNam], [DenNam], [TenCongTy], [HinhThucCongTy], [DiaChiCongTy], [ChiTietCongViec], [DangLam], [Active]) VALUES (2, 5, 2016, 2017, N'Test delete', N'Test delete', N'Test delete', N'Test delete', 1, 0)
INSERT [dbo].[QuaTrinhLamViec] ([Id], [IdThongTinCaNhan], [TuNam], [DenNam], [TenCongTy], [HinhThucCongTy], [DiaChiCongTy], [ChiTietCongViec], [DangLam], [Active]) VALUES (3, 5, 2016, 2017, N'Test delete', N'HHH', N'ASD', N'ASD', NULL, 0)
INSERT [dbo].[QuaTrinhLamViec] ([Id], [IdThongTinCaNhan], [TuNam], [DenNam], [TenCongTy], [HinhThucCongTy], [DiaChiCongTy], [ChiTietCongViec], [DangLam], [Active]) VALUES (4, 6, 123, 345, N'Testlogaa', N'dfg', N'DD', N'DD', 1, 0)
SET IDENTITY_INSERT [dbo].[QuaTrinhLamViec] OFF
SET IDENTITY_INSERT [dbo].[SucKhoe] ON 

INSERT [dbo].[SucKhoe] ([Id], [IdThongTinCaNhan], [ChieuCao], [CanNang], [NhomMau], [TayThuan], [ThiLucMatTrai], [ThiLucMatPhai], [HinhXam], [NgayKhamDot1], [GhiChuSucKhoeDot1], [NgayKhamDot2], [GhiChuSucKhoeDot2], [Active]) VALUES (1, 6, N'1         ', N'1         ', N'A1', 0, 1, 10, 1, CAST(N'2016-06-28 00:00:00.000' AS DateTime), N'aa1', CAST(N'2016-06-29 00:00:00.000' AS DateTime), N'bb1', 0)
INSERT [dbo].[SucKhoe] ([Id], [IdThongTinCaNhan], [ChieuCao], [CanNang], [NhomMau], [TayThuan], [ThiLucMatTrai], [ThiLucMatPhai], [HinhXam], [NgayKhamDot1], [GhiChuSucKhoeDot1], [NgayKhamDot2], [GhiChuSucKhoeDot2], [Active]) VALUES (2, 5, N'123', NULL, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[SucKhoe] ([Id], [IdThongTinCaNhan], [ChieuCao], [CanNang], [NhomMau], [TayThuan], [ThiLucMatTrai], [ThiLucMatPhai], [HinhXam], [NgayKhamDot1], [GhiChuSucKhoeDot1], [NgayKhamDot2], [GhiChuSucKhoeDot2], [Active]) VALUES (3, 5, NULL, NULL, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SucKhoe] ([Id], [IdThongTinCaNhan], [ChieuCao], [CanNang], [NhomMau], [TayThuan], [ThiLucMatTrai], [ThiLucMatPhai], [HinhXam], [NgayKhamDot1], [GhiChuSucKhoeDot1], [NgayKhamDot2], [GhiChuSucKhoeDot2], [Active]) VALUES (4, 6, NULL, NULL, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[SucKhoe] ([Id], [IdThongTinCaNhan], [ChieuCao], [CanNang], [NhomMau], [TayThuan], [ThiLucMatTrai], [ThiLucMatPhai], [HinhXam], [NgayKhamDot1], [GhiChuSucKhoeDot1], [NgayKhamDot2], [GhiChuSucKhoeDot2], [Active]) VALUES (5, 7, NULL, NULL, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[SucKhoe] OFF
SET IDENTITY_INSERT [dbo].[SystemLog] ON 

INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (1, 21, N'ThongTinNopTien', N'Tạo', N'phuhiep11395', CAST(N'2016-07-13 09:36:42.487' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (2, 22, N'ThongTinNopTien', N'Tạo', N'phuhiep11395', CAST(N'2016-07-14 11:23:40.673' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (3, 23, N'ThongTinNopTien', N'Tạo', N'phuhiep11395', CAST(N'2016-07-17 14:42:27.187' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (4, 21, N'ThongTinNopTien', N'Sửa', N'phuhiep11395', CAST(N'2016-07-17 14:43:04.213' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (5, 24, N'ThongTinNopTien', N'Tạo', N'phongketoan', CAST(N'2016-07-17 14:44:59.647' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (6, 24, N'ThongTinNopTien', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 14:56:31.183' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (7, 25, N'ThongTinNopTien', N'Tạo', N'phuhiep11395', CAST(N'2016-07-17 14:59:50.203' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (8, 25, N'ThongTinNopTien', N'Sửa', N'phuhiep11395', CAST(N'2016-07-17 15:01:22.397' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (9, 25, N'ThongTinNopTien', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 15:01:37.153' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (10, 8, N'BangCap', N'Tạo', N'phuhiep11395', CAST(N'2016-07-17 15:07:25.797' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (11, 8, N'BangCap', N'Sửa', N'phuhiep11395', CAST(N'2016-07-17 15:07:40.040' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (12, 8, N'BangCap', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 15:07:47.090' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (13, 0, N'BienBan', N'Tạo', N'phuhiep11395', CAST(N'2016-07-17 15:12:29.523' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (14, 6, N'BienBan', N'Sửa', N'phuhiep11395', CAST(N'2016-07-17 15:13:08.487' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (15, 6, N'BienBan', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 15:13:16.423' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (16, 0, N'BienBan', N'Tạo', N'phuhiep11395', CAST(N'2016-07-17 15:14:31.827' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (17, 0, N'BienBan', N'Tạo', N'phuhiep11395', CAST(N'2016-07-17 15:18:54.230' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (18, 9, N'BienBan', N'Tạo', N'phuhiep11395', CAST(N'2016-07-17 15:23:42.787' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (19, 9, N'BienBan', N'Sửa', N'phuhiep11395', CAST(N'2016-07-17 15:24:15.513' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (20, 9, N'BienBan', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 15:24:22.053' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (21, 4, N'CongTyChungNghe', N'Tạo', N'phuhiep11395', CAST(N'2016-07-17 15:29:35.527' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (22, 4, N'CongTyChungNghe', N'Sửa', N'phuhiep11395', CAST(N'2016-07-17 15:30:11.617' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (23, 4, N'CongTyChungNghe', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 15:30:28.343' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (24, 12, N'CongTyTiepNhan', N'Tạo', N'phuhiep11395', CAST(N'2016-07-17 15:33:43.727' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (25, 12, N'CongTyTiepNhan', N'Sửa', N'phuhiep11395', CAST(N'2016-07-17 15:34:24.040' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (26, 12, N'CongTyTiepNhan', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 15:34:45.207' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (27, 3, N'HopDongDOLAB', N'Tạo hợp đồng', N'phuhiep11395', CAST(N'2016-07-17 15:42:38.703' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (28, 5, N'HopDongDOLAB', N'Xóa học viên khỏi HĐ', N'phuhiep11395', CAST(N'2016-07-17 15:48:32.097' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (29, 3, N'HopDongDOLAB', N'Sửa hợp đồng', N'phuhiep11395', CAST(N'2016-07-17 15:48:39.147' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (30, 5, N'HopDongDOLAB', N'Xóa học viên khỏi HĐ', N'phuhiep11395', CAST(N'2016-07-17 15:49:02.037' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (31, 3, N'HopDongDOLAB', N'Sửa hợp đồng', N'phuhiep11395', CAST(N'2016-07-17 15:49:08.667' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (32, 1, N'HopDongDOLAB', N'Xóa học viên khỏi HĐ', N'phuhiep11395', CAST(N'2016-07-17 15:49:41.927' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (33, 3, N'HopDongDOLAB', N'Sửa hợp đồng', N'phuhiep11395', CAST(N'2016-07-17 15:50:22.720' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (34, 4, N'HopDongDOLAB', N'Tạo hợp đồng', N'phuhiep11395', CAST(N'2016-07-17 15:50:47.060' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (35, 5, N'HopDongDOLAB', N'Tạo hợp đồng', N'phuhiep11395', CAST(N'2016-07-17 15:51:06.197' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (36, 5, N'HopDongDOLAB', N'Sửa hợp đồng', N'phuhiep11395', CAST(N'2016-07-17 15:51:44.187' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (37, 5, N'HopDongDOLAB', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 15:51:59.767' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (38, 6, N'HopDongDOLAB', N'Tạo hợp đồng', N'phuhiep11395', CAST(N'2016-07-17 16:01:42.277' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (39, 6, N'HopDongDOLAB', N'Sửa hợp đồng', N'phuhiep11395', CAST(N'2016-07-17 16:02:07.707' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (40, 12, N'HopDongDOLAB', N'Xóa học viên khỏi HĐ', N'phuhiep11395', CAST(N'2016-07-17 16:02:21.923' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (41, 6, N'HopDongDOLAB', N'Sửa hợp đồng', N'phuhiep11395', CAST(N'2016-07-17 16:02:24.167' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (42, 6, N'HopDongDOLAB', N'Sửa hợp đồng', N'phuhiep11395', CAST(N'2016-07-17 16:02:42.670' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (43, 6, N'KyTucXa', N'Tạo', N'phuhiep11395', CAST(N'2016-07-17 16:05:14.960' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (44, 6, N'KyTucXa', N'Sửa', N'phuhiep11395', CAST(N'2016-07-17 16:05:33.270' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (45, 7, N'NghiepDoan', N'Tạo', N'phuhiep11395', CAST(N'2016-07-17 21:24:52.757' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (46, 7, N'NghiepDoan', N'Sửa', N'phuhiep11395', CAST(N'2016-07-17 21:25:50.697' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (47, 5, N'NghiepDoan', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 21:26:02.750' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (48, 11, N'QuaTrinhHocTap', N'Tạo', N'phuhiep11395', CAST(N'2016-07-17 21:31:54.320' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (49, 5, N'QuaTrinhHocTap', N'Sửa', N'phuhiep11395', CAST(N'2016-07-17 21:32:36.487' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (50, 4, N'QuaTrinhHocTap', N'Sửa', N'phuhiep11395', CAST(N'2016-07-17 21:33:37.657' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (51, 0, N'QuaTrinhLamViec', N'Tạo', N'phuhiep11395', CAST(N'2016-07-17 21:38:49.227' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (52, 4, N'QuaTrinhLamViec', N'Sửa', N'phuhiep11395', CAST(N'2016-07-17 21:39:03.167' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (53, 4, N'QuaTrinhLamViec', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 21:39:14.373' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (54, 0, N'SucKhoe', N'Tạo', N'phuhiep11395', CAST(N'2016-07-17 21:42:42.333' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (55, 5, N'SucKhoe', N'Tạo', N'phuhiep11395', CAST(N'2016-07-17 21:43:50.040' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (56, 1, N'SucKhoe', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 21:46:13.810' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (57, 2, N'SucKhoe', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 21:46:17.560' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (58, 9, N'ThongTinCaNhan', N'Tạo', N'phuhiep11395', CAST(N'2016-07-17 21:50:18.230' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (59, 9, N'ThongTinCaNhan', N'Sửa', N'phuhiep11395', CAST(N'2016-07-17 21:53:39.000' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (60, 9, N'ThongTinCaNhan', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 21:53:48.507' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (61, 4, N'SucKhoe', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 21:55:02.347' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (62, 9, N'ThongTinDuTuyen', N'Tạo', N'phuhiep11395', CAST(N'2016-07-17 22:03:28.337' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (63, 8, N'ThongTinDuTuyen', N'Sửa', N'phuhiep11395', CAST(N'2016-07-17 22:04:23.633' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (64, 8, N'ThongTinDuTuyen', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 22:04:36.533' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (65, 5, N'ThongTinGiaDinh', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 22:09:30.873' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (66, 18, N'ThongTinGiaDinh', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 22:09:33.557' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (67, 6, N'ThongTinGiaDinh', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 22:09:38.703' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (68, 17, N'ThongTinGiaDinh', N'Sửa', N'phuhiep11395', CAST(N'2016-07-17 22:09:57.950' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (69, 26, N'ThongTinGiaDinh', N'Tạo', N'phuhiep11395', CAST(N'2016-07-17 22:10:14.110' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (70, 7, N'ThongTinPhongVan', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 22:43:54.780' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (71, 4, N'ThongTinPhongVan', N'Sửa', N'phuhiep11395', CAST(N'2016-07-17 22:44:01.587' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (72, 9, N'ThongTinPhongVan', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 22:44:33.357' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (73, 7, N'ThongTinVeNuoc', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 22:50:10.997' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (74, 8, N'ThongTinVeNuoc', N'Xóa', N'phuhiep11395', CAST(N'2016-07-17 22:50:13.357' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (75, 9, N'ThongTinVeNuoc', N'Tạo', N'phongnguon', CAST(N'2016-07-17 22:51:04.667' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (76, 10, N'ThongTinVeNuoc', N'Tạo', N'phongnguon', CAST(N'2016-07-17 23:00:11.510' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (77, 3, N'TrungTamGTVL', N'Tạo', N'phongnguon', CAST(N'2016-07-17 23:01:06.460' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (78, 4, N'TrungTamGTVL', N'Tạo', N'phongnguon', CAST(N'2016-07-17 23:01:25.150' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (79, 2, N'TrungTamGTVL', N'Sửa', N'phongnguon', CAST(N'2016-07-17 23:08:53.813' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (80, 4, N'TrungTamGTVL', N'Xóa', N'phongnguon', CAST(N'2016-07-17 23:09:03.057' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (81, 3, N'TrungTamGTVL', N'Xóa', N'phongnguon', CAST(N'2016-07-17 23:09:05.967' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (82, 5, N'TrungTamGTVL', N'Tạo', N'phongnguon', CAST(N'2016-07-17 23:09:22.870' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (83, 5, N'TrungTamGTVL', N'Sửa', N'phongnguon', CAST(N'2016-07-17 23:09:38.283' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (84, 6, N'TrungTamGTVL', N'Tạo', N'phongnguon', CAST(N'2016-07-17 23:09:47.490' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (85, 6, N'TrungTamGTVL', N'Sửa', N'phongnguon', CAST(N'2016-07-17 23:10:04.953' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (86, 5, N'TrungTamGTVL', N'Xóa', N'phongnguon', CAST(N'2016-07-17 23:10:17.150' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (87, 4, N'BienBan', N'Xóa', N'phuhiep11395', CAST(N'2016-07-18 14:06:46.623' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (88, 7, N'BienBan', N'Xóa', N'phuhiep11395', CAST(N'2016-07-18 14:06:49.507' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (89, 8, N'BienBan', N'Xóa', N'phuhiep11395', CAST(N'2016-07-18 14:06:52.563' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (90, 10, N'BienBan', N'Tạo', N'phuhiep11395', CAST(N'2016-07-18 14:08:05.420' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (91, 10, N'BienBan', N'Sửa', N'phuhiep11395', CAST(N'2016-07-18 14:08:12.467' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (92, 10, N'BienBan', N'Sửa', N'phuhiep11395', CAST(N'2016-07-18 14:10:27.010' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (93, 11, N'BienBan', N'Tạo', N'phongxkld', CAST(N'2016-07-18 14:16:31.523' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (94, 10, N'ThongTinPhongVan', N'Tạo', N'phongnguon', CAST(N'2016-07-18 14:20:33.640' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (95, 11, N'ThongTinPhongVan', N'Tạo', N'phongnguon', CAST(N'2016-07-18 14:20:41.550' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (96, 11, N'ThongTinPhongVan', N'Xóa', N'phongnguon', CAST(N'2016-07-18 14:24:22.107' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (97, 4, N'ThongTinPhongVan', N'Sửa', N'phongnguon', CAST(N'2016-07-18 14:24:45.473' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (98, 4, N'ThongTinPhongVan', N'Sửa', N'phongnguon', CAST(N'2016-07-18 14:25:21.043' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (99, 4, N'ThongTinPhongVan', N'Xóa', N'phongnguon', CAST(N'2016-07-18 14:25:32.767' AS DateTime))
GO
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (100, 10, N'ThongTinDuTuyen', N'Tạo', N'phongnguon', CAST(N'2016-07-18 14:26:28.377' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (101, 11, N'ThongTinDuTuyen', N'Tạo', N'phuhiep11395', CAST(N'2016-07-18 14:27:13.133' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (102, 10, N'ThongTinDuTuyen', N'Xóa', N'phuhiep11395', CAST(N'2016-07-18 14:27:20.637' AS DateTime))
INSERT [dbo].[SystemLog] ([no], [id], [TenBang], [HanhDong], [ThucHienBoi], [NgayThucHien]) VALUES (103, 9, N'ThongTinDuTuyen', N'Sửa', N'phuhiep11395', CAST(N'2016-07-18 14:27:30.600' AS DateTime))
SET IDENTITY_INSERT [dbo].[SystemLog] OFF
SET IDENTITY_INSERT [dbo].[ThongTinCaNhan] ON 

INSERT [dbo].[ThongTinCaNhan] ([Id], [MaLuuHoSo], [IdTrungTamGTVL], [HoTen], [TenPhienAmNhat], [GioiTinh], [NgaySinh], [NoiSinh], [TrinhDoVanHoa], [TinhTrangGiaDinh], [CMND], [NgayCap], [NoiCap], [SoHoChieu], [NgayCapHC], [NoiCapHC], [NgayHetHan], [HoKhau], [DiaChiLienLac], [DiaChiTiengAnh], [DienThoaiDiDong], [DienThoaiNha], [CoGiay], [SizeQuanAo], [Active]) VALUES (2, N'123', 1, N'Hiep', N'ABC', 1, CAST(N'1995-06-06 00:00:00.000' AS DateTime), N'Ho Chi Minh', NULL, 0, N'025222829', CAST(N'2009-06-06 00:00:00.000' AS DateTime), N'Bà Rịa - Vũng Tàu', N'025222829', CAST(N'2016-07-13' AS Date), N'Bến Tre', CAST(N'2016-07-23' AS Date), NULL, N'a', N'a', N'1', N'1', 1, N'XXL', 0)
INSERT [dbo].[ThongTinCaNhan] ([Id], [MaLuuHoSo], [IdTrungTamGTVL], [HoTen], [TenPhienAmNhat], [GioiTinh], [NgaySinh], [NoiSinh], [TrinhDoVanHoa], [TinhTrangGiaDinh], [CMND], [NgayCap], [NoiCap], [SoHoChieu], [NgayCapHC], [NoiCapHC], [NgayHetHan], [HoKhau], [DiaChiLienLac], [DiaChiTiengAnh], [DienThoaiDiDong], [DienThoaiNha], [CoGiay], [SizeQuanAo], [Active]) VALUES (4, N'123', 1, N'Bùi Phú Hiệp', N'ヒプ', 1, CAST(N'2016-07-03 00:00:00.000' AS DateTime), N'Ho Chi Minh', NULL, 0, N'025222829', CAST(N'2016-07-03 00:00:00.000' AS DateTime), N'Đồng Tháp', N'025222829', NULL, N'Điện Biên', CAST(N'2016-07-14' AS Date), NULL, N'a', N'a', N'01275904362', N'01275904362', 34, N'XXL', 0)
INSERT [dbo].[ThongTinCaNhan] ([Id], [MaLuuHoSo], [IdTrungTamGTVL], [HoTen], [TenPhienAmNhat], [GioiTinh], [NgaySinh], [NoiSinh], [TrinhDoVanHoa], [TinhTrangGiaDinh], [CMND], [NgayCap], [NoiCap], [SoHoChieu], [NgayCapHC], [NoiCapHC], [NgayHetHan], [HoKhau], [DiaChiLienLac], [DiaChiTiengAnh], [DienThoaiDiDong], [DienThoaiNha], [CoGiay], [SizeQuanAo], [Active]) VALUES (5, N'123', 1, N'Lê Khải Thông', N'ヒプ', 1, CAST(N'1995-07-06 00:00:00.000' AS DateTime), N'Ho Chi Minh', 1, 0, N'025222829', CAST(N'2009-07-03 00:00:00.000' AS DateTime), N'Bắc Giang', N'025222829', CAST(N'2016-07-05' AS Date), N'An Giang', CAST(N'2016-07-15' AS Date), NULL, N'a', N'a', N'01275904362', N'01275904362', 34, N'XXL', 1)
INSERT [dbo].[ThongTinCaNhan] ([Id], [MaLuuHoSo], [IdTrungTamGTVL], [HoTen], [TenPhienAmNhat], [GioiTinh], [NgaySinh], [NoiSinh], [TrinhDoVanHoa], [TinhTrangGiaDinh], [CMND], [NgayCap], [NoiCap], [SoHoChieu], [NgayCapHC], [NoiCapHC], [NgayHetHan], [HoKhau], [DiaChiLienLac], [DiaChiTiengAnh], [DienThoaiDiDong], [DienThoaiNha], [CoGiay], [SizeQuanAo], [Active]) VALUES (6, N'123', 1, N'Lê Thanh Hải', N'ヒプ', 1, CAST(N'2016-07-03 00:00:00.000' AS DateTime), N'Ho Chi Minh', 0, 1, N'111111111', CAST(N'2016-07-03 00:00:00.000' AS DateTime), N'Bà Rịa - Vũng Tàu', N'025222829', CAST(N'2016-07-06' AS Date), N'Bà Rịa - Vũng Tàu', CAST(N'2016-07-07' AS Date), NULL, N'a', N'a', N'01275904362', N'01275904362', 34, N'XL', 1)
INSERT [dbo].[ThongTinCaNhan] ([Id], [MaLuuHoSo], [IdTrungTamGTVL], [HoTen], [TenPhienAmNhat], [GioiTinh], [NgaySinh], [NoiSinh], [TrinhDoVanHoa], [TinhTrangGiaDinh], [CMND], [NgayCap], [NoiCap], [SoHoChieu], [NgayCapHC], [NoiCapHC], [NgayHetHan], [HoKhau], [DiaChiLienLac], [DiaChiTiengAnh], [DienThoaiDiDong], [DienThoaiNha], [CoGiay], [SizeQuanAo], [Active]) VALUES (7, N'123', 1, N'Bùi Phú Hiệp', N'ヒプ', 1, CAST(N'1995-03-11 00:00:00.000' AS DateTime), N'TP.HCM', 5, 0, N'025222829', CAST(N'2016-07-04 00:00:00.000' AS DateTime), N'An Giang', N'025222829', CAST(N'2016-07-05' AS Date), N'An Giang', CAST(N'2016-07-06' AS Date), NULL, N'a', N'a', N'1', N'1', 34, N'XXL', 1)
INSERT [dbo].[ThongTinCaNhan] ([Id], [MaLuuHoSo], [IdTrungTamGTVL], [HoTen], [TenPhienAmNhat], [GioiTinh], [NgaySinh], [NoiSinh], [TrinhDoVanHoa], [TinhTrangGiaDinh], [CMND], [NgayCap], [NoiCap], [SoHoChieu], [NgayCapHC], [NoiCapHC], [NgayHetHan], [HoKhau], [DiaChiLienLac], [DiaChiTiengAnh], [DienThoaiDiDong], [DienThoaiNha], [CoGiay], [SizeQuanAo], [Active]) VALUES (9, N'Test log001', 2, N'Test log', N' テスト ', 1, CAST(N'2016-07-17 00:00:00.000' AS DateTime), N'ASDASDASd', 0, 0, N'123658965', CAST(N'2016-07-17 00:00:00.000' AS DateTime), N'Bình Dương', NULL, NULL, N'0', NULL, NULL, N'sdfsddfdfsdfsdfasdfsdf', NULL, NULL, NULL, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[ThongTinCaNhan] OFF
SET IDENTITY_INSERT [dbo].[ThongTinDuTuyen] ON 

INSERT [dbo].[ThongTinDuTuyen] ([Id], [IdThongTinCaNhan], [NgayDangKy], [UuDiem], [NhuocDiem], [KyNangKhac], [SoThich], [LyDoDiNhat], [VeNuocLamGi], [NguoiGioiThieu], [BanBeBenNhat], [AnhChiBenNhat], [AnhChiDangKyOVimas], [DaDKDiNhatOCongtyKhac], [DaDiNuocNgoai], [DaDiNhat], [GhiChuSoVan], [Active]) VALUES (5, 7, CAST(N'2016-07-14 00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 0)
INSERT [dbo].[ThongTinDuTuyen] ([Id], [IdThongTinCaNhan], [NgayDangKy], [UuDiem], [NhuocDiem], [KyNangKhac], [SoThich], [LyDoDiNhat], [VeNuocLamGi], [NguoiGioiThieu], [BanBeBenNhat], [AnhChiBenNhat], [AnhChiDangKyOVimas], [DaDKDiNhatOCongtyKhac], [DaDiNuocNgoai], [DaDiNhat], [GhiChuSoVan], [Active]) VALUES (6, 5, CAST(N'2016-07-17 00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, 0)
INSERT [dbo].[ThongTinDuTuyen] ([Id], [IdThongTinCaNhan], [NgayDangKy], [UuDiem], [NhuocDiem], [KyNangKhac], [SoThich], [LyDoDiNhat], [VeNuocLamGi], [NguoiGioiThieu], [BanBeBenNhat], [AnhChiBenNhat], [AnhChiDangKyOVimas], [DaDKDiNhatOCongtyKhac], [DaDiNuocNgoai], [DaDiNhat], [GhiChuSoVan], [Active]) VALUES (7, 7, CAST(N'2016-07-18 00:00:00.000' AS DateTime), N'A1', N'f1', N'h1', N'k1', N'r1', N'41', N'd1', N'e1', N'f1', N'2341', 0, 1, 0, N'a', 1)
INSERT [dbo].[ThongTinDuTuyen] ([Id], [IdThongTinCaNhan], [NgayDangKy], [UuDiem], [NhuocDiem], [KyNangKhac], [SoThich], [LyDoDiNhat], [VeNuocLamGi], [NguoiGioiThieu], [BanBeBenNhat], [AnhChiBenNhat], [AnhChiDangKyOVimas], [DaDKDiNhatOCongtyKhac], [DaDiNuocNgoai], [DaDiNhat], [GhiChuSoVan], [Active]) VALUES (8, 5, CAST(N'2016-07-17 00:00:00.000' AS DateTime), N'Testlog', NULL, NULL, NULL, N'Test log', NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, 0)
INSERT [dbo].[ThongTinDuTuyen] ([Id], [IdThongTinCaNhan], [NgayDangKy], [UuDiem], [NhuocDiem], [KyNangKhac], [SoThich], [LyDoDiNhat], [VeNuocLamGi], [NguoiGioiThieu], [BanBeBenNhat], [AnhChiBenNhat], [AnhChiDangKyOVimas], [DaDKDiNhatOCongtyKhac], [DaDiNuocNgoai], [DaDiNhat], [GhiChuSoVan], [Active]) VALUES (9, 5, CAST(N'2016-07-17 00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, 1)
INSERT [dbo].[ThongTinDuTuyen] ([Id], [IdThongTinCaNhan], [NgayDangKy], [UuDiem], [NhuocDiem], [KyNangKhac], [SoThich], [LyDoDiNhat], [VeNuocLamGi], [NguoiGioiThieu], [BanBeBenNhat], [AnhChiBenNhat], [AnhChiDangKyOVimas], [DaDKDiNhatOCongtyKhac], [DaDiNuocNgoai], [DaDiNhat], [GhiChuSoVan], [Active]) VALUES (10, 5, CAST(N'2016-07-18 00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, 0)
INSERT [dbo].[ThongTinDuTuyen] ([Id], [IdThongTinCaNhan], [NgayDangKy], [UuDiem], [NhuocDiem], [KyNangKhac], [SoThich], [LyDoDiNhat], [VeNuocLamGi], [NguoiGioiThieu], [BanBeBenNhat], [AnhChiBenNhat], [AnhChiDangKyOVimas], [DaDKDiNhatOCongtyKhac], [DaDiNuocNgoai], [DaDiNhat], [GhiChuSoVan], [Active]) VALUES (11, 5, CAST(N'2016-07-18 00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, 1)
SET IDENTITY_INSERT [dbo].[ThongTinDuTuyen] OFF
SET IDENTITY_INSERT [dbo].[ThongTinGiaDinh] ON 

INSERT [dbo].[ThongTinGiaDinh] ([Id], [IdThongTinCaNhan], [HoTen], [QuanHe], [NgaySinh], [NgheNghiep], [NoiLamViec], [DiaChi], [SoDienThoai], [Active]) VALUES (4, 2, N'Lê Khải Thông', 4, CAST(N'1995-07-05 00:00:00.000' AS DateTime), N'Sinh viên', N'Hồ Chí Minh', N'10 10th Street Thu Duc District', N'01275904362', 1)
INSERT [dbo].[ThongTinGiaDinh] ([Id], [IdThongTinCaNhan], [HoTen], [QuanHe], [NgaySinh], [NgheNghiep], [NoiLamViec], [DiaChi], [SoDienThoai], [Active]) VALUES (5, 5, N'Ba-Thông', 0, CAST(N'2016-06-27 00:00:00.000' AS DateTime), N'ASD', N'ASD', N'ASD', N'12312313', 0)
INSERT [dbo].[ThongTinGiaDinh] ([Id], [IdThongTinCaNhan], [HoTen], [QuanHe], [NgaySinh], [NgheNghiep], [NoiLamViec], [DiaChi], [SoDienThoai], [Active]) VALUES (6, 5, N'Mẹ-Thông', 1, CAST(N'1973-07-05 00:00:00.000' AS DateTime), N'Nội trợ', N'Bình Dương', N'123A/34 Bình Dương, Tp.TDM', N'0123456789', 0)
INSERT [dbo].[ThongTinGiaDinh] ([Id], [IdThongTinCaNhan], [HoTen], [QuanHe], [NgaySinh], [NgheNghiep], [NoiLamViec], [DiaChi], [SoDienThoai], [Active]) VALUES (7, 5, N'Em-Thông', 4, CAST(N'1997-07-11 00:00:00.000' AS DateTime), N'Học sinh', N'Tp.HCM', N'123s4df65s4f as65d4 as6d54', N'6465465465456', 1)
INSERT [dbo].[ThongTinGiaDinh] ([Id], [IdThongTinCaNhan], [HoTen], [QuanHe], [NgaySinh], [NgheNghiep], [NoiLamViec], [DiaChi], [SoDienThoai], [Active]) VALUES (16, 5, N'Anh-Thông1', 5, CAST(N'1998-07-10 00:00:00.000' AS DateTime), N'1', N'1', N'1', N'1', 0)
INSERT [dbo].[ThongTinGiaDinh] ([Id], [IdThongTinCaNhan], [HoTen], [QuanHe], [NgaySinh], [NgheNghiep], [NoiLamViec], [DiaChi], [SoDienThoai], [Active]) VALUES (17, 5, N'Cậu-Thông111', 7, CAST(N'1998-03-05 00:00:00.000' AS DateTime), N'asd', N'asd asda sdad a', N'dasd asd sad a', N'123123123123', 1)
INSERT [dbo].[ThongTinGiaDinh] ([Id], [IdThongTinCaNhan], [HoTen], [QuanHe], [NgaySinh], [NgheNghiep], [NoiLamViec], [DiaChi], [SoDienThoai], [Active]) VALUES (18, 5, N'Chị-Thông', 6, CAST(N'1968-12-18 00:00:00.000' AS DateTime), N'asd asda 6d', N'dasfsaf', N'ds6f54sd6gf5ds4f6gs4 64s d6f4sd', N'21321354654', 0)
INSERT [dbo].[ThongTinGiaDinh] ([Id], [IdThongTinCaNhan], [HoTen], [QuanHe], [NgaySinh], [NgheNghiep], [NoiLamViec], [DiaChi], [SoDienThoai], [Active]) VALUES (19, 5, N'Tùm lum Thông', 8, CAST(N'2016-06-28 00:00:00.000' AS DateTime), N'ASFDSG', N'&*W^%NKSDJVN', N'sdf546sd f/sd* fsd/f s', N'01234654984', 1)
INSERT [dbo].[ThongTinGiaDinh] ([Id], [IdThongTinCaNhan], [HoTen], [QuanHe], [NgaySinh], [NgheNghiep], [NoiLamViec], [DiaChi], [SoDienThoai], [Active]) VALUES (20, 5, N'KSJLDG', 2, CAST(N'2016-07-07 00:00:00.000' AS DateTime), N'hsaf', N'kjsdh', N'dfigdfgdkvjn sdf nsdi cvn', N'120360652161', 1)
INSERT [dbo].[ThongTinGiaDinh] ([Id], [IdThongTinCaNhan], [HoTen], [QuanHe], [NgaySinh], [NgheNghiep], [NoiLamViec], [DiaChi], [SoDienThoai], [Active]) VALUES (21, 5, N'sdfsdfsd', 0, CAST(N'2016-07-07 00:00:00.000' AS DateTime), N'gdfgdf', N'hfghfgh', N'fdh', N'32424234', 1)
INSERT [dbo].[ThongTinGiaDinh] ([Id], [IdThongTinCaNhan], [HoTen], [QuanHe], [NgaySinh], [NgheNghiep], [NoiLamViec], [DiaChi], [SoDienThoai], [Active]) VALUES (22, 5, N'werwef', 6, CAST(N'2016-07-07 00:00:00.000' AS DateTime), N'sdfsdfsdf', N'dsfsdfgsd', N'gdfgdfgd', N'123123', 1)
INSERT [dbo].[ThongTinGiaDinh] ([Id], [IdThongTinCaNhan], [HoTen], [QuanHe], [NgaySinh], [NgheNghiep], [NoiLamViec], [DiaChi], [SoDienThoai], [Active]) VALUES (23, 5, N'sdfsf', 5, CAST(N'2016-07-07 00:00:00.000' AS DateTime), N'gfgvfgnvbn ', N'bnvbnvb', N'nvnvbn', N'234234', 1)
INSERT [dbo].[ThongTinGiaDinh] ([Id], [IdThongTinCaNhan], [HoTen], [QuanHe], [NgaySinh], [NgheNghiep], [NoiLamViec], [DiaChi], [SoDienThoai], [Active]) VALUES (24, 7, N'a', 0, CAST(N'2016-07-10 00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ThongTinGiaDinh] ([Id], [IdThongTinCaNhan], [HoTen], [QuanHe], [NgaySinh], [NgheNghiep], [NoiLamViec], [DiaChi], [SoDienThoai], [Active]) VALUES (25, 7, N'NVA', 0, CAST(N'1975-07-05 00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ThongTinGiaDinh] ([Id], [IdThongTinCaNhan], [HoTen], [QuanHe], [NgaySinh], [NgheNghiep], [NoiLamViec], [DiaChi], [SoDienThoai], [Active]) VALUES (26, 5, N'Testlog', 0, CAST(N'2016-07-17 00:00:00.000' AS DateTime), N'ASd', N'ASDAS', N'asdad', N'123123', 1)
SET IDENTITY_INSERT [dbo].[ThongTinGiaDinh] OFF
SET IDENTITY_INSERT [dbo].[ThongTinNopTien] ON 

INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (2, 6, 1, N'1', CAST(N'2016-07-07 00:00:00.000' AS DateTime), 0, CAST(20000000 AS Decimal(18, 0)), NULL, 1)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (4, 5, 2, N'THONGTEST001', CAST(N'2016-06-27 00:00:00.000' AS DateTime), 0, CAST(250000 AS Decimal(18, 0)), N'ABCV', 1)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (5, 5, 0, N'THONGTEST002', CAST(N'2016-07-04 00:00:00.000' AS DateTime), 0, CAST(250000 AS Decimal(18, 0)), N'ABC', 1)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (6, 7, 0, N'4', CAST(N'2016-07-01 00:00:00.000' AS DateTime), 0, CAST(30000 AS Decimal(18, 0)), N'ABC', 0)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (7, 5, 0, N'1', CAST(N'2016-07-08 00:00:00.000' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), NULL, 1)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (8, 5, 0, N'1', CAST(N'2016-07-08 00:00:00.000' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), NULL, 0)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (9, 5, 0, N'1', CAST(N'2016-07-08 00:00:00.000' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), NULL, 0)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (10, 5, 0, N'1', CAST(N'2016-07-08 00:00:00.000' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), NULL, 1)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (11, 5, 0, N'1', CAST(N'2016-07-08 00:00:00.000' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), NULL, 1)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (12, 5, 0, N'1', CAST(N'2016-07-08 00:00:00.000' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), NULL, 1)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (13, 5, 0, N'1', CAST(N'2016-07-08 00:00:00.000' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), NULL, 1)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (14, 5, 0, N'1', CAST(N'2016-07-08 00:00:00.000' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), NULL, 1)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (15, 5, 0, N'Test 1', CAST(N'2016-07-10 00:00:00.000' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), NULL, 1)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (16, 5, 0, N'Test 2', CAST(N'2016-07-01 00:00:00.000' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), NULL, 1)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (17, 5, 0, N'Test 3', CAST(N'2016-07-01 00:00:00.000' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), NULL, 1)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (18, 7, 0, N'Test new sau khi fix bug', CAST(N'2016-06-27 00:00:00.000' AS DateTime), 1, CAST(123123123 AS Decimal(18, 0)), N'ASDASD', 0)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (19, 5, 0, N'Test new sau khi fix bug 2', CAST(N'2016-05-30 00:00:00.000' AS DateTime), 0, CAST(1231231231 AS Decimal(18, 0)), N'asdasd11', 1)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (20, 5, 0, N'a', CAST(N'2016-06-28 00:00:00.000' AS DateTime), 1, CAST(1231231 AS Decimal(18, 0)), N'asdasd', 1)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (21, 5, 0, N'Test log', CAST(N'2016-07-13 00:00:00.000' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), NULL, 1)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (22, 5, 0, N'1', CAST(N'2016-07-14 00:00:00.000' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'ABC', 1)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (23, 7, 0, N'TEST LOG CUỐI TUẦN', CAST(N'2016-07-05 00:00:00.000' AS DateTime), 1, CAST(123456 AS Decimal(18, 0)), N'AAA', 1)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (24, 5, 0, N'Test log Ke Toán', CAST(N'2016-07-17 00:00:00.000' AS DateTime), 0, CAST(999 AS Decimal(18, 0)), N'A', 0)
INSERT [dbo].[ThongTinNopTien] ([Id], [IdThongTinCaNhan], [LoaiTien], [SoPhieu], [NgayLapPhieu], [ThuHayChi], [SoTien], [LyDo], [Active]) VALUES (25, 5, 0, N'asasdasdasd', CAST(N'2016-07-17 00:00:00.000' AS DateTime), 0, CAST(123123 AS Decimal(18, 0)), N'aa', 0)
SET IDENTITY_INSERT [dbo].[ThongTinNopTien] OFF
SET IDENTITY_INSERT [dbo].[ThongTinPhongVan] ON 

INSERT [dbo].[ThongTinPhongVan] ([Id], [IdThongTinCaNhan], [NgayPhongVan], [NghePhongVan], [GhiChuPV], [NgayTrungTuyen], [DotTrungTuyen], [NgheTrungTuyenTiengAnh], [NgheTrungTuyenTiengViet], [IdCongTyTiepNhan], [ThoiHanHopDong], [GhiChuSauTrungTuyen], [LopHoc], [NgayNhapHoc], [NgayKetThucKhoaHoc], [GhiChuKhenThuongKyLuat], [IdCongTyChungNghe], [SoPhieuTiepNhan], [GhiChuPhaiCu], [HopDongTTS], [NgayKyHopDong], [HinhAnh], [NgayHuySauKhiTrungTuyen], [LyDoHuy], [Active]) VALUES (1, 7, CAST(N'2016-07-05 00:00:00.000' AS DateTime), N'ES', NULL, CAST(N'2016-07-05 00:00:00.000' AS DateTime), N'1', N'ES', N'Hệ thống nhúng', NULL, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'/UploadedImageData/HinhAnh_51439d53207a4c90bef02353535c548f.png', NULL, NULL, 1)
INSERT [dbo].[ThongTinPhongVan] ([Id], [IdThongTinCaNhan], [NgayPhongVan], [NghePhongVan], [GhiChuPV], [NgayTrungTuyen], [DotTrungTuyen], [NgheTrungTuyenTiengAnh], [NgheTrungTuyenTiengViet], [IdCongTyTiepNhan], [ThoiHanHopDong], [GhiChuSauTrungTuyen], [LopHoc], [NgayNhapHoc], [NgayKetThucKhoaHoc], [GhiChuKhenThuongKyLuat], [IdCongTyChungNghe], [SoPhieuTiepNhan], [GhiChuPhaiCu], [HopDongTTS], [NgayKyHopDong], [HinhAnh], [NgayHuySauKhiTrungTuyen], [LyDoHuy], [Active]) VALUES (3, 7, CAST(N'2016-07-09 00:00:00.000' AS DateTime), N'IS', N'ABC', NULL, N'2', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'/UploadedImageData/HinhAnh_0fb490216ede451c9eff274fbc1886e3.png', NULL, NULL, 0)
INSERT [dbo].[ThongTinPhongVan] ([Id], [IdThongTinCaNhan], [NgayPhongVan], [NghePhongVan], [GhiChuPV], [NgayTrungTuyen], [DotTrungTuyen], [NgheTrungTuyenTiengAnh], [NgheTrungTuyenTiengViet], [IdCongTyTiepNhan], [ThoiHanHopDong], [GhiChuSauTrungTuyen], [LopHoc], [NgayNhapHoc], [NgayKetThucKhoaHoc], [GhiChuKhenThuongKyLuat], [IdCongTyChungNghe], [SoPhieuTiepNhan], [GhiChuPhaiCu], [HopDongTTS], [NgayKyHopDong], [HinhAnh], [NgayHuySauKhiTrungTuyen], [LyDoHuy], [Active]) VALUES (4, 5, CAST(N'2016-07-10 00:00:00.000' AS DateTime), N'Tester', N'ASD', NULL, N'Đợt 1', N'Tester phá đám', N'Tester phá đám', 7, 0, N'a', N'wer', CAST(N'2016-07-06 00:00:00.000' AS DateTime), CAST(N'2016-07-20 00:00:00.000' AS DateTime), N'sdfsdf', 1, N'sdfsdf', N'sdfsdf', N'sdfsdf', CAST(N'2016-07-06 00:00:00.000' AS DateTime), N'/UploadedImageData/HinhAnh_1d8439eb7bc54296bc2cbce79603bfae.jpg', NULL, NULL, 0)
INSERT [dbo].[ThongTinPhongVan] ([Id], [IdThongTinCaNhan], [NgayPhongVan], [NghePhongVan], [GhiChuPV], [NgayTrungTuyen], [DotTrungTuyen], [NgheTrungTuyenTiengAnh], [NgheTrungTuyenTiengViet], [IdCongTyTiepNhan], [ThoiHanHopDong], [GhiChuSauTrungTuyen], [LopHoc], [NgayNhapHoc], [NgayKetThucKhoaHoc], [GhiChuKhenThuongKyLuat], [IdCongTyChungNghe], [SoPhieuTiepNhan], [GhiChuPhaiCu], [HopDongTTS], [NgayKyHopDong], [HinhAnh], [NgayHuySauKhiTrungTuyen], [LyDoHuy], [Active]) VALUES (5, 7, CAST(N'2016-07-10 00:00:00.000' AS DateTime), N'Test delete', NULL, NULL, NULL, NULL, NULL, 5, 0, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, N'', NULL, NULL, 0)
INSERT [dbo].[ThongTinPhongVan] ([Id], [IdThongTinCaNhan], [NgayPhongVan], [NghePhongVan], [GhiChuPV], [NgayTrungTuyen], [DotTrungTuyen], [NgheTrungTuyenTiengAnh], [NgheTrungTuyenTiengViet], [IdCongTyTiepNhan], [ThoiHanHopDong], [GhiChuSauTrungTuyen], [LopHoc], [NgayNhapHoc], [NgayKetThucKhoaHoc], [GhiChuKhenThuongKyLuat], [IdCongTyChungNghe], [SoPhieuTiepNhan], [GhiChuPhaiCu], [HopDongTTS], [NgayKyHopDong], [HinhAnh], [NgayHuySauKhiTrungTuyen], [LyDoHuy], [Active]) VALUES (6, 7, CAST(N'2016-07-10 00:00:00.000' AS DateTime), N'Tester', N'ASD', CAST(N'2016-07-14 00:00:00.000' AS DateTime), N'1', N'A', N'A', 5, 0, N'A', N'a', CAST(N'2016-06-29 00:00:00.000' AS DateTime), CAST(N'2016-07-14 00:00:00.000' AS DateTime), N'a', 1, N'1', N'123', N'123', CAST(N'2016-07-12 00:00:00.000' AS DateTime), N'/UploadedImageData/HinhAnh_7c041c7820564c1390bc12267e8113f2.jpg', CAST(N'2016-07-12 00:00:00.000' AS DateTime), N'a', 1)
INSERT [dbo].[ThongTinPhongVan] ([Id], [IdThongTinCaNhan], [NgayPhongVan], [NghePhongVan], [GhiChuPV], [NgayTrungTuyen], [DotTrungTuyen], [NgheTrungTuyenTiengAnh], [NgheTrungTuyenTiengViet], [IdCongTyTiepNhan], [ThoiHanHopDong], [GhiChuSauTrungTuyen], [LopHoc], [NgayNhapHoc], [NgayKetThucKhoaHoc], [GhiChuKhenThuongKyLuat], [IdCongTyChungNghe], [SoPhieuTiepNhan], [GhiChuPhaiCu], [HopDongTTS], [NgayKyHopDong], [HinhAnh], [NgayHuySauKhiTrungTuyen], [LyDoHuy], [Active]) VALUES (7, 5, CAST(N'2016-07-12 00:00:00.000' AS DateTime), N'A', N'ASDA', CAST(N'2016-06-29 00:00:00.000' AS DateTime), N'A', N'A', N'A', 5, 1, N'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL, NULL, 0)
INSERT [dbo].[ThongTinPhongVan] ([Id], [IdThongTinCaNhan], [NgayPhongVan], [NghePhongVan], [GhiChuPV], [NgayTrungTuyen], [DotTrungTuyen], [NgheTrungTuyenTiengAnh], [NgheTrungTuyenTiengViet], [IdCongTyTiepNhan], [ThoiHanHopDong], [GhiChuSauTrungTuyen], [LopHoc], [NgayNhapHoc], [NgayKetThucKhoaHoc], [GhiChuKhenThuongKyLuat], [IdCongTyChungNghe], [SoPhieuTiepNhan], [GhiChuPhaiCu], [HopDongTTS], [NgayKyHopDong], [HinhAnh], [NgayHuySauKhiTrungTuyen], [LyDoHuy], [Active]) VALUES (8, 5, CAST(N'2016-07-06 00:00:00.000' AS DateTime), N'B', N'z', CAST(N'2016-06-29 00:00:00.000' AS DateTime), N'dsf', N'sdf', NULL, 7, 0, N'sdf', N'ASD', CAST(N'2016-07-16 00:00:00.000' AS DateTime), CAST(N'2016-07-28 00:00:00.000' AS DateTime), N'ggg', 1, N'ggg', N'hh', N'ff', CAST(N'2016-07-07 00:00:00.000' AS DateTime), N'/UploadedImageData/HinhAnh_5ab61c9407814068adfd99863fac94cd.jpg', NULL, NULL, 0)
INSERT [dbo].[ThongTinPhongVan] ([Id], [IdThongTinCaNhan], [NgayPhongVan], [NghePhongVan], [GhiChuPV], [NgayTrungTuyen], [DotTrungTuyen], [NgheTrungTuyenTiengAnh], [NgheTrungTuyenTiengViet], [IdCongTyTiepNhan], [ThoiHanHopDong], [GhiChuSauTrungTuyen], [LopHoc], [NgayNhapHoc], [NgayKetThucKhoaHoc], [GhiChuKhenThuongKyLuat], [IdCongTyChungNghe], [SoPhieuTiepNhan], [GhiChuPhaiCu], [HopDongTTS], [NgayKyHopDong], [HinhAnh], [NgayHuySauKhiTrungTuyen], [LyDoHuy], [Active]) VALUES (9, 5, CAST(N'2016-07-17 00:00:00.000' AS DateTime), N'ASd', N'asd', CAST(N'2016-06-28 00:00:00.000' AS DateTime), N'sdf', N'sdf', N'sdf', 5, 0, N'aaa', NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, N'', NULL, NULL, 1)
INSERT [dbo].[ThongTinPhongVan] ([Id], [IdThongTinCaNhan], [NgayPhongVan], [NghePhongVan], [GhiChuPV], [NgayTrungTuyen], [DotTrungTuyen], [NgheTrungTuyenTiengAnh], [NgheTrungTuyenTiengViet], [IdCongTyTiepNhan], [ThoiHanHopDong], [GhiChuSauTrungTuyen], [LopHoc], [NgayNhapHoc], [NgayKetThucKhoaHoc], [GhiChuKhenThuongKyLuat], [IdCongTyChungNghe], [SoPhieuTiepNhan], [GhiChuPhaiCu], [HopDongTTS], [NgayKyHopDong], [HinhAnh], [NgayHuySauKhiTrungTuyen], [LyDoHuy], [Active]) VALUES (10, 5, CAST(N'2016-07-18 00:00:00.000' AS DateTime), N'Test', N'Test', CAST(N'2016-06-30 00:00:00.000' AS DateTime), N'11', N'ASdsf', N'dsdfsdf', 7, 0, N'a', NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, N'/UploadedImageData/HinhAnh_2b86a7dc4e3f47fb824add819a9ecc64.jpg', NULL, NULL, 1)
INSERT [dbo].[ThongTinPhongVan] ([Id], [IdThongTinCaNhan], [NgayPhongVan], [NghePhongVan], [GhiChuPV], [NgayTrungTuyen], [DotTrungTuyen], [NgheTrungTuyenTiengAnh], [NgheTrungTuyenTiengViet], [IdCongTyTiepNhan], [ThoiHanHopDong], [GhiChuSauTrungTuyen], [LopHoc], [NgayNhapHoc], [NgayKetThucKhoaHoc], [GhiChuKhenThuongKyLuat], [IdCongTyChungNghe], [SoPhieuTiepNhan], [GhiChuPhaiCu], [HopDongTTS], [NgayKyHopDong], [HinhAnh], [NgayHuySauKhiTrungTuyen], [LyDoHuy], [Active]) VALUES (11, 5, CAST(N'2016-07-18 00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[ThongTinPhongVan] OFF
SET IDENTITY_INSERT [dbo].[ThongTinVeNuoc] ON 

INSERT [dbo].[ThongTinVeNuoc] ([Id], [IdThongTinCaNhan], [NgayDi], [NgayVe], [LyDoVe], [ThanhLyHopDong], [NgayThanhLy], [SoHopDongThanhLy], [NgayTron], [NgayBiTrucXuat], [GhiChuTheoDoi], [Active]) VALUES (1, 7, CAST(N'2016-07-14 00:00:00.000' AS DateTime), CAST(N'2016-07-14 00:00:00.000' AS DateTime), N'Test', 0, CAST(N'2016-07-13 00:00:00.000' AS DateTime), N'abc', CAST(N'2016-07-14 00:00:00.000' AS DateTime), CAST(N'2016-07-14 00:00:00.000' AS DateTime), N'ABC', 1)
INSERT [dbo].[ThongTinVeNuoc] ([Id], [IdThongTinCaNhan], [NgayDi], [NgayVe], [LyDoVe], [ThanhLyHopDong], [NgayThanhLy], [SoHopDongThanhLy], [NgayTron], [NgayBiTrucXuat], [GhiChuTheoDoi], [Active]) VALUES (2, 7, CAST(N'2016-07-15 00:00:00.000' AS DateTime), CAST(N'2016-07-15 00:00:00.000' AS DateTime), NULL, 1, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ThongTinVeNuoc] ([Id], [IdThongTinCaNhan], [NgayDi], [NgayVe], [LyDoVe], [ThanhLyHopDong], [NgayThanhLy], [SoHopDongThanhLy], [NgayTron], [NgayBiTrucXuat], [GhiChuTheoDoi], [Active]) VALUES (3, 5, CAST(N'2016-07-18 00:00:00.000' AS DateTime), CAST(N'2016-08-07 00:00:00.000' AS DateTime), N'1', 0, CAST(N'2016-07-14 00:00:00.000' AS DateTime), N'1', NULL, NULL, N'1', 1)
INSERT [dbo].[ThongTinVeNuoc] ([Id], [IdThongTinCaNhan], [NgayDi], [NgayVe], [LyDoVe], [ThanhLyHopDong], [NgayThanhLy], [SoHopDongThanhLy], [NgayTron], [NgayBiTrucXuat], [GhiChuTheoDoi], [Active]) VALUES (4, 5, CAST(N'2016-07-17 00:00:00.000' AS DateTime), CAST(N'2016-07-17 00:00:00.000' AS DateTime), NULL, 1, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ThongTinVeNuoc] ([Id], [IdThongTinCaNhan], [NgayDi], [NgayVe], [LyDoVe], [ThanhLyHopDong], [NgayThanhLy], [SoHopDongThanhLy], [NgayTron], [NgayBiTrucXuat], [GhiChuTheoDoi], [Active]) VALUES (5, 5, CAST(N'2016-07-17 00:00:00.000' AS DateTime), CAST(N'2016-07-17 00:00:00.000' AS DateTime), N'a', 1, CAST(N'2016-07-05 00:00:00.000' AS DateTime), N'a', CAST(N'2016-06-28 00:00:00.000' AS DateTime), CAST(N'2016-07-04 00:00:00.000' AS DateTime), N'aa', 1)
INSERT [dbo].[ThongTinVeNuoc] ([Id], [IdThongTinCaNhan], [NgayDi], [NgayVe], [LyDoVe], [ThanhLyHopDong], [NgayThanhLy], [SoHopDongThanhLy], [NgayTron], [NgayBiTrucXuat], [GhiChuTheoDoi], [Active]) VALUES (6, 5, CAST(N'2016-07-17 00:00:00.000' AS DateTime), CAST(N'2016-07-17 00:00:00.000' AS DateTime), NULL, 1, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ThongTinVeNuoc] ([Id], [IdThongTinCaNhan], [NgayDi], [NgayVe], [LyDoVe], [ThanhLyHopDong], [NgayThanhLy], [SoHopDongThanhLy], [NgayTron], [NgayBiTrucXuat], [GhiChuTheoDoi], [Active]) VALUES (7, 5, CAST(N'2016-07-17 00:00:00.000' AS DateTime), CAST(N'2016-07-17 00:00:00.000' AS DateTime), NULL, 1, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ThongTinVeNuoc] ([Id], [IdThongTinCaNhan], [NgayDi], [NgayVe], [LyDoVe], [ThanhLyHopDong], [NgayThanhLy], [SoHopDongThanhLy], [NgayTron], [NgayBiTrucXuat], [GhiChuTheoDoi], [Active]) VALUES (8, 5, CAST(N'2016-07-17 00:00:00.000' AS DateTime), CAST(N'2016-07-17 00:00:00.000' AS DateTime), NULL, 1, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ThongTinVeNuoc] ([Id], [IdThongTinCaNhan], [NgayDi], [NgayVe], [LyDoVe], [ThanhLyHopDong], [NgayThanhLy], [SoHopDongThanhLy], [NgayTron], [NgayBiTrucXuat], [GhiChuTheoDoi], [Active]) VALUES (9, 5, CAST(N'2016-07-17 00:00:00.000' AS DateTime), CAST(N'2016-07-17 00:00:00.000' AS DateTime), N'ASD', 1, CAST(N'2016-07-05 00:00:00.000' AS DateTime), N'123123', CAST(N'2016-07-06 00:00:00.000' AS DateTime), CAST(N'2016-07-06 00:00:00.000' AS DateTime), N'aaa', 1)
INSERT [dbo].[ThongTinVeNuoc] ([Id], [IdThongTinCaNhan], [NgayDi], [NgayVe], [LyDoVe], [ThanhLyHopDong], [NgayThanhLy], [SoHopDongThanhLy], [NgayTron], [NgayBiTrucXuat], [GhiChuTheoDoi], [Active]) VALUES (10, 5, CAST(N'2016-07-17 00:00:00.000' AS DateTime), CAST(N'2016-07-17 00:00:00.000' AS DateTime), NULL, 1, NULL, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[ThongTinVeNuoc] OFF
SET IDENTITY_INSERT [dbo].[TrungTamGTVL] ON 

INSERT [dbo].[TrungTamGTVL] ([Id], [MaNguon], [TenCoSo], [DiaChi], [DienThoai], [Fax], [SoHDLK], [NgayKyHopDong], [NgayHetHan], [NguoiDaiDien], [ChucDanh], [Active]) VALUES (1, N'1', N'Vimas', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[TrungTamGTVL] ([Id], [MaNguon], [TenCoSo], [DiaChi], [DienThoai], [Fax], [SoHDLK], [NgayKyHopDong], [NgayHetHan], [NguoiDaiDien], [ChucDanh], [Active]) VALUES (2, N'GTVL-01', N'GTVL-01', N'as6d54 as6d54 as6d54as d', N'0123456748', N'01234564798', N'5sd6', CAST(N'2016-07-06 00:00:00.000' AS DateTime), CAST(N'2020-07-11 00:00:00.000' AS DateTime), N'Thông', N'aa', 1)
INSERT [dbo].[TrungTamGTVL] ([Id], [MaNguon], [TenCoSo], [DiaChi], [DienThoai], [Fax], [SoHDLK], [NgayKyHopDong], [NgayHetHan], [NguoiDaiDien], [ChucDanh], [Active]) VALUES (3, N'LOG', N'TestLog', NULL, NULL, NULL, NULL, CAST(N'2016-07-17 00:00:00.000' AS DateTime), NULL, NULL, NULL, 0)
INSERT [dbo].[TrungTamGTVL] ([Id], [MaNguon], [TenCoSo], [DiaChi], [DienThoai], [Fax], [SoHDLK], [NgayKyHopDong], [NgayHetHan], [NguoiDaiDien], [ChucDanh], [Active]) VALUES (4, N'LOGDELETE', N'TestLogDelete', NULL, NULL, NULL, NULL, CAST(N'2016-07-17 00:00:00.000' AS DateTime), NULL, NULL, NULL, 0)
INSERT [dbo].[TrungTamGTVL] ([Id], [MaNguon], [TenCoSo], [DiaChi], [DienThoai], [Fax], [SoHDLK], [NgayKyHopDong], [NgayHetHan], [NguoiDaiDien], [ChucDanh], [Active]) VALUES (5, N'ANAN1', N'TestNe1', N'asdoasjid asodi11', N'1231211', N'31231231', N'sfd1', CAST(N'2016-07-17 00:00:00.000' AS DateTime), CAST(N'2016-07-13 00:00:00.000' AS DateTime), N'dsf1', N'dd1', 0)
INSERT [dbo].[TrungTamGTVL] ([Id], [MaNguon], [TenCoSo], [DiaChi], [DienThoai], [Fax], [SoHDLK], [NgayKyHopDong], [NgayHetHan], [NguoiDaiDien], [ChucDanh], [Active]) VALUES (6, N'ASDASD', N'SADSAd', N'asdasdasd', NULL, NULL, NULL, CAST(N'2016-07-17 00:00:00.000' AS DateTime), CAST(N'2013-07-04 00:00:00.000' AS DateTime), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[TrungTamGTVL] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 19/07/2016 8:33:16 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 19/07/2016 8:33:16 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 19/07/2016 8:33:16 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 19/07/2016 8:33:16 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 19/07/2016 8:33:16 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 19/07/2016 8:33:16 PM ******/
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
