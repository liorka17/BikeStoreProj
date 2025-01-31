SET NOCOUNT ON;  
-- ���� ���� ������ �� ���� ������ ������� ��� �����.

GO  

USE master;  
-- ����� �� ����� ���� ������� ����� (master).

GO  

-- ����� �� �������� ���� ������� ������� �� ����
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'BikeStore')
BEGIN
    ALTER DATABASE BikeStore SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE BikeStore;
END
GO  

-- ����� ��� �������
DECLARE @device_directory NVARCHAR(520);  
-- ����� ����� ����� ���� ��� �������
SELECT @device_directory = SUBSTRING(filename, 1, CHARINDEX(N'master.mdf', LOWER(filename)) - 1)  
FROM master.dbo.sysaltfiles WHERE dbid = 1 AND fileid = 1;

EXECUTE (N'CREATE DATABASE BikeStore  
  ON PRIMARY (NAME = N''BikeStore'', FILENAME = N''' + @device_directory + N'BikeStore.mdf'')  
  LOG ON (NAME = N''BikeStore_log'', FILENAME = N''' + @device_directory + N'BikeStore.ldf'')');
-- ����� ��� ������� "BikeStore" �� ���� ��� ����� ������ ������ �����
GO  

USE BikeStore;  
-- ���� �� ����� ���� ������� "BikeStore".

GO  

-- ����� ������� �� ������
IF OBJECT_ID('dbo.SupplyTransactions', 'U') IS NOT NULL DROP TABLE dbo.SupplyTransactions;
IF OBJECT_ID('dbo.BikeTypes', 'U') IS NOT NULL DROP TABLE dbo.BikeTypes;
IF OBJECT_ID('dbo.Suppliers', 'U') IS NOT NULL DROP TABLE dbo.Suppliers;
IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL DROP TABLE dbo.Customers;
IF OBJECT_ID('dbo.Orders', 'U') IS NOT NULL DROP TABLE dbo.Orders;
IF OBJECT_ID('dbo.OrderDetails', 'U') IS NOT NULL DROP TABLE dbo.OrderDetails;
-- ����� ������� ����� ��� ������ ��� ������ �������
GO  

-- ����� ���� Suppliers
CREATE TABLE dbo.Suppliers (
    SupplierID INT IDENTITY(1, 1) NOT NULL, -- ���� ������ ��� ���
    SupplierName NVARCHAR(100) NOT NULL,   -- �� ����
    ContactName NVARCHAR(50) NOT NULL,    -- �� ��� ����
    Email NVARCHAR(100) NOT NULL,         -- ������ �� ����
    PhoneNumber NVARCHAR(15) NOT NULL,    -- ���� �����
    Address NVARCHAR(250) NOT NULL,       -- ����� ����
    CONSTRAINT PK_SupplierID PRIMARY KEY CLUSTERED (SupplierID) -- ���� ����
);
GO

-- ����� �������� ����� Suppliers
CREATE INDEX IX_SupplierName ON dbo.Suppliers(SupplierName); -- ������ �� �� ����
CREATE INDEX IX_ContactName ON dbo.Suppliers(ContactName); -- ������ �� �� ��� ����
GO

-- ����� ���� BikeTypes
CREATE TABLE dbo.BikeTypes (
    BikeID INT IDENTITY(1, 1) NOT NULL,   -- ���� ������ ��� ��� �������
    BikeSize NVARCHAR(15) NOT NULL,      -- ���� ��������
    Color NVARCHAR(15) NOT NULL,         -- ��� ��������
    Type NVARCHAR(15) NOT NULL,          -- ��� ��������
    StockQuantity INT NOT NULL DEFAULT 0,-- ���� ��������
    SalePrice DECIMAL(18, 2) NOT NULL DEFAULT 0.00, -- ���� ������
    SupplierID INT NOT NULL,             -- ���� ���� (���� ��)
    CONSTRAINT PK_BikeID PRIMARY KEY CLUSTERED (BikeID), -- ���� ����
    CONSTRAINT FK_Bike_Supplier FOREIGN KEY (SupplierID) REFERENCES dbo.Suppliers(SupplierID) -- ��� �� �����
);
GO

-- ����� �������� ����� BikeTypes
CREATE INDEX IX_BikeType ON dbo.BikeTypes(Type); -- ������ �� ��� ��������
CREATE INDEX IX_BikeColor ON dbo.BikeTypes(Color); -- ������ �� ��� ��������
GO

-- ����� ���� SupplyTransactions
CREATE TABLE dbo.SupplyTransactions (
    SupplyID INT IDENTITY(1, 1) NOT NULL, -- ���� ������ ��� ���� �����
    SupplierID INT NOT NULL,              -- ���� ���� (���� ��)
    BikeID INT NOT NULL,                  -- ���� ��� �������� (���� ��)
    Quantity INT NOT NULL,                -- ���� �������� ������
    SupplyDate DATETIME NOT NULL DEFAULT GETDATE(), -- ����� ������
    CONSTRAINT PK_SupplyID PRIMARY KEY (SupplyID), -- ���� ����
    CONSTRAINT FK_Supply_Supplier FOREIGN KEY (SupplierID) REFERENCES dbo.Suppliers(SupplierID), -- ��� �� �����
    CONSTRAINT FK_Supply_Bike FOREIGN KEY (BikeID) REFERENCES dbo.BikeTypes(BikeID) -- ��� �� ���� ��������
);
GO

-- ����� �������� ����� SupplyTransactions
CREATE INDEX IX_SupplyDate ON dbo.SupplyTransactions(SupplyDate); -- ������ �� ����� ������
CREATE INDEX IX_SupplierID_BikeID ON dbo.SupplyTransactions(SupplierID, BikeID); -- ������ ����� �� ���� ���� ��������
GO

-- ����� ���� Customers
CREATE TABLE dbo.Customers (
    CustomerID INT IDENTITY(1, 1) NOT NULL, -- ���� ������ ��� ����
    FirstName NVARCHAR(50) NOT NULL,       -- �� ���� �� �����
    LastName NVARCHAR(50) NOT NULL,        -- �� ����� �� �����
    Email NVARCHAR(100) NOT NULL,          -- ������
    PhoneNumber NVARCHAR(15) NOT NULL,     -- ���� �����
    Address NVARCHAR(250) NOT NULL,        -- �����
    CONSTRAINT PK_CustomerID PRIMARY KEY CLUSTERED (CustomerID), -- ���� ����
    CONSTRAINT UQ_Customers_PhoneNumber UNIQUE (PhoneNumber) -- ����� �������� �� ���� ������
);
GO

-- ���� �������� ������ ��� PhoneNumber
WITH CTE AS (
    SELECT 
        CustomerID, 
        PhoneNumber, 
        ROW_NUMBER() OVER (PARTITION BY PhoneNumber ORDER BY CustomerID) AS RowNum
    FROM Customers
)
DELETE FROM Customers
WHERE CustomerID IN (
    SELECT CustomerID 
    FROM CTE
    WHERE RowNum > 1
);
GO

GO

-- ����� �������� ����� Customers
CREATE INDEX IX_CustomerEmail ON dbo.Customers(Email); -- ������ �� ������
CREATE INDEX IX_CustomerPhoneNumber ON dbo.Customers(PhoneNumber); -- ������ �� ���� �����
GO

-- ����� ���� Orders
CREATE TABLE dbo.Orders (
    OrderID INT IDENTITY(1, 1) NOT NULL, -- ���� ������ ��� �����
    CustomerID INT NOT NULL,             -- ���� ����� (���� ��)
    OrderDate DATETIME NOT NULL,         -- ����� ������
    TotalAmount DECIMAL(18, 2) NOT NULL, -- ���� ���� ������
    CONSTRAINT PK_OrderID PRIMARY KEY CLUSTERED (OrderID), -- ���� ����
    CONSTRAINT FK_Order_Customer FOREIGN KEY (CustomerID) REFERENCES dbo.Customers(CustomerID) -- ��� �� ������
);
GO

-- ����� �������� ����� Orders
CREATE INDEX IX_OrderDate ON dbo.Orders(OrderDate); -- ������ �� ����� ������
CREATE INDEX IX_CustomerID ON dbo.Orders(CustomerID); -- ������ �� ���� �����
GO

-- ����� ���� OrderDetails
CREATE TABLE dbo.OrderDetails (
    OrderDetailID INT IDENTITY(1, 1) NOT NULL, -- ���� ������ ��� ���� ���� �����
    OrderID INT NOT NULL,                      -- ���� ������ (���� ��)
    BikeID INT NOT NULL,                       -- ���� ��� �������� (���� ��)
    Quantity INT NOT NULL,                     -- ���� �������� �������
    UnitPrice DECIMAL(18, 2) NOT NULL,         -- ���� ������
    TotalPrice AS (Quantity * UnitPrice) PERSISTED, -- ���� ���� �����
    CONSTRAINT PK_OrderDetailID PRIMARY KEY CLUSTERED (OrderDetailID), -- ���� ����
    CONSTRAINT FK_Order_OrderDetail FOREIGN KEY (OrderID) REFERENCES dbo.Orders(OrderID), -- ��� �� ������
    CONSTRAINT FK_Bike_OrderDetail FOREIGN KEY (BikeID) REFERENCES dbo.BikeTypes(BikeID) -- ��� �� ���� ��������
);
GO

-- ����� �������� ����� OrderDetails
CREATE INDEX IX_OrderID ON dbo.OrderDetails(OrderID); -- ������ �� ���� ������
CREATE INDEX IX_BikeID ON dbo.OrderDetails(BikeID); -- ������ �� ���� ��� ��������
GO

-- ����� ������ ������ ����� Suppliers
INSERT INTO dbo.Suppliers (SupplierName, ContactName, Email, PhoneNumber, Address)
VALUES ('China Supplier', 'Li Wang', 'china.supplier@example.com', '972-52-1234567', 'Beijing, China'),
       ('Israel Supplier', 'David Cohen', 'israel.supplier@example.com', '972-50-9876543', 'Tel Aviv, Israel'),
       ('USA Supplier', 'John Smith', 'usa.supplier@example.com', '972-52-5555555', 'New York, USA');
GO

INSERT INTO dbo.BikeTypes (BikeSize, Color, Type, StockQuantity, SalePrice, SupplierID)
VALUES 
    -- ������� ����� 29 Inch
    ('29 Inch', 'Red', 'Racing Bikes', 30, 1500.00, 1),
	('29 Inch', 'Black', 'Racing Bikes', 30, 1500.00, 1),
	('29 Inch', 'Blue', 'Racing Bikes', 30, 1500.00, 1),

    -- ������� ����� 26 Inch
    ('26 Inch', 'Red', 'Racing Bikes', 20, 1550.00, 1),
    ('26 Inch', 'Black', 'Road Bikes', 18, 1250.00, 2),
	('26 Inch', 'Blue', 'Road Bikes', 18, 1250.00, 2),

    -- ������� ����� 14 Inch (�����)
    ('14 Inch', 'Blue', 'Kids Bikes', 20, 900.00, 3),
    ('14 Inch', 'Red', 'Kids Bikes', 10, 910.00, 3),
	('14 Inch', 'Black', 'Kids Bikes', 10, 910.00, 3),

    -- ������� ����� 18 Inch
    ('18 Inch', 'Red', 'Racing Bikes', 18, 1600.00, 2),
	('18 Inch', 'Black', 'Racing Bikes', 18, 1600.00, 2),
	('18 Inch', 'Blue', 'Racing Bikes', 18, 1600.00, 2)

GO


-- ����� ������ ������ ����� Customers
INSERT INTO dbo.Customers (FirstName, LastName, Email, PhoneNumber, Address)
VALUES ('Adam', 'Levi', 'adam.levi@example.com', '972-54-3456789', 'Haifa, Israel'),
       ('Noa', 'Cohen', 'noa.cohen@example.com', '972-52-9876543', 'Jerusalem, Israel'),
       ('Daniel', 'Friedman', 'daniel.friedman@example.com', '972-50-1234567', 'Tel Aviv, Israel');
GO

-- ����� 10 ������ ���� 2021
INSERT INTO dbo.Orders (CustomerID, OrderDate, TotalAmount)
VALUES 
(1, '2021-01-05', 1500.00),
(2, '2021-02-12', 1800.00),
(3, '2021-03-15', 2500.00),
(1, '2021-04-20', 1900.00),
(2, '2021-05-25', 1200.00),
(3, '2021-06-30', 2700.00),
(1, '2021-07-18', 2200.00),
(2, '2021-08-15', 1700.00),
(3, '2021-09-20', 2100.00),
(1, '2021-10-25', 3000.00);

-- ����� 10 ������ ���� 2022
INSERT INTO dbo.Orders (CustomerID, OrderDate, TotalAmount)
VALUES 
(1, '2022-01-05', 1500.00),
(2, '2022-02-12', 1800.00),
(3, '2022-03-15', 2500.00),
(1, '2022-04-20', 1900.00),
(2, '2022-05-25', 1200.00),
(3, '2022-06-30', 2700.00),
(1, '2022-07-18', 2200.00),
(2, '2022-08-15', 1700.00),
(3, '2022-09-20', 2100.00),
(1, '2022-10-25', 3000.00);

-- ����� 10 ������ ���� 2023
INSERT INTO dbo.Orders (CustomerID, OrderDate, TotalAmount)
VALUES 
(1, '2023-01-05', 1500.00),
(2, '2023-02-12', 1800.00),
(3, '2023-03-15', 2500.00),
(1, '2023-04-20', 1900.00),
(2, '2023-05-25', 1200.00),
(3, '2023-06-30', 2700.00),
(1, '2023-07-18', 2200.00),
(2, '2023-08-15', 1700.00),
(3, '2023-09-20', 2100.00),
(1, '2023-10-25', 3000.00);

-- ����� 10 ������ ���� 2024
INSERT INTO dbo.Orders (CustomerID, OrderDate, TotalAmount)
VALUES 
(1, '2024-01-05', 1500.00),
(2, '2024-02-12', 1800.00),
(3, '2024-03-15', 2500.00),
(1, '2024-04-20', 1900.00),
(2, '2024-05-25', 1200.00),
(3, '2024-06-30', 2700.00),
(1, '2024-07-18', 2200.00),
(2, '2024-08-15', 1700.00),
(3, '2024-09-20', 2100.00),
(1, '2024-10-25', 3000.00);
GO


-- ����� ������ ������ ����� OrderDetails
INSERT INTO dbo.OrderDetails (OrderID, BikeID, Quantity, UnitPrice)
VALUES (1, 1, 2, 1500.00),
       (2, 2, 1, 1200.00),
       (3, 3, 1, 900.00);
GO

-- ����� ������ ������ ����� SupplyTransactions
INSERT INTO dbo.SupplyTransactions (SupplierID, BikeID, Quantity)
VALUES (1, 1, 10), (2, 2, 20), (3, 3, 5);
GO

CREATE TRIGGER trg_AutoSupplyOnLowStock
ON dbo.BikeTypes
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- ����� ������ ����� SupplyTransactions ���� ����� ���� ���� �-15
    INSERT INTO dbo.SupplyTransactions (SupplierID, BikeID, Quantity)
    SELECT 
        inserted.SupplierID, 
        inserted.BikeID, 
        20 -- ���� ������ (����� ����)
    FROM inserted
    JOIN deleted
        ON inserted.BikeID = deleted.BikeID
    WHERE 
        inserted.StockQuantity < 15 -- ����� ��� ���� �-15
        AND deleted.StockQuantity >= 15; -- ���� ������ ����� ��� 15 �� ����

    -- ����� ����� ����� BikeTypes ���� ������ ���������
    UPDATE dbo.BikeTypes
    SET StockQuantity = StockQuantity + 20 -- ����� ���� �� ���� ������
    WHERE BikeID IN (
        SELECT BikeID
        FROM inserted
        WHERE StockQuantity < 15
    );
END;
GO

-- ����� ���� ���� ��� ����� �� ������
UPDATE dbo.BikeTypes
SET StockQuantity = 10
WHERE BikeID = 3;

-- ����� �� ������� ����� ����� SupplyTransactions
SELECT * FROM dbo.SupplyTransactions;

-- ����� �� ����� ������ ����� BikeTypes
SELECT * FROM dbo.BikeTypes WHERE BikeID = 1;

-- ������
SELECT * FROM dbo.Customers;
SELECT * FROM dbo.Orders;
SELECT * FROM dbo.OrderDetails;
SELECT * FROM dbo.BikeTypes --where BikeID =3
SELECT * FROM dbo.Suppliers;
SELECT * FROM dbo.SupplyTransactions;
GO

--enable TRIGGER trg_AutoSupplyOnLowStock
--ON dbo.BikeTypes