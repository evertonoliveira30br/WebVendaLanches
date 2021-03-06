USE [dbWebLanche]
GO
/****** Object:  StoredProcedure [dbo].[SP_RECUPERA_CARDAPIO]    Script Date: 05/18/2020 02:00:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_RECUPERA_CARDAPIO]
AS
BEGIN

	SELECT L.LancheId, L.Descricao [Lanche],L.LanchePersonalizado,
		  ISNULL(STUFF((SELECT ', ' + I.Descricao 
				  FROM IngredientesLanche AS I_L WITH(NOLOCK)
				  INNER JOIN Ingrediente AS I WITH(NOLOCK) ON I_L.IngredienteId = I.IngredienteId
				  WHERE I_L.LancheId = L.LancheId AND L.LanchePersonalizado = 0            
				  FOR XML PATH('')), 1, 1, ''),'Monte o lanche do seu jeito, com os ingredientes que desejar') [Ingredientes],
		   SUM(CASE WHEN L.LanchePersonalizado = 0 THEN I.Valor ELSE 0 END)[PrecoLanche]
	FROM Lanche AS L WITH(NOLOCK)
	INNER JOIN IngredientesLanche AS ING_LAN WITH(NOLOCK) ON ING_LAN.LancheId = L.LancheId
	INNER JOIN Ingrediente AS I WITH(NOLOCK) ON I.IngredienteId = ING_LAN.IngredienteId
	GROUP BY L.LancheId, L.Descricao,L.LanchePersonalizado

END

