USE [FYP]
GO
/****** Object:  StoredProcedure [dbo].[GetPractitionerProfile]    Script Date: 4/15/2020 12:56:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetPractitionerProfile]
	@AccId uniqueidentifier
AS
BEGIN
SET NOCOUNT ON;

	WITH cte AS
	(
		SELECT
			PR.Id
			, PR.FirstName
			, PR.LastName
			, PR.UserName
			, PR.Gender
			, PR.Religion
			, PR.DateOfBirth
			, PR.EmailAddress
			, CO.Id As CompanyId
			, CO.CompanyName
			, CO.CompanyAddressLine1
			, CO.CompanyAddressLine2
			, CO.CompanyAddressLine3
			, CO.PostalCode
			, CO.City
			, CO.State
			, CO.CompanyPhoneNumber
			, CO.CompanyEmailAddress
			, PR.OfficePhoneNumber
			, PR.Role
			, PR.Specialist
			, PR.Qualification
		FROM
			Practitioners PR
			LEFT JOIN Companies CO ON PR.Company = CO.Id
		WHERE
			PR.Id = @AccId
	)

	SELECT
		*
	FROM
		CTE
END
