------------------ Creating a table for the Cart------------
Create Table Cart
(
CartId int identity(1,1) primary key,
Book_Quantity int default 1,
UserId int not null foreign key (UserId) references BookStore(UserId),
BookId int not null Foreign key (BookId) references Book(BookId)
);

select  *  From Cart
----------------- Creating a procedure for the Add to cart -----------


Create proc SP_AddToCart
( @BookQuantity int,
@UserId int,
@BookId int
)
As
Begin
	insert into cart
	values ( @BookQuantity,@UserId, @BookId);
End

-------------------------Creating procedure for the Remove From Cart -----------------

Create proc SP_RemoveFrom_Cart
(
@CartId int
)
As
Begin
	delete from Cart where CartId = @CartId;
end