USE [master]
GO
/****** Object:  Database [siuuu_cSharp]    Script Date: 4/21/2021 11:51:35 AM ******/
CREATE DATABASE [siuuu_cSharp]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'siuuu_cSharp', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.DANNYLAM\MSSQL\DATA\siuuu_cSharp.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'siuuu_cSharp_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.DANNYLAM\MSSQL\DATA\siuuu_cSharp_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [siuuu_cSharp] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [siuuu_cSharp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [siuuu_cSharp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET ARITHABORT OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [siuuu_cSharp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [siuuu_cSharp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET  DISABLE_BROKER 
GO
ALTER DATABASE [siuuu_cSharp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [siuuu_cSharp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET RECOVERY FULL 
GO
ALTER DATABASE [siuuu_cSharp] SET  MULTI_USER 
GO
ALTER DATABASE [siuuu_cSharp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [siuuu_cSharp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [siuuu_cSharp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [siuuu_cSharp] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [siuuu_cSharp] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [siuuu_cSharp] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'siuuu_cSharp', N'ON'
GO
ALTER DATABASE [siuuu_cSharp] SET QUERY_STORE = OFF
GO
USE [siuuu_cSharp]
GO
/****** Object:  Table [dbo].[COMPRA_PRODUCTO]    Script Date: 4/21/2021 11:51:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COMPRA_PRODUCTO](
	[FACTURA_ID] [int] NOT NULL,
	[PRODUCTO_ID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CUOTA]    Script Date: 4/21/2021 11:51:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CUOTA](
	[CUOTA_ID] [int] IDENTITY(1,1) NOT NULL,
	[TIPO_CUOTA] [varchar](50) NOT NULL,
	[TASA_INTERES] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_PLAN] PRIMARY KEY CLUSTERED 
(
	[CUOTA_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FACTURA]    Script Date: 4/21/2021 11:51:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FACTURA](
	[FACTURA_ID] [int] IDENTITY(1,1) NOT NULL,
	[USUARIO_ID] [int] NOT NULL,
	[PLAN_ID] [int] NOT NULL,
	[MONTO_FACTURA] [numeric](18, 2) NOT NULL,
	[CANT_PRODUCTOS] [int] NOT NULL,
	[ESTADO] [varchar](100) NOT NULL,
	[PAGO_MENSUAL] [numeric](18,2) NOT NULL,
 CONSTRAINT [PK_FACTURA] PRIMARY KEY CLUSTERED 
(
	[FACTURA_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRODUCTO]    Script Date: 4/21/2021 11:51:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCTO](
	[PRODUCTO_ID] [int] IDENTITY(1,1) NOT NULL,
	[NOMBRE] [varchar](50) NOT NULL,
	[DETALLES] [varchar](50) NOT NULL,
	[IMAGEN] [varchar](max) NOT NULL,
	[GARANTIA] [varchar](50) NOT NULL,
	[PRECIO] [decimal](18, 2) NOT NULL,
	[STOCK] [int] NOT NULL,
 CONSTRAINT [PK_PRODUCTO] PRIMARY KEY CLUSTERED 
(
	[PRODUCTO_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIO]    Script Date: 4/21/2021 11:51:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIO](
	[USUARIO_ID] [int] IDENTITY(1,1) NOT NULL,
	[NOMBRE] [varchar](50) NOT NULL,
	[APELLIDOS] [varchar](50) NOT NULL,
	[IDENTIFICACION] [varchar](50) NOT NULL,
	[TELEFONO] [int] NOT NULL,
	[DIRECCION] [varchar](50) NOT NULL,
	[EMAIL] [varchar](50) NOT NULL,
	[PASSWORD] [varchar](50) NOT NULL,
	[ROL] [int] NOT NULL,
 CONSTRAINT [PK_USUARIO] PRIMARY KEY CLUSTERED 
(
	[USUARIO_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CUOTA] ON 

INSERT [dbo].[CUOTA] ([CUOTA_ID], [TIPO_CUOTA], [TASA_INTERES]) VALUES (1, N'3 Meses', CAST(0.03 AS Decimal(18, 2)))
INSERT [dbo].[CUOTA] ([CUOTA_ID], [TIPO_CUOTA], [TASA_INTERES]) VALUES (2, N'6 Meses', CAST(0.06 AS Decimal(18, 2)))
INSERT [dbo].[CUOTA] ([CUOTA_ID], [TIPO_CUOTA], [TASA_INTERES]) VALUES (3, N'9 Meses', CAST(0.09 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[CUOTA] OFF
GO
SET IDENTITY_INSERT [dbo].[PRODUCTO] ON 

INSERT [dbo].[PRODUCTO] ([PRODUCTO_ID], [NOMBRE], [DETALLES], [IMAGEN], [GARANTIA], [PRECIO], [STOCK]) VALUES (1, N'Samsung Galaxy Note20 Ultra', N'Fabricante Samsung', N'https://images.samsung.com/is/image/samsung/latin-galaxy-note20-ultra-n985-sm-n985fzkkgto-frontmysticblack-320814281?$684_547_PNG$', N'No', CAST(53000.40 AS Decimal(18, 2)), 1)
INSERT [dbo].[PRODUCTO] ([PRODUCTO_ID], [NOMBRE], [DETALLES], [IMAGEN], [GARANTIA], [PRECIO], [STOCK]) VALUES (2, N'Apple iPhone 12 PRO MAX 128GB', N'Fabricante Appel', N'https://cdn.tmobile.com/content/dam/t-mobile/en-p/cell-phones/apple/Apple-iPhone-12-Pro-Max/Pacific-Blue/Apple-iPhone-12-Pro-Max-Pacific-Blue-frontimage.png', N'Si', CAST(54000.50 AS Decimal(18, 2)), 2)
INSERT [dbo].[PRODUCTO] ([PRODUCTO_ID], [NOMBRE], [DETALLES], [IMAGEN], [GARANTIA], [PRECIO], [STOCK]) VALUES (5, N'Huawei P40 PRO', N'Fabricante Huawei', N'https://www.movilzona.es/app/uploads/2020/03/Huawei-P40-Pro-GRANDE-1.png', N'Si', CAST(20000.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[PRODUCTO] ([PRODUCTO_ID], [NOMBRE], [DETALLES], [IMAGEN], [GARANTIA], [PRECIO], [STOCK]) VALUES (6, N'Xiaomi Redmi Note 9', N'Fabricante Xiaomi', N'https://www.amcsolutions.pe/wp-content/uploads/2020/09/Redmi-note-9.png', N'Si', CAST(20000.00 AS Decimal(18, 2)), 4)
INSERT [dbo].[PRODUCTO] ([PRODUCTO_ID], [NOMBRE], [DETALLES], [IMAGEN], [GARANTIA], [PRECIO], [STOCK]) VALUES (7, N'LG K71', N'Fabricante LG', N'https://digicompra.com.gt/wp-content/uploads/2020/11/LG_K51s_Azul-900x900.png', N'No', CAST(20400.00 AS Decimal(18, 2)), 2)
SET IDENTITY_INSERT [dbo].[PRODUCTO] OFF
GO
SET IDENTITY_INSERT [dbo].[USUARIO] ON 

INSERT [dbo].[USUARIO] ([USUARIO_ID], [NOMBRE], [APELLIDOS], [IDENTIFICACION], [TELEFONO], [DIRECCION], [EMAIL], [PASSWORD], [ROL]) VALUES (1, N'1', N'1', N'1', 1, N'1', N'1', N'1', 0)
INSERT [dbo].[USUARIO] ([USUARIO_ID], [NOMBRE], [APELLIDOS], [IDENTIFICACION], [TELEFONO], [DIRECCION], [EMAIL], [PASSWORD], [ROL]) VALUES (2, N'Pedro', N'Castillo Hernández', N'431234423', 54331435, N'Heredia, CostaRica', N'pedro@gmail.com', N'123', 0)
INSERT [dbo].[USUARIO] ([USUARIO_ID], [NOMBRE], [APELLIDOS], [IDENTIFICACION], [TELEFONO], [DIRECCION], [EMAIL], [PASSWORD], [ROL]) VALUES (3, N'Maria', N'García Perez', N'344335454', 34552143, N'Heredia,CR', N'maria@hotmail.com', N'123', 0)
SET IDENTITY_INSERT [dbo].[USUARIO] OFF
GO
ALTER TABLE [dbo].[COMPRA_PRODUCTO]  WITH CHECK ADD  CONSTRAINT [FK_COMPRA_PRODUCTO_FACTURA] FOREIGN KEY([FACTURA_ID])
REFERENCES [dbo].[FACTURA] ([FACTURA_ID])
GO
ALTER TABLE [dbo].[COMPRA_PRODUCTO] CHECK CONSTRAINT [FK_COMPRA_PRODUCTO_FACTURA]
GO
ALTER TABLE [dbo].[COMPRA_PRODUCTO]  WITH CHECK ADD  CONSTRAINT [FK_COMPRA_PRODUCTO_PRODUCTO] FOREIGN KEY([PRODUCTO_ID])
REFERENCES [dbo].[PRODUCTO] ([PRODUCTO_ID])
GO
ALTER TABLE [dbo].[COMPRA_PRODUCTO] CHECK CONSTRAINT [FK_COMPRA_PRODUCTO_PRODUCTO]
GO
ALTER TABLE [dbo].[FACTURA]  WITH CHECK ADD  CONSTRAINT [FK_FACTURA_PLAN] FOREIGN KEY([PLAN_ID])
REFERENCES [dbo].[CUOTA] ([CUOTA_ID])
GO
ALTER TABLE [dbo].[FACTURA] CHECK CONSTRAINT [FK_FACTURA_PLAN]
GO
ALTER TABLE [dbo].[FACTURA]  WITH CHECK ADD  CONSTRAINT [FK_FACTURA_USUARIO] FOREIGN KEY([USUARIO_ID])
REFERENCES [dbo].[USUARIO] ([USUARIO_ID])
GO
ALTER TABLE [dbo].[FACTURA] CHECK CONSTRAINT [FK_FACTURA_USUARIO]
GO
USE [master]
GO
ALTER DATABASE [siuuu_cSharp] SET  READ_WRITE 
GO
