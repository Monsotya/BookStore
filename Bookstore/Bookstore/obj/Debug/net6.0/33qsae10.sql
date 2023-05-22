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
GO

CREATE TABLE [Authors] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Surname] nvarchar(max) NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [DateOfDeath] datetime2 NULL,
    CONSTRAINT [PK_Authors] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Books] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [Price] float NOT NULL,
    [PublishedDate] datetime2 NOT NULL,
    [PageNumber] int NOT NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Employees] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Surname] nvarchar(max) NULL,
    [EmployeePosition] int NOT NULL,
    [EmployeeStatus] int NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AuthorBook] (
    [AuthorsId] int NOT NULL,
    [BooksId] int NOT NULL,
    CONSTRAINT [PK_AuthorBook] PRIMARY KEY ([AuthorsId], [BooksId]),
    CONSTRAINT [FK_AuthorBook_Authors_AuthorsId] FOREIGN KEY ([AuthorsId]) REFERENCES [Authors] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AuthorBook_Books_BooksId] FOREIGN KEY ([BooksId]) REFERENCES [Books] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Genres] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [BookId] int NULL,
    CONSTRAINT [PK_Genres] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Genres_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [Books] ([Id])
);
GO

CREATE INDEX [IX_AuthorBook_BooksId] ON [AuthorBook] ([BooksId]);
GO

CREATE INDEX [IX_Genres_BookId] ON [Genres] ([BookId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230424100702_createddatabase', N'6.0.15');
GO

COMMIT;
GO

