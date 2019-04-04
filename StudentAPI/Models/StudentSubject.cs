using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

using StudentAPI.Models;
namespace StudentAPI.Models
{
    public class StudentSubject
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Student")]
        public int StdId { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Subject")]
        public int SubId { get; set; }

        public int Degree { get; set; }

        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }


    }
}