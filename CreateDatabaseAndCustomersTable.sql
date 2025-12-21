IF DB_ID('CustomerTest') IS NULL
BEGIN
    PRINT 'Creating database CustomerTest...';
    CREATE DATABASE CustomerTest;
END
GO

USE CustomerTest;
GO

IF OBJECT_ID('dbo.Customers', 'U') IS NULL
BEGIN
    PRINT 'Creating table dbo.Customers...';
    CREATE TABLE dbo.Customers
    (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        FirstName NVARCHAR(100) NOT NULL,
        LastName  NVARCHAR(100) NOT NULL,
        Email     NVARCHAR(256) NOT NULL,
        IsActive  BIT NOT NULL DEFAULT(1)
    );

    INSERT INTO dbo.Customers (FirstName, LastName, Email, IsActive) VALUES
        ('John',  'Doe',   'john.doe@example.com', 1),
        ('Jane',  'Smith', 'jane.smith@example.com', 1),
        ('Mark',  'Brown', 'mark.brown@example.com', 0);
END
ELSE
BEGIN
    PRINT 'Table dbo.Customers already exists.';
END
GO
