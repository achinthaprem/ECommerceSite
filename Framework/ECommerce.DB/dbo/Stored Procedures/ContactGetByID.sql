
CREATE PROCEDURE [dbo].[ContactGetByID]
	@ID INT
AS

	/* Volume Technologies RADA Generator v6.1 */

	SELECT *
	FROM [Contact]
	WHERE [ID] = @ID