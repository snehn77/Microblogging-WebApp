
CREATE TABLE [dbo].[Tweet] (
    [ID]        UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [UserID]    UNIQUEIDENTIFIER NOT NULL,
    [Message]   NVARCHAR (240)   NOT NULL,
    [CreatedAt] DATETIME         NOT NULL,
    CONSTRAINT [PK_dbo.Tweet] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_dbo.Tweet_dbo.User_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);