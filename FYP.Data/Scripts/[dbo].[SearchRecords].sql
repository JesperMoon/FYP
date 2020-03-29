USE [FYP]
GO
/****** Object:  StoredProcedure [dbo].[SearchRecords]    Script Date: 3/29/2020 9:06:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SearchRecords]
	@AccId uniqueidentifier,
	@RecordId uniqueidentifier,
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
			, PA.FirstName
			, PA.LastName
		FROM
			PatientRecords RF 
			LEFT JOIN Patients PA ON RF.PatientId = PA.Id 
		WHERE
			RF.PractitionerId = @AccId
			AND RF.Id = CASE WHEN @RecordId != '00000000-0000-0000-0000-000000000000' THEN @RecordId ELSE RF.Id END
			AND YEAR(RF.CreatedOn) = CASE WHEN @Year != 0 THEN @Year ELSE YEAR(RF.CreatedOn) END
			AND MONTH(RF.CreatedOn) = CASE WHEN @Month != 0 THEN @Month ELSE MONTH(RF.CreatedOn) END

	)

	SELECT
		*
	FROM
		CTE
END
