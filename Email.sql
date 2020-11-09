USE MailsAndSenders

CREATE TABLE [dbo].[Email] (
[Id] int NOT NULL,
[Value] NVARCHAR (MAX) NOT NULL,
[Name] NVARCHAR (MAX) NOT NULL
);

ALTER TABLE [dbo].[Email]

ADD CONSTRAINT PK_Email_Id PRIMARY KEY CLUSTERED (Id);


INSERT INTO [dbo].[Email] VALUES (1, 'report@litaform.ru', 'Рассыльщик');
INSERT INTO [dbo].[Email] VALUES (2, '79262685273@ya.ru', 'Я');