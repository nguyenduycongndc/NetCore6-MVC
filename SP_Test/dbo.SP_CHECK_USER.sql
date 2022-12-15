USE [DB_TEST]
GO
/****** Object:  StoredProcedure [dbo].[SP_CHECK_USER]    Script Date: 2022/10/18 8:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CHECK_USER] 
	-- Add the parameters for the stored procedure here
	@user_name nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from [dbo].[users] as U 
	where U.user_name = @user_name and U.is_active = 1
END
