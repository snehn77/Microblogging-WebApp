CREATE TABLE [dbo].[LikeTweet] (
    [ID]      UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [TweetID] UNIQUEIDENTIFIER NOT NULL,
    [UserID]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.LikeTweet] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_dbo.LikeTweet_dbo.Tweet_TweetID] FOREIGN KEY ([TweetID]) REFERENCES [dbo].[Tweet] ([ID]),
    CONSTRAINT [FK_dbo.LikeTweet_dbo.User_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);
