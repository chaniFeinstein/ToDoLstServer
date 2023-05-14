// using TodoApi;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.OpenApi.Models;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;
// using System.Text;
// using System.Security.Claims;

// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("OpenPolicy",
//                           policy =>
//                           {
//                               policy.WithOrigins("http://localhost:3000")
//                                                   .AllowAnyHeader()
//                                                   .AllowAnyMethod();
//                           });
// });
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Description = "Keep track of your tasks", Version = "v1" });
//     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//     {
//         Scheme = "Bearer",
//         BearerFormat = "JWT",
//         In = ParameterLocation.Header,
//         Name = "Authorization",
//         Description = "Bearer Authentication with JWT Token",
//         Type = SecuritySchemeType.Http
//     });
//     c.AddSecurityRequirement(new OpenApiSecurityRequirement
//     {
//         {
//             new OpenApiSecurityScheme
//             {
//         Reference = new OpenApiReference
//                 {
//                     Id = "Bearer",
//                     Type = ReferenceType.SecurityScheme
//                 }
//             },
//             new List<string>()
//         }
//     });
// });

// builder.Services.AddDbContext<ToDoDbContext>();

// builder.Services.AddAuthentication(options=>{
//     options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
// })    .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = builder.Configuration["JWT:Issuer"],
//             ValidAudience = builder.Configuration["JWT:Audience"],
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
//         };
//     });

// var app = builder.Build();

// app.UseCors("OpenPolicy");

// if(app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
// app.UseSwagger(options =>
// {
//     options.SerializeAsV2 = true;
// });

// app.UseSwaggerUI(options =>
// {
//     options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//     options.RoutePrefix = string.Empty;
// });

// //app.MapGet("/", ()=> "huo");

// app.MapGet("/todoItems", async (ToDoDbContext db) =>
//    { return await db.Items.ToListAsync();}
// );

// app.MapPost("/login",async (IConfiguration configuration,ToDoDbContext db,[FromBody] User user)=>{
//     if(await db.Users.FirstOrDefaultAsync((u)=>u.UserName==user.UserName && u.Password==user.Password)==null){
//         var claims=new List<Claim>(){
//             new Claim(ClaimTypes.Name,user.UserName);
//         };
//     }
// });

// app.MapPost("/todoitems", async(ToDoDbContext db,[FromBody] Item item ) => {
//     db.Items.Add(item);
//     await db.SaveChangesAsync();
//     return item;
// });

// app.MapPut("/todoitems/{id}", async (ToDoDbContext db,[FromBody] Item item, int id) => {
//     var existItem = await db.Items.FindAsync(id);
//     if(existItem is null) 
//         return Results.NotFound();

//     existItem.IsComplete = item.IsComplete;

//     await db.SaveChangesAsync();
//     return Results.NoContent();
// });

// app.MapDelete("/todoitems/{id}", async (ToDoDbContext db,int id) =>{

//     var existItem=await db.Items.FindAsync(id);
//     if(existItem is null) return Results.NotFound();

//     db.Items.Remove(existItem);
//     await db.SaveChangesAsync();

//     return Results.NoContent();
// });

// app.UseAuthentication();

// app.Run();

