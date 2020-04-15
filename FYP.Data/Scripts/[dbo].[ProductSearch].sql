USE [FYP]
GO
/****** Object:  StoredProcedure [dbo].[ProductSearch]    Script Date: 4/15/2020 12:58:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[ProductSearch]
	@SearchText varchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

	WITH cte AS
	(
		SELECT
			ME.ProductCode
			, ME.ProductName
			, (CO.Id) AS CompanyId
			, CO.PostalCode
			, CO.State
			, CO.CompanyName
			, ME.TotalAmount
		FROM
			Medicines ME
			LEFT JOIN Companies CO ON CO.Id = ME.CompanyId
		where ME.ProductName LIKE @SearchText
	)

	SELECT
		*
	FROM
		CTE
END
