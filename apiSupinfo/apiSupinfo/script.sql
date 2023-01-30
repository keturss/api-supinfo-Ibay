CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Carts` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    CONSTRAINT `PK_Carts` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Products` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ImageUrl` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Products` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Users` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Email` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Username` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Password` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Role` longtext CHARACTER SET utf8mb4 NOT NULL,
    `RefreshToken` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CartItem` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ProductId` int NOT NULL,
    `Quantity` int NOT NULL,
    `CartId` int NULL,
    CONSTRAINT `PK_CartItem` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_CartItem_Carts_CartId` FOREIGN KEY (`CartId`) REFERENCES `Carts` (`Id`),
    CONSTRAINT `FK_CartItem_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `Products` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_CartItem_CartId` ON `CartItem` (`CartId`);

CREATE INDEX `IX_CartItem_ProductId` ON `CartItem` (`ProductId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230130085045_Initial', '7.0.2');

COMMIT;

START TRANSACTION;

ALTER TABLE `Users` DROP COLUMN `Email`;

ALTER TABLE `Users` DROP COLUMN `RefreshToken`;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230130100026_updateUser', '7.0.2');

COMMIT;

START TRANSACTION;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230130104414_modif Product', '7.0.2');

COMMIT;

START TRANSACTION;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230130121529_Cart modif', '7.0.2');

COMMIT;

START TRANSACTION;

DROP TABLE `CartItem`;

ALTER TABLE `Carts` ADD `ProductId` int NOT NULL DEFAULT 0;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230130144329_Finish2', '7.0.2');

COMMIT;

START TRANSACTION;

ALTER TABLE `Products` ADD `SellerId` int NOT NULL DEFAULT 0;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230130154455_Finish3', '7.0.2');

COMMIT;

