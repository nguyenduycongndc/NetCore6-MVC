USE [DB_TEST_BA]
GO
/****** Object:  StoredProcedure [dbo].[SP_CREATE_USER]    Script Date: 2023/02/27 9:00:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CREATE_USER] 
	-- Add the parameters for the stored procedure here
	@user_name nvarchar(MAX),
	@email_address nvarchar(MAX),
	@role_id int,
	@password nvarchar(MAX),
	@salt nvarchar(MAX),
	@created_by int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @Result bit;
	SET NOCOUNT ON;
	insert into [dbo].[users]( [user_name], [email], [full_name], [date_of_joining], [role_id], [is_active], [password], [salt], [created_by], [created_at]) 
		VALUES (@user_name, @email_address, LOWER(@user_name), GETDATE(), @role_id, 1, @password, @salt, @created_by,  GETDATE());
		DECLARE @u_id int = (select Top 1 id from [dbo].[users] as U where U.user_name = @user_name and U.is_active = 1 ORDER BY created_at)
		IF (ISNULL(@u_id, '') <> '')
		BEGIN
			insert into [dbo].[users_roles]([users_id], [roles_id]) 
			VALUES (@u_id, @role_id);
		END
	BEGIN
		set @Result = 1;
		select @Result as Rs
	END
END
