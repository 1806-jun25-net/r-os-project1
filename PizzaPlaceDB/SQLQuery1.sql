select * from Pizzeria.Pizzas;
select * from Pizzeria.OrderPizza;
select * from Pizzeria.pizzaTopping;
select * from Pizzeria.Orders;
select * from Pizzeria.Users;
select * from Pizzeria.Inventory;

insert into  Pizzeria.Inventory(name, is_topping, quantity, location_id)
values ('BBQ Sauce', 0, 60, 1),('BBQ Sauce', 0, 60, 2);

drop table Pizzeria.has_topping;

create table Pizzeria.pizzaTopping
(
	ID  int identity primary key,
	pizza_id int,
	item_id int

);

alter table Pizzeria.pizzaTopping
add foreign key (item_id) references Pizzeria.Inventory(item_id);


ALTER TABLE Orders
ADD FOREIGN KEY (PersonID) REFERENCES Persons(PersonID);


insert into Pizzeria.Inventory (name, is_topping, quantity, location_id)
values ('Cheese',1, 60, 1), ('Cheese',1, 60, 2);

delete
from Pizzeria.Inventory
where  item_id = 23 or item_id = 24;