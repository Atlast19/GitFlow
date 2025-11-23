Create Database SRC
go
use SRC

create table usuarios
(
IDUser int identity(1,1) primary key,
NameUser nvarchar (100) not null,
PasswordUser nvarchar (100) not null,
MatriculaUser nvarchar(100) not null,
AgeUser date not null,
EmailUser nvarchar(500) not null,
)

insert into usuarios ( NameUser, PasswordUser, MatriculaUser, AgeUser, EmailUser)
values('Admin','Admin','20240176','2025-11-18','Admin@gmail.com')

insert into usuarios ( NameUser, PasswordUser, MatriculaUser, AgeUser, EmailUser)
values('Leo','Leo','20240176','2025-11-18','Admin@gmail.com')

select * from usuarios

create table Reservas
(
IDCurso int identity (1,1) primary key,
NombreMaestro nvarchar (100) not null,
NombreMateria nvarchar(100) not null,
Turno nvarchar (100) check (Turno = 'Vespertino' or Turno = 'Matutino'),
HorasInicioCurso time(0) not null, 
HoraFinalCurso time(0) not null,
EspaciosDisponibles int not null,
FechaFin date not null,
HoraFin time(0) not null,
Estado bit default 1 not null,
)



insert into Reservas(NombreMaestro,NombreMateria, Turno, HorasInicioCurso, HoraFinalCurso, EspaciosDisponibles, FechaFin, HoraFin)
values ('Juan Rosario','C# Avanzado','Matutino','13:30','15:00', 2, getdate(), '13:03:00')


select * from Reservas


Create table Inscripciones
(
IDInscripcion int identity(1,1) primary key,
IDCurso int not null,
IDUser int not null,
EmailUser nvarchar (500) not null
Foreign key (IDUser) references usuarios (IDUser),
foreign key (IDCurso) references Reservas (IDCurso)
)

insert into Inscripciones(IDCurso, IDUser ,EmailUser)
values (1,2, 'ahleandro18@gmail.com')

select * from Reservas
select * from usuarios
select * from Inscripciones


select IDCurso from Reservas


CREATE PROCEDURE sp_AgregarInscripcion
    @IDCurso INT,
    @IDUser INT,
    @EmailUser NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Espacios INT;
    DECLARE @InscritosActuales INT;
    DECLARE @FechaFin DATE;
    DECLARE @HoraFin TIME;
    DECLARE @FechaHoraFin DATETIME;
    DECLARE @Estado NVARCHAR(10);

    -- Obtener datos del curso
    SELECT 
        @Espacios = EspaciosDisponibles, 
        @FechaFin = FechaFin,
        @HoraFin = HoraFin,
        @Estado = Estado
    FROM Reservas 
    WHERE IDCurso = @IDCurso;

    IF @Espacios IS NULL
    BEGIN
        RAISERROR('El curso no existe.', 16, 1);
        RETURN;
    END

    -- Validar si el curso ya está marcado como inactivo
    IF @Estado = 0
    BEGIN
        RAISERROR('No se puede inscribir. El curso está inactivo.', 16, 1);
        RETURN;
    END

    -- Combinar fecha y hora de manera precisa usando DATETIMEFROMPARTS
    SET @FechaHoraFin = DATETIMEFROMPARTS(
        YEAR(@FechaFin),
        MONTH(@FechaFin),
        DAY(@FechaFin),
        DATEPART(HOUR, @HoraFin),
        DATEPART(MINUTE, @HoraFin),
        DATEPART(SECOND, @HoraFin),
        0 -- milisegundos
    );

    -- Validar si ya pasó la fecha/hora exacta
    IF GETDATE() >= @FechaHoraFin
    BEGIN
        UPDATE Reservas
        SET Estado = 'False'
        WHERE IDCurso = @IDCurso;

        RAISERROR('No se puede inscribir. La fecha y hora límite del curso ya han pasado o se cumplen en este momento.', 16, 1);
        RETURN;
    END

    -- Verificar si el usuario ya está inscrito en el curso
    IF EXISTS (
        SELECT 1 
        FROM Inscripciones 
        WHERE IDCurso = @IDCurso AND IDUser = @IDUser
    )
    BEGIN
        RAISERROR('Ya estás inscrito en este curso.', 16, 1);
        RETURN;
    END

    -- Contar inscripciones actuales
    SELECT @InscritosActuales = COUNT(*) 
    FROM Inscripciones 
    WHERE IDCurso = @IDCurso;

    -- Verificar si está lleno
    IF @InscritosActuales >= @Espacios
    BEGIN
        UPDATE Reservas
        SET Estado = 'False'
        WHERE IDCurso = @IDCurso;

        RAISERROR('No se puede inscribir. El curso ya está lleno.', 16, 1);
        RETURN;
    END

    -- Insertar inscripción
    INSERT INTO Inscripciones (IDCurso, IDUser, EmailUser)
    VALUES (@IDCurso, @IDUser, @EmailUser);

    -- Cerrar si se llenó
    IF @InscritosActuales + 1 = @Espacios
    BEGIN
        UPDATE Reservas
        SET Estado = 'False'
        WHERE IDCurso = @IDCurso;
    END
END






--CREATE PROCEDURE sp_AgregarInscripcion
--    @IDCurso INT,
--    @IDUser INT,
--    @EmailUser NVARCHAR(255)
--AS
--BEGIN
--    SET NOCOUNT ON;

--    DECLARE @Espacios INT;
--    DECLARE @InscritosActuales INT;
--    DECLARE @FechaFin DATE;
--    DECLARE @HoraFin TIME;
--    DECLARE @FechaHoraFin DATETIME;

--    -- Obtener datos del curso
--    SELECT 
--        @Espacios = EspaciosDisponibles, 
--        @FechaFin = FechaFin,
--        @HoraFin = HoraFin
--    FROM Reservas 
--    WHERE IDCurso = @IDCurso;

--    IF @Espacios IS NULL
--    BEGIN
--        RAISERROR('El curso no existe.', 16, 1);
--        RETURN;
--    END

--    -- Combinar fecha y hora
--    SET @FechaHoraFin = DATEADD(SECOND, DATEDIFF(SECOND, 0, @HoraFin), CAST(@FechaFin AS DATETIME));

--    -- Validar si ya pasó la fecha/hora
--    IF GETDATE() >= @FechaHoraFin
--    BEGIN
--        UPDATE Reservas
--        SET Estado = 'False'
--        WHERE IDCurso = @IDCurso;

--        RAISERROR('No se puede inscribir. La fecha y hora límite del curso ya han pasado o se cumplen en este momento.', 16, 1);
--        RETURN;
--    END

--    -- Contar inscripciones actuales
--    SELECT @InscritosActuales = COUNT(*) 
--    FROM Inscripciones 
--    WHERE IDCurso = @IDCurso;

--    -- Verificar si está lleno
--    IF @InscritosActuales >= @Espacios
--    BEGIN
--        UPDATE Reservas
--        SET Estado = 'False'
--        WHERE IDCurso = @IDCurso;

--        RAISERROR('No se puede inscribir. El curso ya está lleno.', 16, 1);
--        RETURN;
--    END

--    -- Insertar inscripción
--    INSERT INTO Inscripciones (IDCurso, IDUser, EmailUser)
--    VALUES (@IDCurso, @IDUser, @EmailUser);

--    -- Cerrar si se llenó
--    IF @InscritosActuales + 1 = @Espacios
--    BEGIN
--        UPDATE Reservas
--        SET Estado = 'False'
--        WHERE IDCurso = @IDCurso;
--    END
--END

exec sp_AgregarInscripcion @IDCurso = 13 , @IDUser = 25, @EmailUser = 'ahleandro18@gamil.com'


select * from Inscripciones

update Inscripciones set EmailUser = 'cashp0904@gamil.com' where IDInscripcion = 1

drop PROCEDURE sp_AgregarInscripcion
drop table Inscripciones
drop table usuarios
Drop table Reservas
