Create Table Book(
BookId int identity Primary key,
BookName varchar(255) Not Null,
AuthorName varchar(255) Not Null,
BookImage varchar(255) Not Null,
BookDetails varchar(255)Not Null,
ActualPrice int Not Null,
DiscountPrice int Not Null,
Quntity int Not Null,
Rating float,
RatingCount int,
UserId int Foreign key References BookStore(UserId));

drop table Book;

Alter Procedure Sp_AddBook(
    @BookName varchar(255),
    @AuthorName varchar(255),
	@BookImage varchar(255),
	@BookDetails varchar(255),
	@ActualPrice int,
	@DiscountPrice int,
	@Quntity int,
	@Rating float ,
	@RatingCount int,
	@UserId int)
As
Begin
    Begin try
	    Begin transaction
        insert into Book values (@BookName,@AuthorName,@BookImage,@BookDetails,@ActualPrice,@DiscountPrice,@Quntity,@Rating,@RatingCount,@UserId);
		commit transaction
    end try

	begin catch
	    print(error_message())
		rollback transaction
    end catch
End;


select * from Book;



/***************SP for Update Book***************/
Alter Procedure Sp_UpdateBook(
    @BookId int,
	@BookImage varchar(255),
	@BookDetails varchar(255),
	@ActualPrice int,
	@DiscountPrice int,
	@Quntity int,
	@Rating float ,
	@RatingCount int,
	@UserId int)
As
Begin
    Begin try
	    Begin transaction
        Update Book 
		Set BookImage = @BookImage,
		Bookdetails =@BookDetails,
		ActualPrice = @ActualPrice,
		DiscountPrice =@DiscountPrice,
		Quntity =@Quntity,
		Rating =@Rating,
		RatingCount = @RatingCount
		where BookId = @BookId And UserId = @UserId;
		commit transaction
    end try

	begin catch
	    print(error_message())
		rollback transaction
    end catch
End;

/***************SP for Delete Book***************/
Create Procedure Sp_DeleteBook @BookId int,@UserId int
As
Begin
    Begin try
	    Begin transaction
		Delete From Book where BookId = @BookId And UserId = @UserId;
		commit transaction
    end try

	begin catch
	    print(error_message())
		rollback transaction
    end catch
End;



/***************SP for Get Book by BookId***************/
Create Procedure Sp_GetBookById @BookId int,@UserId int
As
Begin
    Begin try
	    Begin transaction
		Select * from Book where BookId = @BookId And UserId = @UserId;
		commit transaction
    end try

	begin catch
	    print(error_message())
		rollback transaction
    end catch
End;

/***************SP for GetAllBook***************/
Create Procedure Sp_GetAllBook @UserId int
As
Begin
    Begin try
	    Begin transaction
		Select * from Book where UserId = @UserId;
		commit transaction
    end try

	begin catch
	    print(error_message())
		rollback transaction
    end catch
End;