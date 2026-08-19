USE [master]
GO
/****** Object:  Database [MarketPlace]    Script Date: 3/4/2024 12:47:34 AM ******/
CREATE DATABASE [MarketPlace]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MyWebStore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MyWebStore.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MyWebStore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MyWebStore_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MarketPlace] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MarketPlace].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MarketPlace] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MarketPlace] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MarketPlace] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MarketPlace] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MarketPlace] SET ARITHABORT OFF 
GO
ALTER DATABASE [MarketPlace] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MarketPlace] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MarketPlace] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MarketPlace] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MarketPlace] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MarketPlace] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MarketPlace] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MarketPlace] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MarketPlace] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MarketPlace] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MarketPlace] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MarketPlace] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MarketPlace] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MarketPlace] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MarketPlace] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MarketPlace] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MarketPlace] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MarketPlace] SET RECOVERY FULL 
GO
ALTER DATABASE [MarketPlace] SET  MULTI_USER 
GO
ALTER DATABASE [MarketPlace] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MarketPlace] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MarketPlace] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MarketPlace] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MarketPlace] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MarketPlace] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MarketPlace', N'ON'
GO
ALTER DATABASE [MarketPlace] SET QUERY_STORE = ON
GO
ALTER DATABASE [MarketPlace] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MarketPlace]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 3/4/2024 12:47:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[category_id] [int] NOT NULL,
	[name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 3/4/2024 12:47:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[client_id] [int] NOT NULL,
	[name] [nvarchar](100) NULL,
	[email] [nvarchar](100) NULL,
	[phone] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[client_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Delivery_Points]    Script Date: 3/4/2024 12:47:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Delivery_Points](
	[delivery_point_id] [int] NOT NULL,
	[address] [nvarchar](100) NULL,
	[city] [nvarchar](100) NULL,
	[rating] [decimal](3, 2) NULL,
	[zipcode] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[delivery_point_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 3/4/2024 12:47:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[employee_id] [int] NOT NULL,
	[delivery_point_id] [int] NULL,
	[name] [nvarchar](100) NULL,
	[email] [nvarchar](100) NULL,
	[phone] [nvarchar](100) NULL,
	[salary] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[employee_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Markets]    Script Date: 3/4/2024 12:47:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Markets](
	[market_id] [int] NOT NULL,
	[name] [nvarchar](100) NULL,
	[city] [nvarchar](100) NULL,
	[address] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[market_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 3/4/2024 12:47:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[order_id] [int] NOT NULL,
	[delivery_point_id] [int] NULL,
	[client_id] [int] NULL,
	[order_date] [datetime] NULL,
	[status] [nvarchar](50) NULL,
	[total_amount] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders_Items]    Script Date: 3/4/2024 12:47:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders_Items](
	[orders_item_id] [int] NOT NULL,
	[order_id] [int] NULL,
	[product_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[orders_item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 3/4/2024 12:47:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[product_id] [int] NOT NULL,
	[product_instance_id] [int] NULL,
	[market_id] [int] NULL,
	[price] [decimal](10, 2) NULL,
	[rating] [decimal](3, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products_Instances]    Script Date: 3/4/2024 12:47:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products_Instances](
	[product_instance_id] [int] NOT NULL,
	[category_id] [int] NULL,
	[name] [nvarchar](100) NULL,
	[description] [nvarchar](max) NULL,
	[availability] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[product_instance_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD FOREIGN KEY([delivery_point_id])
REFERENCES [dbo].[Delivery_Points] ([delivery_point_id])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([client_id])
REFERENCES [dbo].[Clients] ([client_id])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([delivery_point_id])
REFERENCES [dbo].[Delivery_Points] ([delivery_point_id])
GO
ALTER TABLE [dbo].[Orders_Items]  WITH CHECK ADD FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO
ALTER TABLE [dbo].[Orders_Items]  WITH CHECK ADD FOREIGN KEY([product_id])
REFERENCES [dbo].[Products] ([product_id])
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([market_id])
REFERENCES [dbo].[Markets] ([market_id])
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([product_instance_id])
REFERENCES [dbo].[Products_Instances] ([product_instance_id])
GO
ALTER TABLE [dbo].[Products_Instances]  WITH CHECK ADD FOREIGN KEY([category_id])
REFERENCES [dbo].[Categories] ([category_id])
GO
ALTER TABLE [dbo].[Delivery_Points]  WITH CHECK ADD CHECK  (([rating]>=(0.00) AND [rating]<=(5.00)))
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD CHECK  (([rating]>=(0.00) AND [rating]<=(5.00)))
GO
/****** Object:  StoredProcedure [dbo].[GetEmployeeClientOrders]    Script Date: 3/4/2024 12:47:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--UPDATE Orders
--SET total_amount = tab.total
--FROM Orders o 
--JOIN (SELECT oi.order_id, sum(p.price) [total] from Orders_Items oi
--LEFT JOIN Products p ON oi.product_id = p.product_id
--group by oi.order_id) tab ON o.order_id = tab.order_id;

CREATE PROCEDURE [dbo].[GetEmployeeClientOrders]
AS
BEGIN
    SELECT E.name AS employee_name, C.name AS client_name, O.order_date 
    FROM Employees AS E
    JOIN Delivery_Points AS DP ON E.delivery_point_id = DP.delivery_point_id
    JOIN Orders AS O ON DP.delivery_point_id = O.delivery_point_id
    JOIN Clients AS C ON O.client_id = C.client_id;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetExpensiveOrderDetails]    Script Date: 3/4/2024 12:47:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--UPDATE Orders
--SET total_amount = tab.total
--FROM Orders o 
--JOIN (SELECT oi.order_id, sum(p.price) [total] from Orders_Items oi
--LEFT JOIN Products p ON oi.product_id = p.product_id
--group by oi.order_id) tab ON o.order_id = tab.order_id;

CREATE PROCEDURE [dbo].[GetExpensiveOrderDetails]
AS
BEGIN
	DECLARE @expensiveOrderId INT;
	SELECT TOP 1 @expensiveOrderId = order_id
	FROM Orders
	ORDER BY total_amount DESC;
	SELECT OI.order_id, P.price, PI.name AS product_name, M.name AS market_name, COUNT(*) [count]
	FROM Orders_Items AS OI
	JOIN Products AS P ON OI.product_id = P.product_id
	JOIN Products_Instances AS PI ON P.product_instance_id = PI.product_instance_id
	JOIN Markets AS M ON P.market_id = M.market_id
	WHERE OI.order_id = @expensiveOrderId
	GROUP BY OI.order_id, P.price, PI.name, M.name
END

GO
/****** Object:  StoredProcedure [dbo].[GetFilteredProductInstances]    Script Date: 3/4/2024 12:47:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFilteredProductInstances]
    @marketId INT = 3,
    @partialName NVARCHAR(100) = '',
    @offsetRows INT = 2,
    @fetchRows INT = 3
AS
BEGIN
    SELECT m.market_id, m.name, pri.name, p.rating
    FROM Products_Instances pri
    JOIN Products p ON pri.product_instance_id = p.product_instance_id
    JOIN Markets m ON m.market_id = p.market_id
    WHERE m.market_id = @marketId AND pri.name LIKE '%' + @partialName + '%'
    ORDER BY p.rating ASC
    OFFSET @offsetRows ROWS
    FETCH NEXT @fetchRows ROWS ONLY;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetMarketProducts]    Script Date: 3/4/2024 12:47:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetMarketProducts]
    @marketId INT = 3
AS
BEGIN
    SELECT m.market_id, m.name, pri.name
    FROM Products_Instances pri
    JOIN Products p ON pri.product_instance_id = p.product_instance_id
    JOIN Markets m ON m.market_id = p.market_id
    WHERE m.market_id = @marketId;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetProductInstancesByMarketAndName]    Script Date: 3/4/2024 12:47:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProductInstancesByMarketAndName]
    @marketId INT = 3,
    @partialName NVARCHAR(100) = 'by'

AS
BEGIN
    SELECT m.market_id, m.name, pri.name, p.rating
    FROM Products_Instances pri
    JOIN Products p ON pri.product_instance_id = p.product_instance_id
    JOIN Markets m ON m.market_id = p.market_id
    WHERE m.market_id = @marketId AND pri.name LIKE '%' + @partialName + '%';
END;

GO
/****** Object:  StoredProcedure [dbo].[GetProductInstancesByName]    Script Date: 3/4/2024 12:47:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProductInstancesByName]
    @partialName NVARCHAR(100) = 'by'
AS
BEGIN
    SELECT *
    FROM Products_Instances
    WHERE [name] LIKE '%' + @partialName + '%';
END;

GO
/****** Object:  StoredProcedure [dbo].[GetProductInstancesWithRating]    Script Date: 3/4/2024 12:47:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProductInstancesWithRating]
AS
BEGIN
    SELECT pi.[name], p.rating
    FROM Products p
    JOIN Products_Instances pi ON p.product_instance_id = pi.product_instance_id
    ORDER BY p.rating ASC;
END;


GO
/****** Object:  StoredProcedure [dbo].[GetSmallestOrderClients]    Script Date: 3/4/2024 12:47:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--UPDATE Orders
--SET total_amount = tab.total
--FROM Orders o 
--JOIN (SELECT oi.order_id, sum(p.price) [total] from Orders_Items oi
--LEFT JOIN Products p ON oi.product_id = p.product_id
--group by oi.order_id) tab ON o.order_id = tab.order_id;

CREATE PROCEDURE [dbo].[GetSmallestOrderClients]
AS
BEGIN
    WITH OrderQuantities AS (
        SELECT order_id, COUNT(*) AS num_items 
        FROM Orders_Items
        GROUP BY order_id
    ), SmallestOrders AS (
        SELECT TOP 4 order_id, num_items 
        FROM OrderQuantities
        ORDER BY num_items
    )
    SELECT C.name, C.email, C.phone, SO.num_items 
    FROM Clients C
    JOIN Orders AS O ON C.client_id = O.client_id
    JOIN SmallestOrders AS SO ON O.order_id = SO.order_id
    ORDER BY SO.num_items;
END;

GO
/****** Object:  StoredProcedure [dbo].[UpdateEmployeeSalaryBasedOnDeliveryPointRating]    Script Date: 3/4/2024 12:47:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--UPDATE Orders
--SET total_amount = tab.total
--FROM Orders o 
--JOIN (SELECT oi.order_id, sum(p.price) [total] from Orders_Items oi
--LEFT JOIN Products p ON oi.product_id = p.product_id
--group by oi.order_id) tab ON o.order_id = tab.order_id;

CREATE PROCEDURE [dbo].[UpdateEmployeeSalaryBasedOnDeliveryPointRating]
AS
BEGIN
    UPDATE Employees
    SET salary = salary * 0.8
    WHERE delivery_point_id IN (
        SELECT delivery_point_id 
        FROM Delivery_Points
        WHERE rating < 3.5
    );
END;

--GO 

--EXEC GetEmployeeClientOrders
GO
USE [master]
GO
ALTER DATABASE [MarketPlace] SET  READ_WRITE 
GO
