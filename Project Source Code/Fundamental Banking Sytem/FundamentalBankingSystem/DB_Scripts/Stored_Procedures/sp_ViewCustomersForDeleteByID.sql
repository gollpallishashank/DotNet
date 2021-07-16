USE [DB_A6861A_fbs5]
GO

/****** Object:  StoredProcedure [dbo].[sp_ViewCustomersForDeleteByID]    Script Date: 10/25/2020 1:20:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Akhil Totakura
-- Create date: 10/24/2020
-- Description:	View customer details for deleting by id
-- =============================================
CREATE PROCEDURE [dbo].[sp_ViewCustomersForDeleteByID] 
	-- Add the parameters for the stored procedure here
	@id bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @stateid INT 
	DECLARE @cityid INT
	SELECT @stateid = StateID FROM tbl_Customer_Details WHERE CustomerID = @id
	SELECT @cityid = CityID FROM tbl_Customer_Details WHERE CustomerID = @id
	DECLARE @statename nvarchar(max)
	DECLARE @cityname nvarchar(max)
	SELECT @statename = Statename FROM State WHERE Stateid = @stateid
	SELECT @cityname = Cityname FROM City WHERE Cityid = @cityid
	SELECT CustomerID,CustomerName,SSN_ID,DateOfBirth,AddressLine1,AddressLine2,@statename AS Statename, @cityname AS Cityname FROM tbl_Customer_Details
	WHERE CustomerID = @id;
END
GO


