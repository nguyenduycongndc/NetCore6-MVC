USE [DB_TEST_BA]
GO
/****** Object:  StoredProcedure [dbo].[SP_ALL_EMAIL]    Script Date: 2023/02/27 8:54:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_ALL_EMAIL] 
	-- Add the parameters for the stored procedure here
	@email_address nvarchar(MAX),
	@start_number INT,
	@page_size INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--IF (@start_number < 1)
	--	SELECT @start_number = 1;
    
	SELECT * from [dbo].[email] as E
		WHERE 
				@email_address is null 
				or E.email_address LIKE N'%' + @email_address + '%'
				and E.is_deleted = '0'
		ORDER BY id
		OFFSET @start_number ROWS
		--OFFSET @page_size * (@start_number - 1) ROWS
		FETCH NEXT @page_size ROWS ONLY;
END
