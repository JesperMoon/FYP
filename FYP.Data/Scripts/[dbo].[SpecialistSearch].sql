USE [FYP]
GO
/****** Object:  StoredProcedure [dbo].[SpecialistSearch]    Script Date: 2/17/2020 1:57:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SpecialistSearch]
	@SearchText varchar(100),
	@Specialist varchar(100),
	@State varchar(100),
	@PostalCode varchar(100)
AS
BEGIN
SET NOCOUNT ON;

	WITH cte AS
	(
		SELECT
			PR.Id
			, (PR.LastName + ' ' + PR.FirstName) AS SpecialistName
			, PR.Specialist
			, CO.CompanyName
			, CO.CompanyAddressLine1
			, CO.CompanyAddressLine2
			, CO.CompanyAddressLine3
			, CO.CompanyPhoneNumber
			, CO.PostalCode
			, CO.City
			, CO.State

		FROM
			Practitioners PR
			LEFT JOIN Companies CO ON PR.Company = CO.Id
		WHERE
			PR.Specialist = CASE WHEN @Specialist !='All' THEN @Specialist ELSE PR.Specialist END
			AND CO.State = CASE WHEN @State != 'All' THEN @State ELSE CO.State END
			AND CO.PostalCode = CASE WHEN @PostalCode != 'All' THEN @PostalCode ELSE CO.PostalCode END

	)

	SELECT
		*
	FROM
		CTE
	order by
		CTE.SpecialistName
END
