USE [FYP]
GO
/****** Object:  StoredProcedure [dbo].[PatientSearchRecords]    Script Date: 4/15/2020 12:57:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[PatientSearchRecords]
	@AccId uniqueidentifier,
	@Year int,
	@Month int

AS
BEGIN
SET NOCOUNT ON;

	WITH cte AS
	(
		SELECT
			RF.Id
			, RF.CreatedOn
		FROM
			PatientRecords RF 
			LEFT JOIN Patients PA ON RF.PatientId = PA.Id 
		WHERE
			RF.PatientId = @AccId
			AND YEAR(RF.CreatedOn) = CASE WHEN @Year != 0 THEN @Year ELSE YEAR(RF.CreatedOn) END
			AND MONTH(RF.CreatedOn) = CASE WHEN @Month != 0 THEN @Month ELSE MONTH(RF.CreatedOn) END

	)

	SELECT
		*
	FROM
		CTE
END
