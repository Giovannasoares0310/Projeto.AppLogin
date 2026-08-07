create database LoginCore;
use LoginCore;

create table Cliente
(
Id int auto_increment primary key,
Nome Varchar(50) not null,
Nascimento DateTime not null,
Sexo char(1),
CPF Varchar(11) not null,
Telefone Varchar(14) not null,
Email Varchar(50) not null,
Senha Varchar(8) not null,
Situacao char(1) not null
);

create table Colaborador
(
Id int auto_increment primary key,
Nome Varchar(50) not null,
Email Varchar(50) not null,
Senha Varchar(8) not null,
Tipo Varchar(8) not null
);

insert into Cliente values (default, 'Laura', '2008-11-06 19:23:00', 'F', '54909258841', '(11)940407496', 'lauxavi1357@gmail.com', 'Chubb@12', 'D');

insert into Colaborador values (default, 'Valdiscléia', 'Val_Vul67@gmail.com', 'ValVul67', 'Gerente');
insert into Colaborador values (default, 'Jonivaldo', 'Joni_valdinho@gmail.com', 'JojoNini', 'Comum');


select * from Cliente;