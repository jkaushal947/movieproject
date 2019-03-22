create table movies
(
movieid int identity primary key,
movietitle varchar(50) not null,
releasedate date not null,
genre varchar(50) not null,
price float not null,
img varchar(max),
video varchar(max)

)