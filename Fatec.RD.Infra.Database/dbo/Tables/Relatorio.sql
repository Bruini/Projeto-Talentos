CREATE TABLE [dbo].[Relatorio]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[IdTipoRelatorio] INT NOT NULL,
	[Descricao] VARCHAR(100) NOT NULL, 
    [Comentario] VARCHAR(100) NOT NULL, 
    [DataCriacao] DATETIME NOT NULL, 
    CONSTRAINT [PK_Relatorio] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Relatorio_TipoRelatorio] FOREIGN KEY ([IdTipoRelatorio]) REFERENCES [TipoRelatorio]([Id]) 
)
