USE [FYP]
GO
/****** Object:  StoredProcedure [dbo].[GetRecordsDirectory]    Script Date: 4/15/2020 12:57:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetRecordsDirectory]
	@AccId uniqueidentifier
AS
BEGIN
SET NOCOUNT ON;

	SELECT
		RF.Id
		, RF.CreatedOn
		, PA.FirstName
		, PA.LastName
	FROM
		PatientRecords RF 
		LEFT JOIN Patients PA ON PA.Id = RF.PatientId
	WHERE
		RF.PractitionerId = @AccId
		AND RF.FileContents != 0x00
END
