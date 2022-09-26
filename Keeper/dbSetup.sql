CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


CREATE TABLE IF NOT EXISTS profiles(
  profileId VARCHAR(255) NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',


  FOREIGN KEY (profileId) REFERENCES accounts(id)
) default charset utf8 COMMENT '';

DROP
TABLE profiles;

CREATE TABLE IF NOT EXISTS keeps(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  description TEXT NOT NULL,  
  img VARCHAR(500) NOT NULL,
  views INT NOT NULL DEFAULT 0,
  kept INT NOT NULL DEFAULT 0,
  shares INT NOT NULL DEFAULT 0,
  

  FOREIGN KEY (creatorId) REFERENCES accounts(id)
)default charset utf8 COMMENT '';

ALTER TABLE keeps
DROP COLUMN vaultKeepId;

CREATE TABLE IF NOT EXISTS vaultKeeps(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL,
  vaultId INT NOT NULL,
  keepId INT NOT NULL,

  FOREIGN KEY (creatorId) REFERENCES accounts(id),
  FOREIGN KEY (vaultId) REFERENCES vaults(id),
  FOREIGN KEY (keepId) REFERENCES keeps(id)
)default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaults(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
   createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(255) NOT NULL,
  isPrivate BOOLEAN NOT NULL DEFAULT false,

  FOREIGN KEY (creatorId) REFERENCES accounts(id)

)default charset utf8 COMMENT '';


INSERT INTO vaults
(name, img, description, creatorId)
VALUES
('My Favorites', 'https://images.unsplash.com/photo-1632809199725-72a4245e846b?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MXx8dHJlYXN1cmUlMjBjaGVzdHxlbnwwfHwwfHw%3D&auto=format&fit=crop&w=500&q=60', 'These are my Favorite Ones', '632de323b9fe8bd9e1aa5a18');

SELECT 
v.*,
a.*
FROM vaults v
JOIN accounts a ON a.id = v.creatorId;





ALTER TABLE vaults
ADD COLUMN img VARCHAR(255) NOT NULL;


DROP TABLE keeps;

INSERT INTO keeps
(creatorId, name, description, img)
VALUES
('62fa6842368b261972f93aa2', 'Happy Place', 'Where I go to be Happy!', 'https://images.unsplash.com/photo-1473448912268-2022ce9509d8?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8NHx8Zm9yZXN0fGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60');


SELECT 
k.*,
a.*
FROM keeps k
JOIN accounts a ON a.id = k.creatorId;





  SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE a.id = @creatorId;