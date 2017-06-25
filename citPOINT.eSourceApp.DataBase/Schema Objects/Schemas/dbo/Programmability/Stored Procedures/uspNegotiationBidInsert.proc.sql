CREATE PROCEDURE [dbo].[uspNegotiationBidInsert]
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
	
	INSERT INTO [dbo].[NegotiationBid] ([NegotiationBidID], [NegotiationID], [BidID], [IsClosed], [eNegUserID], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @NegotiationBidID, @NegotiationID, @BidID, @IsClosed, @eNegUserID, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [NegotiationBidID], [NegotiationID], [BidID], [IsClosed], [eNegUserID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[NegotiationBid]
	WHERE  [NegotiationBidID] = @NegotiationBidID
	-- End Return Select <- do not remove
               
	COMMIT