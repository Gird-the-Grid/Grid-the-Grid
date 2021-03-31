# Programming languages and technologies
*made on 30/03/2021* 

Contents:

* [Summary](#summary)
  * [Issue](#issue)
  * [Decision](#decision)
  * [Status](#status)
* [Details](#details)
* [Notes](#notes)


## Summary


### Issue

We need to choose programming languages for our software. We have two major needs: a front-end programming language suitable for web applications, and a back-end programming language suitable for server applications. As well as a database, for efficiently storing the users data.

### Decision

We are choosing
- Blazor web framework for the front-end

- ASP.NET Core server application (.Net5.0) for the back-end 

- MongoDB for storing the clients data, such as passwords, emails, grid parameters, etc.



### Status

Decided. We are open to new alternatives, if problems with the current ones arise.


## Details

### Argument
#### Summary per each:
We choose Blazor and .NET because it allows us to write the code for the client and server in the same technology (.NET).
Also, the same classes can be shared by both client and server code. Meaning that using Blazor will provide the stability, consistency and productivity of .NET, in comparison to other front-end frameworks.
For storage, we choose to use MongoDB, because is a cloud-base storage, that offers high performance, it has a cost-effective DBMS and is easy to scale.
## Notes

Any notes here.
