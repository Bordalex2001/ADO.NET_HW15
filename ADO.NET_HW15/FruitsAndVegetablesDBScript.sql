USE [master]
GO
/****** Object:  Database [FruitsAndVegetablesDB]    Script Date: 29.08.2024 11:33:51 ******/
CREATE DATABASE [FruitsAndVegetablesDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Fruits and vegetables', FILENAME = N'C:\Users\user\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\Fruits and vegetables.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Fruits and vegetables_log', FILENAME = N'C:\Users\user\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\Fruits and vegetables.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FruitsAndVegetablesDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET ANSI_NULLS ON 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET ANSI_PADDING ON 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET ARITHABORT ON 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET RECOVERY FULL 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET  MULTI_USER 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET QUERY_STORE = OFF
GO
USE [FruitsAndVegetablesDB]
GO
/****** Object:  Table [dbo].[List]    Script Date: 29.08.2024 11:33:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[List](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Type] [nchar](10) NOT NULL,
	[Color] [nvarchar](20) NOT NULL,
	[CaloricContent] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Caloric content of each product]    Script Date: 29.08.2024 11:33:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create view [dbo].[Caloric content of each product] (Name, CaloricContent) as select Name, CaloricContent from List;
GO
ALTER TABLE [dbo].[List]  WITH NOCHECK ADD  CONSTRAINT [CK_List_Type] CHECK  (([Type]=N'Фрукти' OR [Type]=N'Овочі' OR [Type]=N'Ягоди'))
GO
ALTER TABLE [dbo].[List] CHECK CONSTRAINT [CK_List_Type]
GO
/****** Object:  StoredProcedure [dbo].[AddProduct]    Script Date: 29.08.2024 11:33:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddProduct]
    @name NVARCHAR(50),
    @type NCHAR(10),
    @color NVARCHAR(20),
    @calories int
AS INSERT INTO List(Name, Type, Color, CaloricContent) VALUES(@name, @type, @color, @calories);
GO
/****** Object:  StoredProcedure [dbo].[AvgOfCalories]    Script Date: 29.08.2024 11:33:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Create view [Caloric content of each product] (Name, CaloricContent) as select Name, CaloricContent from List;
--Create procedure MinOfCalories @calories int output as select @calories=min(CaloricContent) from List;
--Create procedure MaxOfCalories @calories int output as select @calories=max(CaloricContent) from List;
Create procedure [dbo].[AvgOfCalories] @calories int output as select @calories=avg(CaloricContent) from List;
GO
/****** Object:  StoredProcedure [dbo].[CaloriesAboveValue]    Script Date: 29.08.2024 11:33:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Create view [Caloric content of each product] (Name, CaloricContent) as select Name, CaloricContent from List;
--Create procedure MinOfCalories @calories int output as select @calories=min(CaloricContent) from List;
--Create procedure MaxOfCalories @calories int output as select @calories=max(CaloricContent) from List;
--Create procedure AvgOfCalories @calories int output as select @calories=avg(CaloricContent) from List;
--Create procedure CountVegetables @vegetables int output as select @vegetables = count(*) from List where Type = N'Овочі';
--Create procedure CountFruits @fruits int output as select @fruits = count(*) from List where Type = N'Фрукти' or Type = N'Ягоди';
/*CREATE PROCEDURE CountByColor
    @color NVARCHAR(20),
    @quantity INT OUTPUT
AS
BEGIN
    SELECT @quantity = COUNT(*) 
    FROM List
    WHERE Color = @color;
END;*/
/*CREATE PROCEDURE CountEachColor
AS
BEGIN
    SELECT Color, COUNT(*) AS Count
    FROM List
    GROUP BY Color;
END;*/
/*CREATE PROCEDURE CaloriesBelowValue
    @calories INT
AS
BEGIN
    SELECT *
    FROM List
    WHERE CaloricContent < @calories;
END;*/
CREATE PROCEDURE [dbo].[CaloriesAboveValue]
    @calories INT
AS
BEGIN
    SELECT *
    FROM List
    WHERE CaloricContent > @calories;
END;
GO
/****** Object:  StoredProcedure [dbo].[CaloriesBelowValue]    Script Date: 29.08.2024 11:33:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Create view [Caloric content of each product] (Name, CaloricContent) as select Name, CaloricContent from List;
--Create procedure MinOfCalories @calories int output as select @calories=min(CaloricContent) from List;
--Create procedure MaxOfCalories @calories int output as select @calories=max(CaloricContent) from List;
--Create procedure AvgOfCalories @calories int output as select @calories=avg(CaloricContent) from List;
--Create procedure CountVegetables @vegetables int output as select @vegetables = count(*) from List where Type = N'Овочі';
--Create procedure CountFruits @fruits int output as select @fruits = count(*) from List where Type = N'Фрукти' or Type = N'Ягоди';
/*CREATE PROCEDURE CountByColor
    @color NVARCHAR(20),
    @quantity INT OUTPUT
AS
BEGIN
    SELECT @quantity = COUNT(*) 
    FROM List
    WHERE Color = @color;
END;*/
/*CREATE PROCEDURE CountEachColor
AS
BEGIN
    SELECT Color, COUNT(*) AS Count
    FROM List
    GROUP BY Color;
END;*/
CREATE PROCEDURE [dbo].[CaloriesBelowValue]
    @calories INT
AS
BEGIN
    SELECT *
    FROM List
    WHERE CaloricContent < @calories;
END;
GO
/****** Object:  StoredProcedure [dbo].[CaloriesInRange]    Script Date: 29.08.2024 11:33:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CaloriesInRange]
    @minCalories INT, @maxCalories INT
AS
BEGIN
    SELECT *
    FROM List
    WHERE CaloricContent BETWEEN @minCalories AND @maxCalories;
END;
GO
/****** Object:  StoredProcedure [dbo].[CountByColor]    Script Date: 29.08.2024 11:33:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Create view [Caloric content of each product] (Name, CaloricContent) as select Name, CaloricContent from List;
--Create procedure MinOfCalories @calories int output as select @calories=min(CaloricContent) from List;
--Create procedure MaxOfCalories @calories int output as select @calories=max(CaloricContent) from List;
--Create procedure AvgOfCalories @calories int output as select @calories=avg(CaloricContent) from List;
--Create procedure CountVegetables @vegetables int output as select @vegetables = count(*) from List where Type = N'Овочі';
--Create procedure CountFruits @fruits int output as select @fruits = count(*) from List where Type = N'Фрукти' or Type = N'Ягоди';
CREATE PROCEDURE [dbo].[CountByColor]
    @color NVARCHAR(20),
    @quantity INT OUTPUT
AS
BEGIN
    SELECT @quantity = COUNT(*) 
    FROM List
    WHERE Color = @color;
END;
GO
/****** Object:  StoredProcedure [dbo].[CountEachColor]    Script Date: 29.08.2024 11:33:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Create view [Caloric content of each product] (Name, CaloricContent) as select Name, CaloricContent from List;
--Create procedure MinOfCalories @calories int output as select @calories=min(CaloricContent) from List;
--Create procedure MaxOfCalories @calories int output as select @calories=max(CaloricContent) from List;
--Create procedure AvgOfCalories @calories int output as select @calories=avg(CaloricContent) from List;
--Create procedure CountVegetables @vegetables int output as select @vegetables = count(*) from List where Type = N'Овочі';
--Create procedure CountFruits @fruits int output as select @fruits = count(*) from List where Type = N'Фрукти' or Type = N'Ягоди';
/*CREATE PROCEDURE CountByColor
    @color NVARCHAR(20),
    @quantity INT OUTPUT
AS
BEGIN
    SELECT @quantity = COUNT(*) 
    FROM List
    WHERE Color = @color;
END;*/
CREATE PROCEDURE [dbo].[CountEachColor]
AS
BEGIN
    SELECT Color, COUNT(*) AS Count
    FROM List
    GROUP BY Color;
END;
GO
/****** Object:  StoredProcedure [dbo].[CountFruits]    Script Date: 29.08.2024 11:33:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[CountFruits] @fruits int output as select @fruits = count(*) from List where Type = N'Фрукти' or Type = N'Ягоди';
GO
/****** Object:  StoredProcedure [dbo].[CountVegetables]    Script Date: 29.08.2024 11:33:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[CountVegetables] @vegetables int output as select @vegetables = count(*) from List where Type = N'Овочі';
GO
/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 29.08.2024 11:33:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*CREATE PROCEDURE AddColor
    @name NVARCHAR(50),
    @type NCHAR(10),
    @color NVARCHAR(20),
    @calories int
AS INSERT INTO List(Name, Type, Color, CaloricContent) VALUES(@name, @type, @color, @calories);*/

/*CREATE PROCEDURE UpdateProduct
    @id INT,
    @name NVARCHAR(50),
    @type NCHAR(10),
    @color NVARCHAR(20),
    @calories INT
AS UPDATE List SET Name = @name, Type = @type, Color = @color, CaloricContent = @calories where Id = @id;*/

CREATE PROCEDURE [dbo].[DeleteProduct] @id INT AS DELETE FROM List where Id = @id;
GO
/****** Object:  StoredProcedure [dbo].[MaxOfCalories]    Script Date: 29.08.2024 11:33:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Create view [Caloric content of each product] (Name, CaloricContent) as select Name, CaloricContent from List;
--Create procedure MinOfCalories @calories int output as select @calories=min(CaloricContent) from List;
Create procedure [dbo].[MaxOfCalories] @calories int output as select @calories=max(CaloricContent) from List;
GO
/****** Object:  StoredProcedure [dbo].[MinOfCalories]    Script Date: 29.08.2024 11:33:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Create view [Caloric content of each product] (Name, CaloricContent) as select Name, CaloricContent from List;
Create procedure [dbo].[MinOfCalories] @calories int output
as select @calories=min(CaloricContent) from List;
GO
/****** Object:  StoredProcedure [dbo].[UpdateProduct]    Script Date: 29.08.2024 11:33:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*CREATE PROCEDURE AddColor
    @name NVARCHAR(50),
    @type NCHAR(10),
    @color NVARCHAR(20),
    @calories int
AS INSERT INTO List(Name, Type, Color, CaloricContent) VALUES(@name, @type, @color, @calories);*/

CREATE PROCEDURE [dbo].[UpdateProduct]
    @id INT,
    @name NVARCHAR(50),
    @type NCHAR(10),
    @color NVARCHAR(20),
    @calories INT
AS UPDATE List SET Name = @name, Type = @type, Color = @color, CaloricContent = @calories where Id = @id;
GO
USE [master]
GO
ALTER DATABASE [FruitsAndVegetablesDB] SET  READ_WRITE 
GO
