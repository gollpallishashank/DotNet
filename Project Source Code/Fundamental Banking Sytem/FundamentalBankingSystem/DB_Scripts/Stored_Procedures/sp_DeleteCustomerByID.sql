USE [DB_A6861A_fbs5]
GO

/****** Object:  StoredProcedure [dbo].[sp_DeleteCustomerByID]    Script Date: 10/25/2020 1:17:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Akhil Totakura
-- Create date: 10/24/2020
-- Description:	Delete customer details by id
-- =============================================
CREATE  PROCEDURE [dbo].[sp_DeleteCustomerByID] 
	-- Add the parameters for the stored procedure here
	@id BIGINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @count int
	SELECT @count = COUNT(*) FROM tbl_Customer_Account_Details WHERE CustomerID = @id;
 
    -- Insert statements for procedure here
	IF(@count = 0)
	BEGIN
	DELETE FROM tbl_Customer_Details WHERE CustomerID = @id;
	SELECT 1
	END
	ELSE
	BEGIN
	SELECT 2
	END

END
GO


