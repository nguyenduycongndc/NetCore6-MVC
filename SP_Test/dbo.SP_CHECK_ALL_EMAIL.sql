USE [DB_TEST_BA]
GO
/****** Object:  StoredProcedure [dbo].[SP_CHECK_ALL_EMAIL]    Script Date: 2023/02/27 8:55:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CHECK_ALL_EMAIL]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from [dbo].[email] as E 
	where E.is_deleted <> 1 and E.email_address is not null
END
