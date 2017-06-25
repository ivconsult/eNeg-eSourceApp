CREATE PROCEDURE [dbo].[uspUserMappingInsert]
	@eNegUserID uniqueidentifier,
    @eSourceUserID uniqueidentifier,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[UserMapping] ([eNegUserID], [eSourceUserID], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @eNegUserID, @eSourceUserID, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [eNegUserID], [eSourceUserID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[UserMapping]
	WHERE  [eNegUserID] = @eNegUserID
	-- End Return Select <- do not remove
               
	COMMIT