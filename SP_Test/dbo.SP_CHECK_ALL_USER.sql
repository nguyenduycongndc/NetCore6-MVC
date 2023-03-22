USE [DB_TEST_BA]
GO
/****** Object:  StoredProcedure [dbo].[SP_CHECK_ALL_USER]    Script Date: 2023/02/27 8:56:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CHECK_ALL_USER] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from users as U
	where (ISNULL(U.is_deleted, 0) <> 1)
END
