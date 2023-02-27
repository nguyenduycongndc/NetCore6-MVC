USE [DB_TEST_BA]
GO
/****** Object:  StoredProcedure [dbo].[SP_CREATE_EMAIL]    Script Date: 2023/02/27 8:59:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CREATE_EMAIL] 
	-- Add the parameters for the stored procedure here
	@email_address nvarchar(MAX),
	@cc nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @Result bit;
	SET NOCOUNT ON;
	insert into [dbo].[email]( [email_address], [cc], [is_deleted], [created_at], [deleted_at]) 
		VALUES (@email_address, @cc, 0, GETDATE(), null);
	BEGIN
		set @Result = 1;
		select @Result as Rs
	END
END
