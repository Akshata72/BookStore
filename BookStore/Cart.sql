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

select * From Cart;

alter proc SP_RemoveFrom_Cart
(
@CartId int,
@UserId int
)
As
Begin
  if (Exists(Select * From Cart where CartId = @CartId ))
  Begin
	delete from Cart where CartId = @CartId And UserId = @UserId ;
  end
  else
    Print 'CartId dose not exist'
end;

exec SP_RemoveFrom_Cart 1 ,1 ;
delete from Cart where CartId =1;



-------------------------Creating procedure for update -----------------
alter procedure SP_UpdateCart(@BookQuantity int,
@UserId int,
@CartId int
)
As
Begin
	update Cart set Book_Quantity = @BookQuantity where CartId = @CartId And UserId = @UserId;
End

exec SP_UpdateCart 5,1,1


-------------------------Creating procedure for Get All -----------------
alter procedure GetAllCart(
@UserId int
)
AS
Begin
	select
		c.CartId,
		c.BookId,
		c.UserId,
		c.Book_Quantity,
		b.BookName,
		b.BookImage,
		b.AuthorName,
		b.DiscountPrice,
		b.ActualPrice
		from Cart c
		inner join Book b
		on c.BookId = b.BookId
		where c.UserId = @UserId;
end
