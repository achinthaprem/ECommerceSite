
CREATE PROCEDURE [dbo].[OrderUpdate]
	@ID INT,
	@account_id INT,
	@date_created DATETIME,
	@status INT,
	@payment_method INT,
	@total_amount DECIMAL(18, 2)
AS

	/* Volume Technologies RADA Generator v6.1 */

	UPDATE [Order] SET
		[account_id] = @account_id,
		[date_created] = @date_created,
		[status] = @status,
		[payment_method] = @payment_method,
		[total_amount] = @total_amount
	WHERE [ID] = @ID