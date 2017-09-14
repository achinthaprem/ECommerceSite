
CREATE PROCEDURE [dbo].[ContactDelete]
	@ID INT
AS

	/* Volume Technologies RADA Generator v6.1 */

	DELETE FROM [Contact]
	WHERE [ID] = @ID