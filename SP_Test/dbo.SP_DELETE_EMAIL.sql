USE [DB_TEST_BA]
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_EMAIL]    Script Date: 2023/02/27 9:01:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_DELETE_EMAIL]
	-- Add the parameters for the stored procedure here
	@id int,
	@deleted_by int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	declare @Result bit;
	SET NOCOUNT ON;
	If exists (SELECT * FROM email WHERE id = @id)
    -- Insert statements for procedure here
	BEGIN
			UPDATE email
					set 
						email.is_deleted = 1,
						email.deleted_at = GETDATE(),
						email.deleted_by = @deleted_by
					from email 
					WHERE email.id = @id
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
