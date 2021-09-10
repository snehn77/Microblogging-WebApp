CREATE TABLE [dbo].[User] (
    [ID]           UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [FirstName]    NVARCHAR (256)   NOT NULL,
    [LastName]     NVARCHAR (256)   NOT NULL,
    [PasswordHash] NVARCHAR (MAX)   NOT NULL,
    [Email]        NVARCHAR (256)   NOT NULL,
    [PhoneNumber]  NVARCHAR (MAX)   NOT NULL,
    [Image]        NVARCHAR (MAX)   NOT NULL,
    [Country]      NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [Un_dbo.User_Email] UNIQUE NONCLUSTERED ([Email] ASC)
);