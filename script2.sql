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
CREATE TABLE [ticketingInventory] (
    [EventTicketInventoryId] int NOT NULL IDENTITY,
    [eventId] int NOT NULL,
    [eventName] nvarchar(max) NOT NULL,
    [eventDescription] nvarchar(max) NOT NULL,
    [ticketTier] nvarchar(max) NOT NULL,
    [pricePerTicket] decimal(18,2) NOT NULL,
    [total] int NOT NULL,
    [available] int NOT NULL,
    CONSTRAINT [PK_ticketingInventory] PRIMARY KEY ([EventTicketInventoryId])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250405235246_initial', N'9.0.3');

CREATE TABLE [Status] (
    [StatusId] int NOT NULL IDENTITY,
    [status] int NOT NULL,
    CONSTRAINT [PK_Status] PRIMARY KEY ([StatusId])
);

CREATE TABLE [booking] (
    [BookingId] int NOT NULL IDENTITY,
    [eventId] int NOT NULL,
    [userEmail] nvarchar(max) NOT NULL,
    [transactionRef] uniqueidentifier NOT NULL,
    [ReservedTime] datetime2 NOT NULL,
    [FinalizationTime] datetime2 NOT NULL,
    [StatusId] int NOT NULL,
    [Notes] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_booking] PRIMARY KEY ([BookingId]),
    CONSTRAINT [FK_booking_Status_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [Status] ([StatusId]) ON DELETE CASCADE
);

CREATE TABLE [bookingTicketTiers] (
    [BookingTicketTierId] int NOT NULL IDENTITY,
    [TierName] nvarchar(max) NOT NULL,
    [ReservedQuantity] nvarchar(max) NOT NULL,
    [TotalPrice] decimal(18,2) NOT NULL,
    [BookingId] int NOT NULL,
    CONSTRAINT [PK_bookingTicketTiers] PRIMARY KEY ([BookingTicketTierId]),
    CONSTRAINT [FK_bookingTicketTiers_booking_BookingId] FOREIGN KEY ([BookingId]) REFERENCES [booking] ([BookingId]) ON DELETE CASCADE
);

CREATE INDEX [IX_booking_StatusId] ON [booking] ([StatusId]);

CREATE INDEX [IX_bookingTicketTiers_BookingId] ON [bookingTicketTiers] ([BookingId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250406163040_bookings', N'9.0.3');

ALTER TABLE [booking] DROP CONSTRAINT [FK_booking_Status_StatusId];

DROP TABLE [Status];

DROP INDEX [IX_booking_StatusId] ON [booking];

DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[booking]') AND [c].[name] = N'StatusId');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [booking] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [booking] DROP COLUMN [StatusId];

ALTER TABLE [booking] ADD [status] nvarchar(max) NOT NULL DEFAULT N'';

ALTER TABLE [booking] ADD CONSTRAINT [CK_Booking_Status_Valid] CHECK ([Status] IN ('Confirmed', 'Reserved', 'Failed'));

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250406163641_bookings-uppated', N'9.0.3');

ALTER TABLE [booking] ADD [totalPrice] decimal(18,2) NOT NULL DEFAULT 0.0;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250406192720_bookings-uppated2', N'9.0.3');

COMMIT;
GO

