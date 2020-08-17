IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200704103557_UpdatedItemOrder')
BEGIN
    ALTER TABLE [ItemOrder] ADD [Message] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200704103557_UpdatedItemOrder')
BEGIN
    ALTER TABLE [ItemOrder] ADD [Table] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200704103557_UpdatedItemOrder')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200704103557_UpdatedItemOrder', N'3.1.5');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200706054209_Checkup')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200706054209_Checkup', N'3.1.5');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200707045023_AddedAvailabilityToItemModel')
BEGIN
    ALTER TABLE [Item] ADD [Available] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200707045023_AddedAvailabilityToItemModel')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200707045023_AddedAvailabilityToItemModel', N'3.1.5');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200715062458_AddedDailyDealOrder')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200715062458_AddedDailyDealOrder')
BEGIN
    CREATE INDEX [IX_DailyDealOrder_DailyDeal] ON [DailyDealOrder] ([DailyDeal]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200715062458_AddedDailyDealOrder')
BEGIN
    CREATE INDEX [IX_DailyDealOrder_OrderType] ON [DailyDealOrder] ([OrderType]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200715062458_AddedDailyDealOrder')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200715062458_AddedDailyDealOrder', N'3.1.5');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200715072623_FixedDailyDeal')
BEGIN
    EXEC sp_rename N'[DailyDeal].[DailyealID]', N'DailyDealID', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200715072623_FixedDailyDeal')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200715072623_FixedDailyDeal', N'3.1.5');
END;

GO

