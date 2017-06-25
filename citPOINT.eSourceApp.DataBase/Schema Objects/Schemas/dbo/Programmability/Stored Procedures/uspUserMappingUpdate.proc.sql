CREATE PROCEDURE [dbo].[uspUserMappingUpdate]
	@eNegUserID uniqueidentifier,
    @eSourceUserID uniqueidentifier,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[UserMapping]
	SET    [eNegUserID] = @eNegUserID, [eSourceUserID] = @eSourceUserID, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [eNegUserID] = @eNegUserID
	
	-- Begin Return Select <- do not remove
	SELECT [eNegUserID], [eSourceUserID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[UserMapping]
	WHERE  [eNegUserID] = @eNegUserID	
	-- End Return Select <- do not remove

	COMMIT TRAN