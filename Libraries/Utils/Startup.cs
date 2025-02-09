using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;

namespace Utils
{
    public partial class Startup
    {
        public static void ConfigConstants(WebApplicationBuilder builder, params Type[] additionals)
        {
            List<Type> constants = new List<Type>()
            {
                typeof(Utils.Constants.COMMON),
                typeof(Utils.Constants.MAIL),
                typeof(Utils.Constants.WEB)
            };
            if (additionals != null)
                constants.AddRange(additionals.ToList());

            foreach (Type constant in constants)
            {
                foreach (PropertyInfo prop in constant.GetProperties())
                {
                    string key = string.Format("Constants:{0}", prop.Name);
                    if (builder.Configuration[key] != null)
                    {
                        if (prop.PropertyType == typeof(int)
                            || prop.PropertyType == typeof(int?))
                        {
                            int i;
                            if (int.TryParse(builder.Configuration[key], out i))
                                prop.SetValue(null, i, null);
                        }
                        else if (prop.PropertyType == typeof(decimal)
                            || prop.PropertyType == typeof(decimal?))
                        {
                            decimal i;
                            if (decimal.TryParse(builder.Configuration[key], out i))
                                prop.SetValue(null, i, null);
                        }
                        else if (prop.PropertyType == typeof(bool)
                                    || prop.PropertyType == typeof(bool?))
                        {
                            bool i;
                            if (bool.TryParse(builder.Configuration[key], out i))
                                prop.SetValue(null, i, null);
                        }
                        else
                            prop.SetValue(null, builder.Configuration[key], null);
                    }
                }
            }
        }
        public static AuthenticationBuilder ConfigAuthentication(WebApplicationBuilder builder)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims

            AuthenticationBuilder auth = builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.RequireAuthenticatedSignIn = true; // Add By Sommai P. Sep 23, 2020
                })
                .AddJwtBearer(cfg =>
                {
                    // cfg.RequireHttpsMetadata = false;
                    // cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = builder.Configuration["JwtIssuer"],
                        ValidAudience = builder.Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"])),

                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(90);
            });
            builder.Services.Configure<IISOptions>(options =>
            {
                options.AutomaticAuthentication = true;
            });

            return auth;
        }
        public static void ConfigCors(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    if (string.IsNullOrWhiteSpace(Utils.Constants.WEB.CLIENT_URL))
                    {
                        policy
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    }
                    else
                    {
                        string[] urls = Utils.Constants.WEB.CLIENT_URL.Split(",").ToArray();

                        policy
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithOrigins(urls)
                            .AllowCredentials();
                    }
                });
            });
        }
        public static void UseCors(WebApplication app)
        {
            app.UseCors("default");
        }

        public static void ConfigRequestSize(WebApplicationBuilder builder)
        {
            builder.Services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });
            builder.Services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });
        }
        public static void ConfigController(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                    options.SerializerSettings.Converters.Add(new Utils.Converters.JsonDateTimeConverter());
                });
        }
        public static void ConfigUtils(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<Web.Services.IFileUpload, Web.Services.FileUpload>();
        }

        public static void ConfigSwagger(WebApplicationBuilder builder)
        {
            bool useSwagger = false;
            if (builder.Configuration["Swagger:Enabled"] != null)
            {
                bool.TryParse(builder.Configuration["Swagger:Enabled"], out useSwagger);
            }
            if (useSwagger)
            {
                string swaggerTitle = "Document";
                if (builder.Configuration["Swagger:Title"] != null)
                {
                    swaggerTitle = builder.Configuration["Swagger:Title"];
                }

                string swaggerVersion = "v1";
                if (builder.Configuration["Swagger:Version"] != null)
                {
                    swaggerVersion = "v" + builder.Configuration["Swagger:Version"];
                }

                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(swaggerVersion, new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = swaggerTitle,
                        Version = swaggerVersion
                    });
                    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Name = "Authorization",
                        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                        Description = "Please insert JWT with Bearer info field.",

                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme
                        }
                    });
                    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                    {
                        {
                            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                            {
                                Scheme = "bearer",
                                BearerFormat = "JWT",
                                Name = "Authorization",
                                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                                Description = "Please insert JWT with Bearer info field.",

                                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                                {
                                    Id = JwtBearerDefaults.AuthenticationScheme,
                                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme
                                }
                            },
                            new string[] { }
                        }
                    });
                });
                builder.Services.AddSwaggerGenNewtonsoftSupport();
            }
        }
        public static void UseSwagger(WebApplicationBuilder builder, WebApplication app)
        {
            bool useSwagger = false;
            if (builder.Configuration["Swagger:Enabled"] != null)
            {
                bool.TryParse(builder.Configuration["Swagger:Enabled"], out useSwagger);
            }
            if (useSwagger)
            {
                string swaggerTitle = "Document";
                if (builder.Configuration["Swagger:Title"] != null)
                {
                    swaggerTitle = builder.Configuration["Swagger:Title"];
                }

                string swaggerVersion = "v1";
                if (builder.Configuration["Swagger:Version"] != null)
                {
                    swaggerVersion = "v" + builder.Configuration["Swagger:Version"];
                }



                app.UseSwagger();


                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(
                        string.Format("/swagger/{0}/swagger.json", swaggerVersion),
                        string.Format("{0} {1}", swaggerTitle, swaggerVersion));
                    c.DefaultModelsExpandDepth(-1); // Ensure this is configured correctly
                    //c.DocExpansion(DocExpansion.None);
                });

            }
        }

        public static void UseForwardedHeaders(WebApplication app)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
            });
            app.Use((context, next) =>
            {
                if (Utils.Constants.WEB.SERVER_PROTOCOL != null)
                    context.Request.Scheme = Utils.Constants.WEB.SERVER_PROTOCOL;

                return next();
            });
        }

    }

    public partial class StartupAPIMicoService
    {
        public static void StartupCreateBuilder(WebApplicationBuilder builder)
        {
            //Utils.Startup.ConfigConstants(builder, typeof(Authentication.Constants.AUTH));
            AuthenticationBuilder auth = Utils.Startup.ConfigAuthentication(builder);
            Utils.Startup.ConfigRequestSize(builder);
            Utils.Startup.ConfigCors(builder);
            Utils.Startup.ConfigController(builder);
            Utils.Startup.ConfigUtils(builder);
            Utils.Startup.ConfigSwagger(builder);



        }


        public static void StartupCreateApplication(WebApplicationBuilder builder, WebApplication app)
        {

            Utils.Startup.UseCors(app);
            Utils.Startup.UseSwagger(builder, app);
            Utils.Startup.UseForwardedHeaders(app);



            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();


        }
        public static void StartupCreateBuilderV_1(WebApplicationBuilder builder)
        {





            // JWT Configuration
            var secret = "182a61aa-dcf9-4e54-812d-95ace611b6fd";
            var issuer = "MES";
            var audience = "MES";

            var key = Encoding.UTF8.GetBytes(secret);

            // Add authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero // ปิดเผื่อเวลา
                    };
                });

            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Configure Swagger
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductionOperation API", Version = "v1" });

                // เพิ่มการรองรับ Authorization ผ่าน JWT
                option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference= new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id=JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new string[] { }
                    }
                });
            });




        }

        public static void StartupCreateApplicationV_1(WebApplicationBuilder builder, WebApplication app)
        {

            // Configure middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }

    }
}