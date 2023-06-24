# stored-procedure-library-DotNet
Biblioteca para consulta de dados no banco via Stored Procedure utilizando .Net core

##

## Prerequisites

✔ - .Net 5.0

✔ - Visual code ou Visual Studio 2022

✔ - SQLServer

## Stored Procedure Utilizada

```
  
CREATE OR ALTER PROCEDURE SP_GetPessoas (@Nome VARCHAR(50))
AS
BEGIN

SELECT
	p.Id,
	p.Nome,
	p.Idade
	FROM Pessoas p
END
GO
  
```

## Quick Start

```
  
  git clone https://github.com/leandro-SI/stored-procedure-library-DotNet
  
  Atualizar as migrations (update-database)
  
  run project
  
```
