CREATE TABLE [dbo].[Despesa] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [IdTipoDespesa]   INT             NOT NULL,
    [IdTipoPagamento] INT             NOT NULL,
    [Data]            DATETIME        NOT NULL,
    [Valor]           DECIMAL (18, 2) NOT NULL,
    [Comentario]      VARCHAR (100)   NOT NULL,
    [DataCriacao]     DATETIME        NOT NULL,
    CONSTRAINT [PK_Despesa] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Despesa_TipoDespesa] FOREIGN KEY ([IdTipoDespesa]) REFERENCES [TipoDespesa]([Id]), 
    CONSTRAINT [FK_Despesa_TipoPagamento] FOREIGN KEY ([IdTipoPagamento]) REFERENCES [TipoPagamento]([Id])
);

