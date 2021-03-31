# TODOS
## High
- Add interfaces (for everything?)
- Add CRUD handlers (sepparate bussines from presentation layer) (move DB CRUD operations from controllers, only call methods)
![Imaginatorul](https://cdn.discordapp.com/attachments/701769597959012424/826827378331746324/unknown.png)
- Verify that async methods return Tasks
- Verify that ApiControllers return Task<IActionResult>
- Add Github Project ad transfer tasks there (and document them better)
## Low
- Steal code from Olariu
- Change Db logic to be async (Services + Querries)
- Add Abstract BaseEntity class
- (Maybe) separate each crud opperation in Command + Handler using the MediatR (or not)
- See how to inject .env as a configuration to Startup.services
- Add public to public classes
- Add services.SaveChangesAsync if possible (helps with memory caching)
- Add BaseApiController (if needed)
- Create Payload Validation Middleware (if possible)
- Version the APIs 

![Imaginatorul](https://cdn.discordapp.com/attachments/701769597959012424/826826097575460924/unknown.png)

