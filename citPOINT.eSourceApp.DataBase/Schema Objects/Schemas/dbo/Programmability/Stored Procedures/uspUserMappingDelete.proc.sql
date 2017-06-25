CREATE PROCEDURE [dbo].[uspUserMappingDelete]
	@eNegUserID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	Update [dbo].[UserMapping]
	SET Deleted = 0, DeletedOn = GETDATE() 
	WHERE  [eNegUserID] = @eNegUserID

	UPDATE [dbo].[NegotiationBid]
	SET Deleted = 0, DeletedOn = GETDATE() 
	WHERE  [eNegUserID] = @eNegUserID

	COMMIT