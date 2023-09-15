CREATE DATABASE BD_DISCIPLINA;

create table disciplina(

	coddisciplina int identity (1,1) primary key,
	nomedisciplina varchar(80) not null,
	ementa varchar(80) not null,
	cargahoraria varchar(20) not null,
	bibliografia varchar(255) not null,	
	fotodisciplina varbinary(max)

);