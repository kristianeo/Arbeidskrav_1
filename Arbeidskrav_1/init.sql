DROP DATABASE IF EXISTS GeneratedCharacters;
CREATE DATABASE GeneratedCharacters;

use GeneratedCharacters;

CREATE TABLE GameCharacter (
    characterID INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(255),
    class VARCHAR(255),
    hitPoints VARCHAR(255),
    xpLevel2 INT,
    primeRequisite VARCHAR(255)    
);

CREATE TABLE AbilityScores (
    abilityID INT PRIMARY KEY AUTO_INCREMENT,
    characterID VARCHAR(255),
    ability VARCHAR(255),
    score INT,
    FOREIGN KEY (characterID) references GameCharacter (characterID)
);