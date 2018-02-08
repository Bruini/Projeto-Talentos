CREATE TABLE [dbo].[TipoDespesa] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Descricao]   VARCHAR (100) NOT NULL,
    [DataCriacao] DATETIME      NOT NULL,
    CONSTRAINT [PK_TipoDespesa] PRIMARY KEY CLUSTERED ([Id] ASC)
);

