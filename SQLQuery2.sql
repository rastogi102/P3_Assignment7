create database LibraryDB
use LibraryDB


create table Books
(
BookId int primary key,
Title nvarchar(50),
Author nvarchar(50),
Genre nvarchar(50),
Quantity int
)



insert into Books (BookId, Title, Author, Genre, Quantity)
values (1, 'The Great Gatsby', 'F. Scott Fitzgerald', 'Fiction', 10);

insert into Books (BookId, Title, Author, Genre, Quantity)
values (2, 'To Kill a Mockingbird', 'Harper Lee', 'Fiction', 8);

insert into Books (BookId, Title, Author, Genre, Quantity)
values (3, '1984', 'George Orwell', 'Science Fiction', 15);

insert into Books (BookId, Title, Author, Genre, Quantity)
values (4, 'Pride and Prejudice', 'Jane Austen', 'Classic', 12);

insert into Books (BookId, Title, Author, Genre, Quantity)
values (5, 'Harry Potter and the Sorcerer''s Stone', 'J.K. Rowling', 'Fantasy', 20);

select * from Books