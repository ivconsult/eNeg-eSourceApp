CREATE PROCEDURE [dbo].[uspNegotiationBidUpdate]
	@NegotiationBidID uniqueidentifier,
    @NegotiationID uniqueidentifier,
    @BidID uniqueidentifier,
    @IsClosed bit,
    @eNegUserID uniqueidentifier,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[NegotiationBid]
	SET    [NegotiationBidID] = @NegotiationBidID, [NegotiationID] = @NegotiationID, [BidID] = @BidID, [IsClosed] = @IsClosed, [eNegUserID] = @eNegUserID, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [NegotiationBidID] = @NegotiationBidID
	
	-- Begin Return Select <- do not remove
	SELECT [NegotiationBidID], [NegotiationID], [BidID], [IsClosed], [eNegUserID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[NegotiationBid]
	WHERE  [NegotiationBidID] = @NegotiationBidID	
	-- End Return Select <- do not remove

	COMMIT TRAN