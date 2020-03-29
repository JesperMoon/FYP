USE [FYP]
GO
/****** Object:  StoredProcedure [dbo].[GetAuthorizedPractitioners]    Script Date: 3/29/2020 9:04:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetAuthorizedPractitioners]
	@AccId uniqueidentifier
AS
BEGIN
SET NOCOUNT ON;

	WITH cte AS
	(
		SELECT
			(PR.Id) AS PractitionerId
			, (PR.LastName + ' ' + PR.FirstName) AS PractitionerName
			, PR.Specialist
			, CO.CompanyName
			, CO.PostalCode
			, CO.City
			, CO.State
			, AP.CreatedOn
		FROM
			AuthorizePractitioners AP
			LEFT JOIN Practitioners PR ON PR.Id = AP.PractitionerId
			LEFT JOIN Companies CO ON CO.Id = PR.Company
		WHERE
			AP.PatientId = @AccId
	)

	SELECT
		*
	FROM
		CTE
END
