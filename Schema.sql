CREATE TABLE Constants (
	NAME VARCHAR(40) NOT NULL,
	DATA_TYPE VARCHAR(40) NOT NULL,
	VALUE VARCHAR(40) NOT NULL
	);

CREATE TABLE TransactionTable (
	TRANSACTION_ID INTEGER PRIMARY KEY AUTOINCREMENT,
	DATE_CREATED VARCHAR(11) NOT NULL,
	AMOUNT DECIMAL(8,2) NOT NULL,
	TRANSACTION_TYPE VARCHAR(40) NOT NULL,
	CATEGORY VARCHAR(50) NOT NULL,
	DESCRIPTION TEXT,
	DATE_MODIFIED VARCHAR(11)
	);

CREATE TABLE ModificationTable (
	MODIFICATION_ID INTEGER PRIMARY KEY AUTOINCREMENT,
	TRANSACTION_ID INTEGER NOT NULL,
	ORIGINAL_AMOUNT DECIMAL(8,2),
	ORIGINAL_CATEGORY VARCHAR(50),
	ORIGINAL_DESCRIPTION TEXT,
	NEW_AMOUNT DECIMAL(8,2),
	NEW_CATEGORY VARCHAR(40),
	NEW_DESCRIPTION TEXT,
	CURRENT_DATE VARCHAR(11) NOT NULL,
	PREVIOUS_DATE VARCHAR(11),
	REASON_FOR_CHANGE TEXT,
	FOREIGN KEY(TRANSACTION_ID) REFERENCES TransactionTable(TRANSACTION_ID)
	);

INSERT INTO Constants VALUES('ORIGINAL BALANCE', 'DECIMAL', '0.00');
INSERT INTO Constants VALUES('CURRENT BALANCE', 'DECIMAL', '0.00');