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
END
GO

-- Seed random-ish demo data only if table is empty
IF NOT EXISTS (SELECT 1 FROM dbo.Customers)
BEGIN
    PRINT 'Inserting random demo customers...';

    DECLARE @i INT = 1;
    DECLARE @max INT = 20;

    DECLARE @FirstNames TABLE (Id INT IDENTITY(1,1), Name NVARCHAR(100));
    DECLARE @LastNames  TABLE (Id INT IDENTITY(1,1), Name NVARCHAR(100));

    INSERT INTO @FirstNames (Name) VALUES
        (N'John'), (N'Jane'), (N'Mark'), (N'Anna'), (N'Peter'),
        (N'Sara'), (N'Michael'), (N'Laura'), (N'David'), (N'Emma');

    INSERT INTO @LastNames (Name) VALUES
        (N'Doe'), (N'Smith'), (N'Brown'), (N'Johnson'), (N'Williams'),
        (N'Taylor'), (N'Miller'), (N'Wilson'), (N'Moore'), (N'Clark');

    WHILE @i <= @max
    BEGIN
        DECLARE @fnId INT = 1 + ABS(CHECKSUM(NEWID())) % 10;
        DECLARE @lnId INT = 1 + ABS(CHECKSUM(NEWID())) % 10;

        DECLARE @FirstName NVARCHAR(100);
        DECLARE @LastName  NVARCHAR(100);
        DECLARE @Email     NVARCHAR(256);
        DECLARE @IsActive  BIT;

        SELECT @FirstName = Name FROM @FirstNames WHERE Id = @fnId;
        SELECT @LastName  = Name FROM @LastNames  WHERE Id = @lnId;

        SET @Email = LOWER(@FirstName + '.' + @LastName + CAST(@i AS NVARCHAR(10)) + '@example.com');
        SET @IsActive = CASE WHEN ABS(CHECKSUM(NEWID())) % 2 = 0 THEN 1 ELSE 0 END;

        INSERT INTO dbo.Customers (FirstName, LastName, Email, IsActive)
        VALUES (@FirstName, @LastName, @Email, @IsActive);

        SET @i += 1;
    END
END
ELSE
BEGIN
    PRINT 'dbo.Customers already contains data. Skipping seed.';
END
GO
