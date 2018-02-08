CREATE TABLE [dbo].[RelatorioDespesa]
(
	[IdRelatorio] INT NOT NULL , 
    [IdDespesa] INT NOT NULL, 
    PRIMARY KEY ([IdDespesa], [IdRelatorio]), 
    CONSTRAINT [FK_RelatorioDespesa_Relatorio] FOREIGN KEY ([IdRelatorio]) REFERENCES [Relatorio]([Id]),
	CONSTRAINT [FK_RelatorioDespesa_Despesa] FOREIGN KEY ([IdDespesa]) REFERENCES [Despesa]([Id])
)
