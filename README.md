# Simple Library

This library mangament is a demo made of :

* ASP.NET 5
* SQL Server (local db)  
* Angularjs
* Typescript

<img src="https://d.img.vision/mgh/dashboard.png" align="left"
     alt="dashboard" width="500" height="400">
          
<img src="https://d.img.vision/mgh/menus.png" align="left"
     alt="menus" width="200" height="300">
                            
<img src="https://d.img.vision/mgh/users.png" style="margin-top:20px"
     alt="users" width="500" height="300">

# How to

After run the app, two databases will be created:

* FinLib : main/operational database and managed by ef core
* FinLib_Auditing : for auditing/logging purpose by NLog

There are 3 roles:

* admin (can access to everything) (user:admin pwd:123qwe!@#QWE)
* librarian (can borrow a book to a customer) (user:librarian1 pwd:123qwe!@#QWE)
* customer (can browse books) (user:customer1 pwd:123qwe!@#QWE)
