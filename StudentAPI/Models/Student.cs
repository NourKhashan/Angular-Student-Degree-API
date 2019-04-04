using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAPI.Models
{
    public class Student
    {

        public int Id { get; set; }

        public string Name { get; set; }


        public string Image { get; set; }

        public virtual List<StudentSubject> StudentSubjects { get; set; }



    }
}