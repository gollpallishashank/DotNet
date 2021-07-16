USE [DB_A6861A_fbs5]
GO

/****** Object:  StoredProcedure [dbo].[sp_getcities]    Script Date: 10/25/2020 1:19:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Akhil Totakura
-- Create date: 10/24/2020
-- Description:	Get Cities upon state id
-- =============================================
CREATE PROCEDURE [dbo].[sp_getcities] 
	@stateid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM City WHERE Stateid = @stateid;
END
GO


