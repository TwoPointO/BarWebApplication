IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200801205402_InitialSmarterAsp')
BEGIN
    CREATE TABLE [Category] (
        [CategoryID] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Category] PRIMARY KEY ([CategoryID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200801205402_InitialSmarterAsp')
BEGIN
    CREATE TABLE [DailyDeal] (
        [DailyDealID] int NOT NULL IDENTITY,
        [DailyDealName] nvarchar(50) NULL,
        [DailyDealImage] varbinary(max) NULL,
        [DailyDealDescription] nvarchar(500) NULL,
        [DailyDealPrice] decimal(18, 0) NULL,
        [DailyDealQuantity] int NULL,
        [DailyDealDate] datetime NULL,
        CONSTRAINT [PK__DailyDea__2E1B31B29C667241] PRIMARY KEY ([DailyDealID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200801205402_InitialSmarterAsp')
BEGIN
    CREATE TABLE [OrderType] (
        [TypeID] bit NOT NULL,
        [OrderType] nvarchar(50) NOT NULL,
        CONSTRAINT [PK__OrderTyp__516F0395FC748589] PRIMARY KEY ([TypeID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200801205402_InitialSmarterAsp')
BEGIN
    CREATE TABLE [Reservation] (
        [ReservationID] int NOT NULL IDENTITY,
        [DateAndTime] datetime NOT NULL,
        [LastName] nvarchar(50) NOT NULL,
        [PeopleNo] int NOT NULL,
        CONSTRAINT [PK_Reservation] PRIMARY KEY ([ReservationID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200801205402_InitialSmarterAsp')
BEGIN
    CREATE TABLE [Item] (
        [ItemID] int NOT NULL IDENTITY,
        [ItemName] nvarchar(50) NOT NULL,
        [Price] decimal(18, 0) NOT NULL,
        [ItemDescription] nvarchar(500) NULL,
        [ImageData] varbinary(max) NULL,
        [Category] int NOT NULL,
        [Available] bit NOT NULL,
        CONSTRAINT [PK_Item] PRIMARY KEY ([ItemID]),
        CONSTRAINT [FK__Item__Category__398D8EEE] FOREIGN KEY ([Category]) REFERENCES [Category] ([CategoryID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200801205402_InitialSmarterAsp')
BEGIN
    CREATE TABLE [DailyDealOrder] (
        [OrderID] int NOT NULL IDENTITY,
        [DailyDeal] int NOT NULL,
        [Quantity] int NOT NULL,
        [Price] decimal(18, 0) NOT NULL,
        [OrderType] bit NOT NULL,
        [CurrentStatus] bit NULL,
        [OrderMessage] nvarchar(500) NULL,
        [Table] int NOT NULL,
        [Message] nvarchar(max) NULL,
        CONSTRAINT [PK_DailyDealOrder] PRIMARY KEY ([OrderID]),
        CONSTRAINT [FK_DailyDealOrder_DailyDeal_DailyDeal] FOREIGN KEY ([DailyDeal]) REFERENCES [DailyDeal] ([DailyDealID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_DailyDealOrder_OrderType_OrderType] FOREIGN KEY ([OrderType]) REFERENCES [OrderType] ([TypeID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200801205402_InitialSmarterAsp')
BEGIN
    CREATE TABLE [ItemOrder] (
        [OrderID] int NOT NULL IDENTITY,
        [Item] int NOT NULL,
        [Quantity] int NOT NULL,
        [Price] decimal(18, 0) NOT NULL,
        [OrderType] bit NOT NULL,
        [CurrentStatus] bit NULL,
        [OrderMessage] nvarchar(500) NULL,
        [Table] int NOT NULL,
        [Message] nvarchar(max) NULL,
        CONSTRAINT [PK__ItemOrde__C3905BAF9BDD4A3C] PRIMARY KEY ([OrderID]),
        CONSTRAINT [FK__ItemOrder__Item__3E52440B] FOREIGN KEY ([Item]) REFERENCES [Item] ([ItemID]) ON DELETE NO ACTION,
        CONSTRAINT [FK__ItemOrder__Order__3F466844] FOREIGN KEY ([OrderType]) REFERENCES [OrderType] ([TypeID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200801205402_InitialSmarterAsp')
BEGIN
    CREATE INDEX [IX_DailyDealOrder_DailyDeal] ON [DailyDealOrder] ([DailyDeal]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200801205402_InitialSmarterAsp')
BEGIN
    CREATE INDEX [IX_DailyDealOrder_OrderType] ON [DailyDealOrder] ([OrderType]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200801205402_InitialSmarterAsp')
BEGIN
    CREATE INDEX [IX_Item_Category] ON [Item] ([Category]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200801205402_InitialSmarterAsp')
BEGIN
    CREATE INDEX [IX_ItemOrder_Item] ON [ItemOrder] ([Item]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200801205402_InitialSmarterAsp')
BEGIN
    CREATE INDEX [IX_ItemOrder_OrderType] ON [ItemOrder] ([OrderType]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200801205402_InitialSmarterAsp')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200801205402_InitialSmarterAsp', N'3.1.6');
END;

GO

