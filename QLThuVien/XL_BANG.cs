using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace QLThuVien
{
    class XL_BANG: DataTable
    {
        #region Bien Cuc Bo
        public static String Chuoi_lien_ket;
        private SqlDataAdapter mBo_Doc_Ghi = new SqlDataAdapter();
        private SqlConnection mKet_noi;
        public string chuoi_csdl;
        private String mChuoi_SQL;
        private String mTen_bang;
        #endregion
        #region Cac thuoc tinh
        public String Chuoi_SQL
        {
            get { return mChuoi_SQL; }
            set { mChuoi_SQL = value; }
        }

        public String Ten_bang
        {
            get { return mTen_bang; }
            set { mTen_bang = value; }
        }

        public int So_dong
        {
            get { return this.DefaultView.Count; }
        }
        #endregion
        #region Cac phuong thuc khoi tao
        public XL_BANG(): base() { }
        //Tạo bảng mới với pTen_bang
        public XL_BANG(String pTen_bang)
        {
            mTen_bang = pTen_bang;
            Doc_bang();
        }
        //Tạo mới bảng với câu truy vấn
        public XL_BANG(String pTen_bang, String pChuoi_SQL)
        {
            mTen_bang = pTen_bang;
            mChuoi_SQL = pChuoi_SQL;
            Doc_bang();
        }
        #endregion

        public void Doc_bang()
        {
            if (mChuoi_SQL == null)
                mChuoi_SQL = "SELECT * FROM " + mTen_bang;
            if (mKet_noi == null)
                mKet_noi = new SqlConnection(Chuoi_lien_ket);
            try
            {
                mBo_Doc_Ghi = new SqlDataAdapter(mChuoi_SQL, mKet_noi);
                mBo_Doc_Ghi.FillSchema(this, SchemaType.Mapped);
                mBo_Doc_Ghi.Fill(this);
                mBo_Doc_Ghi.RowUpdated += new SqlRowUpdatedEventHandler(mBo_Doc_Ghi_RowUpdated);
                SqlCommandBuilder Bo_phat_sinh = new SqlCommandBuilder(mBo_Doc_Ghi);
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public Boolean Ghi()
        {
            Boolean ket_qua = true;
            try
            {
                mBo_Doc_Ghi.Update(this);
                this.RejectChanges();
            }
            catch(SqlException ex)
            {
                this.RejectChanges();
                ket_qua = false;
            }
            return ket_qua;
        }

        //Lọc Dữ Liệu
        public void Loc_du_lieu(String pDieu_kien)
        {
            try
            {
                this.DefaultView.RowFilter = pDieu_kien;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public int Thuc_hien_lenh(String Lenh)
        {
            try
            {
                SqlCommand Cau_lenh = new SqlCommand(Lenh, mKet_noi);
                mKet_noi.Open();
                int ket_qua = Cau_lenh.ExecuteNonQuery();
                mKet_noi.Close();
                return ket_qua;
            }
            catch
            {
                return -1;
            }
        }

        public Object Thuc_hien_lenh_tinh_toan(String Lenh)
        {
            try
            {
                SqlCommand Cau_lenh = new SqlCommand(Lenh, mKet_noi);
                mKet_noi.Open();
                Object ket_qua = Cau_lenh.ExecuteScalar();
                mKet_noi.Close();
                return ket_qua;
            }
            catch
            {
                return null;
            }
            
            
        }
      
        private void mBo_Doc_Ghi_RowUpdated(object sender, SqlRowUpdatedEventArgs e)
        {
            if(this.PrimaryKey[0].AutoIncrement)
            {
                if ((e.Status==UpdateStatus.Continue)&&(e.StatementType == StatementType.Insert))
                {
                    SqlCommand cmd = new SqlCommand("Select @@IDENTLY", mKet_noi);
                    e.Row.ItemArray[0] = cmd.ExecuteScalar();
                    e.Row.AcceptChanges();
                }
            }
        }


    }
    
}
