USE [DB_A6861A_fbs5]
GO

/****** Object:  StoredProcedure [dbo].[sp_UpdateCustomerDetails]    Script Date: 10/25/2020 1:20:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Akhil Totakura
-- Create date: 10/24/2020
-- Description:	Update customer details
-- =============================================
CREATE  PROCEDURE [dbo].[sp_UpdateCustomerDetails] 
	-- Add the parameters for the stored procedure here
	@id BIGINT,
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
	UPDATE tbl_Customer_Details
	SET CustomerName = @name, SSN_ID = @ssn, DateOfBirth = @dob, AddressLine1 = @address1, AddressLine2 = @address2, StateID = @state, CityID = @city
	WHERE CustomerID = @id;

END
GO


