using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class FetchStudentMiddleware
    {
        Student[] students =
       {
            new Student(1,"Naval","8th",13),
            new Student(2,"Naval1","7th",12),
            new Student(3,"Naval2","6th",11),
            new Student(4,"Naval3","5th",10)

        };
        private readonly RequestDelegate _next;

        public FetchStudentMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            //if
            string path = httpContext.Request.Path.ToString();
            if(!path.Equals("/"))
            {                
                Student student = GetStudentInfo(path);
                if (student != null)
                {
                    httpContext.Response.ContentType = "text/html";
                    httpContext.Response.WriteAsync("<h3> Student ID : " + student.Student_ID + "</h3>");
                    httpContext.Response.WriteAsync("<h3> Student Name : " + student.Student_Name + "</h3>");
                    httpContext.Response.WriteAsync("<h3> Student Class : " + student.Student_Class + "</h3>");
                    httpContext.Response.WriteAsync("<h3> Student Age : " + student.Student_Age + "</h3>");
                }
            }
           
            return _next(httpContext);
        }

        private Student GetStudentInfo(string path)
        {
            string PathStr = path.ToString();
            int Student_Id = Convert.ToInt32(PathStr.Substring(1, PathStr.Length - 1));
            Student[] StudentsInfo = students.Where(s => s.Student_ID == Student_Id).ToArray();
            if (StudentsInfo.Length > 0)
            {
                return StudentsInfo[0];
            }
            else
            {
                return null;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class FetchStudentMiddlewareExtensions
    {
        public static IApplicationBuilder UseFetchStudentMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FetchStudentMiddleware>();
        }
    }
}
