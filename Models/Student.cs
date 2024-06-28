using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudentsManagement.Models
{
    public class Student
    {
        public int ID { set; get; }
        [Required(ErrorMessage = "Nhập họ và tên sinh viên")]
        [Display(Name ="Họ và Tên")]
        public string FullName { set; get; }
        [Required(ErrorMessage = "Moi nhap giới tính")]
        [Display(Name = "Giới tính")]
        public string Sex { set; get; }
        [Required(ErrorMessage = "Mời nhập năm sinh")]
        [Display(Name = "Năm sinh")]
        public string NamSinh { set; get; }

        [Required(ErrorMessage ="Moi nhap dia chi")]
        [Display(Name ="Địa chỉ")]
        public string Address { set; get; }
    }
    class StudentList
    {
        DBConnection db;
        public StudentList()
        {

        db = new DBConnection(); 
        }
        public List<Student> getStudent(string ID)
        {
            string sql;
            if (string.IsNullOrEmpty(ID))
                sql = "SELECT * FROM Students";
            else
                sql = "SELECT * FROM Students WHERE Id = " + ID;
            List<Student> stuList = new List<Student>();   
            DataTable dt = new DataTable();
            SqlConnection con = db.getConnection();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            con.Open();
            da.Fill(dt);
            da.Dispose();
            con.Close();
            Student tmpStu;
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                tmpStu = new Student();
                tmpStu.ID = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                tmpStu.FullName = dt.Rows[i]["Fullname"].ToString();
                tmpStu.Sex = dt.Rows[i]["Sex"].ToString();
                tmpStu.NamSinh = dt.Rows[i]["NamSinh"].ToString();
                tmpStu.Address = dt.Rows[i]["Address"].ToString();
                stuList.Add(tmpStu);

            }
            return stuList;
        }
        public void AddStudent(Student stu)
        {
            string sql = "INSERT INTO Students(Fullname, Sex, NamSinh, Address) " +
                 "VALUES(N'" + stu.FullName + "', N'" + stu.Sex + "', N'" + stu.NamSinh + "', N'" + stu.Address + "')";
            SqlConnection con = db.getConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close() ;
        }
        public void UpdateStudent(Student stu)
        {
            string sql = "UPDATE Students SET Fullname = N'" + stu.FullName + "', Sex = N'" + stu.Sex + "', NamSinh = N'" + stu.NamSinh + "', Address = N'" + stu.Address + "' WHERE ID = " + stu.ID;
            SqlConnection con = db.getConnection();
            SqlCommand cmd = new SqlCommand (sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public void DeleteStudent(Student stu)
        {
            string sql = "DELETE Students WHERE ID = " + stu.ID;
            SqlConnection con = db.getConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
    }
}