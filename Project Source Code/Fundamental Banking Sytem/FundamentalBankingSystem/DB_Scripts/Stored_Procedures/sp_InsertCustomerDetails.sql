USE [DB_A6861A_fbs5]
GO

/****** Object:  StoredProcedure [dbo].[sp_InsertCustomerDetails]    Script Date: 10/25/2020 1:19:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Akhil Totakura
-- Create date: 10/24/2020
-- Description:	Insert customer details
-- =============================================
CREATE PROCEDURE [dbo].[sp_InsertCustomerDetails] 
	-- Add the parameters for the stored procedure here
	@name nvarchar(max),
	@ssn BIGINT,
	@dob datetime,
	@address1 nvarchar(max),
	@address2 nvarchar(max),
	@state int,
	@city int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO tbl_Customer_Details VALUES(@name,@ssn,@dob,@address1,@address2,@state,@city);
END
GO


