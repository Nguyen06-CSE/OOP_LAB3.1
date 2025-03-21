using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB3._1
{
    public enum GioiTinh
    {
        Nam,
        Nu
    }

    public class ThueBao
    {
        public string DiaChi { get; set; }
        public GioiTinh GioiTinh { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public List<string> SoDT { get; set; }
        public string SoCMND { get; set; }
        public DateTime NgayDangKy { get; set; }
        public string LoaiThueBao { get; set; }
        public DateTime NgayLapDat { get; set; }
        public string NhaDichVu { get; set; }

        public ThueBao() { }

        public ThueBao(string diaChi, GioiTinh gioiTinh, string hoTen, DateTime ngaySinh, List<string> soDT, string soCMND)
        {
            this.DiaChi = diaChi;
            this.GioiTinh = gioiTinh;
            this.HoTen = hoTen;
            this.NgaySinh = ngaySinh;
            this.SoDT = new List<string>(soDT);
            this.SoCMND = soCMND;
        }

        public ThueBao(string tb)
        {
            string[] s = tb.Split(',');
            SoCMND = s[0];
            HoTen = s[1];
            NgaySinh = DateTime.Parse(s[2]);
            GioiTinh = (s[3] == "Nam") ? GioiTinh.Nam : GioiTinh.Nu;
            SoDT = s[4].Split('|').ToList();
            DiaChi = s[5];
        }

        public void Xuat()
        {
            Console.WriteLine($"{DiaChi,-20} {GioiTinh,-10} {HoTen,-20} {NgaySinh.ToShortDateString(),-12} {string.Join(", ", SoDT),-15} {SoCMND,-10}");
        }

        public string ThanhPho
        {
            get
            {
                int vt = DiaChi.LastIndexOf('-');
                return vt >= 0 ? DiaChi.Substring(vt + 1).Trim() : DiaChi;
            }
        }

        public void ThemThueBao(List<ThueBao> danhSachThueBao)
        {
            ThueBao tb = new ThueBao();
            Console.Write("Nhap dia chi: ");
            tb.DiaChi = Console.ReadLine();

            Console.Write("Nhap gioi tinh (1) Nam (2) Nu: ");
            int gt = int.Parse(Console.ReadLine());
            tb.GioiTinh = (gt == 1) ? GioiTinh.Nam : GioiTinh.Nu;

            Console.Write("Nhap ho ten: ");
            tb.HoTen = Console.ReadLine();

            Console.Write("Nhap so dien thoai (nhap 'x' de ket thuc): ");
            tb.SoDT = new List<string>();
            while (true)
            {
                string sdt = Console.ReadLine();
                if (sdt.ToLower() == "x") break;
                tb.SoDT.Add(sdt);
            }

            Console.Write("Nhap so CMND: ");
            tb.SoCMND = Console.ReadLine();

            Console.Write("Nhap ngay sinh (yyyy-MM-dd): ");
            tb.NgaySinh = DateTime.Parse(Console.ReadLine());

            tb.NgayDangKy = DateTime.Now;

            danhSachThueBao.Add(tb);
            Console.WriteLine("Them thanh cong!");
        }

        public void KhoiTaoNgayDangKy()
        {
            while (true)
            {
                Console.Write("Nhập ngày đăng ký (yyyy-MM-dd): ");
                string input = Console.ReadLine();

                if (DateTime.TryParse(input, out DateTime ngayDK))
                {
                    this.NgayDangKy = ngayDK;
                    break;
                }
                else
                {
                    Console.WriteLine("Ngày không hợp lệ. Vui lòng nhập lại theo định dạng yyyy-MM-dd.");
                }
            }
        }

        public int DemSoLuongSoDT()
        {
            return SoDT.Count;
        }
    }

    public class DanhSachThueBao
    {
        private List<ThueBao> collection = new List<ThueBao>();

        public void SapXepThueBao()
        {
            for (int i = 0; i < collection.Count - 1; i++)
            {
                for (int j = i + 1; j < collection.Count; j++)
                {
                    if (string.Compare(collection[i].HoTen, collection[j].HoTen) > 0 ||
                        (collection[i].HoTen == collection[j].HoTen && collection[i].DemSoLuongSoDT() < collection[j].DemSoLuongSoDT()))
                    {
                        ThueBao temp = collection[i];
                        collection[i] = collection[j];
                        collection[j] = temp;
                    }
                }
            }
        }

        public void ThemThueBao(ThueBao tb)
        {
            collection.Add(tb);
        }

        public void XuatDanhSach()
        {
            foreach (var tb in collection)
            {
                tb.Xuat();
            }
        }
        public void ThemLoaiThueBao(ThueBao tb)
        {
            Console.WriteLine("nhap loai thue bao cua ban (co dinh hoac di dong)");
            tb.LoaiThueBao = Console.ReadLine();
            
            if(tb.LoaiThueBao.CompareTo("co dinh") == 0)
            {
                Console.Write("Nhap ngay lap dat (yyyy-MM-dd): ");
                tb.NgayLapDat = DateTime.Parse(Console.ReadLine());

            }
            else
            {
                Console.WriteLine("nhap nha dich vu");
                tb.NhaDichVu = Console.ReadLine();
                
            }
        }
    }
}
