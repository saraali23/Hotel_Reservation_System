
go
create or Alter proc Search @p0 varchar(50)
as
	Select r.Id,r.NumberOfGuests,r.Room_floor,r.Room_number,r.Room_Type,r.Arrival_time,r.Leaving_time,r.Check_in,r.Supply_status,c.First_name,c.Last_name,c.Birth_day,c.Gender,c.Phone_number,c.Email_address,c.Street_address,c.Apt_suite,c.City,c.State,c.ZipCode,p.Payment_type,p.Card_type,p.Card_number,p.Card_exp,Card_cvc,p.Total_bill,s.Breakfast,s.Lunch,s.Dinner,s.Cleaning,s.Towel,s.S_surprise,s.Food_bill
	from Reservations r,Customers c,Payments p,Services s 
	where c.Id=r.CustomerId and p.Id=r.PaymentId and s.Id=r.ServicesId and(  r.id like '%' +@p0+ '%' OR c.last_name like '%' +@p0+ '%' OR first_name like '%' +@p0+ '%' OR gender like  '%' +@p0+ '%' OR state like  '%' +@p0+ '%' OR city like  '%' +@p0+ '%' OR room_number like '%' +@p0+ '%' OR room_type like  '%' +@p0+ '%' OR  email_address like  '%' +@p0+ '%' OR phone_number like  '%' +@p0+ '%')

exec Search 'a'

create or Alter proc GetAllData 
as
	Select r.Id,r.NumberOfGuests,r.Room_floor,r.Room_number,r.Room_Type,r.Arrival_time,r.Leaving_time,r.Check_in,r.Supply_status,c.First_name,c.Last_name,c.Birth_day,c.Gender,c.Phone_number,c.Email_address,c.Street_address,c.Apt_suite,c.City,c.State,c.ZipCode,p.Payment_type,p.Card_type,p.Card_number,p.Card_exp,Card_cvc,p.Total_bill,s.Breakfast,s.Lunch,s.Dinner,s.Cleaning,s.Towel,s.S_surprise,s.Food_bill
	from Reservations r,Customers c,Payments p,Services s 
	where c.Id=r.CustomerId and p.Id=r.PaymentId and s.Id=r.ServicesId
exec Search 'a'