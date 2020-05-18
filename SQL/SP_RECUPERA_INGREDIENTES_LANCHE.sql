USE [dbWebLanche]
GO
/****** Object:  StoredProcedure [dbo].[SP_RECUPERA_INGREDIENTES_LANCHE]    Script Date: 05/18/2020 02:00:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_RECUPERA_INGREDIENTES_LANCHE]
AS
BEGIN	
	
	--LANCHE PADRÃO	
	SELECT L.LancheId, I.IngredienteId,I.Descricao,I.Valor,ING_LAN.QuantidadeIngrediente,I.PodeAlterar	  
	FROM Ingrediente AS I WITH(NOLOCK)
	INNER JOIN IngredientesLanche AS ING_LAN WITH(NOLOCK) ON ING_LAN.IngredienteId = I.IngredienteId		
	INNER JOIN Lanche AS L WITH(NOLOCK) ON L.LancheId  = ING_LAN.LancheId			

END

--EXEC [SP_RECUPERA_INGREDIENTES_LANCHE] 6