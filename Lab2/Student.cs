using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Student
    {
        public string Mssv { get; set; }
        public string Hoten { get; set; }
        public string Dienthoai { get; set; }
        public float Diemtoan { get; set; }
        public float Diemvan { get; set; }
        public float DTB {  get; set; }

        public Student(string mssv, string hoten, string dienthoai, float diemtoan, float diemvan)
        {
            Mssv = mssv;
            Hoten = hoten;
            Dienthoai = dienthoai;
            Diemtoan = diemtoan;
            Diemvan = diemvan;
            DTB = (diemtoan + diemvan) / 2;
        }
    }
}
