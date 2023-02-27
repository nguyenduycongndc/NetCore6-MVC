USE [DB_TEST_BA]
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_USER]    Script Date: 2023/02/27 9:01:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_DELETE_USER]
	-- Add the parameters for the stored procedure here
	@id int,
	@deleted_by int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	declare @Result bit;
	SET NOCOUNT ON;
	If exists (SELECT * FROM users WHERE id = @id)
    -- Insert statements for procedure here
	BEGIN
			UPDATE users
					set 
						users.is_active = 0,
						users.deleted_at = GETDATE(),
						users.deleted_by = @deleted_by
					from users 
					WHERE users.id = @id
			BEGIN
				set @Result = 1;
				select @Result as Rs
			END
		END
	ELSE
		BEGIN
			set @Result = 0;
			select @Result as Rs
		END
END
