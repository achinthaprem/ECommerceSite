
CREATE PROCEDURE [dbo].[ContactListByReadStatus]
	@read_status INT
AS

	/* Volume Technologies RADA Generator v6.1 */

	SELECT *
	INTO #temp
	FROM [Contact]
	WHERE [read_status] = @read_status

	SELECT *
	FROM #temp