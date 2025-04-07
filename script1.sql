IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Venues] (
    [VenueId] int NOT NULL IDENTITY,
    [street] nvarchar(max) NOT NULL,
    [city] nvarchar(max) NOT NULL,
    [state] nvarchar(max) NOT NULL,
    [zipCode] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Venues] PRIMARY KEY ([VenueId])
);

CREATE TABLE [Events] (
    [EventId] int NOT NULL IDENTITY,
    [eventName] nvarchar(max) NOT NULL,
    [eventDescription] nvarchar(max) NOT NULL,
    [startDate] datetime2 NOT NULL,
    [isÀctive] bit NOT NULL,
    [endDate] datetime2 NOT NULL,
    [VenueId] int NOT NULL,
    [totalQuantity] int NOT NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY ([EventId]),
    CONSTRAINT [FK_Events_Venues_VenueId] FOREIGN KEY ([VenueId]) REFERENCES [Venues] ([VenueId]) ON DELETE CASCADE
);

CREATE TABLE [PricingTier] (
    [PricingTierId] int NOT NULL IDENTITY,
    [tierName] nvarchar(max) NOT NULL,
    [price] decimal(18,2) NOT NULL,
    [totalTicket] int NOT NULL,
    [EventId] int NOT NULL,
    CONSTRAINT [PK_PricingTier] PRIMARY KEY ([PricingTierId]),
    CONSTRAINT [FK_PricingTier_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [Events] ([EventId]) ON DELETE CASCADE
);

CREATE INDEX [IX_Events_VenueId] ON [Events] ([VenueId]);

CREATE INDEX [IX_PricingTier_EventId] ON [PricingTier] ([EventId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250403163550_InitialCreate', N'9.0.3');

ALTER TABLE [PricingTier] DROP CONSTRAINT [FK_PricingTier_Events_EventId];

DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PricingTier]') AND [c].[name] = N'EventId');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [PricingTier] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [PricingTier] ALTER COLUMN [EventId] int NULL;

ALTER TABLE [PricingTier] ADD CONSTRAINT [FK_PricingTier_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [Events] ([EventId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250406012028_newChange', N'9.0.3');

COMMIT;
GO

