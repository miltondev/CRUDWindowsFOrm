using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1.Models;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");
            Display();
        }

        public void Display()
        {
            using (StudentInformationEntities _entity=new StudentInformationEntities())
            {
                List<StudentInformation> _studentList = new List<StudentInformation>();
                _studentList = _entity.StudentDetails.Select(x => new StudentInformation
                {
                    Id=x.Id,
                    Name=x.Name,
                    Age=x.Age,
                    City=x.City,
                    Gender=x.Gender
                }).ToList();
                dataGridView1.DataSource = _studentList;
            }
        }

        
        private void btnSave_Click(object sender, EventArgs e)   // Save button click event
        {
            StudentDetail stu = new StudentDetail();
            stu.Name = txtName.Text;   
            stu.Age = Convert.ToInt32(txtAge.Text);
            stu.City = txtCity.Text;
            stu.Gender = cmbGender.SelectedItem.ToString();
            bool result = SaveStudentDetails(stu); // calling SaveStudentDetails method to save the record in table.Here passing a student details object as parameter
            ShowStatus(result, "Save");
        }
        public bool SaveStudentDetails(StudentDetail Stu) // calling SaveStudentMethod for insert a new record
        {
            bool result = false;
            using (StudentInformationEntities _entity = new StudentInformationEntities())
            {
                _entity.StudentDetails.AddObject(Stu);  
                _entity.SaveChanges(); 
                result = true;
            }
            return result;
        }
       
       
        private void btnUpdate_Click(object sender, EventArgs e) // Update button click event
        {
            StudentDetail stu = SetValues(Convert.ToInt32(lblID.Text), txtName.Text, Convert.ToInt32(txtAge.Text), txtCity.Text, cmbGender.SelectedItem.ToString()); // Binding values to StudentInformationModel
            bool result = UpdateStudentDetails(stu); // calling UpdateStudentDetails Method
            ShowStatus(result, "Update");
        }
        public bool UpdateStudentDetails(StudentDetail Stu) // UpdateStudentDetails method for update a existing Record
        {
            bool result = false;
            using (StudentInformationEntities _entity = new StudentInformationEntities())
            {
                StudentDetail _student = _entity.StudentDetails.Where(x => x.Id == Stu.Id).Select(x => x).FirstOrDefault();
                _student.Name = Stu.Name;
                _student.Age = Stu.Age;
                _student.City = Stu.City;
                _student.Gender = Stu.Gender;
                _entity.SaveChanges();
                result = true;
            }
            return result;
        }

        private void btnDelete_Click(object sender, EventArgs e) //Delete Button Event
        {
            StudentDetail stu = SetValues(Convert.ToInt32(lblID.Text), txtName.Text, Convert.ToInt32(txtAge.Text), txtCity.Text, cmbGender.SelectedItem.ToString()); // Binding values to StudentInformationModel
            bool result = DeleteStudentDetails(stu); //Calling DeleteStudentDetails Method
            ShowStatus(result, "Delete");
        }
        public bool DeleteStudentDetails(StudentDetail Stu) // DeleteStudentDetails method to delete record from table
        {
            bool result = false;
            using (StudentInformationEntities _entity = new StudentInformationEntities())
            {
                StudentDetail _student = _entity.StudentDetails.Where(x => x.Id == Stu.Id).Select(x => x).FirstOrDefault();
                _entity.StudentDetails.DeleteObject(_student);  
                _entity.SaveChanges();
                result = true;
            }
            return result;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) //Calling Datagridview cell click to Update and Delete
        {
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows) // foreach datagridview selected rows values
                {
                    lblID.Text = row.Cells[0].Value.ToString(); 
                    txtName.Text = row.Cells[1].Value.ToString();
                    txtAge.Text = row.Cells[2].Value.ToString();
                    txtCity.Text = row.Cells[3].Value.ToString();
                    cmbGender.SelectedItem = row.Cells[4].Value.ToString();
                }
            }
        }

        public StudentDetail SetValues(int Id, string Name, int age, string City, string Gender) //Setvalues method for binding field values to StudentInformation Model class
        {
            StudentDetail stu = new StudentDetail();
            stu.Id = Id;
            stu.Name = Name;
            stu.Age = age;
            stu.City = City;
            stu.Gender = Gender;
            return stu;
        }

        public void ShowStatus(bool result, string Action) // validate the Operation Status and Show the Messages To User
        { 
            if (result)
            {
                if (Action.ToUpper() == "SAVE")
                {
                    MessageBox.Show("Saved Successfully!..", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Action.ToUpper() == "UPDATE")
                {
                    MessageBox.Show("Updated Successfully!..", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Deleted Successfully!..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Something went wrong!. Please try again!..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ClearFields();
            Display();
        }

        public void ClearFields() // Clear the fields after Insert or Update or Delete operation
        {
            txtName.Text = "";
            txtAge.Text = "";
            txtCity.Text = "";
            cmbGender.SelectedIndex = -1;
        }

      
    }
}
