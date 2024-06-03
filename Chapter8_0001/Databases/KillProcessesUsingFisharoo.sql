
DECLARE @i INT
DECLARE @error INT
DECLARE @x INT
declare @command nvarchar(max)

CREATE TABLE #LIVE_SPIDS (nid INT IDENTITY(1,1), spid INT NOT NULL)
SET @error = 0
SET @i = 1

TRUNCATE TABLE #LIVE_SPIDS

INSERT INTO #LIVE_SPIDS
SELECT 
	spid
FROM Master.dbo.SYSPROCESSES
WHERE dbid IN (SELECT dbid FROM Master.dbo.SYSDATABASES WHERE name = 'Fisharoo')
	AND spid <> @@SPID

SET @x = (SELECT MIN(nid) FROM #LIVE_SPIDS)

WHILE @x <= (SELECT MAX(nid) FROM #LIVE_SPIDS)
BEGIN

	SET @command = 'KILL ' + CONVERT(VARCHAR, (SELECT spid FROM #LIVE_SPIDS WHERE nid = @x))

	EXECUTE sp_executesql @command--, NO OUTPUT
	SET @error = @error + @@ERROR

	SET @x = @x + 1

END
	
DROP TABLE #LIVE_SPIDS





CREATE TABLE #TmpWho
(spid INT, ecid INT, status VARCHAR(150), loginame VARCHAR(150),
hostname VARCHAR(150), blk INT, dbname VARCHAR(150), cmd VARCHAR(150), request_id int)

INSERT INTO 	#TmpWho
EXEC      	sp_who
DECLARE 	@spid INT
DECLARE 	@tString varchar(15)
DECLARE 	@getspid CURSOR

SET @getspid = 	CURSOR FOR
SELECT      	spid
FROM     	#TmpWho
WHERE      	dbname = 'mydb'OPEN @getspid

FETCH NEXT FROM @getspid INTO @spid
WHILE @@FETCH_STATUS = 0

BEGIN
SET @tString = 'KILL ' + Cast(@spid as varchar)
EXEC(@tString)
FETCH NEXT FROM @getspid INTO @spid
END

CLOSE @getspid
DEALLOCATE @getspid

DROP TABLE #TmpWho