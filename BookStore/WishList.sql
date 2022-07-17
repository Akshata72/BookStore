Create Table WishList(
WishListId int identity Primary Key,
UserId int not null foreign key (UserId) references BookStore(UserId),
BookId int not null Foreign key (BookId) references Book(BookId));

---- Creating procedure for Add to WishList -------
CREATE PROCEDURE Sp_AddWishList(@UserId int,@BookId int)
AS
Begin
   Begin Try
        insert into WishList values(@UserId,@BookId);
   End Try
   
   Begin Catch
   select
   Error_Message() As ErrorMessage,
   Error_Line() as errorline
   End Catch
End;   

select * from BookStore;

select * from WishList;

------SP for Remove From WishList---------
Alter Procedure Sp_RemoveWishList(@WishListId int,@UserId int)
As
Begin
   Begin Try
      Delete from WishList where WishListId = @WishListId And UserId = @UserId;
   End Try

   Begin Catch
   select
   Error_Message() As ErrorMessage,
   Error_Line() As ErrorLine
   End Catch
End;

--------SP for Get All  From WishList-----
Create Procedure Sp_GetAll_FromWishList(@UserId int)
As
Begin
   Begin Try
    select w.UserId,
	       w.BookId,
		   w.WishListId,
		   b.BookName,
		   b.BookImage,
		   b.AuthorName,
		   b.DiscountPrice,
		   b.ActualPrice
    from WishList w
	inner join Book b
		on w.BookId = b.BookId
		where w.UserId =@UserId;
   End try

   Begin Catch
   select
      Error_Message() As ErrorMessage,
	  error_line() As errorline
   End Catch
End;