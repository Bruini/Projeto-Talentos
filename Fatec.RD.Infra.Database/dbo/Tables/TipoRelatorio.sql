CREATE TABLE [dbo].[TipoRelatorio] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Descricao]   VARCHAR (100) NOT NULL,
    [DataCriacao] DATETIME      NOT NULL,
    CONSTRAINT [PK_TipoRelatorio] PRIMARY KEY CLUSTERED ([Id] ASC)
);

