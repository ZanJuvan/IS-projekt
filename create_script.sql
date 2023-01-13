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

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Drustvo] (
    [Id] int NOT NULL IDENTITY,
    [Naziv] nvarchar(max) NOT NULL,
    [Lokacija] nvarchar(max) NULL,
    [ApplicationUserId] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Drustvo] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [City] nvarchar(max) NULL,
    [DrustvoId] int NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUsers_Drustvo_DrustvoId] FOREIGN KEY ([DrustvoId]) REFERENCES [Drustvo] ([Id])
);
GO

CREATE TABLE [Dogodek] (
    [ID] int NOT NULL IDENTITY,
    [Naziv] nvarchar(max) NOT NULL,
    [Lokacija] nvarchar(max) NOT NULL,
    [Opis] nvarchar(max) NULL,
    [DrustvoId] int NOT NULL,
    CONSTRAINT [PK_Dogodek] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Dogodek_Drustvo_DrustvoId] FOREIGN KEY ([DrustvoId]) REFERENCES [Drustvo] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Cebeljnjak] (
    [ID] int NOT NULL IDENTITY,
    [Naslov] nvarchar(max) NULL,
    [UporabnikId] nvarchar(max) NOT NULL,
    [ApplicationUserId] nvarchar(450) NULL,
    CONSTRAINT [PK_Cebeljnjak] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Cebeljnjak_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id])
);
GO

CREATE TABLE [Panj] (
    [PanjID] int NOT NULL IDENTITY,
    [Naziv] nvarchar(max) NULL,
    [CebeljnjakID] int NOT NULL,
    CONSTRAINT [PK_Panj] PRIMARY KEY ([PanjID]),
    CONSTRAINT [FK_Panj_Cebeljnjak_CebeljnjakID] FOREIGN KEY ([CebeljnjakID]) REFERENCES [Cebeljnjak] ([ID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Evidenca] (
    [ID] int NOT NULL IDENTITY,
    [KratekOpis] nvarchar(max) NULL,
    [CistostCebele] bit NULL,
    [Moc] int NULL,
    [Mirnost] bit NULL,
    [Rojivost] bit NULL,
    [Zalega] bit NULL,
    [IzrezovanjeTrotovine] bit NULL,
    [ZalogaHrane] bit NULL,
    [SteviloVsSatnic] int NULL,
    [donosMedu] int NULL,
    [MenjavaMatice] bit NULL,
    [Datum] datetime2 NOT NULL,
    [PanjId] int NOT NULL,
    CONSTRAINT [PK_Evidenca] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Evidenca_Panj_PanjId] FOREIGN KEY ([PanjId]) REFERENCES [Panj] ([PanjID]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE INDEX [IX_AspNetUsers_DrustvoId] ON [AspNetUsers] ([DrustvoId]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_Cebeljnjak_ApplicationUserId] ON [Cebeljnjak] ([ApplicationUserId]);
GO

CREATE INDEX [IX_Dogodek_DrustvoId] ON [Dogodek] ([DrustvoId]);
GO

CREATE INDEX [IX_Evidenca_PanjId] ON [Evidenca] ([PanjId]);
GO

CREATE INDEX [IX_Panj_CebeljnjakID] ON [Panj] ([CebeljnjakID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230109205725_Initial', N'6.0.10');
GO

COMMIT;
GO

