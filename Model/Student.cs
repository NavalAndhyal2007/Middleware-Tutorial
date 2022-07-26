using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class Student
    {
        public Student(int sid, string sname, string sclass, int sage)
        {
            this.Student_ID = sid;
            this.Student_Name = sname;
            this.Student_Class = sclass;
            this.Student_Age = sage;
        }
        public int Student_ID { get; set; }
        public string Student_Name { get; set; }
        public string Student_Class { get; set; }
        public int Student_Age { get; set; }
    }
}
