
open database Northwind

use Employees

while Employees.MoveNext()
	?? Employees.GetField("EmployeeID")
	?? " "
	?? Employees.GetField("FirstName")
	?? " "
	? Employees.GetField("LastName")
end

