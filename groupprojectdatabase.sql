--create database PRN212_Vaccines_Tracking_2

-- Updated Table Creation Script with IDENTITY
CREATE TABLE [Customer] (
    [id] INTEGER IDENTITY(1,1) NOT NULL UNIQUE,
    [username] VARCHAR(50) NOT NULL,
    [password] VARCHAR(100) NOT NULL,
    [dateOfBirth] DATETIME NOT NULL,
    [email] VARCHAR(100) NOT NULL,
    [phone] VARCHAR(20) NOT NULL,
    [createdAt] DATETIME NOT NULL DEFAULT GETDATE(),
    [status] INTEGER NOT NULL,
    PRIMARY KEY([id])
);
GO

CREATE TABLE [Child] (
    [id] INTEGER IDENTITY(1,1) NOT NULL UNIQUE,
    [parentId] INTEGER NOT NULL,
    [name] VARCHAR(100) NOT NULL,
    [dateOfBirth] DATETIME NOT NULL,
    [gender] CHAR(1) NOT NULL CHECK ([gender] IN ('M', 'F', 'O')),
    [status] INTEGER NOT NULL,
    PRIMARY KEY([id])
);
GO

CREATE TABLE [Vaccine] (
    [id] INTEGER IDENTITY(1,1) NOT NULL UNIQUE,
    [name] VARCHAR(100) NOT NULL,
    [price] MONEY NOT NULL,
    [description] VARCHAR(500) NOT NULL,
    [dosesTimes] INTEGER NOT NULL,
    PRIMARY KEY([id])
);
GO

CREATE TABLE [VaccineDetails] (
    [vaccineDetailsId] INTEGER IDENTITY(1,1) NOT NULL,
    [vaccineId] INTEGER NOT NULL,
    [entryDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [quantity] BIGINT NOT NULL,
    [status] INTEGER NOT NULL,
    PRIMARY KEY([vaccineDetailsId])
);
GO

CREATE TABLE [Booking] (
    [id] INTEGER IDENTITY(1,1) NOT NULL UNIQUE,
    [parentId] INTEGER NOT NULL,
    [finalPrice] MONEY NOT NULL,
    [createdAt] DATETIME NOT NULL DEFAULT GETDATE(),
    [status] INTEGER NOT NULL,
    PRIMARY KEY([id])
);
GO

CREATE TABLE [Booking_Vaccine] (
    [bookingId] INTEGER NOT NULL,
    [vaccineId] INTEGER NOT NULL,
    PRIMARY KEY([bookingId], [vaccineId])
);
GO

CREATE TABLE [Booking_Child] (
    [bookingId] INTEGER NOT NULL,
    [childId] INTEGER NOT NULL,
    PRIMARY KEY([bookingId], [childId])
);
GO

CREATE TABLE [VaccinesTracking] (
	[id] INTEGER IDENTITY(1,1) NOT NULL UNIQUE,
	[previousId] INTEGER,
	[dateVaccination] DATETIME NOT NULL,
	[childId] INTEGER NOT NULL,
	[parentId] INTEGER NOT NULL,
	[bookingId] INTEGER NOT NULL,
	[vaccineId] INTEGER NOT NULL,
	[status] INTEGER NOT NULL,
	PRIMARY KEY([id])
);
GO

ALTER TABLE [VaccineDetails]
ADD FOREIGN KEY([vaccineId]) REFERENCES [Vaccine]([id])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Child]
ADD FOREIGN KEY([parentId]) REFERENCES [Customer]([id])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Booking]
ADD FOREIGN KEY([parentId]) REFERENCES [Customer]([id])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Booking_Vaccine]
ADD FOREIGN KEY([bookingId]) REFERENCES [Booking]([id])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Booking_Vaccine]
ADD FOREIGN KEY([vaccineId]) REFERENCES [Vaccine]([id])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Booking_Child]
ADD FOREIGN KEY([bookingId]) REFERENCES [Booking]([id])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Booking_Child]
ADD FOREIGN KEY([childId]) REFERENCES [Child]([id])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [VaccinesTracking]
ADD FOREIGN KEY([previousId]) REFERENCES [VaccinesTracking]([id])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [VaccinesTracking]
ADD FOREIGN KEY([childId]) REFERENCES [Child]([id])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [VaccinesTracking]
ADD FOREIGN KEY([parentId]) REFERENCES [Customer]([id])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [VaccinesTracking]
ADD FOREIGN KEY([bookingId]) REFERENCES [Booking]([id])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [VaccinesTracking]
ADD FOREIGN KEY([vaccineId]) REFERENCES [Vaccine]([id])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO

-- Foreign Key Constraints remain the same as in your original script

-- Insert Test Data
-- Customers
SET IDENTITY_INSERT [Customer] OFF;
INSERT INTO [Customer] ([username], [password], [dateOfBirth], [email], [phone], [createdAt], [status])
VALUES 
('johndoe', 'hashed_password_1', '1985-05-15', 'john.doe@example.com', '+1-555-1234', GETDATE(), 1),
('janesmith', 'hashed_password_2', '1990-08-22', 'jane.smith@example.com', '+1-555-5678', GETDATE(), 1),
('mikebrown', 'hashed_password_3', '1988-03-10', 'mike.brown@example.com', '+1-555-9012', GETDATE(), 1);
GO

-- Vaccines
SET IDENTITY_INSERT [Vaccine] OFF;
INSERT INTO [Vaccine] ([name], [price], [description],[dosesTimes])
VALUES 
('MMR Vaccine', 89.99, 'Measles, Mumps, and Rubella combination vaccine',1),
('Polio Vaccine', 65.50, 'Inactivated Polio Vaccine (IPV)',2),
('Hepatitis B Vaccine', 45.75, 'Hepatitis B prevention vaccine',3);
GO

-- Children
SET IDENTITY_INSERT [Child] OFF;
INSERT INTO [Child] ([parentId], [name], [dateOfBirth], [gender], [status])
VALUES 
(1, 'Emma Doe', '2021-03-10', 'F', 1),
(2, 'Liam Smith', '2020-11-05', 'M', 1),
(3, 'Sophia Brown', '2022-01-15', 'F', 1);
GO

-- Bookings
SET IDENTITY_INSERT [Booking] OFF;
INSERT INTO [Booking] ([parentId], [finalPrice], [createdAt], [status])
VALUES 
(1, 155.49, GETDATE(), 1),
(2, 120.50, GETDATE(), 1),
(3, 200.00, GETDATE(), 1);
GO

-- Booking_Vaccine
INSERT INTO [Booking_Vaccine] ([bookingId], [vaccineId])
VALUES 
(1, 1),
(2, 2),
(3, 3);
GO

-- Booking_Child
INSERT INTO [Booking_Child] ([bookingId], [childId])
VALUES 
(1, 1),
(2, 2),
(3, 3);
GO

-- VaccineDetails
SET IDENTITY_INSERT [VaccineDetails] OFF;
INSERT INTO [VaccineDetails] ([vaccineId], [entryDate], [quantity], [status])
VALUES 
(1, GETDATE(), 100, 1),
(2, GETDATE(), 75, 1),
(3, GETDATE(), 50, 1);
GO

-- VaccinesTracking
SET IDENTITY_INSERT [VaccinesTracking] OFF;
INSERT INTO [VaccinesTracking] ([previousId], [dateVaccination], [childId], [parentId], [bookingId], [vaccineId], [status])
VALUES 
(1, GETDATE(), 1, 1, 1,1,1),
(2, GETDATE(), 2, 2, 2,2,0),
(3, GETDATE(), 3, 3, 3,3,1);
GO

-- Optional: Verify inserted data
SELECT * FROM [Customer];
SELECT * FROM [Child];
SELECT * FROM [Vaccine];
SELECT * FROM [VaccineDetails];
SELECT * FROM [Booking];
SELECT * FROM [Booking_Vaccine];
SELECT * FROM [Booking_Child];
SELECT * FROM [VaccinesTracking];
GO