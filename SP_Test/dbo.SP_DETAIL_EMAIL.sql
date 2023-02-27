USE [DB_TEST_BA]
GO
/****** Object:  StoredProcedure [dbo].[SP_DETAIL_EMAIL]    Script Date: 2023/02/27 9:01:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_DETAIL_EMAIL] 
	-- Add the parameters for the stored procedure here
	@email_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from [dbo].[email] as E 
	where E.id = @email_id and ISNULL (E.is_deleted,0) <> 1
END
