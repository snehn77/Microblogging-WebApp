CREATE TABLE [dbo].[Follow] (
    [ID]              UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Follower_UserID] UNIQUEIDENTIFIER NOT NULL,
    [Followed_UserID] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.Follow] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_dbo.Follow_dbo.User_Follower_UserID] FOREIGN KEY ([Follower_UserID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_dbo.Follow_dbo.User_Followed_UserID] FOREIGN KEY ([Followed_UserID]) REFERENCES [dbo].[User] ([ID])
);