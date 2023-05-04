DELIMITER $$

CREATE PROCEDURE COUNT_AUTHS (IN TEXT _NAME, OUT TOTAL BIGINT)

BEGIN
    DECLARE TOTALCOUNT BIGINT;
    SELECT 
        COUNT(*) INTO TOTALCOUNT 
    FROM AUTH A
    WHERE A.NAME = _NAME;

    SET TOTAL = TOTALCOUNT;
END;

$$ DELIMITER  ;