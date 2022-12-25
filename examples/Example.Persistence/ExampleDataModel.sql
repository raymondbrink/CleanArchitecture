﻿-- Script was generated by Devart Entity Developer, Version 6.12.1354.0
-- Script date 17-2-2022 11:17:16
-- Target Server: SQL Server
-- Server Version: 2016

-- 
-- Creating a table dbo.Translations 
-- 
CREATE TABLE dbo.Translations (
   Id INT NOT NULL IDENTITY,
   Culture NVARCHAR(5) NOT NULL,
   CONSTRAINT PK_Translations PRIMARY KEY (Id)
)
GO

-- 
-- Creating a table dbo.Companies 
-- 
CREATE TABLE dbo.Companies (
   Id UNIQUEIDENTIFIER NOT NULL,
   Name NVARCHAR(100) NOT NULL,
   CreatedAtUtc DATETIME2 NOT NULL,
   CONSTRAINT PK_Companies PRIMARY KEY (Id)
)
GO

-- 
-- Creating a table dbo.Customers 
-- 
CREATE TABLE dbo.Customers (
   Id INT NOT NULL IDENTITY,
   FamilyName NVARCHAR(50) NOT NULL,
   GivenName NVARCHAR(50),
   StoreId UNIQUEIDENTIFIER,
   CONSTRAINT PK_Customers PRIMARY KEY (Id)
)
GO

-- 
-- Creating a table dbo.Stores 
-- 
CREATE TABLE dbo.Stores (
   Id UNIQUEIDENTIFIER NOT NULL,
   CONSTRAINT PK_Stores PRIMARY KEY (Id),
   CONSTRAINT FK_Stores_Companies FOREIGN KEY (Id) REFERENCES dbo.Companies (Id)
)
GO

-- 
-- Creating a table dbo.Employees 
-- 
CREATE TABLE dbo.Employees (
   Id INT NOT NULL IDENTITY,
   FamilyName NVARCHAR(50) NOT NULL,
   GivenName NVARCHAR(50),
   CompanyId UNIQUEIDENTIFIER,
   CONSTRAINT PK_Employees PRIMARY KEY (Id),
   CONSTRAINT FK_Employees_Companies_0 FOREIGN KEY (CompanyId) REFERENCES dbo.Companies (Id)
)
GO

-- 
-- Creating a table dbo.Manufacturers 
-- 
CREATE TABLE dbo.Manufacturers (
   Id UNIQUEIDENTIFIER NOT NULL,
   FamilyName NVARCHAR(50) NOT NULL,
   GivenName NVARCHAR(50),
   CONSTRAINT PK_Manufacturers PRIMARY KEY (Id),
   CONSTRAINT FK_Manufacturers_Companies FOREIGN KEY (Id) REFERENCES dbo.Companies (Id)
)
GO

-- 
-- Creating a table dbo.Products 
-- 
CREATE TABLE dbo.Products (
   Id INT NOT NULL IDENTITY,
   ManufacturerId UNIQUEIDENTIFIER,
   AvailableFrom DATE NOT NULL,
   AvailableUntil DATE,
   ArchivedAtUtc DATETIME2,
   ArchivedBy NVARCHAR(100),
   CONSTRAINT PK_Products PRIMARY KEY (Id),
   CONSTRAINT FK_Products_Manufacturers_0 FOREIGN KEY (ManufacturerId) REFERENCES dbo.Manufacturers (Id)
)
GO

-- 
-- Creating a table dbo.StoreProducts 
-- 
CREATE TABLE dbo.StoreProducts (
   Id BIGINT NOT NULL IDENTITY,
   StoreId UNIQUEIDENTIFIER,
   ProductId INT,
   InStock INT NOT NULL,
   CONSTRAINT PK_StoreProducts PRIMARY KEY (Id),
   CONSTRAINT FK_StoreProducts_Products_0 FOREIGN KEY (ProductId) REFERENCES dbo.Products (Id)
)
GO

-- 
-- Creating a table dbo.ProductTranslations 
-- 
CREATE TABLE dbo.ProductTranslations (
   Id INT NOT NULL,
   ProductId INT,
   Name NVARCHAR(100) NOT NULL,
   Description NVARCHAR(100),
   CONSTRAINT PK_ProductTranslations PRIMARY KEY (Id),
   CONSTRAINT FK_ProductTranslations_Translations FOREIGN KEY (Id) REFERENCES dbo.Translations (Id),
   CONSTRAINT FK_ProductTranslations_Products_1 FOREIGN KEY (ProductId) REFERENCES dbo.Products (Id)
)
GO

