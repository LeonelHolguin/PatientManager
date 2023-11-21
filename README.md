#Script pata la DB luego de correr el migration:

insert into Users (Users.Name, LastName, Email, UserName, Users.Password, Users.Role, Created, CreatedBy) values ('John', 'Doe', 'DSFSDF@DFSGFDS.com', 'JohnDoe', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'Administrator', GETDATE(), 'Pedro');

insert into Users (Users.Name, LastName, Email, UserName, Users.Password, Users.Role, Created, CreatedBy) values ('martina', 'Doe', 'DSFSDF@DFSGFDS.com', 'Martina', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'Assistant', GETDATE(), 'Pedro');

-La contraseña de ambos es 12345
-Lo realice asi ya que los usuarios son manejados por un admin y no existe un registro externo, solo se puede tener usuarios si alguien desde dentro de la app lo hace, o desde la misma DB
