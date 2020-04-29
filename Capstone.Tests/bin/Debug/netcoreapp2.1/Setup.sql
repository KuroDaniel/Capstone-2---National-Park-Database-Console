Delete From reservation
delete from site
delete from campground
delete from park

insert into park
(name, location, establish_date, area, visitors, description)
values('Twilight Park', 'Ohio', '1999-12-25', 9001, 2000000, 'A beautiful park that has a great sunset, perfect for eating ice cream!')

Declare @newParkId int = (SELECT @@IDENTITY);

insert into campground
(park_id, name, open_from_mm, open_to_mm, daily_fee)
VALUES
((SELECT park_id from park where name = 'Twilight Park'), 'Sea Salt Path', 3, 9,'100.69');

Declare @newCampgroundId int = (SELECT @@IDENTITY);

insert into site
(campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities)
VALUES((SELECT campground_id FROM campground where name = 'Sea Salt Path'), 1, 20, 'true', 35, 'false');

DECLARE @newSiteId int = (Select @@IDENTITY);

insert into reservation
(site_id, name, from_date, to_date, create_date)
VALUES
((select site_id from site where site_number = 1), 'Rat family reservation', '2020-02-17', '2020-02-21', GETDATE());

DECLARE @newReservationId int = (select @@IDENTITY);

SELECT @newParkId as newParkId
SELECT @newCampgroundId as newCampgroundId
SELECT @newSiteId as newSiteId
SELECT @newReservationId as newReservationId
