USE [DB_A6861A_fbs5]
GO

/****** Object:  StoredProcedure [dbo].[sp_ViewCustomers]    Script Date: 10/25/2020 1:20:26 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Akhil Totakura
-- Create date: 10/24/2020
-- Description:	View customer details
-- =============================================
CREATE PROCEDURE [dbo].[sp_ViewCustomers] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT CustomerID,CustomerName,SSN_ID,DateOfBirth,CONCAT(AddressLine1,AddressLine2) AS [Address] FROM tbl_Customer_Details;
END
GO


