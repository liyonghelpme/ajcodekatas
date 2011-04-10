
This is an example of NHibernate 3.x mapping:
- Three tables, one abstract, two concrete ones.
- Two tables

using the mapping strategy Table Per Concrete Class

To generate the database (in \.SQLEXPRESS) run

ExecuteAll.cmd

in Sql folder.

You can specify the SQL Server using a parameter, e.g.:

ExecuteAll.cmd (local)

If you don't provide a parameter, .\SQLEXPRESS is assumed.

More info about my NHibernate examples at:
http://ajlopez.wordpress.com/category/nhibernate/
http://msmvps.com/blogs/lopez/archive/tags/NHibernate/default.aspx

Angel "Java" Lopez
http://www.ajlopez.com
http://twitter.com/ajlopez
