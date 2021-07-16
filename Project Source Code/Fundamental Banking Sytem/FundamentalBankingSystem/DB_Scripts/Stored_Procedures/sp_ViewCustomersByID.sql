USE [DB_A6861A_fbs5]
GO

/****** Object:  StoredProcedure [dbo].[sp_ViewCustomersByID]    Script Date: 10/25/2020 1:20:37 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Akhil Totakura
-- Create date: 10/24/2020
-- Description:	View customer details by id
-- =============================================
CREATE PROCEDURE [dbo].[sp_ViewCustomersByID] 
	-- Add the parameters for the stored procedure here
	@id bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT * FROM tbl_Customer_Details WHERE CustomerID = @id;
END
GO


