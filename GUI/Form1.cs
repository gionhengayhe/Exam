using BUS;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GUI
{
    public partial class frmSinhVien : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly LopService lopService = new LopService();
        public frmSinhVien()
        {
            InitializeComponent();
        }

        private void frmSinhVien_Load(object sender, EventArgs e)
        {

            var dssv = studentService.GetAll();
            var dslop = lopService.GetAll();
            FillComboBox(dslop);
            BindGrid(dssv);
        }
        private void FillComboBox(List<Lop> dslop)
        {
            cmbLop.DataSource = dslop;
            cmbLop.ValueMember = "MaLop";
            cmbLop.DisplayMember = "TenLop";
        }

        private void BindGrid(List<SinhVien> dssv)
        {
            lvSinhVien.Items.Clear();
            foreach (var s in dssv)
            {
                ListViewItem lvi = new ListViewItem(s.MaSV);
                lvi.SubItems.Add(s.HoTenSV);
                lvi.SubItems.Add(s.NgaySinh.ToString());
                lvi.SubItems.Add(s.Lop.TenLop);
                lvSinhVien.Items.Add(lvi);
            }
        }



        private void lvSinhVien_Click(object sender, EventArgs e)
        {
            
                txtMaSV.Text = lvSinhVien.SelectedItems[0].SubItems[0].Text;
                txtHoten.Text = lvSinhVien.SelectedItems[0].SubItems[1].Text;
                dtNgaysinh.Text = lvSinhVien.SelectedItems[0].SubItems[2].Text;
                cmbLop.Text = lvSinhVien.SelectedItems[0].SubItems[3].Text;
            
        }
        private void Clearform()
        {
            txtHoten.Text = "";
            txtMaSV.Text = "";
            dtNgaysinh.Text = "";
            cmbLop.SelectedIndex = 0;
        }




        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn muốn thoát?", "Thông báo", MessageBoxButtons.YesNo);
            if (rs == DialogResult.Yes)
                this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            SinhVien a = new SinhVien { MaSV = txtMaSV.Text, HoTenSV = txtHoten.Text, NgaySinh = dtNgaysinh.Value, MaLop = cmbLop.SelectedValue.ToString() };
            studentService.InsertUpdate(a);
            var dssv = studentService.GetAll();
            BindGrid(dssv);
            if (sender == btnThem)
                MessageBox.Show("Thêm dữ liệu thành công!");
            if (sender == btnSua)
                MessageBox.Show("Cập nhật dữ liệu thành công!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SinhVien a = studentService.FindByID(txtMaSV.Text);
            if(a!= null) {
                DialogResult rs = MessageBox.Show("Bạn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo);
                if (rs == DialogResult.Yes)
                {
                    studentService.Delete(txtMaSV.Text);
                    var dssv = studentService.GetAll();
                    BindGrid(dssv);
                }

            }
            else
                MessageBox.Show("Không tìm thấy mã SV");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var dssv = studentService.GetAll();
            BindGrid(dssv);
            if (txtTim.Text != "")
            {
                foreach (ListViewItem item in lvSinhVien.Items)
                {
                    if (item.SubItems[1].Text.ToLower().Contains(txtTim.Text.ToLower()) == false)
                        lvSinhVien.Items.Remove(item);
                }
            }
        }

    }
}
