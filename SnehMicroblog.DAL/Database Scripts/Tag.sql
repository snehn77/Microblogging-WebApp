CREATE TABLE [dbo].[Tag] (
    [ID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [TweetID]     UNIQUEIDENTIFIER NOT NULL,
    [TagName]     NVARCHAR (30)    NOT NULL,
    [SearchCount] INT              DEFAULT ((0)) NULL,
    CONSTRAINT [PK_dbo.Tag] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_dbo.Tag_dbo.Tweet_TweetID] FOREIGN KEY ([TweetID]) REFERENCES [dbo].[Tweet] ([ID]),
    CONSTRAINT [FK_Tag_Tweet] FOREIGN KEY ([TweetID]) REFERENCES [dbo].[Tweet] ([ID]) ON DELETE CASCADE
);