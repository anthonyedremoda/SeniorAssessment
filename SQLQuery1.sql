 CREATE TABLE EventBookings (
         Id INT IDENTITY PRIMARY KEY,
         EventUdi NVARCHAR(255) NOT NULL,
         Name NVARCHAR(100) NOT NULL,
         Email NVARCHAR(100) NOT NULL,
         Note NVARCHAR(500) NULL,
         CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
     );