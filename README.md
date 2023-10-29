# .NET 6.0 Razor Pages CRUD project

An ASP.NET Core application project with ASP.NET Razor Pages content.

## Based on [BoostMyTool](https://www.youtube.com/@BoostMyTool/) tutorials:

[![](https://markdown-videos.vercel.app/youtube/T-e554Zt3n4)](https://youtu.be/T-e554Zt3n4)

[![](https://markdown-videos.vercel.app/youtube/JKgAU6XgGZs)](https://youtu.be/JKgAU6XgGZs)

## Database creation and connectionString

Create a database with the name of your chioce.

Search the code, replace ``ALIMPC`` and ``URSdatabase`` with your own **Server name** and **Database** name respectively.

## Database query

```
CREATE TABLE users (
  userId INT PRIMARY KEY IDENTITY,
  username VARCHAR (100) NOT NULL,
  email VARCHAR (150) NOT NULL UNIQUE,
  created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE roles (
  roleId INT PRIMARY KEY IDENTITY,
  roleName VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE status (
  statusId INT PRIMARY KEY IDENTITY,
  statusName VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE userRoles (
  userId INT,
  roleId INT,
  PRIMARY KEY (userId, roleId),
  FOREIGN KEY (userId) REFERENCES users(userId),
  FOREIGN KEY (roleId) REFERENCES roles(roleId)
);

CREATE TABLE userStatus (
  userId INT,
  statusId INT,
  PRIMARY KEY (userId, statusId),
  FOREIGN KEY (userId) REFERENCES users(userId),
  FOREIGN KEY (statusId) REFERENCES status(statusId)
);


INSERT INTO status (statusName)
VALUES
('active'),
('deactive');

INSERT INTO roles (roleName)
VALUES
('su'),
('admin'),
('customer'),
('guest');

INSERT INTO users(username, email)
VALUES
('Bill Gates', 'bill.gates@microsoft.com'),
('Elon Musk', 'elon.musk@spacex.com'),
('Steve Jobs', 'steve.jobs@apple.com'),
('Shuhei Yoshida', 'shuhei.yoshida@playstation.com'),
('John McAfee', 'john.mcafee@mcafee.com'),
('Phil Spencer', 'phil.spencer@xbox.com');


INSERT INTO userStatus(userId, statusId)
VALUES
(1, 1),
(2, 1),
(3, 2),
(4, 1),
(5, 2),
(6, 1);

INSERT INTO userRoles(userId, roleId)
VALUES
(1, 1),
(2, 2),
(3, 2),
(4, 3),
(5, 4),
(6, 3);
```

## Contributing

For major changes, please open an issue first
to discuss what you would like to change.

## COPYRIGHT/COPYLEFT

October 2023
