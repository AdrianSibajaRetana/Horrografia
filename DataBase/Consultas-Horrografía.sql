CREATE DATABASE baseDatos

USE baseDatos

CREATE TABLE Nivel (
                       id int NOT NULL AUTO_INCREMENT,
                       Nombre VARCHAR(30) NOT NULL,
                       Descripcion VARCHAR(255) NOT NULL,
                       ErroresPermitidos INT NOT NULL,
                       NumeroDeItems INT NOT NULL,
                       PRIMARY KEY(id)
)

CREATE TABLE Escuela (
                         Nombre VARCHAR(255) NOT NULL,
                         Codigo VARCHAR(255) NOT NULL,
                         PRIMARY KEY(Codigo)
)

CREATE TABLE Pista(
                      id INT NOT NULL AUTO_INCREMENT,
                      Pista VARCHAR(255) NOT NULL,
                      PRIMARY KEY(id)
)

CREATE TABLE Item (
                      id int NOT NULL AUTO_INCREMENT,
                      FormaCorrecta VARCHAR(255) NOT NULL,
                      PistaId INT,
                      NivelId INT,
                      PRIMARY KEY(id),
                      FOREIGN KEY (PistaId) REFERENCES Pista(id) ON DELETE SET NULL,
                      FOREIGN KEY (NivelId) REFERENCES Nivel(id) ON DELETE SET NULL
)

CREATE TABLE Tag (
                     id int NOT NULL AUTO_INCREMENT,
                     Tag VARCHAR(255) NOT NULL,
                     PRIMARY KEY(id)
)

CREATE TABLE ItemTag(
                        idTag INT NOT NULL,
                        idItem INT NOT NULL,
                        FOREIGN KEY (idTag) REFERENCES Tag(id) ON DELETE CASCADE,
                        FOREIGN KEY (idItem) REFERENCES Item(id) ON DELETE CASCADE
)

CREATE TABLE FormaIncorrecta(
                                Itemid INT,
                                Forma VARCHAR(255) NOT NULL,
                                FOREIGN KEY (Itemid) REFERENCES Item(id) ON DELETE CASCADE
)


CREATE TABLE Reporte(
                        id int NOT NULL AUTO_INCREMENT,
                        idUsuario VARCHAR(255) NOT NULL,
                        idNivel int NOT NULL,
                        CantidadErrores int NOT NULL,
                        Puntuacion INT NOT NULL,
                        PRIMARY KEY(id),
                        FOREIGN KEY (idNivel) REFERENCES Nivel(id),
                        FOREIGN KEY (idUsuario) REFERENCES aspnetusers(Id)
)

CREATE TABLE ContieneError(
                      idReporte INT NOT NULL,
                      idItem INT NOT NULL,
                      respuesta VARCHAR(255) NOT NULL,
                      FOREIGN KEY (idReporte) REFERENCES Reporte(id),
                      FOREIGN KEY (idItem) REFERENCES Item(id)
)


# Para añadir propiedades a la tabla de nivel.
# Se tienen que borrar las siguientes tablas en orden ascendiente. 
# Se hace el cambio en nivel. 
# Se crean las tablas de nuevo en orden descendente.  nivel
DROP TABLE IF EXISTS nivel;
DROP TABLE IF EXISTS Reporte;
DROP TABLE IF EXISTS contieneerror;

# Para añadir propiedades a la tabla de item:
DROP TABLE IF EXISTS FormaIncorrecta;
DROP TABLE IF EXISTS contieneerror;
DROP TABLE IF EXISTS ItemTag;
DROP TABLE IF EXISTS item;

## Consulta para conseguir items de un nivel. 
SELECT * 
FROM Item
JOIN pertenecea
ON item.id = pertenecea.idItem
WHERE pertenecea.idNivel = 20

## Consulta para conseguir todas las formas incorrectas de los items de un nivel
SELECT *
FROM formaincorrecta
         JOIN item
              ON item.id = formaincorrecta.Itemid
         JOIN pertenecea
              ON item.id = pertenecea.idItem
WHERE pertenecea.idNivel = 20

## Comando para crear un administrador arbitrario
INSERT INTO aspnetuserroles(UserId, RoleId) VALUES('1315072f-24da-4749-ac89-9f2a95377a8c', '896b75c1-ab2c-4401-bdbe-1e5b42dde3ef');


##Selecciona el nombre de usuario de los profesores de una escuela. 
SELECT NombreDeUsuario
FROM aspnetusers
INNER JOIN aspnetuserroles
ON aspnetusers.Id = aspnetuserroles.UserId
INNER JOIN aspnetroles
ON aspnetuserroles.RoleId = aspnetroles.Id
WHERE aspnetroles.Name = 'Profesor'
AND aspnetusers.CodigoEscuela = '12345678';

SELECT * FROM escuela

                  # Para creación de usuario
CREATE USER 'adrian'@'localhost' IDENTIFIED BY '1234'
GRANT ALL PRIVILEGES ON * . * TO 'adrian'@'localhost';