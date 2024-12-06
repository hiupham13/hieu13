using System;
using System.Windows.Forms;
namespace bai2._2
{
    public partial class Form1 : Form
    {
        private List<SinhVien> danhSachSinhVien = new List<SinhVien>();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cbbChuyenNganh.SelectedIndex = 0;
            rbtnNu.Checked = true;
            txtTongNam.Text = "0";
            txtTongNu.Text = "0";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {

                string maSV = txtMaSV.Text;
                string hoTen = txtHoTen.Text;
                string chuyenNganh = cbbChuyenNganh.SelectedItem.ToString();
                float diemTB;

                if (!float.TryParse(txtDiemTB.Text, out diemTB))
                {
                    MessageBox.Show("Vui lòng nhập điểm trung bình hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string gioiTinh = rbtnNam.Checked ? "Nam" : "Nu";

                if (string.IsNullOrWhiteSpace(maSV) || string.IsNullOrWhiteSpace(hoTen))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                var sinhVienHienTai = danhSachSinhVien.FirstOrDefault(sv => sv.MASV == maSV);
                if (sinhVienHienTai != null)
                {

                    sinhVienHienTai.HOTEN = hoTen;
                    sinhVienHienTai.CHUYENNGANH = chuyenNganh;
                    sinhVienHienTai.DIEMTB = diemTB;
                    sinhVienHienTai.GIOITINH = gioiTinh;


                    foreach (DataGridViewRow row in dgvQLSV.Rows)
                    {
                        if (row.Cells[0].Value.ToString() == maSV)
                        {
                            row.Cells[1].Value = hoTen;
                            row.Cells[2].Value = gioiTinh;
                            row.Cells[3].Value = diemTB;
                            row.Cells[4].Value = chuyenNganh;
                            break;
                        }
                    }

                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    CapNhatThongKe();
                    return;
                }


                SinhVien sinhVienMoi = new SinhVien
                {
                    MASV = maSV,
                    HOTEN = hoTen,
                    CHUYENNGANH = chuyenNganh,
                    DIEMTB = diemTB,
                    GIOITINH = gioiTinh
                };


                danhSachSinhVien.Add(sinhVienMoi);


                dgvQLSV.Rows.Add(sinhVienMoi.MASV, sinhVienMoi.HOTEN, sinhVienMoi.GIOITINH, sinhVienMoi.DIEMTB, sinhVienMoi.CHUYENNGANH);


                ClearInputs();


                CapNhatThongKe();


                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtMaSV.Text = string.Empty;
            txtHoTen.Text = string.Empty;
            txtDiemTB.Text = string.Empty;
            cbbChuyenNganh.SelectedIndex = 0;
            rbtnNu.Checked = true;
        }
        private void dgvQLSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex >= 0)
                {

                    string maSV = dgvQLSV.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string hoTen = dgvQLSV.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string gioiTinh = dgvQLSV.Rows[e.RowIndex].Cells[2].Value.ToString();
                    string diemTB = dgvQLSV.Rows[e.RowIndex].Cells[3].Value.ToString();
                    string chuyenNganh = dgvQLSV.Rows[e.RowIndex].Cells[4].Value.ToString();

                    txtMaSV.Text = maSV;
                    txtHoTen.Text = hoTen;
                    txtDiemTB.Text = diemTB;
                    cbbChuyenNganh.SelectedItem = chuyenNganh;
                    if (gioiTinh == "Nam")
                        rbtnNam.Checked = true;
                    else
                        rbtnNu.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatThongKe()
        {
            int tongSVNam = danhSachSinhVien.Count(sv => sv.GIOITINH == "Nam");
            int tongSVNu = danhSachSinhVien.Count(sv => sv.GIOITINH == "Nu");

            txtTongNam.Text = tongSVNam.ToString();
            txtTongNu.Text = tongSVNu.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgvQLSV.SelectedRows.Count > 0)
                {

                    string maSVToXoa = dgvQLSV.SelectedRows[0].Cells[0].Value.ToString();


                    var sinhVienToXoa = danhSachSinhVien.FirstOrDefault(sv => sv.MASV == maSVToXoa);

                    if (sinhVienToXoa != null)
                    {

                        var confirmResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa sinh viên với MSSV: {maSVToXoa}?",
                                                            "Xác nhận xóa",
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Warning);

                        if (confirmResult == DialogResult.Yes)
                        {
                            danhSachSinhVien.Remove(sinhVienToXoa);
                            dgvQLSV.Rows.RemoveAt(dgvQLSV.SelectedRows[0].Index);
                            CapNhatThongKe();
                            MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sinh viên cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một sinh viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvQLSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cbbChuyenNganh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
