USE [FYP]
GO
/****** Object:  StoredProcedure [dbo].[SearchRecords]    Script Date: 4/2/2020 2:41:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PractitionerSearchProduct]
	@AccId uniqueidentifier,
	@SearchText varchar(MAX),
	@ProductCode varchar(MAX)

AS
BEGIN
SET NOCOUNT ON;

	WITH cte AS
	(
		SELECT
			ME.Id as Id
			, ME.ProductCode
			, ME.ProductName
			, ME.ExpiryDate
			, ME.TotalAmount
			, ME.Threshold
		FROM
			Medicines ME 
			LEFT JOIN Practitioners PR ON PR.Id = @AccId
		WHERE
			ME.CompanyId = PR.Company
			AND ME.ProductName = CASE WHEN @SearchText != 'All' THEN @SearchText ELSE ME.ProductName END
			AND ME.ProductCode = CASE WHEN @ProductCode != 'All' THEN @ProductCode ELSE ME.ProductCode END
	)

	SELECT
		*
	FROM
		CTE
END
