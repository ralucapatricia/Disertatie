CREATE TABLE places (
    placeId INT AUTO_INCREMENT PRIMARY KEY,
    address VARCHAR(255) NOT NULL,
    date DATE NOT NULL,
    description TEXT,
    imageUriC VARCHAR(255),
    locationId INT,
    title VARCHAR(255) NOT NULL,
    user VARCHAR(255) NOT NULL,
    FOREIGN KEY (locationId) REFERENCES locations(locationId)
);
CREATE TABLE locations (
    locationId INT AUTO_INCREMENT PRIMARY KEY,
    lat DECIMAL(10, 8) NOT NULL,
    lng DECIMAL(11, 8) NOT NULL
);

CREATE TABLE messages (
    messageId INT AUTO_INCREMENT PRIMARY KEY,
    date DATE NOT NULL,
    message TEXT NOT NULL,
    userEmail VARCHAR(255) NOT NULL,
);