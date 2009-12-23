
use database Northwind

use Employees

while Employees.ReadNext()
	?? Employees.GetField("EmployeeID")
	?? " "
	?? Employees.GetField("FirstName")
	?? " "
	? Employees.GetField("LastName")
end

